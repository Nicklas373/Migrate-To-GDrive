@echo off
cd ..
set /p TtimePath=<conf/cli_backup/cliTimeInit
if %TtimePath% == Anytime goto :anytime
if %TtimePath% == Today goto :today
if %TtimePath% == null goto :error

:anytime
cd bat
call MigrateToGDrive_AT.bat
if %ERRORLEVEL% == 0 goto :anytime_success
if %ERRORLEVEL% == 1 goto :err1
if %ERRORLEVEL% == 2 goto :err2
if %ERRORLEVEL% == 4 goto :err4
if %ERRORLEVEL% == 5 goto :err5
echo # MigrateToGDrive v1.1 >> "log/err"
echo Init Result				: Error >> "log/err"
echo Reason				: Unspecified Error: %errorlevel% >> "log/err"
echo Backup Type				: Init >> "log/err"
echo. >> "log/err"
goto :endofline

:anytime_success
echo "Backup with Anytime is Success!"
goto :endofline

:today
cd bat
call MigrateToGDrive_TD.bat
if %ERRORLEVEL% == 0 goto :today_success
if %ERRORLEVEL% == 1 goto :err1
if %ERRORLEVEL% == 2 goto :err2
if %ERRORLEVEL% == 4 goto :err4
if %ERRORLEVEL% == 5 goto :err5
echo # MigrateToGDrive v1.1 >> "log/err"
echo Init Result			: Error >> "log/err"
echo Reason			: Unspecified Error: %errorlevel% >> "log/err"
echo Backup Type			: Init >> "log/err"
echo. >> "log/err"
goto :endofline

:today_success
echo "Backup with Today is Success!"
goto :endofline

:error
echo # MigrateToGDrive v1.1 >> "log/err"
echo Init Result			: Error >> "log/err"
echo Reason			: Configuration file not found >> "log/err"
echo Backup Type			: Init >> "log/err"
echo. >> "log/err"
goto :endofline

:err1
echo # MigrateToGDrive v1.1 >> "log/err" 
echo Init Result			: Error >> "log/err"
echo Reason			: Necessary files not found >> "log/err"
echo Backup Type			: Init >> "log/err"
echo. >> "log/err"
goto :endofline

:err2
echo # MigrateToGDrive v1.1 >> "log/err" 
echo Init Result			: Error >> "log/err"
echo Reason			: Terminate by users >> "log/err"
echo Backup Type			: Init >> "log/err"
echo. >> "log/err"
goto :endofline

:err4
echo # MigrateToGDrive v1.1 >> "log/err"
echo Init Result			: Error >> "log/err"
echo Reason			: Insufficient permissions >> "log/err"
echo Backup Type			: Init >> "log/err"
echo. >> "log/err"
goto :endofline

:err5
echo # MigrateToGDrive v1.1 >> "log/err" 
echo Init Result			: Error >> "log/err"
echo Reason			: Disk write error >> "log/err"
echo Backup Type			: Init >> "log/err"
echo. >> "log/err"
goto :endofline

:endofline
exit