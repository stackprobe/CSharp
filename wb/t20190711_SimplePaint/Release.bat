C:\Factory\Tools\RDMD.exe /RC out

COPY SimplePaint\SimplePaint\bin\Release\SimplePaint.exe out
COPY SimplePaint\SimplePaint\bin\Release\Chocolate.dll out
COPY SimplePaint\SimplePaint\bin\Release\Chocomint.dll out

C:\Factory\SubTools\zip.exe /O out SimplePaint

IF NOT "%1" == "/-P" PAUSE
