C:\Factory\Tools\RDMD.exe /RC out

COPY WCluster\WCluster\bin\Release\WCluster.exe out
COPY WCluster\Compress\bin\Release\Compress.exe out\���k.exe
COPY WCluster\Decompress\bin\Release\Decompress.exe out\�W�J.exe
C:\Factory\Tools\xcp.exe doc out

C:\Factory\SubTools\zip.exe /O out WCluster

PAUSE
