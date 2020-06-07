C:\Factory\Tools\RDMD.exe /RC out

COPY AntiScreenSaver\AntiScreenSaver\bin\Release\AntiScreenSaver.exe out

COPY /B Tools\CTools.exe out
C:\Factory\SubTools\EmbedConfig.exe --factory-dir-disabled out\CTools.exe

C:\Factory\Tools\xcp.exe doc out

C:\Factory\SubTools\zip.exe /O out NoScreenSaverMusMv

IF NOT "%1" == "/-P" PAUSE
