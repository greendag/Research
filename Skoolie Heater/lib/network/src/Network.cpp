#include <Arduino.h>
#include <WiFi.h>

#include "system.h"
#include "network.h"

void Network::Init(String ssid, String password)
{
    if (ssid != "")
    {
        _sys.Log(_module, "Establishing a connection to %s.\n", ssid.c_str());
        if (!ConnectToStation(ssid, password))
        {
            _sys.Log(_module, "Unable to establish a connection to the network.\n");
            _sys.Log(_module, "Enabling an Access point (%s).\n", GetAPSSID().c_str());
            EnableApMode(GetAPSSID());
        }
    }
    else
    {
        _sys.Log(_module, "Enabling the Access point (%s).\n", GetAPSSID().c_str());
        EnableApMode(GetAPSSID());
    }
}

String Network::GetAPSSID()
{
    return "SKOOLIE_" + WiFi.macAddress();
}

const char *Network::GetModeDescription(wifi_mode_t mode)
{
    switch ((int)mode)
    {
        case 0:
            return "Null";

        case 1:
            return "Station";

        case 2:
            return "Access Point";

        case 3:
            return "Station + Access Point";

        case 4:
            return "Max";

        default:
            return "Unknown";
    }
}

const char *Network::GetConnectionStaus(wl_status_t status)
{
    switch ((int)status)
    {
    case 0:
            return "Idle";

    case 1:
            return "No SSID available";

    case 2:
            return "Scan complete";

    case 3:
            return "Connected";

    case 4:
            return "Connection failed";

    case 5:
            return "Connection lost";

    case 6:
            return "Disconnected";

    default:
            return "Unknown";
    }
}
