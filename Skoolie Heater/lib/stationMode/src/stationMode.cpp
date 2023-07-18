#include <Arduino.h>
#include <WiFi.h>
#include <ESPAsyncWebServer.h>
#include <AsyncTCP.h>

#include "stationMode.h"
#include "system.h"

bool StationMode::Connect(const char *ssid, const char *password)
{
    if (ssid == "" || password == "")
    {
        return false;
    }

    wifi_mode_t wifiMode = WiFi.getMode();

    if (wifiMode != WIFI_STA)
    {
        WiFi.mode(WIFI_STA);
        delay(10);
    }

    WiFi.begin(ssid, password);

    wl_status_t wifiConnectionStatus = ConnectToWifi();

    if (wifiConnectionStatus != WL_CONNECTED)
    {
        return false;
    }

    //RegisterRoutes();
    //TODO: start web server

    return true;
}
