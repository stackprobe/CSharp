C:\Factory\Tools\RDMD.exe /RC out

COPY WCluster\WCluster\bin\Release\WCluster.exe out
C:\Factory\Tools\xcp.exe doc out

C:\Factory\SubTools\zip.exe /O out WCluster

PAUSE
