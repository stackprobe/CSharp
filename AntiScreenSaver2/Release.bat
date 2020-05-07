C:\Factory\Tools\RDMD.exe /RC out

COPY NoScreenSaver\NoScreenSaver\bin\Release\NoScreenSaver.exe out

C:\Factory\Tools\xcp.exe doc out

C:\Factory\SubTools\zip.exe /O out NoScreenSaver

IF NOT "%1" == "/-P" PAUSE
