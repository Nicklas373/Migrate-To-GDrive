Module GlobalModul
    Public Sub CheckFileExist(path As String, trim As String)
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
    Public Sub PrepareNotif(path As String)
        If File.Exists(path) Then
            GC.Collect()
            GC.WaitForPendingFinalizers()
            File.Delete(path)
            File.Create(path).Dispose()
        Else
            File.Create(path).Dispose()
        End If
    End Sub
    Public Sub ClearLog(log As String, log2 As String)
        If File.Exists(log) Then
            File.Delete(log)
            File.Create(log).Dispose()
            MsgBox(log2 & " file reset !", MsgBoxStyle.Information, "MigrateToGDrive")
        Else
            MsgBox(log2 & " file is not exist !", MsgBoxStyle.Critical, "MigrateToGDrive")
        End If
    End Sub
    Public Sub ManualBackup(bat As String)
        Dim psi As New ProcessStartInfo(bat)
        psi.RedirectStandardError = False
        psi.RedirectStandardOutput = False
        psi.CreateNoWindow = False
        psi.WindowStyle = ProcessWindowStyle.Hidden
        psi.UseShellExecute = False
        Dim process As Process = Process.Start(psi)
        process.WaitForExit()
    End Sub
    Public Function PathVal(path As String, line As Integer) As String
        Dim value As String
        If New FileInfo(path).Length = 0 Then
            value = "null"
            Return value
        Else
            value = File.ReadAllLines(path).ElementAt(line).ToString
            Return value
        End If
    End Function
    Public Function ConfVal(line As Integer, confPath As String) As String
        Dim value As String
        If New FileInfo(confPath).Length = 0 Then
            value = "null"
            Return value
        Else
            value = File.ReadAllLines(confPath).ElementAt(line).ToString
            Return value
        End If
    End Function
    Public Function GetSrcDriveSize(dir As String) As String
        Dim srcDir As String
        If dir = "" Then
            srcDir = ""
            Return srcDir
        Else
            Dim freeSpaceSrc As Double = (My.Computer.FileSystem.GetDriveInfo(dir.Remove(3)).TotalFreeSpace / 1024 / 1024 / 1024)
            Dim totalspaceSrc As Double = (My.Computer.FileSystem.GetDriveInfo(dir.Remove(3)).TotalSize / 1024 / 1024 / 1024)
            srcDir = "Source drive size : " & Format(freeSpaceSrc, "###.##").ToString & " GB" & " | " & Format(totalspaceSrc, "###.##").ToString & " GB"
            Return srcDir
        End If
    End Function
    Public Function GetDestDriveSize(dir As String) As String
        Dim destDir As String
        If dir = "" Then
            destDir = ""
            Return destDir
        Else
            Dim freeSpaceDest As Double = (My.Computer.FileSystem.GetDriveInfo(dir.Remove(3)).TotalFreeSpace / 1024 / 1024 / 1024)
            Dim totalspaceDest As Double = (My.Computer.FileSystem.GetDriveInfo(dir.Remove(3)).TotalSize / 1024 / 1024 / 1024)
            destDir = "Destination drive size : " & Format(freeSpaceDest, "###.##").ToString & " GB" & " | " & Format(totalspaceDest, "###.##").ToString & " GB"
            Return destDir
        End If
    End Function
End Module