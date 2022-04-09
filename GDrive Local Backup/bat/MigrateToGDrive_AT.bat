@echo off
cd ..
set /p TsrcPath=<conf/srcPath
set /p TdestPath=<conf/destPath
if exist "%TsrcPath%" (
	if exist "%TdestPath%" (
		call xcopy "%TsrcPath%" "%TdestPath%" /s /y >> "log/log"
		if %ERRORLEVEL% == 0 goto :next
		echo. >> "log/err"
		echo Auto Backup with AnyTime Preferences Was Failed !, error code: %errorlevel% >> "log/err"
		echo Last Backup Time: %date% - %time% >> "log/err"
		echo Backup Type: Auto >> "log/err"
		goto :endofscript

		:next
		echo Auto Backup with AnyTime Preferences Was Success! >> "log/log"
		echo Last Backup Time: %date% - %time% >> "log/log"
		echo Backup Preferences: Anytime >> "log/log"
		echo Backup Type: Auto >> "log/log"
		echo. >> "log/log"
		goto :endofscript
	) else (
		echo. >> "log/err"
		echo Auto Backup with AnyTime Preferences Was Failed ! >> "log/err"
		echo Reason : Destination Path Was Not Exists ! >> "log/err"
		echo Source Path : %TsrcPath% >> "log/err"
		echo Destination Path : %TdestPath% >> "log/err"
		echo Last Backup Time: %date% - %time% >> "log/err"
		echo Backup Type: Auto >> "log/err"
		goto :endofscript
	)
) else (
	echo. >> "log/err"
	echo Auto Backup with AnyTime Preferences Was Failed ! >> "log/err"
	echo Reason : Source Path Was Not Exists ! >> "log/err"
	echo Source Path : %TsrcPath% >> "log/err"
	echo Destination Path : %TdestPath% >> "log/err"
	echo Last Backup Time: %date% - %time% >> "log/err"
	echo Backup Type: Auto >> "log/err"
	goto :endofscript
)

:endofscript
exit