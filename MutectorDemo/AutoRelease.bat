IF NOT EXIST MRecver\. GOTO END
CLS
rem �����[�X���� qrum ���܂��B
PAUSE

CALL qq
cx **

CALL _Release.bat /-P

MOVE out\MutectorDemo.zip S:\�����[�X��\.

START "" /B /WAIT /DC:\home\bat syncRev

CALL qrumauto rel

rem **** AUTO RELEASE COMPLETED ****

:END
