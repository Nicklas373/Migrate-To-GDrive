Public Class Form1
    Dim filedialog As New FolderBrowserDialog
    Dim confPath As String = "conf/config"
    Dim logPath As String = "log/log"
    Dim errPath As String = "log/err"
    Dim lastResult As String = "log/lastResult"
    Dim lastErr As String = "log/lastErr"
    Dim timePath As String = "conf/time"
    Dim cliSrcPath As String = "conf/srcPath"
    Dim cliDestPath As String = "conf/destPath"
    Dim cliDatePath As String = "conf/datePath"
    Dim uiSrcPath As String = "conf/uiSrcPath"
    Dim uiDestPath As String = "conf/uiDestPath"
    Dim uiDatePath As String = "conf/uiDatePath"
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        filedialog.InitialDirectory = Environment.SpecialFolder.UserProfile
        filedialog.ShowDialog()
    End Sub
    Private Sub FolderBrowserDialog1_Disposed(sender As Object, e As EventArgs) Handles Button1.Click
        TextBox1.Text = filedialog.SelectedPath.ToString
        getSrcDriveSize()
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        filedialog.InitialDirectory = Environment.SpecialFolder.UserProfile
        filedialog.ShowDialog()
    End Sub
    Private Sub FolderBrowserDialog2_Disposed(sender As Object, e As EventArgs) Handles Button3.Click
        TextBox2.Text = filedialog.SelectedPath.ToString
        getDestDriveSize()
    End Sub
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim uiTrimSrc As String
        Dim uiTrimDest As String
        RichTextBox1.Text = "Processing ..."
        If TextBox1.Text = "" Then
            MsgBox("Source folder is empty !, please fill source folder location", MsgBoxStyle.Critical, "MigrateToGDrive")
            RichTextBox1.Text = ""
        Else
            If TextBox2.Text = "" Then
                MsgBox("Destination folder is empty !, Please fill source folder location", MsgBoxStyle.Critical, "MigrateToGDrive")
                RichTextBox1.Text = ""
            ElseIf ComboBox1.Text = "" Then
                MsgBox("Backup preference is empty !, Please select backup preference first !", MsgBoxStyle.Critical, "MigrateToGDrive")
                RichTextBox1.Text = ""
            Else
                If ComboBox1.Text = "Anytime" Then
                    uiTrimSrc = TextBox1.Text
                    uiTrimDest = TextBox2.Text
                    If Directory.Exists(uiTrimSrc) Then
                        If Directory.Exists(uiTrimDest) Then
                            checkFileExist(uiSrcPath, uiTrimSrc)
                            checkFileExist(uiDestPath, uiTrimDest)
                            prepareNotif(lastResult)
                            prepareNotif(lastErr)
                            manualBackup("bat/MigrateToGDrive_AT_MN.bat")
                        Else
                            checkFileExist(lastResult, "err")
                            checkFileExist(lastErr, "Destination drive not exist !")
                        End If
                    Else
                        checkFileExist(lastResult, "err")
                        checkFileExist(lastErr, "Source drive not exist !")
                    End If
                ElseIf ComboBox1.Text = "Today" Then
                    If File.Exists(uiDatePath) Then
                        GC.Collect()
                        GC.WaitForPendingFinalizers()
                        File.Delete(uiDatePath)
                        File.Create(uiDatePath).Dispose()
                        Dim destWriter As New StreamWriter(uiDatePath, True)
                        Dim dt As Date = Today
                        destWriter.WriteLine(dt.ToString("MM-dd-yyyy"))
                        destWriter.Close()
                    Else
                        File.Create(uiDatePath).Dispose()
                        Dim destWriter As New StreamWriter(uiDatePath, True)
                        Dim dt As Date = Today
                        destWriter.WriteLine(dt.ToString("MM-dd-yyyy"))
                        destWriter.Close()
                    End If
                    uiTrimSrc = TextBox1.Text
                    uiTrimDest = TextBox2.Text
                    If Directory.Exists(uiTrimSrc) Then
                        If Directory.Exists(uiTrimDest) Then
                            checkFileExist(uiSrcPath, uiTrimSrc)
                            checkFileExist(uiDestPath, uiTrimDest)
                            prepareNotif(lastResult)
                            prepareNotif(lastErr)
                            manualBackup("bat/MigrateToGDrive_TD_MN.bat")
                        Else
                            checkFileExist(lastResult, "err")
                            checkFileExist(lastErr, "Destination drive not exist !")
                        End If
                    Else
                        checkFileExist(lastResult, "err")
                        checkFileExist(lastErr, "Source drive not exist !")
                    End If
                ElseIf ComboBox1.Text = "From Date" Then
                    If File.Exists(uiDatePath) Then
                        GC.Collect()
                        GC.WaitForPendingFinalizers()
                        File.Delete(uiDatePath)
                        File.Create(uiDatePath).Dispose()
                        Dim destWriter As New StreamWriter(uiDatePath, True)
                        Dim dt As Date = DateTimePicker1.Value.ToShortDateString
                        destWriter.WriteLine(dt.ToString("MM/dd/yyyy"))
                        destWriter.Close()
                    Else
                        File.Create(uiDatePath).Dispose()
                        Dim destWriter As New StreamWriter(uiDatePath, True)
                        Dim dt As Date = DateTimePicker1.Value.ToShortDateString
                        destWriter.WriteLine(dt.ToString("MM/dd/yyyy"))
                        destWriter.Close()
                    End If
                    uiTrimSrc = TextBox1.Text
                    uiTrimDest = TextBox2.Text
                    If Directory.Exists(uiTrimSrc) Then
                        If Directory.Exists(uiTrimDest) Then
                            checkFileExist(uiSrcPath, uiTrimSrc)
                            checkFileExist(uiDestPath, uiTrimDest)
                            prepareNotif(lastResult)
                            prepareNotif(lastErr)
                            manualBackup("bat/MigrateToGDrive_TD_MN.bat")
                        Else
                            checkFileExist(lastResult, "err")
                            checkFileExist(lastErr, "Destination drive not exist !")
                        End If
                    Else
                        checkFileExist(lastResult, "err")
                        checkFileExist(lastErr, "Source drive not exist !")
                    End If
                End If
                showNotif()
            End If
        End If
    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DateTimePicker1.Visible = False
        DateTimePicker1.Format = DateTimePickerFormat.Custom
        DateTimePicker1.CustomFormat = "MM-dd-yyyy"
        Label4.Visible = False
        Dim dt As Date = Today
        TextBox3.Text = dt.ToString("MM-dd-yyyy")
        TextBox3.Enabled = False
        TextBox3.Visible = False
    End Sub
    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.Text = "From Date" Then
            DateTimePicker1.Visible = True
            Label4.Visible = True
            TextBox3.Visible = True
        Else
            DateTimePicker1.Visible = False
            Label4.Visible = False
            TextBox3.Visible = False
        End If
    End Sub
    Private Sub SettingsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SettingsToolStripMenuItem.Click
        Dim menu_settings = New Settings()
        menu_settings.Show()
    End Sub
    Private Sub LogToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LogToolStripMenuItem.Click
        Dim menu_log = New Log()
        menu_log.Show()
    End Sub
    Private Sub getSrcDriveSize()
        Dim trimSrc As String
        trimSrc = TextBox1.Text
        Dim freeSpaceSrc As Double = (My.Computer.FileSystem.GetDriveInfo(trimSrc.Remove(3)).TotalFreeSpace / 1024 / 1024 / 1024)
        Dim totalspaceSrc As Double = (My.Computer.FileSystem.GetDriveInfo(trimSrc.Remove(3)).TotalSize / 1024 / 1024 / 1024)
        Label3.Text = ""
        Label3.Text = "Source drive size : " & Format(freeSpaceSrc, "###.##").ToString & " GB" & " | " & Format(totalspaceSrc, "###.##").ToString & " GB"
    End Sub
    Private Sub getDestDriveSize()
        Dim trimDest As String
        trimDest = TextBox2.Text
        Dim freeSpaceDest As Double = (My.Computer.FileSystem.GetDriveInfo(trimDest.Remove(3)).TotalFreeSpace / 1024 / 1024 / 1024)
        Dim totalspaceDest As Double = (My.Computer.FileSystem.GetDriveInfo(trimDest.Remove(3)).TotalSize / 1024 / 1024 / 1024)
        Label1.Text = ""
        Label1.Text = "Destination drive size : " & Format(freeSpaceDest, "###.##").ToString & " GB" & " | " & Format(totalspaceDest, "###.##").ToString & " GB"
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
    Private Sub prepareNotif(path As String)
        If File.Exists(path) Then
            GC.Collect()
            GC.WaitForPendingFinalizers()
            File.Delete(path)
            File.Create(path).Dispose()
        Else
            File.Create(path).Dispose()
        End If
    End Sub
    Private Sub showNotif()
        If File.Exists(lastResult) Then
            Dim lastRest As String = pathVal(lastResult, 0)
            If pathVal(lastResult, 0).Equals("success") Then
                MsgBox("Backup success !", MsgBoxStyle.Information, "MigrateToGDrive")
                showLog("Backup", logPath)
            ElseIf pathVal(lastResult, 0).Equals("err") Then
                MsgBox("Backup error !", MsgBoxStyle.Critical, "MigrateToGDrive")
                RichTextBox1.Text = ""
                If File.Exists(lastErr) Then
                    If pathVal(lastErr, 0).Equals("") Then
                        MsgBox("Unknown error reason !", MsgBoxStyle.Critical, "MigrateToGDrive")
                        RichTextBox1.Text = ""
                    Else
                        MsgBox(pathVal(lastErr, 0), MsgBoxStyle.Critical, "MigrateToGDrive")
                    End If
                Else
                    MsgBox("Error file not found !", MsgBoxStyle.Critical, "MigrateToGDrive")
                    RichTextBox1.Text = ""
                End If
            Else
                MsgBox("Unknown result status !", MsgBoxStyle.Critical, "MigrateToGDrive")
                RichTextBox1.Text = ""
            End If
        Else
            MsgBox("Result file not found !", MsgBoxStyle.Critical, "MigrateToGDrive")
            RichTextBox1.Text = ""
        End If
    End Sub
    Private Sub showLog(log As String, path As String)
        RichTextBox1.Text = ""
        If File.Exists(path) Then
            If New FileInfo(path).Length.Equals(0) Then
                MsgBox(log & " File is empty !", MsgBoxStyle.Critical, "MigrateToGDrive")
            Else
                RichTextBox1.Text = File.ReadAllText(path)
            End If
        Else
            MsgBox(log & " File does not exist !", MsgBoxStyle.Critical, "MigrateToGDrive")
        End If
    End Sub
    Private Sub manualBackup(bat As String)
        Dim psi As New ProcessStartInfo(bat)
        psi.RedirectStandardError = True
        psi.RedirectStandardOutput = True
        psi.CreateNoWindow = True
        psi.WindowStyle = ProcessWindowStyle.Hidden
        psi.UseShellExecute = False
        Dim process As Process = Process.Start(psi)
        process.WaitForExit()
    End Sub
    Private Function pathVal(path As String, line As Integer) As String
        Dim value As String
        If New FileInfo(path).Length = 0 Then
            value = "null"
            Return value
        Else
            value = File.ReadAllLines(path).ElementAt(line).ToString
            Return value
        End If
    End Function
End Class