#ifndef UTILITIES_H_
#define UTILITIES_H_

#include <Arduino.h>
#include <WiFi.h>

class Utilities
{
    public:
        static const char *GetModeDescription(wifi_mode_t mode);
        static const char *GetConnectionStaus(wl_status_t status);
};

#endif
