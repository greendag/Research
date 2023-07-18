#ifndef ACCESSPOINT_H_
#define ACCESSPOINT_H_

#include <Arduino.h>
#include <WiFi.h>
#include <ESPAsyncWebServer.h>
#include <AsyncTCP.h>

#include "utilities.h"
#include "system.h"

class AccessPoint
{
public:
    AccessPoint()
    {
        _server = new AsyncWebServer(80);
    }
    void Start(String ssid);

private:
    System _sys;
    const char *_module = "accessPoint";
    AsyncWebServer *_server;
    static void handleNotFoundRequests(AsyncWebServerRequest *request)
    {
        request->send(404, "text/plain", "Not found");
    }
    static void HandleHomeRequest(AsyncWebServerRequest *request)
    {
        String html = "";
        request->send(200, "text/html", html);
    }
    static String processor(const String &var)
    {
        //_sys.Log(_module, "In processor. var: %s\n", var);
        return String("Return value from processor");
    }
};

#endif
