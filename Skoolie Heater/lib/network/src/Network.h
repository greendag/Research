#ifndef NETWORK_H_
#define NETWORK_H_

#include <Arduino.h>

#include "accessPoint.h"
#include "stationMode.h"

class Network
{
public:
    Network() {}
    void Init(const char *ssid, const char *password, AccessPoint &accessPoint, StationMode &stationMode);
    wifi_mode_t GetMode();
    String GetAPServiceSetId();

private:
    System _sys;
    const char *_module = "network";
};

#endif
