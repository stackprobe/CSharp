C:\Factory\Tools\RDMD.exe /RC out

COPY MRecver\MRecver\bin\Release\MRecver.exe out
COPY MSender\MSender\bin\Release\MSender.exe out

TYPE NUL > out\MRecver.sig
TYPE NUL > out\MSender.sig

C:\Factory\Tools\xcp.exe doc out

C:\Factory\SubTools\zip.exe /O out MutectorDemo

IF NOT "%1" == "/-P" PAUSE
