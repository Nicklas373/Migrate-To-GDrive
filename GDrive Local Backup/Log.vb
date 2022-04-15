Public Class Log
    Dim logPath As String = "log/log"
    Dim errPath As String = "log/err"
    Dim confPath As String = "conf/config"
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        showLog("Backup history", logPath)
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        showLog("Error history", errPath)
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim validation As Integer
        validation = MsgBox("This will remove all history !", vbExclamation + vbYesNo + vbDefaultButton2, "MigrateToGDrive")
        If validation = vbYes Then
            clearLog(logPath, "Backup history")
            clearLog(errPath, "Error history")
        Else
            MsgBox("Cancel remove history !", MsgBoxStyle.Information, "MigrateToGDrive")
        End If
    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If File.Exists(logPath) Then
            File.Copy(logPath, Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) & "/log.txt")
            If File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) & "/log.txt") Then
                MsgBox("Export backup history success !", MsgBoxStyle.Information, "MigrateToGDrive")
            Else
                MsgBox("Export backup history failed !", MsgBoxStyle.Critical, "MigrateToGDrive")
            End If
        End If
        If File.Exists(errPath) Then
            File.Copy(errPath, Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) & "/err.txt")
            If File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) & "/err.txt") Then
                MsgBox("Export error history success !", MsgBoxStyle.Information, "MigrateToGDrive")
            Else
                MsgBox("Export error history failed !", MsgBoxStyle.Critical, "MigrateToGDrive")
            End If
        End If
    End Sub
    Private Sub showLog(log As String, path As String)
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
    Private Sub clearLog(log As String, log2 As String)
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
End Class