#include <Arduino.h>
#include <LittleFS.h>

#include "system.h"
#include "config.h"

void Config::Init()
{
    // initialize file system
    if (!LittleFS.begin(true))
    {
        _sys.Log(_module, "Creating file system\n");
    }

    LoadConfig();
}

String Config::GetWifiSsid()
{
    return _wifiSsid;
}

String Config::GetWifiPassword()
{
    return _wifiPassword;
}
