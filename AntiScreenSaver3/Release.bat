C:\Factory\Tools\RDMD.exe /RC out

COPY NoScreenSaver\NoScreenSaver\bin\Release\NoScreenSaver.exe out\NoScreenSaverCmd.exe

COPY /B Tools\CTools.exe out
C:\Factory\SubTools\EmbedConfig.exe --factory-dir-disabled out\CTools.exe

C:\Factory\Tools\xcp.exe doc out

C:\Factory\SubTools\zip.exe /O out NoScreenSaverCmd

IF NOT "%1" == "/-P" PAUSE
