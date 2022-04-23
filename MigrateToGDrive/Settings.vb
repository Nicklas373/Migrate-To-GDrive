Imports Microsoft.Win32.TaskScheduler
Public Class Settings
    Dim filedialog As New FolderBrowserDialog
    Dim confPath As String = "conf/config"
    Dim logPath As String = "log/log"
    Dim errPath As String = "log/err"
    Dim timePath As String = "conf/cli_backup/cliTimeInit"
    Dim cliSrcPath As String = "conf/cli_backup/cliSrcPath"
    Dim cliDestPath As String = "conf/cli_backup/cliDestPath"
    Dim cliDatePath As String = "conf/cli_backup/cliDatePath"
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
                    WriteForAutoBackup(confPath, cliSrcPath, cliDestPath, cliDatePath, timePath)
                    MsgBox("Config created !", MsgBoxStyle.Information, "MigrateToGDrive")
                Else
                    File.Create(confPath).Dispose()
                    Dim writer As New StreamWriter(confPath, True)
                    writer.WriteLine("Migrate To GDrive Config v1.0")
                    writer.WriteLine("Source Directory: " & TextBox1.Text)
                    writer.WriteLine("Destination Directory: " & TextBox2.Text)
                    writer.WriteLine("Backup Preferences: " & ComboBox1.Text)
                    writer.Close()
                    WriteForAutoBackup(confPath, cliSrcPath, cliDestPath, cliDatePath, timePath)
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
                    DailyTrigger(custdate, 1, CInt(TextBox3.Text), CustRepDurValDaily(ComboBox5.Text), CustRepDurIntDaily(ComboBox4.Text), ComboBox5.Text, ComboBox4.Text)
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
                        WeeklyTrigger(custdate, 1, CInt(TextBox4.Text), CustRepDurValWeek(ComboBox6.Text), CustRepDurIntWeek(ComboBox7.Text), Cb1, Cb2, Cb3, Cb4, Cb5, Cb6, Cb7, ComboBox6.Text, ComboBox7.Text)
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
        AllowTransparency = False
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
    Private Sub GetSrcDriveSize(conf As Integer)
        Dim trimSrc As String
        If conf = 1 Then
            If File.Exists(confPath) Then
                If ConfVal(1, confPath).Equals("null") Then
                    Label2.Text = ""
                Else
                    trimSrc = ConfVal(1, confPath).Replace("Source Directory: ", "")
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
    Private Sub GetDestDriveSize(conf As Integer)
        Dim trimDest As String
        If conf = 1 Then
            If File.Exists(confPath) Then
                If ConfVal(2, confPath).Equals("null") Then
                    Label1.Text = ""
                Else
                    trimDest = ConfVal(2, confPath).Replace("Destination Directory: ", "")
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
    Private Sub GetBackPref()
        If File.Exists(confPath) Then
            If ConfVal(3, confPath).Replace("Backup Preferences: ", "").Equals("Anytime") Then
                ComboBox1.SelectedIndex = 0
            ElseIf ConfVal(3, confPath).Replace("Backup Preferences: ", "").Equals("Today") Then
                ComboBox1.SelectedIndex = 1
            End If
        End If
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
    Private Sub ShowLog(log As String, path As String)
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
    Private Function Cb1() As DaysOfTheWeek
        Dim monday As DaysOfTheWeek
        If CheckBox1.Checked Then
            monday = DaysOfTheWeek.Monday
            Return monday
        Else
            monday = DaysOfTheWeek.Sunday
            Return monday
        End If
    End Function
    Private Function Cb2() As DaysOfTheWeek
        Dim tuesday As DaysOfTheWeek
        If CheckBox2.Checked Then
            tuesday = DaysOfTheWeek.Tuesday
            Return tuesday
        Else
            tuesday = DaysOfTheWeek.Sunday
            Return tuesday
        End If
    End Function
    Private Function Cb3() As DaysOfTheWeek
        Dim wednesday As DaysOfTheWeek
        If CheckBox3.Checked Then
            wednesday = DaysOfTheWeek.Wednesday
            Return wednesday
        Else
            wednesday = DaysOfTheWeek.Sunday
            Return wednesday
        End If
    End Function
    Private Function Cb4() As DaysOfTheWeek
        Dim thursday As DaysOfTheWeek
        If CheckBox4.Checked Then
            thursday = DaysOfTheWeek.Thursday
            Return thursday
        Else
            thursday = DaysOfTheWeek.Sunday
            Return thursday
        End If
    End Function
    Private Function Cb5() As DaysOfTheWeek
        Dim friday As DaysOfTheWeek
        If CheckBox5.Checked Then
            friday = DaysOfTheWeek.Friday
            Return friday
        Else
            friday = DaysOfTheWeek.Sunday
            Return friday
        End If
    End Function
    Private Function Cb6() As DaysOfTheWeek
        Dim saturday As DaysOfTheWeek
        If CheckBox6.Checked Then
            saturday = DaysOfTheWeek.Saturday
            Return saturday
        Else
            saturday = DaysOfTheWeek.Sunday
            Return saturday
        End If
    End Function
    Private Function Cb7() As DaysOfTheWeek
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