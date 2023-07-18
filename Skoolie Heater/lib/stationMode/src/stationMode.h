#ifndef STATIONMODE_H_
#define STATIONMODE_H_

#include <Arduino.h>
#include <WiFi.h>
#include <ESPAsyncWebServer.h>
#include <AsyncTCP.h>

#include "utilities.h"
#include "system.h"

class StationMode
{
public:
    StationMode()
    {
        _server = new AsyncWebServer(80);
    }
    bool Connect(const char *ssid, const char *password);

private:
    System _sys;
    const char *_module = "stationMode";
    AsyncWebServer *_server;

    wl_status_t ConnectToWifi()
    {
#define WIFI_CONNECT_TIMEOUT 10000
        _sys.Log(_module, "Connecting to wifi\n");
        unsigned long startTime = millis();
        while (WiFi.status() != WL_CONNECTED && millis() - startTime < WIFI_CONNECT_TIMEOUT)
        {
            delay(100);
        }

        wl_status_t status = WiFi.status();

        _sys.Log(_module, "%s\n", Utilities::GetConnectionStaus(status));

        return status;
    }

    // void RegisterRoute()
    // {
    //     _server.on("/", HTTP_GET, [](AsyncWebServerRequest *request)
    //                { request->send(LittleFS, "/index.html", "text/html", false, processor); });
    // }
};

#endif