C:\Factory\Tools\RDMD.exe /RC out

COPY SSRBClient\SSRBClient\bin\Release\SSRBClient.exe out
COPY SSRBServer\SSRBServer\bin\Release\SSRBServer.exe out
COPY WSSRBServer\WSSRBServer\bin\Release\WSSRBServer.exe out

C:\Factory\Tools\xcp.exe doc out

C:\Factory\SubTools\zip.exe /O out SSRunBatch2

PAUSE
