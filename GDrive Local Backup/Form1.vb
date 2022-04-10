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
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        filedialog.InitialDirectory = Environment.SpecialFolder.UserProfile
        filedialog.ShowDialog()
    End Sub
    Private Sub FolderBrowserDialog2_Disposed(sender As Object, e As EventArgs) Handles Button3.Click
        TextBox2.Text = filedialog.SelectedPath.ToString
    End Sub
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim uiTrimSrc As String
        Dim uiTrimDest As String
        RichTextBox1.Text = "Processing ..."
        If TextBox1.Text = "" Then
            MsgBox("Source Folder Is Empty !, Please Fill Source Folder Location", MsgBoxStyle.Critical)
            RichTextBox1.Text = ""
        Else
            If TextBox2.Text = "" Then
                MsgBox("Destination Folder Is Empty !, Please Fill Source Folder Location", MsgBoxStyle.Critical)
                RichTextBox1.Text = ""
            Else

                If ComboBox1.Text = "" Then
                    MsgBox("Backup Time Is Empty !, Please Select Backup Time First !", MsgBoxStyle.Critical)
                    RichTextBox1.Text = ""
                ElseIf ComboBox1.Text = "Anytime" Then
                    uiTrimSrc = TextBox1.Text
                    uiTrimDest = TextBox2.Text
                    checkFileExist(uiSrcPath, uiTrimSrc)
                    checkFileExist(uiDestPath, uiTrimDest)
                    prepareNotif(lastResult)
                    prepareNotif(lastErr)
                    manualBackup("bat/MigrateToGDrive_AT_MN.bat")
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
                    checkFileExist(uiSrcPath, uiTrimSrc)
                    checkFileExist(uiDestPath, uiTrimDest)
                    prepareNotif(lastResult)
                    prepareNotif(lastErr)
                    manualBackup("bat/MigrateToGDrive_TD_MN.bat")
                End If
                Threading.Thread.Sleep(2000)
                getDriveSize()
                showNotif()
            End If
        End If
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        MsgBox("To Configure Auto Backup, Please Go To Menu -> Settings Then Configure Specific Directory !", MsgBoxStyle.Information)
        MsgBox("Contact Your Administrator After Configure Auto Backup Directory !", MsgBoxStyle.Information)
    End Sub
    Private Sub SettingsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SettingsToolStripMenuItem.Click
        Dim menu_settings = New Settings()
        menu_settings.Show()
    End Sub
    Private Sub LogToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LogToolStripMenuItem.Click
        Dim menu_log = New Log()
        menu_log.Show()
    End Sub
    Private Sub getDriveSize()
        Dim trimSrc As String
        Dim trimDest As String
        trimSrc = TextBox1.Text
        trimDest = TextBox2.Text
        Dim freeSpaceSrc As String = (My.Computer.FileSystem.GetDriveInfo(trimSrc.Remove(3)).TotalFreeSpace / 1024 / 1024 / 1024).ToString
        Dim totalspaceSrc As String = (My.Computer.FileSystem.GetDriveInfo(trimSrc.Remove(3)).TotalSize / 1024 / 1024 / 1024).ToString
        Dim freeSpaceDest As String = (My.Computer.FileSystem.GetDriveInfo(trimDest.Remove(3)).TotalFreeSpace / 1024 / 1024 / 1024).ToString
        Dim totalspaceDest As String = (My.Computer.FileSystem.GetDriveInfo(trimDest.Remove(3)).TotalSize / 1024 / 1024 / 1024).ToString
        If freeSpaceDest.Length <= 4 Then
            If totalspaceDest.Length <= 4 Then
                Label1.Text = ""
                Label1.Text = "Destination Drive Size : " & freeSpaceDest & " GB" & " | " & totalspaceDest & " GB"
            ElseIf totalspaceDest.Length > 4 & totalspaceDest.Length <= 7 Then
                Dim fixedTotalSpaceDest As String = totalspaceDest.Remove(5)
                Label1.Text = ""
                Label1.Text = "Destination Drive Size : " & freeSpaceDest & " GB" & " | " & fixedTotalSpaceDest & " GB"
            End If
        ElseIf freeSpaceDest > 4 & freeSpaceDest <= 7 Then
            Dim fixedFreeSpaceDest As String = freeSpaceDest.Remove(5)
            If totalspaceDest.Length <= 4 Then
                Label1.Text = ""
                Label1.Text = "Destination Drive Size : " & fixedFreeSpaceDest & " GB" & " | " & totalspaceDest & " GB"
            ElseIf totalspaceDest.Length > 4 & totalspaceDest <= 7 Then
                Dim fixedTotalSpaceDest As String = totalspaceDest.Remove(5)
                Label1.Text = ""
                Label1.Text = "Destination Drive Size : " & fixedFreeSpaceDest & " GB" & " | " & fixedTotalSpaceDest & " GB"
            End If
        End If
        If freeSpaceSrc.Length <= 4 Then
            If totalspaceSrc.Length <= 4 Then
                Label3.Text = ""
                Label3.Text = "Source Drive Size : " & freeSpaceSrc & " GB" & " | " & totalspaceSrc & " GB"
            ElseIf totalspaceSrc.Length > 4 & totalspaceSrc.Length <= 7 Then
                Dim fixedTotalSpaceSrc As String = totalspaceSrc.Remove(5)
                Label3.Text = ""
                Label3.Text = "Source Drive Size : " & freeSpaceSrc & " GB" & " | " & fixedTotalSpaceSrc & " GB"
            End If
        ElseIf freeSpaceSrc > 4 & freeSpaceSrc <= 7 Then
            Dim fixedFreeSpaceSrc As String = freeSpaceSrc.Remove(5)
            If totalspaceSrc.Length <= 4 Then
                Label3.Text = ""
                Label3.Text = "Source Drive Size : " & fixedFreeSpaceSrc & " GB" & " | " & totalspaceSrc & " GB"
            ElseIf totalspaceSrc.Length > 4 & totalspaceSrc <= 7 Then
                Dim fixedTotalSpaceSrc As String = totalspaceSrc.Remove(5)
                Label3.Text = ""
                Label3.Text = "Source Drive Size : " & fixedFreeSpaceSrc & " GB" & " | " & fixedTotalSpaceSrc & " GB"
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
                MsgBox("Backup Success !", MsgBoxStyle.Information)
                showLog("Backup", logPath)
            ElseIf pathVal(lastResult, 0).Equals("err") Then
                MsgBox("Backup Error !", MsgBoxStyle.Critical)
                RichTextBox1.Text = ""
                If File.Exists(lastErr) Then
                    If pathVal(lastErr, 0).Equals("") Then
                        MsgBox("Unknown Error Reason !", MsgBoxStyle.Critical)
                        MsgBox("Please Contact Your Administrator !", MsgBoxStyle.Critical)
                        RichTextBox1.Text = ""
                    Else
                        MsgBox(pathVal(lastErr, 0), MsgBoxStyle.Critical)
                    End If
                Else
                    MsgBox("Error File Not Found !", MsgBoxStyle.Critical)
                    MsgBox("Please Contact Your Administrator !", MsgBoxStyle.Critical)
                    RichTextBox1.Text = ""
                End If
            Else
                MsgBox("Unknown Result Status !", MsgBoxStyle.Critical)
                MsgBox("Please Contact Your Administrator !", MsgBoxStyle.Critical)
                RichTextBox1.Text = ""
            End If
        Else
            MsgBox("Result File Not Found !", MsgBoxStyle.Critical)
            MsgBox("Please Contact Your Administrator !", MsgBoxStyle.Critical)
            RichTextBox1.Text = ""
        End If
    End Sub
    Private Sub showLog(log As String, path As String)
        RichTextBox1.Text = ""
        If File.Exists(path) Then
            If New FileInfo(path).Length.Equals(0) Then
                MsgBox(log & " File Is Empty !", MsgBoxStyle.Critical)
            Else
                RichTextBox1.Text = File.ReadAllText(path)
            End If
        Else
            MsgBox(log & " File Does Not Exist !", MsgBoxStyle.Critical)
        End If
    End Sub
    Private Sub manualBackup(bat As String)
        Dim psi As New ProcessStartInfo(bat)
        psi.RedirectStandardError = True
        psi.RedirectStandardOutput = True
        psi.CreateNoWindow = False
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