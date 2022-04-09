@echo off
set /p TsrcPath=<conf/uiSrcPath
set /p TdestPath=<conf/uiDestPath
set /p TdatePath=<conf/uiDatePath
if exist "%TsrcPath%" (
	if exist "%TdestPath%" (
		call xcopy "%TsrcPath%" "%TdestPath%" /s /d:%TdatePath% /y >> "log/log"
		if %ERRORLEVEL% == 0 goto :next
		echo. >> "log/err"
		echo err>> "log/lastResult"
		echo Backup with Today Preferences Was Failed !, error code: %errorlevel% >> "log/err"
		echo Backup with Today Preferences Was Failed !, error code: %errorlevel% >> "log/lastErr"
		echo Last Backup Time: %date% - %time% >> "log/err"
		echo Backup Type: Auto >> "log/err"
		goto :endofscript

		:next
		echo success>> "log/lastResult"
		echo Backup with Today Preferences Was Success! >> "log/log"
		echo Backup Time: %date% - %time% >> "log/log"
		echo Backup Preferences: Today >> "log/log"
		echo Backup Type: Manual >> "log/log"
		echo. >> "log/log"
		goto :endofscript
	) else (
		echo. >> "log/err"
		echo err>> "log/lastResult"
		echo Backup with Today Preferences Was Failed ! >> "log/err"
		echo Source Path : %TsrcPath% >> "log/err"
		echo Destination Path : %TdestPath% >> "log/err"
		echo Reason : Destination Path Was Not Exists ! >> "log/err"
		echo Reason : Destination Path Was Not Exists ! >> "log/lastErr"
		echo Last Backup Time: %date% - %time% >> "log/err"
		echo Backup Type: Manual >> "log/err"
		goto :endofscript
	)
) else (
	echo. >> "log/err"
	echo err>> "log/lastResult"
	echo Backup with Today Preferences Was Failed ! >> "log/err"
	echo Source Path : %TsrcPath% >> "log/err"
	echo Destination Path : %TdestPath% >> "log/err"
	echo Reason : Source Path Was Not Exists ! >> "log/err"
	echo Reason : Source Path Was Not Exists ! >> "log/lastErr"
	echo Last Backup Time: %date% - %time% >> "log/err"
	echo Backup Type: Manual >> "log/err"
	goto :endofscript
)

:endofscript
exit