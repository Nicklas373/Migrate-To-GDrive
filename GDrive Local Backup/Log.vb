Public Class Log
    Dim logPath As String = "log/log"
    Dim errPath As String = "log/err"
    Dim confPath As String = "conf/config"
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        showLog("Backup Log", logPath)
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        showLog("Error Log", errPath)
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim validation As Integer
        validation = MsgBox("This Will Remove All Log File !, Remove Log ?", vbExclamation + vbYesNo + vbDefaultButton2, "Clear Log")
        If validation = vbYes Then
            clearLog(logPath, "Backup Log")
            clearLog(errPath, "Error Log")
        Else
            MsgBox("Cancel Remove Log !", MsgBoxStyle.Information)
        End If
    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If File.Exists(logPath) Then
            File.Copy(logPath, Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) & "/log.txt")
            If File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) & "/log.txt") Then
                MsgBox("Export Backup Log Success !", MsgBoxStyle.Information)
            Else
                MsgBox("Export Backup Log Failed !", MsgBoxStyle.Critical)
            End If
        End If
        If File.Exists(errPath) Then
            File.Copy(errPath, Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) & "/err.txt")
            If File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) & "/err.txt") Then
                MsgBox("Export Error Log Success !", MsgBoxStyle.Information)
            Else
                MsgBox("Export Error Log Failed !", MsgBoxStyle.Critical)
            End If
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
    Private Sub clearLog(log As String, log2 As String)
        If File.Exists(log) Then
            File.Delete(log)
            File.Create(log).Dispose()
            MsgBox(log2 & " File Cleared !", MsgBoxStyle.Information)
            RichTextBox1.Text = ""
        Else
            MsgBox(log2 & " File Is Not Exist !", MsgBoxStyle.Critical)
            RichTextBox1.Text = ""
        End If
    End Sub
End Class