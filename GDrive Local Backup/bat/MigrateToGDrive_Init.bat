@echo off
cd ..
set /p TtimePath=<conf/time
echo "%TtimePath%"
if %TtimePath% == Anytime goto :anytime
if %TtimePath% == Today goto :today

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

:endofline
exit