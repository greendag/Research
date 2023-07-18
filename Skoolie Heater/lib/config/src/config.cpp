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

const char *Config::GetWifiSsid()
{
    return _wifiSsid.c_str();
}

const char *Config::GetWifiPassword()
{
    return _wifiPassword.c_str();
}
