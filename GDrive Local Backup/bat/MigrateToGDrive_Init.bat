@echo off
cd ..
set /p TtimePath=<conf/timeInit
if %TtimePath% == Anytime goto :anytime
if %TtimePath% == Today goto :today
if %TtimePath% == null goto :error

:anytime
cd bat
call MigrateToGDrive_AT.bat
if %ERRORLEVEL% == 0 goto :anytime_success
echo "Error to backup with Anytime, err: %errorlevel%"
goto :endofline

:anytime_success
echo "Backup with Anytime is Success!"
goto :endofline

:today
cd bat
call MigrateToGDrive_TD.bat
if %ERRORLEVEL% == 0 goto :today_success
echo "Error to backup with today, err: %errorlevel%"
goto :endofline

:today_success
echo "Backup with Today is Success!"
goto :endofline

:error
echo. >> "log/err"
echo Auto Backup Init Was Failed ! >> "log/err"
echo Reason: Backup Preferences Was Not Exist Or Config Is Not Exist ! >> "log/err"
echo Last Backup Time: %date% - %time% >> "log/err"
echo Backup Type: Init >> "log/err"
goto :endofline

:endofline
exit