C:\Factory\Tools\RDMD.exe /RC out

COPY SSRBClient\SSRBClient\bin\Release\SSRBClient.exe out
COPY SSRBServer\SSRBServer\bin\Release\SSRBServer.exe out

C:\Factory\SubTools\zip.exe /O out SSRunBatch

PAUSE
