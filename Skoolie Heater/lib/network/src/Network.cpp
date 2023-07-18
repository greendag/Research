#include <Arduino.h>
#include <WiFi.h>

#include "system.h"
#include "network.h"
#include "accessPoint.h"
#include "stationMode.h"

void Network::Init(const char *ssid, const char *password, AccessPoint &accessPoint, StationMode &stationMode)
{
    if (strcmp(ssid, "") != 0)
    {
        _sys.Log(_module, "Establishing a connection to %s.\n", ssid);
        if (!stationMode.Connect(ssid, password))
        {
            _sys.Log(_module, "Unable to establish a connection to the network.\n");
            _sys.Log(_module, "Enabling an Access point (%s).\n", GetAPServiceSetId().c_str());
            accessPoint.Start(GetAPServiceSetId());
        }
    }
    else
    {
        _sys.Log(_module, "Enabling the Access point (%s).\n", GetAPServiceSetId().c_str());
        accessPoint.Start(GetAPServiceSetId());
    }
}

String Network::GetAPServiceSetId()
{
    return "SKOOLIE_" + WiFi.macAddress();
}

wifi_mode_t Network::GetMode()
{
    return WiFi.getMode();
}
