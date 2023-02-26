#ifndef NETWORK_H_
#define NETWORK_H_

class Network
{
public:
    void Init(String ssid, String password);
    const char *GetModeDescription(wifi_mode_t mode);
    const char *GetConnectionStaus(wl_status_t status);

private:
    System _sys;
    const char *_module = "network";

    String GetAPSSID();

    wl_status_t Connect()
    {
#define WIFI_CONNECT_TIMEOUT 10000
        _sys.Log(_module, "Connecting to wifi\n");
        unsigned long startTime = millis();
        while (WiFi.status() != WL_CONNECTED && millis() - startTime < WIFI_CONNECT_TIMEOUT)
        {
            delay(100);
        }

        wl_status_t status = WiFi.status();

        _sys.Log(_module, "%s\n", GetConnectionStaus(status));

        return status;
    }

private:
    bool ConnectToStation(String ssid, String password)
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

        WiFi.begin(ssid.c_str(), password.c_str());

        return Connect() != WL_CONNECTED;
    }

    void EnableApMode(String ssid)
    {
        wifi_mode_t wifiMode = WiFi.getMode();

        if (wifiMode != WIFI_AP)
        {
            WiFi.mode(WIFI_AP);
            delay(10);
        }

        WiFi.softAP(ssid.c_str());
    }
};

#endif
