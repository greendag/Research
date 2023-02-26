#include <Arduino.h>
#include <WiFi.h>

#include "system.h"
#include "config.h"
#include "network.h"

void setup() {
  const char *module = "main";

  Serial.begin(115200);

  System sys;

  sys.Log(module, "Booting...\n");
  sys.Log(module, "Cause %d:    %s\n", sys.GetResetReasonCode(0), sys.GetResetReason(0));
  sys.Log(module, "Chip ID:    %05X\n", sys.GetChipId());

  Config config;
  config.Init();

  Network network;
  network.Init(config.GetWifiSsid(), config.GetWifiPassword());
}

void loop() {
  // put your main code here, to run repeatedly:
}