C:\Factory\Tools\RDMD.exe /RC out

COPY NoScreenSaver\NoScreenSaver\bin\Release\NoScreenSaver.exe out\NoScreenSaverCmd.exe

C:\Factory\Tools\xcp.exe doc out

C:\Factory\SubTools\zip.exe /O out NoScreenSaverCmd

IF NOT "%1" == "/-P" PAUSE
