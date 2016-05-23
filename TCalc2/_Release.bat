C:\Factory\Tools\RDMD.exe /RC out

COPY /B FatCalc\FatCalc\bin\Release\FatCalc.exe out
COPY /B doc\* out

COPY /B KillAndBoot.exe out
C:\Factory\SubTools\EmbedConfig.exe --factory-dir-disabled out\KillAndBoot.exe

C:\Factory\SubTools\zip.exe /O out FatCalc

PAUSE
