#include <Arduino.h>
#include <WiFi.h>

#include "utilities.h"

const char *Utilities::GetModeDescription(wifi_mode_t mode)
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

const char *Utilities::GetConnectionStaus(wl_status_t status)
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
