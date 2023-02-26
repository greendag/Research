#include <Arduino.h>
#include "FS.h"
#include <LittleFS.h>
#include <time.h>
#include <LinkedList.h>

/* You only need to format LittleFS the first time you run a
   test or else use the LITTLEFS plugin to create a partition
   https://github.com/lorol/arduino-esp32littlefs-plugin */
   
#define FORMAT_LITTLEFS_IF_FAILED true

void listDir(fs::FS &fs, const char * dirname, uint8_t levels)
{
  LinkedList<String> directoryList = LinkedList<String>();

  Serial.printf(" Directory of %s\r\n\r\n", dirname);

  File root = fs.open(dirname);
  if(!root)
  {
    Serial.println("File Not Found\r\n");
    return;
  }
  
  if(!root.isDirectory())
  {
    Serial.println("File Not Found\r\n");
    return;
  }

  int fileCount = 0;
  int totalBytes = 0;

  File file = root.openNextFile();
  
  while(file)
  {
    time_t t= file.getLastWrite();
    struct tm * tmstruct = localtime(&t);
    Serial.printf("%d-%02d-%02d %02d:%02d:%02d",(tmstruct->tm_year)+1900,( tmstruct->tm_mon)+1, tmstruct->tm_mday,tmstruct->tm_hour , tmstruct->tm_min, tmstruct->tm_sec);

    if(file.isDirectory())
    {
      Serial.printf("   <DIR>          %s\r\n", file.name());

      if(levels)
      {
        directoryList.add(file.name());
      }
    } 
    else 
    {
      fileCount++;
      totalBytes += file.size();

      Serial.printf("           %6ld %s\r\n", file.size(), file.name());
    }
    
    file = root.openNextFile();
  }

  Serial.printf("          %4ld File(s)        %7ld bytes\r\n\r\n", fileCount, totalBytes);

  // print out the nested folders
  int directoryCount = directoryList.size();
  for (int i = 0; i < directoryCount; i++) 
  {
    String subDirName = directoryList.get(i);
    String path = dirname + subDirName;
    listDir(fs, path.c_str(), levels -1);
  }

  directoryList.clear();
}

void createDir(fs::FS &fs, const char * path)
{
  Serial.printf("Creating Dir: %s\n", path);
  if(fs.mkdir(path))
  {
    Serial.println("Dir created");
  } 
  else 
  {
    Serial.println("mkdir failed");
  }
}

void removeDir(fs::FS &fs, const char * path)
{
  Serial.printf("Removing Dir: %s\n", path);

  if(fs.rmdir(path))
  {
    Serial.println("Dir removed");
  } 
  else 
  {
    Serial.println("rmdir failed");
  }
}

void readFile(fs::FS &fs, const char * path)
{
  Serial.printf("Reading file: %s\r\n", path);

  File file = fs.open(path);
  if(!file || file.isDirectory())
  {
    Serial.println("- failed to open file for reading");
    return;
  }

  Serial.println("- read from file:");
  while(file.available())
  {
    Serial.write(file.read());
  }

  file.close();
}

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
    Serial.println("- write failed");
  }

  file.close();
}

void appendFile(fs::FS &fs, const char * path, const char * message)
{
  Serial.printf("Appending to file: %s\r\n", path);

  File file = fs.open(path, FILE_APPEND);

  if(!file)
  {
    Serial.println("- failed to open file for appending");
    return;
  }

  if(file.print(message))
  {
    Serial.println("- message appended");
  } 
  else 
  {
    Serial.println("- append failed");
  }

  file.close();
}

void renameFile(fs::FS &fs, const char * path1, const char * path2)
{
  Serial.printf("Renaming file %s to %s\r\n", path1, path2);

  if (fs.rename(path1, path2)) 
  {
    Serial.println("- file renamed");
  } 
  else 
  {
    Serial.println("- rename failed");
  }
}

void deleteFile(fs::FS &fs, const char * path)
{
  Serial.printf("Deleting file: %s\r\n", path);

  if(fs.remove(path))
  {
    Serial.println("- file deleted");
  } 
  else 
  {
    Serial.println("- delete failed");
  }
}

// SPIFFS-like write and delete file

// See: https://github.com/esp8266/Arduino/blob/master/libraries/LittleFS/src/LittleFS.cpp#L60
void writeFile2(fs::FS &fs, const char * path, const char * message)
{
  if(!fs.exists(path))
  {
    if (strchr(path, '/')) 
    {
      Serial.printf("Create missing folders of: %s\r\n", path);
      char *pathStr = strdup(path);
      
      if (pathStr) 
      {
        char *ptr = strchr(pathStr, '/');

        while (ptr)
        {
          *ptr = 0;
          fs.mkdir(pathStr);
          *ptr = '/';
          ptr = strchr(ptr+1, '/');
        }
      }

      free(pathStr);
    }
  }

  Serial.printf("Writing file to: %s\r\n", path);
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
    Serial.println("- write failed");
  }
  
  file.close();
}

// See:  https://github.com/esp8266/Arduino/blob/master/libraries/LittleFS/src/LittleFS.h#L149
void deleteFile2(fs::FS &fs, const char * path)
{
  Serial.printf("Deleting file and empty folders on path: %s\r\n", path);

  if(fs.remove(path))
  {
    Serial.println("- file deleted");
  } 
  else 
  {
    Serial.println("- delete failed");
  }

  char *pathStr = strdup(path);
  if (pathStr) 
  {
    char *ptr = strrchr(pathStr, '/');
    if (ptr) 
    {
      Serial.printf("Removing all empty folders on path: %s\r\n", path);
    }
  
    while (ptr) 
    {
      *ptr = 0;
      fs.rmdir(pathStr);
      ptr = strrchr(pathStr, '/');
    }

    free(pathStr);
  }
}

void setup()
{
  Serial.begin(115200);
  
  if(!LittleFS.begin(true))
  {
    Serial.println("LittleFS Mount Failed");
    return;
  }

  listDir(LittleFS, "/", 10);
}

void loop()
{ }
