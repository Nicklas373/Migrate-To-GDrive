# MigrateToGDrive

MigrateToGDrive is a simple application that primary have aim to backup data from directory to directory that was specified from users
with several time frame options and include with scheduler management that can do backup automatically. And also support some essentials
features like backup as archive with or without encryption for security (Only for password).

# Additional library:
- 7-Zip
- SHA-256 (Encryption / Decryption)
- SyncFusion (PDF Compression)
- Windows Task Scheduler

# App Function
- Normal Backup
  * Source Folder
  * Destination Folder
  * Time Range: Anytime, Recent Date, Custom Date to Recent Date

- Advanced Backup
  * Source Folder
  * Destination Folder
  * Backup Type (Archive & Archive with password)
  * Compress level (Using 7-ZIP default compression level)
  * Compress type (.7z & .zip)
  * Password type (No encrypt, SHA-256)

- Restore Backup
  * Archive file
  * Destination Folder
  * Encryption Key (If archive was backup by using SHA-256)
  * Encryption Method (No Encrypt or SHA-256)
  
- Auto Backup
  * Source Folder
  * Destination Folder
  * Time Range: Anytime, Recent Date
  * Scheduler: Daily, Weekly
  * Daily Scheduler: Set day, Set time, Recurs every X days, Repeat task every X (minutes, hours or days), For a duration of X (minutes, hours or days)
  * Weekly Scheduler: Set day, Set time, Recurs every X weeks, Recurs in days (Multiple Specified Days), Repeat task every X (minutes, hours or days), For a duration of X (minutes, hours or days)
 
- Task Info
  * Check Task
  * Check Config
  * Run Task
  * Reset Task
  * Reset Config
 
- History Info
  * Check Backup History
  * Check Archive History
  * Check Restore History
  * Check Error History
  * Clear History
  * Export History
  
- PDF Compression
  * Source PDF
  * Save Location
  * Compression Level
  * Incremental Compression
  * Optimize Fonts
  * Optimize Page Contents
  * Remove MetaData
  * Size (Before & After compression)

# App Compatibility
- [.NET Desktop Runtime 6.0](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)

# App Screenshoot
<p align="left">
<img width="480" height="240" src="https://github.com/Nicklas373/Migrate-To-GDrive/raw/master/MigrateToGDrive/Screenshots/Main%20Menu.png">&nbsp;&nbsp;&nbsp;
<img width="480" height="240" src="https://github.com/Nicklas373/Migrate-To-GDrive/raw/master/MigrateToGDrive/Screenshots/Normal%20Backup%20Menu.png">&nbsp;&nbsp;&nbsp;
<img width="480" height="240" src="https://github.com/Nicklas373/Migrate-To-GDrive/raw/master/MigrateToGDrive/Screenshots/Advanced%20Backup%20Menu.png">&nbsp;&nbsp;&nbsp;
<img width="480" height="240" src="https://github.com/Nicklas373/Migrate-To-GDrive/raw/master/MigrateToGDrive/Screenshots/Restore%20Backup%20Menu.png">&nbsp;&nbsp;&nbsp;
<img width="480" height="240" src="https://github.com/Nicklas373/Migrate-To-GDrive/raw/master/MigrateToGDrive/Screenshots/Log%20Menu.png">&nbsp;&nbsp;&nbsp;
<img width="480" height="240" src="https://github.com/Nicklas373/Migrate-To-GDrive/raw/master/MigrateToGDrive/Screenshots/Settings%20Menu.png">&nbsp;&nbsp;&nbsp;
<img width="480" height="240" src="https://github.com/Nicklas373/Migrate-To-GDrive/raw/master/MigrateToGDrive/Screenshots/PDF%20Compress%20Menu.png">&nbsp;&nbsp;&nbsp;
<img width="480" height="240" src="https://github.com/Nicklas373/Migrate-To-GDrive/raw/master/MigrateToGDrive/Screenshots/About%20Menu.png">&nbsp;&nbsp;&nbsp;
</p>

# Note
- For installation under Program Files or Program Files (x86) make sure to set permissions for current users to grant all access
  or application will failed to read configuration file or install this application outside both of that folder, since this application
  doesn't need modification to registry. In other words, this is a portable applications.
- Google Drive logo was used due the main customized aim while making this app, i'll changes the logo and maybe with the name if that
  will make issue later
- This application in the first place have to aim to backup one folder tree that are shared on local network in the office into google drive,
  that has mapped into windows as standalone drive then backup was configure by scheduler (depends on what company want options). Google Drive
  name and logo was taken to make "familiar in case" in term of introduction to the app and function, nothing more than that.
- And also this is only for my personal usage, if anyone want to use. Just use it at your own risk !

# Additional References
- [7-ZIP](https://www.7-zip.org/)
- [SyncFusion](https://www.syncfusion.com/)

# Image Source
- <a href="https://www.flaticon.com/free-icons/history" title="history icons">History icons created by Izwar Muis - Flaticon</a>
- <a href="https://www.flaticon.com/free-icons/backup" title="backup icons">Backup icons created by Smashicons - Flaticon</a>
- <a href="https://www.flaticon.com/free-icons/backup" title="backup icons">Backup icons created by Freepik - Flaticon</a>
- <a href="https://www.flaticon.com/free-icons/dust" title="dust icons">Dust icons created by Flat Icons - Flaticon</a>
- <a href="https://www.flaticon.com/free-icons/criteria" title="criteria icons">Criteria icons created by Designspace Team - Flaticon</a>
- <a href="https://www.flaticon.com/free-icons/delete" title="delete icons">Delete icons created by Alfredo Hernandez - Flaticon</a>
- <a href="https://www.flaticon.com/free-icons/edit" title="edit icons">Edit icons created by Pixel perfect - Flaticon</a>
- <a href="https://www.flaticon.com/free-icons/reset" title="reset icons">Reset icons created by Andrean Prabowo - Flaticon</a>
- <a href="https://www.flaticon.com/free-icons/tasks" title="tasks icons">Tasks icons created by Kiranshastry - Flaticon</a>
- <a href="https://www.flaticon.com/free-icons/github" title="github icons">Github icons created by Royyan Wijaya - Flaticon</a>

# HANA-CI Build Project 2016 - 2022