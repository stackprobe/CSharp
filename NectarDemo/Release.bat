C:\Factory\Tools\RDMD.exe /RC out

COPY NRecver\NRecver\bin\Release\NRecver.exe out
COPY NSender\NSender\bin\Release\NSender.exe out

TYPE NUL > out\NRecver.sig
TYPE NUL > out\NSender.sig

C:\Factory\Tools\xcp.exe doc out

C:\Factory\SubTools\zip.exe /O out NectarDemo

IF NOT "%1" == "/-P" PAUSE
