Public Class Settings
    Dim filedialog As New FolderBrowserDialog
    Dim confPath As String = "conf/config"
    Dim logPath As String = "log/log"
    Dim errPath As String = "log/err"
    Dim timePath As String = "conf/time"
    Dim cliSrcPath As String = "conf/srcPath"
    Dim cliDestPath As String = "conf/destPath"
    Dim cliDatePath As String = "conf/datePath"
    Dim uiSrcPath As String = "conf/uiSrcPath"
    Dim uiDestPath As String = "conf/uiDestPath"
    Dim uiDatePath As String = "conf/uiDatePath"
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox6.Text = ""
        Label2.Text = "Source Drive Size: N/A | N/A"
        Label1.Text = "Destination Drive Size: N/A | N/A"
        TextBox1.ReadOnly = False
        TextBox2.ReadOnly = False
        Button3.Visible = True
        Button4.Visible = False
        Button5.Visible = True
        Button6.Visible = False
        Button7.Visible = False
        Label7.Visible = True
        ComboBox1.Visible = True
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.ReadOnly = True Then
            MsgBox("Configuration Menu Is Locked, Please Click Edit !", MsgBoxStyle.Information)
        Else
            FileDialog.InitialDirectory = Environment.SpecialFolder.UserProfile
            FileDialog.ShowDialog()
        End If
    End Sub
    Private Sub FolderBrowserDialog1_Disposed(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.ReadOnly = True Then
            TextBox1.Text = ""
            Label2.Text = "Source Drive Size: N/A | N/A"
        Else
            TextBox1.Text = filedialog.SelectedPath.ToString
        End If
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If TextBox2.ReadOnly = True Then
            MsgBox("Configuration Menu Is Locked, Please Click Edit !", MsgBoxStyle.Information)
        Else
            filedialog.InitialDirectory = Environment.SpecialFolder.UserProfile
            filedialog.ShowDialog()
        End If
    End Sub
    Private Sub FolderBrowserDialog2_Disposed(sender As Object, e As EventArgs) Handles Button2.Click
        If TextBox2.ReadOnly = True Then
            TextBox2.Text = ""
            Label1.Text = "Destination Drive Size: N/A | N/A"
        Else
            TextBox2.Text = filedialog.SelectedPath.ToString
        End If
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If TextBox1.Text = "" Then
            MsgBox("Please fill Source Folder !", MsgBoxStyle.Critical)
        ElseIf TextBox2.Text = "" Then
            MsgBox("Please fill Destination Folder !", MsgBoxStyle.Critical)
        Else
            If ComboBox1.Text = "" Then
                MsgBox("Please Select Backup Time !", MsgBoxStyle.Critical)
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
                    MsgBox("Config Created !", MsgBoxStyle.Information)
                Else
                    File.Create(confPath).Dispose()
                    Dim writer As New StreamWriter(confPath, True)
                    writer.WriteLine("Migrate To GDrive Config v1.0")
                    writer.WriteLine("Source Directory: " & TextBox1.Text)
                    writer.WriteLine("Destination Directory: " & TextBox2.Text)
                    writer.WriteLine("Backup Preferences: " & ComboBox1.Text)
                    writer.Close()
                    writeForAutoBackup()
                    MsgBox("Config Created !", MsgBoxStyle.Information)
                End If
                getDriveSize()
                TextBox1.ReadOnly = True
                TextBox2.ReadOnly = True
                Button3.Visible = False
                Button4.Visible = True
                Button5.Visible = False
                Button6.Visible = True
                Button7.Visible = True
                Label7.Visible = False
                ComboBox1.Visible = False
            End If
        End If
    End Sub
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox1.ReadOnly = True
        TextBox2.ReadOnly = True
        Button3.Visible = False
        Button4.Visible = True
        Button5.Visible = False
        Button6.Visible = True
        Button7.Visible = True
        Label7.Visible = False
        ComboBox1.Visible = False
    End Sub
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        If File.Exists(confPath) Then
            TextBox3.Text = ""
            TextBox4.Text = ""
            TextBox6.Text = ""
            If confVal(1).Replace("Source Directory: ", "").Equals("null") Or confVal(2).Replace("Destination Directory: ", "").Equals("null") Or confVal(3).Replace("Backup Preferences: ", "").Equals("null") Then
                TextBox3.Text = ""
                TextBox4.Text = ""
                TextBox6.Text = ""
                MsgBox("Config File Is Empty, Please Save Config First !", MsgBoxStyle.Exclamation)
            Else
                TextBox3.Text = confVal(1).Replace("Source Directory: ", "")
                TextBox4.Text = confVal(2).Replace("Destination Directory: ", "")
                TextBox6.Text = confVal(3).Replace("Backup Preferences: ", "")
            End If
        Else
            MsgBox("Config File Is Not Exist, Please Save Config First !", MsgBoxStyle.Critical)
        End If
    End Sub
    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        clearLog(confPath, "Config")
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox6.Text = ""
        TextBox1.ReadOnly = True
        TextBox2.ReadOnly = True
        Button3.Visible = False
        Button4.Visible = True
        Button5.Visible = False
        Button6.Visible = True
        Label7.Visible = False
        ComboBox1.Visible = False
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
    Private Sub getDriveSize()
        Dim trimSrc As String
        Dim trimDest As String
        If File.Exists(confPath) Then
            If confVal(1).Equals("null") Then
                Label2.Text = ""
                Label2.Text = "Source Drive Size : N/A GB | N/A GB"
                Label1.Text = ""
                Label1.Text = "Destination Drive Size : N/A GB | N/A GB"
            Else
                If confVal(2).Equals("null") Then
                    Label2.Text = ""
                    Label2.Text = "Source Drive Size : N/A GB | N/A GB"
                    Label1.Text = ""
                    Label1.Text = "Destination Drive Size : N/A GB | N/A GB"
                Else
                    trimSrc = confVal(1).Replace("Source Directory: ", "")
                    trimDest = confVal(2).Replace("Destination Directory: ", "")
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
                            Label2.Text = ""
                            Label2.Text = "Source Drive Size : " & freeSpaceSrc & " GB" & " | " & totalspaceSrc & " GB"
                        ElseIf totalspaceSrc.Length > 4 & totalspaceSrc.Length <= 7 Then
                            Dim fixedTotalSpaceSrc As String = totalspaceSrc.Remove(5)
                            Label2.Text = ""
                            Label2.Text = "Source Drive Size : " & freeSpaceSrc & " GB" & " | " & fixedTotalSpaceSrc & " GB"
                        End If
                    ElseIf freeSpaceSrc > 4 & freeSpaceSrc <= 7 Then
                        Dim fixedFreeSpaceSrc As String = freeSpaceSrc.Remove(5)
                        If totalspaceSrc.Length <= 4 Then
                            Label2.Text = ""
                            Label2.Text = "Source Drive Size : " & fixedFreeSpaceSrc & " GB" & " | " & totalspaceSrc & " GB"
                        ElseIf totalspaceSrc.Length > 4 & totalspaceSrc <= 7 Then
                            Dim fixedTotalSpaceSrc As String = totalspaceSrc.Remove(5)
                            Label2.Text = ""
                            Label2.Text = "Source Drive Size : " & fixedFreeSpaceSrc & " GB" & " | " & fixedTotalSpaceSrc & " GB"
                        End If
                    End If
                End If
            End If
        Else
            Dim driveSrcPath As String
            Dim driveDestPath As String
            If TextBox1.Text = "" Then
                Label2.Text = ""
                Label2.Text = "Source Drive Size : N/A GB | N/A GB"
                Label1.Text = ""
                Label1.Text = "Destination Drive Size : N/A GB | N/A GB"
            Else
                If TextBox2.Text = "" Then
                    Label2.Text = ""
                    Label2.Text = "Source Drive Size : N/A GB | N/A GB"
                    Label1.Text = ""
                    Label1.Text = "Destination Drive Size : N/A GB | N/A GB"
                Else
                    driveSrcPath = TextBox1.Text
                    driveDestPath = TextBox2.Text
                    Dim freeSpaceSrc As String = (My.Computer.FileSystem.GetDriveInfo(driveSrcPath.Remove(3)).TotalFreeSpace / 1024 / 1024 / 1024).ToString
                    Dim totalspaceSrc As String = (My.Computer.FileSystem.GetDriveInfo(driveSrcPath.Remove(3)).TotalSize / 1024 / 1024 / 1024).ToString
                    Dim freeSpaceDest As String = (My.Computer.FileSystem.GetDriveInfo(driveDestPath.Remove(3)).TotalFreeSpace / 1024 / 1024 / 1024).ToString
                    Dim totalspaceDest As String = (My.Computer.FileSystem.GetDriveInfo(driveDestPath.Remove(3)).TotalSize / 1024 / 1024 / 1024).ToString
                    If freeSpaceSrc.Length <= 4 Then
                        If totalspaceSrc.Length <= 4 Then
                            Label2.Text = ""
                            Label2.Text = "Source Drive Size : " & freeSpaceSrc & " GB" & " | " & totalspaceSrc & " GB"
                        ElseIf totalspaceSrc.Length > 4 & totalspaceSrc.Length <= 7 Then
                            Dim fixedTotalSpaceSrc As String = totalspaceSrc.Remove(5)
                            Label2.Text = ""
                            Label2.Text = "Source Drive Size : " & freeSpaceSrc & " GB" & " | " & fixedTotalSpaceSrc & " GB"
                        End If
                    ElseIf freeSpaceSrc > 4 & freeSpaceSrc <= 7 Then
                        Dim fixedFreeSpaceSrc As String = freeSpaceSrc.Remove(5)
                        If totalspaceSrc.Length <= 4 Then
                            Label2.Text = ""
                            Label2.Text = "Source Drive Size : " & fixedFreeSpaceSrc & " GB" & " | " & totalspaceSrc & " GB"
                        ElseIf totalspaceSrc.Length > 4 & totalspaceSrc <= 7 Then
                            Dim fixedTotalSpaceSrc As String = totalspaceSrc.Remove(5)
                            Label2.Text = ""
                            Label2.Text = "Source Drive Size : " & fixedFreeSpaceSrc & " GB" & " | " & fixedTotalSpaceSrc & " GB"
                        End If
                    End If
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
                End If
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
    Private Sub clearLog(log As String, log2 As String)
        If File.Exists(log) Then
            File.Delete(log)
            File.Create(log).Dispose()
            MsgBox(log2 & " File Reset !", MsgBoxStyle.Information)
        Else
            MsgBox(log2 & " File Is Not Exist !", MsgBoxStyle.Critical)
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
            MsgBox("Please Select Backup Preferences First !", MsgBoxStyle.Critical)
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
End Class