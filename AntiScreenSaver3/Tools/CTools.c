#include "C:\Factory\Common\all.h"

int main(int argc, char **argv)
{
	if(argIs("/CR"))
	{
		SetThreadExecutionState(ES_SYSTEM_REQUIRED);
		SetThreadExecutionState(ES_DISPLAY_REQUIRED);
		return;
	}
	error(); // 不明なコマンド引数
}
