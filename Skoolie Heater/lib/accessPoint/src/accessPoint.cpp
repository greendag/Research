#include <Arduino.h>
#include <WiFi.h>
#include <ESPAsyncWebServer.h>
#include <AsyncTCP.h>
#include <LittleFS.h>

#include "accessPoint.h"

#define CACHE_HEADER "max-age=86400"

void AccessPoint::Start(String ssid)
{
    WiFi.mode(WIFI_AP);

    WiFi.softAP(ssid.c_str());

    _sys.Log(_module, "Access Point IP Address: %s\n", WiFi.softAPIP().toString());

    _server->onNotFound(handleNotFoundRequests);

    _server->on("/", HTTP_GET, [](AsyncWebServerRequest *request)
              { 
                Serial.println("I am here");
                request->send(LittleFS, "/web//index.html", "text/html", false, processor); });

    _server->serveStatic("/", LittleFS, "/web", CACHE_HEADER).setDefaultFile("index.html");

    _server->begin();
}
