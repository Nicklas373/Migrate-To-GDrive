@echo off
set /p TsrcPath=<conf/uiSrcPath
set /p TdestPath=<conf/uiDestPath
set /p TdatePath=<conf/uiDatePath
for /f "skip=1" %%x in ('wmic os get localdatetime') do if not defined MyDate set MyDate=%%x
for /f %%x in ('wmic path win32_localtime get /format:list ^| findstr "="') do set %%x
set fmonth=00%Month%
set fday=00%Day%
set today=%fmonth:~-2%/%fday:~-2%/%Year%
if exist "%TsrcPath%" (
	if exist "%TdestPath%" (
		call xcopy "%TsrcPath%" "%TdestPath%" /e /f /d:%TdatePath% /i /k /v /y >> "log/log"
		if %ERRORLEVEL% == 0 goto :next
		if %ERRORLEVEL% == 1 goto :err1
		if %ERRORLEVEL% == 2 goto :err2
		if %ERRORLEVEL% == 4 goto :err4
		if %ERRORLEVEL% == 5 goto :err5
		echo err>> "log/lastResult"
		echo # MigrateToGDrive v1.0 >> "log/err" 
		echo Backup Result		: Error >> "log/err"
		echo Reason				: Unspecified Error: %errorlevel% >> "log/err"
		echo Unspecified Error: %errorlevel% >> "log/lastErr"
		echo Source Path		: %TsrcPath% >> "log/err" 
		echo Destination Path		: %TdestPath% >> "log/err" 
		echo Backup Time		: %date% - %time% >> "log/err"
		echo Backup Pref		: From Date %TdatePath% to today >> "log/err"
		echo Backup Type		: Manual >> "log/err"
		echo. >> "log/err"
		goto :endofscript

		:err1
		echo err>> "log/lastResult"
		echo # MigrateToGDrive v1.0 >> "log/err" 
		echo Backup Result		: Error >> "log/err"
		echo Reason				: No files or folder found to backup >> "log/err"
		echo No files or folder found to backup >> "log/lastErr"
		echo Source Path		: %TsrcPath% >> "log/err 
		echo Destination Path		: %TdestPath% >> "log/err"
		echo Backup Time		: %date% - %time% >> "log/err"
		echo Backup Pref		: From Date %TdatePath% to %today% >> "log/err"
		echo Backup Type		: Manual >> "log/err"
		echo. >> "log/err"
		goto :endofscript

		:err2
		echo err>> "log/lastResult"
		echo # MigrateToGDrive v1.0 >> "log/err" 
		echo Backup Result		: Error >> "log/err"
		echo Reason				: Process terminate by user >> "log/err"
		echo Process terminate by user >> "log/lastErr"
		echo Source Path		: %TsrcPath% >> "log/err" 
		echo Destination Path		: %TdestPath% >> "log/err" 
		echo Backup Time		: %date% - %time% >> "log/err"
		echo Backup Pref		: From Date %TdatePath% to %today% >> "log/err"
		echo Backup Type		: Manual >> "log/err"
		echo. >> "log/err"
		goto :endofscript

		:err4
		echo err>> "log/lastResult"
		echo # MigrateToGDrive v1.0 >> "log/err" 
		echo Backup Result		: Error >> "log/err"
		echo Reason				: Insufficient permissions >> "log/err"
		echo Insufficient permissions >> "log/lastErr"
		echo Source Path		: %TsrcPath% >> "log/err" 
		echo Destination Path		: %TdestPath% >> "log/err" 
		echo Backup Time		: %date% - %time% >> "log/err"
		echo Backup Pref		: From Date %TdatePath% to %today% >> "log/err"
		echo Backup Type		: Manual >> "log/err"
		echo. >> "log/err"
		goto :endofscript

		:err5
		echo err>> "log/lastResult"
		echo #MigrateToGDrive v1.0 >> "log/err" 
		echo Backup Result		: Error >> "log/err"
		echo Reason				: Disk write error occured >> "log/err"
		echo Disk write error occured >> "log/lastErr"
		echo Source Path		: %TsrcPath%
		echo Destination Path		: %TdestPath%
		echo Backup Time		: %date% - %time% >> "log/err"
		echo Backup Pref		: From Date %TdatePath% to %today% >> "log/err"
		echo Backup Type		: Manual >> "log/err"
		echo. >> "log/err"
		goto :endofscript

		:next
		echo success>> "log/lastResult"
		echo # MigrateToGDrive v1.0 >> "log/log"
		echo Backup Result		: Success >> "log/log"
		echo Source Path		: %TsrcPath% >> "log/log"
		echo Destination Path		: %TdestPath% >> "log/log"
		echo Backup Time		: %date% - %time% >> "log/log"
		echo Backup Pref		: From Date %TdatePath% to %today% >> "log/log"
		echo Backup Type		: Manual >> "log/log"
		echo. >> "log/log"
		goto :endofscript
	) else (
		echo err>> "log/lastResult"
		echo # MigrateToGDrive v1.0 >> "log/err"
		echo Backup Result		: Error >> "log/err"
		echo Reason				: Destination path not exist >> "log/err"
		echo Destination path not exist >> "log/lastErr"
		echo Source Path		: %TsrcPath% >> "log/err"
		echo Destination Path		: %TdestPath% >> "log/err"
		echo Backup Time		: %date% - %time% >> "log/err"
		echo Backup Pref		: From Date %TdatePath% to %today% >> "log/err"
		echo Backup Type		: Manual >> "log/err"
		echo. >> "log/err"
		goto :endofscript
	)
) else (
	echo err>> "log/lastResult"
	echo # MigrateToGDrive v1.0 >> "log/err"
	echo Backup Result		: Error >> "log/err"
	echo Reason				: Source path not exist >> "log/err"
	echo Reason				: Source path not exist >> "log/lastErr"
	echo Source Path		: %TsrcPath% >> "log/err"
	echo Destination Path		: %TdestPath% >> "log/err"
	echo Backup Time		: %date% - %time% >> "log/err"
	echo Backup Pref		: From Date %TdatePath% to %today% >> "log/err"
	echo Backup Type		: Manual >> "log/err"
	echo. >> "log/err"
	goto :endofscript
)

:endofscript
exit