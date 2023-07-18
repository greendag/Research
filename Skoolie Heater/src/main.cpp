#include <Arduino.h>

#include "fileExplorer.h"
#include "app.h"

//#define LoadFileExplorer

void setup()
{
#ifdef LoadFileExplorer
  fileExplorerSetup();
#else
  appSetup();
#endif
}

void loop()
{
#ifdef LoadFileExplorer
  fileExplorerLoop();
#else
  appLoop();
#endif
}