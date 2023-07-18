#ifndef CONFIG_H_
#define CONFIG_H_

#include <ArduinoJson.h>
#include <LittleFS.h>

#include "system.h"

class Config
{
    public:
        Config() {} 
        void Init();
        const char *GetWifiSsid();
        const char *GetWifiPassword();

    private:
        System _sys;
        const char *_module = "config";
        String _wifiSsid = "";
        String _wifiPassword = "";

        void LoadConfig()
        {
            if (!LittleFS.exists("/config.json"))
            {
                _sys.Log(_module, "/config.json does not exist\n");
                return;
            }

            File configFile = LittleFS.open("/config.json");
            if (!configFile)
            {
                _sys.Log(_module, "Unable to open the file /config.json\n");
                return;
            }

            size_t size = configFile.size();
            std::unique_ptr<char[]> buf(new char[size]);
            configFile.readBytes(buf.get(), size);
            configFile.close();

            String arg;
            StaticJsonBuffer<512> jsonBuffer;
            JsonObject &json = jsonBuffer.parseObject(buf.get());

            arg = json["ssid"].as<char *>();
            if (arg)
            {
                _wifiSsid = arg;
            }

            arg = json["password"].as<char *>();
            if (arg)
            {
                _wifiPassword = arg;
            }

            _sys.Log(_module, "Network Credentials\n");
            _sys.Log(_module, "SSID:       %s\n", _wifiSsid.c_str());
            _sys.Log(_module, "Password:   %s\n", _wifiPassword.c_str());
        }
};

#endif