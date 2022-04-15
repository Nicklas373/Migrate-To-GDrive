Imports Microsoft.Win32.TaskScheduler
Public Class Settings
    Dim filedialog As New FolderBrowserDialog
    Dim confPath As String = "conf/config"
    Dim logPath As String = "log/log"
    Dim errPath As String = "log/err"
    Dim timePath As String = "conf/timeInit"
    Dim cliSrcPath As String = "conf/srcPath"
    Dim cliDestPath As String = "conf/destPath"
    Dim cliDatePath As String = "conf/datePath"
    Dim uiSrcPath As String = "conf/uiSrcPath"
    Dim uiDestPath As String = "conf/uiDestPath"
    Dim uiDatePath As String = "conf/uiDatePath"
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        TextBox1.ReadOnly = False
        TextBox2.ReadOnly = False
        Button3.Visible = True
        Button4.Visible = False
        Button5.Visible = True
        Label7.Visible = True
        ComboBox1.Visible = True
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.ReadOnly = True Then
            MsgBox("Configuration menu is locked, Please click edit !", MsgBoxStyle.Information, "MigrateToGDrive")
        Else
            filedialog.InitialDirectory = Environment.SpecialFolder.UserProfile
            filedialog.ShowDialog()
        End If
    End Sub
    Private Sub FolderBrowserDialog1_Disposed(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.ReadOnly = False Then
            TextBox1.Text = filedialog.SelectedPath.ToString
            getSrcDriveSize(2)
        End If
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If TextBox2.ReadOnly = True Then
            MsgBox("Configuration menu is locked, Please click edit !", MsgBoxStyle.Information, "MigrateToGDrive")
        Else
            filedialog.InitialDirectory = Environment.SpecialFolder.UserProfile
            filedialog.ShowDialog()
        End If
    End Sub
    Private Sub FolderBrowserDialog2_Disposed(sender As Object, e As EventArgs) Handles Button2.Click
        If TextBox2.ReadOnly = False Then
            TextBox2.Text = filedialog.SelectedPath.ToString
            getDestDriveSize(2)
        End If
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If TextBox1.Text = "" Then
            MsgBox("Please fill source folder !", MsgBoxStyle.Critical, "MigrateToGDrive")
        ElseIf TextBox2.Text = "" Then
            MsgBox("Please fill destination folder !", MsgBoxStyle.Critical, "MigrateToGDrive")
        Else
            If ComboBox1.Text = "" Then
                MsgBox("Please select backup time !", MsgBoxStyle.Critical, "MigrateToGDrive")
            Else
                If File.Exists(confPath) Then
                    GC.Collect()
                    GC.WaitForPendingFinalizers()
                    File.Delete(confPath)
                    File.Create(confPath).Dispose()
                    Dim writer As New StreamWriter(confPath, True)
                    writer.WriteLine("Migrate To GDrive Config v1.0")
                    writer.WriteLine("Source Directory: " & TextBox1.Text)
                    writer.WriteLine("Destination Directory: " & TextBox2.Text)
                    writer.WriteLine("Backup Preferences: " & ComboBox1.Text)
                    writer.Close()
                    writeForAutoBackup()
                    MsgBox("Config created !", MsgBoxStyle.Information, "MigrateToGDrive")
                Else
                    File.Create(confPath).Dispose()
                    Dim writer As New StreamWriter(confPath, True)
                    writer.WriteLine("Migrate To GDrive Config v1.0")
                    writer.WriteLine("Source Directory: " & TextBox1.Text)
                    writer.WriteLine("Destination Directory: " & TextBox2.Text)
                    writer.WriteLine("Backup Preferences: " & ComboBox1.Text)
                    writer.Close()
                    writeForAutoBackup()
                    MsgBox("Config created !", MsgBoxStyle.Information, "MigrateToGDrive")
                End If
                TextBox1.ReadOnly = True
                TextBox2.ReadOnly = True
                Button3.Visible = False
                Button4.Visible = True
                Button5.Visible = False
                Label7.Visible = False
                ComboBox1.Visible = False
            End If
        End If
    End Sub
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        TextBox1.ReadOnly = True
        TextBox1.Text = ""
        TextBox2.Text = ""
        Label1.Text = ""
        Label2.Text = ""
        ComboBox1.SelectedIndex = 0
        TextBox2.ReadOnly = True
        Button3.Visible = False
        Button4.Visible = True
        Button5.Visible = False
        Label7.Visible = False
        ComboBox1.Visible = False
    End Sub
    Private Sub Button7_Click_2(sender As Object, e As EventArgs) Handles Button7.Click
        ComboBox2.Enabled = True
        Button6.Visible = True
        Button7.Visible = False
        Button8.Visible = True
        TextBox3.Text = ""
        TextBox4.Text = ""
        ComboBox4.SelectedIndex = 0
        ComboBox5.SelectedIndex = 0
        ComboBox6.SelectedIndex = 0
        ComboBox7.SelectedIndex = 0
        CheckBox7.Checked = False
        CheckBox1.Checked = False
        CheckBox2.Checked = False
        CheckBox3.Checked = False
        CheckBox4.Checked = False
        CheckBox5.Checked = False
        CheckBox6.Checked = False
        If GroupBox4.Visible = True Then
            GroupBox4.Enabled = False
        End If
        If GroupBox5.Enabled = True Then
            GroupBox5.Enabled = False
        End If
    End Sub
    Private Sub Button6_Click_1(sender As Object, e As EventArgs) Handles Button6.Click
        If ComboBox2.Text = "" Then
            MsgBox("Please select backup trigger first !", MsgBoxStyle.Critical, "MigrateToGDrive")
        Else
            If ComboBox2.Text = "Daily" Then
                If TextBox3.Text = "" Then
                    MsgBox("Recurs day can not empty !", MsgBoxStyle.Critical, "MigrateToGDrive")
                Else
                    Dim custdate As String = DateTimePicker3.Value.ToLongDateString & " " & DateTimePicker4.Value.ToLongTimeString
                    If ComboBox4.Text = "Disabled" Then
                        ComboBox5.SelectedIndex = 0
                        MsgBox("If repeat task is disabled, then repeat duration will be disable", vbExclamation, "MigrateToGDrive")
                    End If
                    If ComboBox5.Text = "Disabled" Then
                        ComboBox4.SelectedIndex = 0
                        ComboBox5.SelectedIndex = 0
                        MsgBox("If repeat duration is disabled, then repeat task will be disable", vbExclamation, "MigrateToGDrive")
                    End If
                    DailyTrigger(custdate, 1, CInt(TextBox3.Text), custRepDurValDaily(), custRepDurIntDaily())
                    ComboBox2.Enabled = False
                    GroupBox4.Enabled = False
                    Button6.Visible = False
                    Button7.Visible = True
                    Button8.Visible = False
                End If
            ElseIf ComboBox2.Text = "Weekly" Then
                If TextBox4.Text = "" Then
                    MsgBox("Recurs week can not empty !", MsgBoxStyle.Critical, "MigrateToGDrive")
                Else
                    If CheckBox7.Checked = False And CheckBox1.Checked = False And CheckBox2.Checked = False And CheckBox3.Checked = False And CheckBox4.Checked = False And CheckBox5.Checked = False And CheckBox6.Checked = False Then
                        MsgBox("Recurs in day can not empty !", vbCritical, "MigrateToGDrive")
                    Else
                        Dim custdate As String = DateTimePicker5.Value.ToLongDateString & " " & DateTimePicker6.Value.ToLongTimeString
                        If ComboBox7.Text = "Disabled" Then
                            ComboBox6.SelectedIndex = 0
                            MsgBox("If repeat task is disabled, then repeat duration will be disable", vbExclamation, "MigrateToGDrive")
                        End If
                        If ComboBox6.Text = "Disabled" Then
                            ComboBox7.SelectedIndex = 0
                            ComboBox6.SelectedIndex = 0
                            MsgBox("If repeat duration is disabled, then repeat task will be disable", vbExclamation, "MigrateToGDrive")
                        End If
                        WeeklyTrigger(custdate, 1, CInt(TextBox4.Text), custRepDurValWeek, custRepDurIntWeek, cb1, cb2, cb3, cb4, cb5, cb6, cb7)
                        ComboBox2.Enabled = False
                        GroupBox5.Enabled = False
                        Button6.Visible = False
                        Button7.Visible = True
                        Button8.Visible = False
                    End If
                End If
            End If
        End If
    End Sub
    Private Sub Button8_Click_1(sender As Object, e As EventArgs) Handles Button8.Click
        GroupBox4.Enabled = False
        GroupBox5.Enabled = False
        ComboBox2.Enabled = False
        Button6.Visible = False
        Button7.Visible = True
        Button8.Visible = False
    End Sub
    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        If ComboBox2.Text = "Weekly" Then
            GroupBox5.Enabled = True
            GroupBox4.Enabled = False
            TextBox4.Text = ""
            ComboBox6.SelectedIndex = 0
            ComboBox7.SelectedIndex = 0
            CheckBox7.Checked = False
            CheckBox1.Checked = False
            CheckBox2.Checked = False
            CheckBox3.Checked = False
            CheckBox4.Checked = False
            CheckBox5.Checked = False
            CheckBox6.Checked = False
        ElseIf ComboBox2.Text = "Daily" Then
            GroupBox4.Enabled = True
            GroupBox5.Enabled = False
            TextBox3.Text = ""
            ComboBox4.SelectedIndex = 0
            ComboBox5.SelectedIndex = 0
        End If
    End Sub
    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        ShowTask("MigrateToGDrive")
    End Sub
    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        showLog("Config", confPath)
    End Sub
    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        Using tService As New TaskService()
            Dim tTask As Task = tService.GetTask("MigrateToGDrive")
            If tTask Is Nothing Then
                MsgBox("MigrateToGDrive scheduler not exist !", MsgBoxStyle.Information, "MigrateToGDrive")
            Else
                tService.RootFolder.DeleteTask("MigrateToGDrive")
                RichTextBox1.Text = ""
                MsgBox("MigrateToGDrive scheduler removed !", MsgBoxStyle.Information, "MigrateToGDrive")
            End If
        End Using
    End Sub
    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        clearLog(confPath, "Config")
        If File.Exists(timePath) Then
            GC.Collect()
            GC.WaitForPendingFinalizers()
            File.Delete(timePath)
            File.Create(timePath).Dispose()
            Dim timeWriter As New StreamWriter(timePath, True)
            timeWriter.WriteLine("null")
            timeWriter.Close()
        Else
            GC.Collect()
            GC.WaitForPendingFinalizers()
            File.Delete(timePath)
            File.Create(timePath).Dispose()
            Dim timeWriter As New StreamWriter(timePath, True)
            timeWriter.WriteLine("null")
            timeWriter.Close()
        End If
        TextBox1.Text = ""
        TextBox2.Text = ""
        Label1.Text = ""
        Label2.Text = ""
        ComboBox1.ResetText()
    End Sub
    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        If File.Exists(confPath) Then
            Using tService As New TaskService()
                Dim tTask As Task = tService.GetTask("MigrateToGDrive")
                If tTask Is Nothing Then
                    MsgBox("MigrateToGDrive scheduler not exist !", MsgBoxStyle.Critical, "MigrateToGDrive")
                    MsgBox("Please create new scheduler first !", MsgBoxStyle.Critical, "MigrateToGDrive")
                Else
                    tTask.Run()
                    MsgBox("MigrateToGDrive is running !", MsgBoxStyle.Information, "MigrateToGDrive")
                End If
            End Using
        Else
            MsgBox("Config file is not exist !, Please configure directory first !", MsgBoxStyle.Critical, "MigrateToGDrive")
        End If
    End Sub
    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged
        If TextBox3.Text = "0" Then
            MsgBox("Can not set 0 days for recurs day !", MsgBoxStyle.Critical, "MigrateToGDrive")
            MsgBox("Please set another value", MsgBoxStyle.Information, "MigrateToGDrive")
            TextBox3.Text = ""
        End If
    End Sub
    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles TextBox4.TextChanged
        If TextBox4.Text = "0" Then
            MsgBox("Can not set 0 days for recurs day !", MsgBoxStyle.Critical, "MigrateToGDrive")
            MsgBox("Please set another value", MsgBoxStyle.Information, "MigrateToGDrive")
            TextBox4.Text = ""
        End If
    End Sub
    Private Sub ComboBox4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox4.SelectedIndexChanged
        If GroupBox4.Enabled = True Then
            If ComboBox4.Text = "Disabled" Then
                ComboBox5.SelectedIndex = 0
                MsgBox("If repeat task is disabled, then repeat duration will be disable", vbExclamation, "MigrateToGDrive")
            End If
        End If
    End Sub

    Private Sub ComboBox5_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox5.SelectedIndexChanged
        If GroupBox4.Enabled = True Then
            If ComboBox5.Text = "Disabled" Then
                ComboBox4.SelectedIndex = 0
                ComboBox5.SelectedIndex = 0
                MsgBox("If repeat duration is disabled, then repeat task will be disable", vbExclamation, "MigrateToGDrive")
            End If
        End If
    End Sub

    Private Sub ComboBox7_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox7.SelectedIndexChanged
        If GroupBox5.Enabled = True Then
            If ComboBox7.Text = "Disabled" Then
                ComboBox6.SelectedIndex = 0
                MsgBox("If repeat task is disabled, then repeat duration will be disable", vbExclamation, "MigrateToGDrive")
            End If
        End If
    End Sub

    Private Sub ComboBox6_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox6.SelectedIndexChanged
        If GroupBox5.Enabled = True Then
            If ComboBox6.Text = "Disabled" Then
                ComboBox7.SelectedIndex = 0
                ComboBox6.SelectedIndex = 0
                MsgBox("If repeat duration is disabled, then repeat task will be disable", vbExclamation, "MigrateToGDrive")
            End If
        End If
    End Sub
    Private Sub Settings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        GroupBox4.Enabled = False
        GroupBox5.Enabled = False
        Button6.Visible = False
        Button8.Visible = False
        DateTimePicker4.Format = DateTimePickerFormat.Time
        DateTimePicker4.ShowUpDown = True
        DateTimePicker6.Format = DateTimePickerFormat.Time
        DateTimePicker6.ShowUpDown = True
        ComboBox2.Enabled = False
        getSrcDriveSize(1)
        getDestDriveSize(1)
        getBackPref()
    End Sub
    Private Sub DailyTrigger(custDate As String, repDayInt As Integer, custInt As Integer, custrepdur As Integer, custrepint As Integer)
        Dim appPath As String = Application.StartupPath()
        Using tService As New TaskService()
            Dim tTask As Task = tService.GetTask("MigrateToGDrive")
            Dim repDurVal As Integer = custRepDurValDecDaily()
            Dim repDurInt As Integer = custRepDurIntDecDaily()
            Dim tDefinition As TaskDefinition = tService.NewTask
            Dim tTrigger As New DailyTrigger()
            If tTask Is Nothing Then
                tDefinition.RegistrationInfo.Description = "MigrateToGDrive - Daily Task"
                tTrigger.StartBoundary = custDate
                If repDayInt = 1 Then
                    tTrigger.DaysInterval = custInt
                End If
                If repDurInt = 1 Then
                    tTrigger.Repetition.Interval = TimeSpan.FromMinutes(custrepint)
                ElseIf repDurInt = 2 Then
                    tTrigger.Repetition.Interval = TimeSpan.FromHours(custrepint)
                End If
                If repDurVal = 1 Then
                    tTrigger.Repetition.Duration = TimeSpan.FromMinutes(custrepdur)
                ElseIf repDurVal = 2 Then
                    tTrigger.Repetition.Duration = TimeSpan.FromHours(custrepdur)
                ElseIf repDurVal = 3 Then
                    tTrigger.Repetition.Duration = TimeSpan.FromDays(custrepdur)
                End If
                tDefinition.Triggers.Add(tTrigger)
                tDefinition.Actions.Add(New ExecAction(appPath & "bat\MigrateToGDrive_Init.bat", "", appPath & "bat"))
                tService.RootFolder.RegisterTaskDefinition("MigrateToGDrive", tDefinition)
                FindTask("MigrateToGDrive")
            Else
                Dim validation As Integer
                validation = MsgBox("Old scheduler already exist !, Create a new scheduler ?", vbExclamation + vbYesNo + vbDefaultButton2, "MigrateToGDrive")
                If validation = vbYes Then
                    tService.RootFolder.DeleteTask("MigrateToGDrive")
                    tDefinition.RegistrationInfo.Description = "MigrateToGDrive - Daily Task"
                    tTrigger.StartBoundary = custDate
                    If repDayInt = 1 Then
                        tTrigger.DaysInterval = custInt
                    End If
                    If repDurInt = 1 Then
                        tTrigger.Repetition.Interval = TimeSpan.FromMinutes(custrepint)
                    ElseIf repDurInt = 2 Then
                        tTrigger.Repetition.Interval = TimeSpan.FromHours(custrepint)
                    End If
                    If repDurVal = 1 Then
                        tTrigger.Repetition.Duration = TimeSpan.FromMinutes(custrepdur)
                    ElseIf repDurVal = 2 Then
                        tTrigger.Repetition.Duration = TimeSpan.FromHours(custrepdur)
                    ElseIf repDurVal = 3 Then
                        tTrigger.Repetition.Duration = TimeSpan.FromDays(custrepdur)
                    End If
                    tDefinition.Triggers.Add(tTrigger)
                    tDefinition.Actions.Add(New ExecAction(appPath & "bat\MigrateToGDrive_Init.bat", "", appPath & "bat"))
                    tService.RootFolder.RegisterTaskDefinition("MigrateToGDrive", tDefinition)
                    FindTask("MigrateToGDrive")
                Else
                    MsgBox("Cancel To Created Scheduler !", MsgBoxStyle.Information, "MigrateToGDrive")
                End If
            End If
        End Using
    End Sub
    Private Sub WeeklyTrigger(custdate As String, repDayInt As Integer, custInt As Integer, custrepdur As Integer, custrepint As Integer, cb1 As DaysOfTheWeek, cb2 As DaysOfTheWeek, cb3 As DaysOfTheWeek, cb4 As DaysOfTheWeek, cb5 As DaysOfTheWeek, cb6 As DaysOfTheWeek, cb7 As DaysOfTheWeek)
        Dim appPath As String = Application.StartupPath()
        Dim repDurVal As Integer = custRepDurValDecWeek()
        Dim repDurInt As Integer = custRepDurIntDecWeek()
        Using tService As New TaskService()
            Dim tTask As Task = tService.GetTask("MigrateToGDrive")
            Dim tDefinition As TaskDefinition = tService.NewTask
            Dim tTrigger As New WeeklyTrigger()
            If tTask Is Nothing Then
                tDefinition.RegistrationInfo.Description = "MigrateToGDrive - Weekly Task"
                tTrigger.StartBoundary = custdate
                tTrigger.DaysOfWeek = cb1 Or cb2 Or cb3 Or cb4 Or cb5 Or cb6 Or cb7
                If repDayInt = 1 Then
                    tTrigger.WeeksInterval = custInt
                End If
                If repDurInt = 1 Then
                    tTrigger.Repetition.Interval = TimeSpan.FromMinutes(custrepint)
                ElseIf repDurInt = 2 Then
                    tTrigger.Repetition.Interval = TimeSpan.FromHours(custrepint)
                End If
                If repDurVal = 1 Then
                    tTrigger.Repetition.Duration = TimeSpan.FromMinutes(custrepdur)
                ElseIf repDurVal = 2 Then
                    tTrigger.Repetition.Duration = TimeSpan.FromHours(custrepdur)
                ElseIf repDurVal = 3 Then
                    tTrigger.Repetition.Duration = TimeSpan.FromDays(custrepdur)
                End If
                tDefinition.Triggers.Add(tTrigger)
                tDefinition.Actions.Add(New ExecAction(appPath & "bat\MigrateToGDrive_Init.bat", "", appPath & "bat"))
                tService.RootFolder.RegisterTaskDefinition("MigrateToGDrive", tDefinition)
                FindTask("MigrateToGDrive")
            Else
                Dim validation As Integer
                validation = MsgBox("Old scheduler already exist !, Create a new scheduler ?", vbExclamation + vbYesNo + vbDefaultButton2, "MigrateToGDrive")
                If validation = vbYes Then
                    tService.RootFolder.DeleteTask("MigrateToGDrive")
                    tDefinition.RegistrationInfo.Description = "MigrateToGDrive - Weekly Task"
                    tTrigger.StartBoundary = custdate
                    tTrigger.DaysOfWeek = cb1 Or cb2 Or cb3 Or cb4 Or cb5 Or cb6 Or cb7
                    If repDayInt = 1 Then
                        tTrigger.WeeksInterval = custInt
                    End If
                    If repDurInt = 1 Then
                        tTrigger.Repetition.Interval = TimeSpan.FromMinutes(custrepint)
                    ElseIf repDurInt = 2 Then
                        tTrigger.Repetition.Interval = TimeSpan.FromHours(custrepint)
                    End If
                    If repDurVal = 1 Then
                        tTrigger.Repetition.Duration = TimeSpan.FromMinutes(custrepdur)
                    ElseIf repDurVal = 2 Then
                        tTrigger.Repetition.Duration = TimeSpan.FromHours(custrepdur)
                    ElseIf repDurVal = 3 Then
                        tTrigger.Repetition.Duration = TimeSpan.FromDays(custrepdur)
                    End If
                    tDefinition.Triggers.Add(tTrigger)
                    tDefinition.Actions.Add(New ExecAction(appPath & "bat\MigrateToGDrive_Init.bat", "", appPath & "bat"))
                    tService.RootFolder.RegisterTaskDefinition("MigrateToGDrive", tDefinition)
                    FindTask("MigrateToGDrive")
                Else
                    MsgBox("Cancel created scheduler !", MsgBoxStyle.Information, "MigrateToGDrive")
                End If
            End If
        End Using
    End Sub
    Private Function confVal(line As Integer) As String
        Dim value As String
        If New FileInfo(confPath).Length = 0 Then
            value = "null"
            Return value
        Else
            value = File.ReadAllLines(confPath).ElementAt(line).ToString
            Return value
        End If
    End Function
    Private Sub getSrcDriveSize(conf As Integer)
        Dim trimSrc As String
        If conf = 1 Then
            If File.Exists(confPath) Then
                If confVal(1).Equals("null") Then
                    Label2.Text = ""
                Else
                    trimSrc = confVal(1).Replace("Source Directory: ", "")
                    If Directory.Exists(trimSrc) Then
                        Dim freeSpaceSrc As Double = (My.Computer.FileSystem.GetDriveInfo(trimSrc.Remove(3)).TotalFreeSpace / 1024 / 1024 / 1024)
                        Dim totalspaceSrc As Double = (My.Computer.FileSystem.GetDriveInfo(trimSrc.Remove(3)).TotalSize / 1024 / 1024 / 1024)
                        TextBox1.Text = trimSrc
                        Label2.Text = ""
                        Label2.Text = "Source drive size : " & Format(freeSpaceSrc, "###.##").ToString & " GB" & " | " & Format(totalspaceSrc, "###.##").ToString & " GB"
                    Else
                        TextBox1.Text = "Source directory not exist !"
                        Label2.Text = ""
                    End If
                End If
            End If
        ElseIf conf = 2 Then
            Dim driveSrcPath As String
            If TextBox1.Text = "" Then
                Label2.Text = ""
            ElseIf TextBox1.Text = "Source directory not exist !" Then
                Label2.Text = ""
            Else
                driveSrcPath = TextBox1.Text
                Dim freeSpaceSrc As Double = (My.Computer.FileSystem.GetDriveInfo(driveSrcPath.Remove(3)).TotalFreeSpace / 1024 / 1024 / 1024)
                Dim totalspaceSrc As Double = (My.Computer.FileSystem.GetDriveInfo(driveSrcPath.Remove(3)).TotalSize / 1024 / 1024 / 1024)
                Label2.Text = ""
                Label2.Text = "Source drive size : " & Format(freeSpaceSrc, "###.##").ToString & " GB" & " | " & Format(totalspaceSrc, "###.##").ToString & " GB"
            End If
        End If
    End Sub
    Private Sub getDestDriveSize(conf As Integer)
        Dim trimDest As String
        If conf = 1 Then
            If File.Exists(confPath) Then
                If confVal(2).Equals("null") Then
                    Label1.Text = ""
                Else
                    trimDest = confVal(2).Replace("Destination Directory: ", "")
                    If Directory.Exists(trimDest) Then
                        Dim freeSpaceDest As Double = (My.Computer.FileSystem.GetDriveInfo(trimDest.Remove(3)).TotalFreeSpace / 1024 / 1024 / 1024)
                        Dim totalspaceDest As Double = (My.Computer.FileSystem.GetDriveInfo(trimDest.Remove(3)).TotalSize / 1024 / 1024 / 1024)
                        TextBox2.Text = trimDest
                        Label1.Text = ""
                        Label1.Text = "Destination drive size : " & Format(freeSpaceDest, "###.##").ToString & " GB" & " | " & Format(totalspaceDest, "###.##").ToString & " GB"
                    Else
                        TextBox2.Text = "Destination drive not exists !"
                        Label1.Text = ""
                    End If
                End If
            End If
        ElseIf conf = 2 Then
            Dim driveDestPath As String
            If TextBox2.Text = "" Then
                Label1.Text = ""
            ElseIf TextBox2.Text.Equals("Destination drive not exists !") Then
                Label1.Text = ""
            Else
                driveDestPath = TextBox2.Text
                Dim freeSpaceDest As Double = (My.Computer.FileSystem.GetDriveInfo(driveDestPath.Remove(3)).TotalFreeSpace / 1024 / 1024 / 1024)
                Dim totalspaceDest As Double = (My.Computer.FileSystem.GetDriveInfo(driveDestPath.Remove(3)).TotalSize / 1024 / 1024 / 1024)
                Label1.Text = ""
                Label1.Text = "Destination drive size : " & Format(freeSpaceDest, "###.##").ToString & " GB" & " | " & Format(totalspaceDest, "###.##").ToString & " GB"
            End If
        End If
    End Sub
    Private Sub checkFileExist(path As String, trim As String)
        If File.Exists(path) Then
            GC.Collect()
            GC.WaitForPendingFinalizers()
            File.Delete(path)
            File.Create(path).Dispose()
            Dim writer As New StreamWriter(path, True)
            writer.WriteLine(trim)
            writer.Close()
        Else
            File.Create(path).Dispose()
            Dim writer As New StreamWriter(path, True)
            writer.WriteLine(trim)
            writer.Close()
        End If
    End Sub
    Private Sub getBackPref()
        If File.Exists(confPath) Then
            If confVal(3).Replace("Backup Preferences: ", "").Equals("Anytime") Then
                ComboBox1.SelectedIndex = 0
            ElseIf confVal(3).Replace("Backup Preferences: ", "").Equals("Today") Then
                ComboBox1.SelectedIndex = 1
            End If
        End If
    End Sub
    Private Sub clearLog(log As String, log2 As String)
        If File.Exists(log) Then
            File.Delete(log)
            File.Create(log).Dispose()
            MsgBox(log2 & " file reset !", MsgBoxStyle.Information, "MigrateToGDrive")
        Else
            MsgBox(log2 & " file is not exist !", MsgBoxStyle.Critical, "MigrateToGDrive")
        End If
    End Sub
    Private Sub writeForAutoBackup()
        Dim trimSrc As String
        Dim trimDest As String
        Dim trimBak As String
        trimSrc = confVal(1).Replace("Source Directory: ", "")
        trimDest = confVal(2).Replace("Destination Directory: ", "")
        trimBak = confVal(3).Replace("Backup Preferences: ", "")
        checkFileExist(cliSrcPath, trimSrc)
        checkFileExist(cliDestPath, trimDest)
        If trimBak = "null" Then
            MsgBox("Please select backup preferences first !", MsgBoxStyle.Critical, "MigrateToGDrive")
        Else
            If trimBak = "Today" Then
                If File.Exists(cliDatePath) Then
                    GC.Collect()
                    GC.WaitForPendingFinalizers()
                    File.Delete(cliDatePath)
                    File.Create(cliDatePath).Dispose()
                    Dim destWriter As New StreamWriter(cliDatePath, True)
                    Dim dt As Date = Today
                    destWriter.WriteLine(dt.ToString("MM-dd-yyyy"))
                    destWriter.Close()
                    If File.Exists(timePath) Then
                        GC.Collect()
                        GC.WaitForPendingFinalizers()
                        File.Delete(timePath)
                        File.Create(timePath).Dispose()
                        Dim timeWriter As New StreamWriter(timePath, True)
                        timeWriter.WriteLine("Today")
                        timeWriter.Close()
                    Else
                        GC.Collect()
                        GC.WaitForPendingFinalizers()
                        File.Delete(timePath)
                        File.Create(timePath).Dispose()
                        Dim timeWriter As New StreamWriter(timePath, True)
                        timeWriter.WriteLine("null")
                        timeWriter.Close()
                    End If
                End If
            Else
                File.Create(cliDatePath).Dispose()
                Dim destWriter As New StreamWriter(cliDatePath, True)
                Dim dt As Date = Today
                destWriter.WriteLine("Anytime")
                destWriter.Close()
                If File.Exists(timePath) Then
                    GC.Collect()
                    GC.WaitForPendingFinalizers()
                    File.Delete(timePath)
                    File.Create(timePath).Dispose()
                    Dim timeWriter As New StreamWriter(timePath, True)
                    timeWriter.WriteLine("Anytime")
                    timeWriter.Close()
                Else
                    GC.Collect()
                    GC.WaitForPendingFinalizers()
                    File.Delete(timePath)
                    File.Create(timePath).Dispose()
                    Dim timeWriter As New StreamWriter(timePath, True)
                    timeWriter.WriteLine("null")
                    timeWriter.Close()
                End If
            End If
        End If
    End Sub
    Private Sub FindTask(strTaskName As String)
        Using tService As New TaskService()
            Dim tTask As Task = tService.GetTask(strTaskName)
            If tTask Is Nothing Then
                MsgBox("Scheduler failed to create !", MsgBoxStyle.Critical, "MigrateToGDrive")
            Else
                MsgBox("Scheduler successfully created !", MsgBoxStyle.Information, "MigrateToGDrive")
            End If
        End Using
    End Sub
    Private Sub ShowTask(taskName As String)
        Using tService As New TaskService()
            Dim tTask As Task = tService.GetTask(taskName)
            If tTask Is Nothing Then
                MsgBox("MigrateToGDrive scheduler not exist !", MsgBoxStyle.Critical, "MigrateToGDrive")
                MsgBox("Please create new scheduler first !", MsgBoxStyle.Critical, "MigrateToGDrive")
                RichTextBox1.Text = ""
            Else
                RichTextBox1.Text = "Task Name: " & tTask.Name & vbCrLf & "Task State: " & tTask.State.ToString & vbCrLf &
                                    "Task Path: " & tTask.Path.ToString & vbCrLf &
                                    "Next Runtime: " & tTask.NextRunTime.ToLongDateString & " " & tTask.NextRunTime.ToLongTimeString & vbCrLf &
                                    "Last Runtime: " & tTask.LastRunTime.ToLongDateString & " " & tTask.LastRunTime.ToLongTimeString & vbCrLf &
                                    "Last Task Result: " & tTask.LastTaskResult.ToString & vbCrLf &
                                    "Total Failed Task: " & tTask.NumberOfMissedRuns.ToString & vbCrLf
            End If
        End Using
    End Sub
    Private Sub showLog(log As String, path As String)
        RichTextBox1.Text = ""
        If File.Exists(path) Then
            If New FileInfo(path).Length.Equals(0) Then
                MsgBox(log & " file is empty !", MsgBoxStyle.Critical, "MigrateToGDrive")
            Else
                RichTextBox1.Text = File.ReadAllText(path)
            End If
        Else
            MsgBox(log & " file does not exist !", MsgBoxStyle.Critical, "MigrateToGDrive")
        End If
    End Sub
    Private Function custRepDurIntDaily() As Integer
        Dim task As Integer
        If ComboBox4.Text = "Disabled" Then
            task = 0
            Return task
        ElseIf ComboBox4.Text = "5 Minutes" Then
            task = 5
            Return task
        ElseIf ComboBox4.Text = "10 Minutes" Then
            task = 10
            Return task
        ElseIf ComboBox4.Text = "15 Minutes" Then
            task = 15
            Return task
        ElseIf ComboBox4.Text = "30 Minutes" Then
            task = 30
            Return task
        ElseIf ComboBox4.Text = "1 Hours" Then
            task = 1
            Return task
        Else
            task = 0
            Return task
        End If
    End Function
    Private Function custRepDurValDaily() As Integer
        Dim repDurValue As Integer
        If ComboBox5.Text = "Disabled" Then
            repDurValue = 0
            Return repDurValue = 0
        ElseIf ComboBox5.Text = "15 Minutes" Then
            repDurValue = 15
            Return repDurValue
        ElseIf ComboBox5.Text = "30 Minutes" Then
            repDurValue = 30
            Return repDurValue
        ElseIf ComboBox5.Text = "1 Hours" Then
            repDurValue = 1
            Return repDurValue
        ElseIf ComboBox5.Text = "12 Hours" Then
            repDurValue = 12
            Return repDurValue
        ElseIf ComboBox5.Text = "1 Day" Then
            repDurValue = 1
            Return repDurValue
        Else
            repDurValue = 0
            Return repDurValue
        End If
    End Function
    Private Function custRepDurIntDecDaily() As Integer
        Dim repDurIntDec As Integer
        If ComboBox4.Text = "Disabled" Then
            repDurIntDec = 0
            Return repDurIntDec
        ElseIf ComboBox4.Text = "5 Minutes" Then
            repDurIntDec = 1
            Return repDurIntDec
        ElseIf ComboBox4.Text = "10 Minutes" Then
            repDurIntDec = 1
            Return repDurIntDec
        ElseIf ComboBox4.Text = "15 Minutes" Then
            repDurIntDec = 1
            Return repDurIntDec
        ElseIf ComboBox4.Text = "30 Minutes" Then
            repDurIntDec = 1
            Return repDurIntDec
        ElseIf ComboBox4.Text = "1 Hours" Then
            repDurIntDec = 2
            Return repDurIntDec
        Else
            repDurIntDec = 0
            Return repDurIntDec
        End If
    End Function
    Private Function custRepDurValDecDaily() As Integer
        Dim repDurValDec As Integer
        If ComboBox5.Text = "Disabled" Then
            repDurValDec = 0
            Return repDurValDec
        ElseIf ComboBox5.Text = "15 Minutes" Then
            repDurValDec = 1
            Return repDurValDec
        ElseIf ComboBox5.Text = "30 Minutes" Then
            repDurValDec = 1
            Return repDurValDec
        ElseIf ComboBox5.Text = "1 Hours" Then
            repDurValDec = 2
            Return repDurValDec
        ElseIf ComboBox5.Text = "12 Hours" Then
            repDurValDec = 2
            Return repDurValDec
        ElseIf ComboBox5.Text = "1 Day" Then
            repDurValDec = 3
            Return repDurValDec
        Else
            repDurValDec = 0
            Return repDurValDec
        End If
    End Function

    Private Function custRepDurIntWeek() As Integer
        Dim task As Integer
        If ComboBox7.Text = "Disabled" Then
            task = 0
            Return task
        ElseIf ComboBox7.Text = "5 Minutes" Then
            task = 5
            Return task
        ElseIf ComboBox7.Text = "10 Minutes" Then
            task = 10
            Return task
        ElseIf ComboBox7.Text = "15 Minutes" Then
            task = 15
            Return task
        ElseIf ComboBox7.Text = "30 Minutes" Then
            task = 30
            Return task
        ElseIf ComboBox7.Text = "1 Hours" Then
            task = 1
            Return task
        Else
            task = 0
            Return task
        End If
    End Function
    Private Function custRepDurValWeek() As Integer
        Dim repDurValue As Integer
        If ComboBox6.Text = "Disabled" Then
            repDurValue = 0
            Return repDurValue = 0
        ElseIf ComboBox6.Text = "15 Minutes" Then
            repDurValue = 15
            Return repDurValue
        ElseIf ComboBox6.Text = "30 Minutes" Then
            repDurValue = 30
            Return repDurValue
        ElseIf ComboBox6.Text = "1 Hours" Then
            repDurValue = 1
            Return repDurValue
        ElseIf ComboBox6.Text = "12 Hours" Then
            repDurValue = 12
            Return repDurValue
        ElseIf ComboBox6.Text = "1 Day" Then
            repDurValue = 1
            Return repDurValue
        Else
            repDurValue = 0
            Return repDurValue
        End If
    End Function
    Private Function custRepDurIntDecWeek() As Integer
        Dim repDurIntDec As Integer
        If ComboBox7.Text = "Disabled" Then
            repDurIntDec = 0
            Return repDurIntDec
        ElseIf ComboBox7.Text = "5 Minutes" Then
            repDurIntDec = 1
            Return repDurIntDec
        ElseIf ComboBox7.Text = "10 Minutes" Then
            repDurIntDec = 1
            Return repDurIntDec
        ElseIf ComboBox7.Text = "15 Minutes" Then
            repDurIntDec = 1
            Return repDurIntDec
        ElseIf ComboBox7.Text = "30 Minutes" Then
            repDurIntDec = 1
            Return repDurIntDec
        ElseIf ComboBox7.Text = "1 Hours" Then
            repDurIntDec = 2
            Return repDurIntDec
        Else
            repDurIntDec = 0
            Return repDurIntDec
        End If
    End Function
    Private Function custRepDurValDecWeek() As Integer
        Dim repDurValDec As Integer
        If ComboBox6.Text = "Disabled" Then
            repDurValDec = 0
            Return repDurValDec
        ElseIf ComboBox6.Text = "15 Minutes" Then
            repDurValDec = 1
            Return repDurValDec
        ElseIf ComboBox6.Text = "30 Minutes" Then
            repDurValDec = 1
            Return repDurValDec
        ElseIf ComboBox6.Text = "1 Hours" Then
            repDurValDec = 2
            Return repDurValDec
        ElseIf ComboBox6.Text = "12 Hours" Then
            repDurValDec = 2
            Return repDurValDec
        ElseIf ComboBox6.Text = "1 Day" Then
            repDurValDec = 3
            Return repDurValDec
        Else
            repDurValDec = 0
            Return repDurValDec
        End If
    End Function
    Private Function cb1() As DaysOfTheWeek
        Dim monday As DaysOfTheWeek
        If CheckBox1.Checked Then
            monday = DaysOfTheWeek.Monday
            Return monday
        Else
            monday = DaysOfTheWeek.Sunday
            Return monday
        End If
    End Function
    Private Function cb2() As DaysOfTheWeek
        Dim tuesday As DaysOfTheWeek
        If CheckBox2.Checked Then
            tuesday = DaysOfTheWeek.Tuesday
            Return tuesday
        Else
            tuesday = DaysOfTheWeek.Sunday
            Return tuesday
        End If
    End Function
    Private Function cb3() As DaysOfTheWeek
        Dim wednesday As DaysOfTheWeek
        If CheckBox3.Checked Then
            wednesday = DaysOfTheWeek.Wednesday
            Return wednesday
        Else
            wednesday = DaysOfTheWeek.Sunday
            Return wednesday
        End If
    End Function
    Private Function cb4() As DaysOfTheWeek
        Dim thursday As DaysOfTheWeek
        If CheckBox4.Checked Then
            thursday = DaysOfTheWeek.Thursday
            Return thursday
        Else
            thursday = DaysOfTheWeek.Sunday
            Return thursday
        End If
    End Function
    Private Function cb5() As DaysOfTheWeek
        Dim friday As DaysOfTheWeek
        If CheckBox5.Checked Then
            friday = DaysOfTheWeek.Friday
            Return friday
        Else
            friday = DaysOfTheWeek.Sunday
            Return friday
        End If
    End Function
    Private Function cb6() As DaysOfTheWeek
        Dim saturday As DaysOfTheWeek
        If CheckBox6.Checked Then
            saturday = DaysOfTheWeek.Saturday
            Return saturday
        Else
            saturday = DaysOfTheWeek.Sunday
            Return saturday
        End If
    End Function
    Private Function cb7() As DaysOfTheWeek
        Dim sunday As DaysOfTheWeek
        If CheckBox7.Checked Then
            sunday = DaysOfTheWeek.Sunday
            Return sunday
        Else
            sunday = DaysOfTheWeek.Sunday
            Return sunday
        End If
    End Function
End Class