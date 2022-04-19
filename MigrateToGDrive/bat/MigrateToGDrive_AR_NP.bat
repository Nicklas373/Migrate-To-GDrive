@echo off
set /p TinstPath=<conf\adv_backup\advInstPath
set /p TsrcPath=<conf\adv_backup\advSrcPath
set /p TdestPath=<conf\adv_backup\advDestPath
set /p TcompLevel=<conf\adv_backup\advCompLvl
set /p TcompType=<conf\adv_backup\advCompType
set /p TrndmStrg=<conf\adv_backup\advRandomStrg
set /p TcompExt=<conf\adv_backup\advCompExt
set /p TencType=<conf\adv_backup\advEncType
for /f "skip=1" %%x in ('wmic os get localdatetime') do if not defined MyDate set MyDate=%%x
for /f %%x in ('wmic path win32_localtime get /format:list ^| findstr "="') do set %%x
set fmonth=00%Month%
set fday=00%Day%
set today=%fmonth:~-2%-%fday:~-2%-%Year%
if exist "%TsrcPath%" (
	if exist "%TdestPath%" (
		call "%TinstPath%\7z\7za" a %TcompType% %TcompLvl% MigrateToGDrive_%today%_%TrndmStrg%.%TcompExt% "%TsrcPath%\*"
		if %ERRORLEVEL% == 0 goto :next
		echo err>> "log/lastResult"
		echo. >> "log/adverr"
		echo # MigrateToGDrive v1.0 >> "log/adverr"
		echo Compress Result				: Error >> "log/adverr"
		echo Compress Time					: %date% - %time% >> "log/adverr"
		echo Reason					: Unspecified Error: %errorlevel% >> "log/adverr"
		echo Compress Time				: %date% - %time% >> "log/adverr"
		echo Encryption type				: %TencType% >> "log/adverr"
		echo. >> "log/adverr"
		goto :endofscript
		
		:next
		move "*.%TcompExt%" "%TdestPath%"
		echo. >> "log/advlog"
		echo success>> "log/lastResult"
		echo # MigrateToGDrive v1.0 >> "log/advlog"
		echo Compress Result				: Success >> "log/advlog"
		echo Compress Time					: %date% - %time% >> "log/advlog"
		echo File Name					: MigrateToGDrive_%today%_%TrndmStrg%.%TcompExt% >> "log/advlog"
		echo Key File					: MigrateToGDrive_%TrndmStrg%_KEY.enc >> "log/advlog"
		echo Location					: "%TdestPath%" >> "log/advlog"
		echo Encryption type				: %TencType% >> "log/advlog"
		echo. >> "log/advlog"
		goto :endofscript
	) else (
	echo. >> "log/adverr"
		echo # MigrateToGDrive v1.0 >> "log/adverr"
		echo Compress Result				: Error >> "log/adverr"
		echo Compress Time					: %date% - %time% >> "log/adverr"
		echo Reason					: Destination path not exist! >> "log/adverr"
		echo Compress Time				: %date% - %time% >> "log/adverr"
		echo Encryption type				: %TencType% >> "log/adverr"
		echo. >> "log/adverr"
		goto :endofscript
	)
) else (
	echo. >> "log/adverr"
	echo # MigrateToGDrive v1.0 >> "log/adverr"
	echo Compress Result				: Error >> "log/adverr"
	echo Compress Time					: %date% - %time% >> "log/adverr"
	echo Reason					: Source path not exist! >> "log/adverr"
	echo Compress Time				: %date% - %time% >> "log/adverr"
	echo Encryption type				: %TencType% >> "log/adverr"
	echo. >> "log/adverr"
	goto :endofscript
)

:endofscript
exit