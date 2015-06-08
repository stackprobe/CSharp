C:\Factory\Tools\RDMD.exe /RC out
C:\Factory\Tools\RDMD.exe /MD out\Client
C:\Factory\Tools\RDMD.exe /MD out\Server

COPY C:\Factory\Labo\Socket\tunnel\namedTrack.exe out\Client
COPY C:\Factory\Labo\Socket\tunnel\revClient.exe out\Client
COPY C:\Factory\SubTools\Chat\Chat.exe out\Client
COPY C:\Factory\SubTools\Chat\FileSv.exe out\Client
COPY C:\Factory\SubTools\Chat\namedTrackHttp.exe out\Client

COPY C:\Factory\SubTools\Chat\ChatSv.exe out\Server
COPY C:\Factory\Labo\Socket\tunnel\revServer.exe out\Server

FOR /R out %%E IN (*.exe) DO (
	C:\Factory\SubTools\EmbedConfig.exe --factory-dir-disabled "%%E"
)

MD out\Client\tmp
> out\Client\tmp\1.txt TYPE NUL

MD out\Server\tmp
> out\Server\tmp\1.txt TYPE NUL

COPY C:\Factory\Resource\CP932.txt out\Client
COPY C:\Factory\Resource\JIS0208.txt out\Client
COPY C:\Factory\SubTools\Chat\FileListTemplate.html_ out\Client
COPY C:\Factory\Resource\JIS0208.txt out\Server

COPY Client\WChat\bin\Release\WChat.exe out\Client
COPY Server\WChatSv\bin\Release\WChatSv.exe out\Server

C:\Factory\Tools\xcp.exe doc out

REN out\Client WChat_Client
REN out\Server WChat_Server

C:\Factory\SubTools\zip.exe /O out WChat

PAUSE
