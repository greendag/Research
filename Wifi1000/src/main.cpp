#include <Arduino.h>
#include <WiFi.h>
#include <ESPAsyncWebServer.h>
#include <AsyncTCP.h>
#include <LittleFS.h>

AsyncWebServer server(80);

const char* input_parameter1 = "ssid";
const char* input_parameter2 = "pass";
const char* input_parameter3 = "ip";

//Variables to save values from HTML form
String ssid;
String pass;
String ip;

// File paths to save input values permanently
const char* SSID_path = "/ssid.txt";
const char* Password_path = "/pass.txt";
const char* IP_path = "/ip.txt";

IPAddress localIP;
IPAddress gateway(192, 168, 68, 1);
IPAddress subnet(255, 255, 255, 0);

unsigned long previous_time = 0;
const long Delay = 10000; 

const int ledPin = 26;
String ledState;

// Read File from LittleFS
String readFile(fs::FS &fs, const char * path)
{
  Serial.printf("Reading file: %s\r\n", path);

  File file = fs.open(path);
  
  if(!file || file.isDirectory())
  {
    Serial.println("- failed to open file for reading");
    return String();
  }
  
  String fileContent;
  while(file.available())
  {
    fileContent = file.readStringUntil('\n');
    break;     
  }

  return fileContent;
}

// Write file to LittleFS
void writeFile(fs::FS &fs, const char * path, const char * message)
{
  Serial.printf("Writing file: %s\r\n", path);

  File file = fs.open(path, FILE_WRITE);
  
  if(!file)
  {
    Serial.println("- failed to open file for writing");
    return;
  }
  
  if(file.print(message))
  {
    Serial.println("- file written");
  } 
  else 
  {
    Serial.println("- frite failed");
  }
}

// Initialize WiFi
bool initialize_Wifi() 
{ 
  if(ssid=="" || ip=="")
  {
    Serial.println("Undefined SSID or IP address.");
    return false;
  }

  WiFi.mode(WIFI_STA);
  localIP.fromString(ip.c_str());

  if (!WiFi.config(localIP, gateway , subnet))
  {
    Serial.println("STA Failed to configure");
    return false;
  }

  WiFi.begin(ssid.c_str(), pass.c_str());
  Serial.println("Connecting to WiFi...");

  unsigned long current_time = millis();
  previous_time = current_time;

  while(WiFi.status() != WL_CONNECTED) 
  {
    current_time = millis();

    if (current_time - previous_time >= Delay) 
    {
      Serial.println("Failed to connect.");
      return false;
    }
  }

  Serial.println(WiFi.localIP());
  return true;
}

// Replaces placeholder with LED state value
String processor(const String& var) 
{
  if(var == "GPIO_STATE") 
  {
    if(digitalRead(ledPin)) 
    {
      ledState = "OFF";
    }
    else 
    {
      ledState = "ON";
    }

    return ledState;
  }

  return String();
}

void AccessPoint()
{
  Serial.println("Setting Access Point");
  WiFi.softAP("ESP32-WIFI-MANAGER", NULL);

  IPAddress IP = WiFi.softAPIP();
  Serial.print("AP IP address: ");
  Serial.println(IP); 

  // Web Server Root URL
  server.on("/", HTTP_GET, [](AsyncWebServerRequest *request)
  {
    request->send(LittleFS, "/wifimanager.html", "text/html");
  });
  
  server.serveStatic("/", LittleFS, "/");
  
  server.on("/", HTTP_POST, [](AsyncWebServerRequest *request) 
  {
    int params = request->params();
    
    for(int i=0;i<params;i++)
    {
      AsyncWebParameter* p = request->getParam(i);
      
      if(p->isPost())
      {          
        // HTTP POST ssid value
        if (p->name() == input_parameter1) 
        {
          ssid = p->value().c_str();
          Serial.print("SSID set to: ");
          Serial.println(ssid);

          // Write file to save value
          writeFile(LittleFS, SSID_path, ssid.c_str());
        }
        
        // HTTP POST pass value
        if (p->name() == input_parameter2) 
        {
          pass = p->value().c_str();
          Serial.print("Password set to: ");
          Serial.println(pass);

          // Write file to save value
          writeFile(LittleFS, Password_path, pass.c_str());
        }
        
        // HTTP POST ip value
        if (p->name() == input_parameter3) 
        {
          ip = p->value().c_str();
          Serial.print("IP Address set to: ");
          Serial.println(ip);

          // Write file to save value
          writeFile(LittleFS, IP_path, ip.c_str());
        }
      }
    }

    request->send(200, "text/plain", "Success. ESP32 will now restart. Connect to your router and go to IP address: " + ip);

    delay(3000);
    ESP.restart();
  });

  server.begin();
}

void StationMode()
{
  server.on("/", HTTP_GET, [](AsyncWebServerRequest *request) 
  {
    request->send(LittleFS, "/index.html", "text/html", false, processor);
  });

  server.serveStatic("/", LittleFS, "/");
  
  server.on("/ledon", HTTP_GET, [](AsyncWebServerRequest *request) 
  {
    digitalWrite(ledPin, LOW);
    request->send(LittleFS, "/index.html", "text/html", false, processor);
  });

  server.on("/ledoff", HTTP_GET, [](AsyncWebServerRequest *request) 
  {
    digitalWrite(ledPin, HIGH);
    request->send(LittleFS, "/index.html", "text/html", false, processor);
  });

  server.begin();
}

void setup() 
{
  Serial.begin(115200);

  if (!LittleFS.begin(true)) 
  {
    Serial.println("An error has occurred while mounting files system");
  }

  Serial.println("File system mounted successfully");

  pinMode(ledPin, OUTPUT);
  digitalWrite(ledPin, HIGH);
  
  ssid = readFile(LittleFS, SSID_path);
  pass = readFile(LittleFS, Password_path);
  ip = readFile(LittleFS, IP_path);

  Serial.println(ssid);
  Serial.println(pass);
  Serial.println(ip);

  if(initialize_Wifi()) 
  {
    StationMode();
  }
  else 
  {
    AccessPoint();
  }
}

void loop()
{ }
