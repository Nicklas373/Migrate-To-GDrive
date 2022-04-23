Public Class Log
    Dim logPath As String = "log/log"
    Dim advLogPath As String = "log/advlog"
    Dim resLogPath As String = "log/reslog"
    Dim errPath As String = "log/err"
    Dim advErrPath As String = "log/adverr"
    Dim resErrPath As String = "log/reserr"
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        showLog("Backup history", logPath)
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        showLog("Backup error history", errPath)
    End Sub
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        showLog("Archive history", advLogPath)
    End Sub
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        showLog("Archive error history", advErrPath)
    End Sub
    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        ShowLog("Restore history", resLogPath)
    End Sub
    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        ShowLog("Restore error history", resErrPath)
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim validation As Integer
        validation = MsgBox("This will remove all history !", vbExclamation + vbYesNo + vbDefaultButton2, "MigrateToGDrive")
        If validation = vbYes Then
            clearLog(logPath, "Backup history")
            clearLog(advLogPath, "Archive history")
            clearLog(errPath, "Backup error history")
            ClearLog(advErrPath, "Archive error history")
            ClearLog(resLogPath, "Restore history")
            ClearLog(resErrPath, "Restore error history")
        Else
            MsgBox("Cancel remove history !", MsgBoxStyle.Information, "MigrateToGDrive")
        End If
    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        ExportLog(logPath, "log", "Backup history")
        ExportLog(advLogPath, "advLog", "Archive history")
        ExportLog(errPath, "err", "Backup error history")
        ExportLog(advErrPath, "advErr", "Archive error history")
        ExportLog(resLogPath, "resLog", "Restore history")
        ExportLog(resErrPath, "resErr", "Restore error history")
    End Sub
    Private Sub Log_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AllowTransparency = False
    End Sub
    Private Sub ShowLog(log As String, path As String)
        RichTextBox1.Text = ""
        If File.Exists(path) Then
            If New FileInfo(path).Length.Equals(0) Then
                MsgBox(log & " is empty !", MsgBoxStyle.Critical, "MigrateToGDrive")
            Else
                RichTextBox1.Text = File.ReadAllText(path)
            End If
        Else
            MsgBox(log & " does not exist !", MsgBoxStyle.Critical, "MigrateToGDrive")
        End If
    End Sub
    Private Sub ClearLog(log As String, log2 As String)
        If File.Exists(log) Then
            File.Delete(log)
            File.Create(log).Dispose()
            MsgBox(log2 & " cleared !", MsgBoxStyle.Information, "MigrateToGDrive")
            RichTextBox1.Text = ""
        Else
            MsgBox(log2 & " is not exist !", MsgBoxStyle.Critical, "MigrateToGDrive")
            RichTextBox1.Text = ""
        End If
    End Sub
    Private Sub ExportLog(logpath As String, filename As String, logname As String)
        If File.Exists(logpath) Then
            File.Copy(logpath, Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) & "/" & filename & ".txt")
            If File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) & "/" & filename & ".txt") Then
                MsgBox("Export " & logname & " success !", MsgBoxStyle.Information, "MigrateToGDrive")
            Else
                MsgBox("Export " & logname & " failed !", MsgBoxStyle.Critical, "MigrateToGDrive")
            End If
        End If
    End Sub
End Class