C:\Factory\Tools\RDMD.exe /RM C:\app\CSTools

CALL newcsrr

CALL qq
cx **

COPY C:\Dev\CSharp\Chocolate\Chocolate\bin\Release\Chocolate.dll C:\app\CSTools

ReleaseTools\ReleaseCSExe.exe . C:\app\CSTools

C:\Factory\Tools\Cluster.exe /M S:\_kit\CSTools\CSTools.clu C:\app\CSTools
C:\Factory\SubTools\zip.exe /P S:\_kit\CSTools\CSTools.zip C:\app\CSTools CSTools
CALL C:\home\bat\syncKit.bat

PAUSE
