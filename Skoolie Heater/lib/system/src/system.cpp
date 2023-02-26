#include <Arduino.h>
#include <rom/rtc.h>
#include <esp_log.h>

#include "system.h"

uint32_t g_minFreeHeap = -1;

void System::Log(const char *tag, const char *format, ...)
{
#define BUFFER_SIZE 150
    char buf[BUFFER_SIZE];
    ets_printf("%08d [%-6s] ", millis(), tag);

    va_list args;
    va_start(args, format);
    vsnprintf(buf, BUFFER_SIZE, format, args);
    ets_printf(buf);
    va_end(args);

    if (ESP.getFreeHeap() < g_minFreeHeap)
    {
        g_minFreeHeap = ESP.getFreeHeap();
    }
}

long System::GetChipId()
{
    return ESP.getEfuseMac();
}

int System::GetResetReasonCode(int core)
{
    return (int)rtc_get_reset_reason(core);
}

const char *System::GetResetReason(int core)
{
    switch ((int)rtc_get_reset_reason(core))
    {
        case 1:
            return "Vbat power on reset";

        case 3:
            return "Software reset digital core";

        case 4:
            return "Legacy watch dog reset digital core";

        case 5:
            return "Deep Sleep reset digital core";

        case 6:
            return "Reset by SLC module, reset digital core";

        case 7:
            return "Timer Group0 Watch dog reset digital core";

        case 8:
            return "Timer Group1 Watch dog reset digital core";

        case 9:
            return "RTC Watch dog Reset digital core";

        case 10:
            return "Instrusion tested to reset CPU";

        case 11:
            return "Time Group reset CPU";

        case 12:
            return "Software reset CPU";

        case 13:
            return "RTC Watch dog Reset CPU";

        case 14:
            return "for APP CPU, reseted by PRO CPU";

        case 15:
            return "Reset when the vdd voltage is not stable";

        case 16:
            return "RTC Watch dog reset digital core and rtc module";

        default:
            return "";
    }
}
