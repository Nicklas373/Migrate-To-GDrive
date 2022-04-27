Public Class Log
    ReadOnly logPath As String = "log/log"
    ReadOnly advLogPath As String = "log/advlog"
    ReadOnly resLogPath As String = "log/reslog"
    ReadOnly roboLogPath As String = "log/robolog"
    ReadOnly errPath As String = "log/err"
    ReadOnly advErrPath As String = "log/adverr"
    ReadOnly resErrPath As String = "log/reserr"
    Dim logCountPath As String = "log/expLog"
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
            ClearLog(roboLogPath, "Robocopy hustory")
            If New FileInfo(logPath).Length.Equals(0) Or New FileInfo(advLogPath).Length.Equals(0) Or New FileInfo(resLogPath).Length.Equals(0) Or New FileInfo(errPath).Length.Equals(0) Or New FileInfo(advErrPath).Length.Equals(0) Or New FileInfo(resErrPath).Length.Equals(0) Then
                MsgBox("All history cleared !", MsgBoxStyle.Information, "MigrateToGDrive")
                RichTextBox1.Text = ""
            End If
        Else
                MsgBox("Cancel remove history !", MsgBoxStyle.Information, "MigrateToGDrive")
        End If
    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        ExportLog(logPath, "log", "Backup history", logCountPath)
        ExportLog(advLogPath, "advLog", "Archive history", logCountPath)
        ExportLog(errPath, "err", "Backup error history", logCountPath)
        ExportLog(advErrPath, "advErr", "Archive error history", logCountPath)
        ExportLog(resLogPath, "resLog", "Restore history", logCountPath)
        ExportLog(resErrPath, "resErr", "Restore error history", logCountPath)
        ExportLog(roboLogPath, "robolog", "Robolog history", logCountPath)
        If CInt(PathVal(logCountPath, 0)) = 7 Then
            MsgBox("All log exported !", MsgBoxStyle.Information, "MigrateToGDrive")
        Else
            MsgBox("Export log error !", MsgBoxStyle.Critical, "MigrateToGDrive")
        End If
        PrepareNotif(logCountPath)
        RichTextBox1.Text = ""
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
    Private Shared Sub ClearLog(log As String, log2 As String)
        If File.Exists(log) Then
            File.Delete(log)
            File.Create(log).Dispose()
        End If
    End Sub
    Private Shared Sub ExportLog(logpath As String, filename As String, logname As String, logCountPath As String)
        Dim logCount As Integer
        Dim curCount As Integer
        Dim totalCount As Integer
        If File.Exists(logpath) Then
            If File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) & "/" & filename & ".txt") Then
                File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) & "/" & filename & ".txt")
                File.Copy(logpath, Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) & "/" & filename & ".txt")
            Else
                File.Copy(logpath, Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) & "/" & filename & ".txt")
            End If
            logCount += 1
        End If
        curCount = CheckNull(logCountPath)
        totalCount = curCount + logCount
        PrepareNotif(logCountPath)
        Dim destwriter As New StreamWriter(logCountPath, True)
        destwriter.WriteLine(totalCount)
        destwriter.Close()
    End Sub

    Private Shared Function CheckNull(curCount As String) As Integer
        Dim result As Integer
        If PathVal(curCount, 0).Equals("null") Then
            result = 0
            Return result
        Else
            result = CInt(PathVal(curCount, 0))
            Return result
        End If
    End Function
End Class