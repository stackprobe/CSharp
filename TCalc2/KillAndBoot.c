#include "C:\Factory\Common\all.h"

int main(int argc, char **argv)
{
	uint procId = toValue(nextArg());

	execute_x(xcout("TASKKILL /PID %u /F", procId));
	sleep(2000);
	execute("START FatCalc.exe");
}
