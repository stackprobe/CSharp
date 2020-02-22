IF NOT EXIST Hechima_is_here.sig GOTO END
CLS
rem リリースして qrum します。
PAUSE

CALL ff
cx **

CD /D %~dp0.

IF NOT EXIST Hechima_is_here.sig GOTO END

CALL qq
cx **

CALL _Release.bat /-P

MOVE out\HechimaClient.zip S:\リリース物\.

START "" /B /WAIT /DC:\home\bat syncRev

CALL qrumauto rel

rem **** AUTO RELEASE COMPLETED ****

:END
