#pragma comment(lib, "user32.lib")

#include "C:\Factory\Common\all.h"

int main(int argc, char **argv)
{
	ClipCursor(NULL);

	if(argIs("/P"))
	{
		sint x = atoi(nextArg());
		sint y = atoi(nextArg());

		SetCursorPos(x, y);

		return;
	}
	error(); // 不明なコマンド引数
}
