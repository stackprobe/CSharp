C:\Factory\Tools\RDMD.exe /RC out

COPY /B HechimaClient\HechimaClient\bin\Release\HechimaClient.exe out
COPY /B HechimaClient2\HechimaClient2\bin\Release\HechimaClient2.exe out

COPY /B C:\Factory\Labo\Socket\tunnel\crypTunnel.exe out
C:\Factory\SubTools\EmbedConfig.exe --factory-dir-disabled out\crypTunnel.exe

C:\Factory\Tools\xcp.exe doc out

C:\Factory\SubTools\zip.exe /O out HechimaClient

PAUSE
