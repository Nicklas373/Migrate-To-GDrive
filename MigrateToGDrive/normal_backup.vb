Public Class normal_backup
    Dim filedialog As New FolderBrowserDialog
    Dim logPath As String = "log/log"
    Dim roboPath As String = "log/robolog"
    Dim lastResult As String = "log/lastResult"
    Dim lastErr As String = "log/lastErr"
    Dim uiSrcPath As String = "conf/nrm_backup/nrmSrcPath"
    Dim uiDestPath As String = "conf/nrm_backup/nrmDestPath"
    Dim uiFrDatePath As String = "conf/nrm_backup/nrmFrDatePath"
    Dim uiReDatePath As String = "conf/nrm_backup/nrmReDatePath"
    Dim uiToDatePath As String = "conf/nrm_backup/nrmToDatePath"
    Dim uiProcessorCount As String = "conf/nrm_backup/nrmProcessor"
    Private Sub normal_backup_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DateTimePicker1.Visible = False
        DateTimePicker1.Format = DateTimePickerFormat.Custom
        DateTimePicker1.CustomFormat = "MM-dd-yyyy"
        DateTimePicker2.Visible = False
        DateTimePicker2.Format = DateTimePickerFormat.Custom
        DateTimePicker2.CustomFormat = "MM-dd-yyyy"
        Label4.Visible = False
        TextBox1.ReadOnly = True
        TextBox2.ReadOnly = True
        AllowTransparency = False
        WriteLogicalCount(uiProcessorCount)
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        filedialog.InitialDirectory = Environment.SpecialFolder.UserProfile
        filedialog.ShowDialog()
    End Sub
    Private Sub FolderBrowserDialog1_Disposed(sender As Object, e As EventArgs) Handles Button1.Click
        TextBox1.Text = FileDialog.SelectedPath.ToString
        Label3.Text = GetSrcDriveSize(TextBox1.Text)
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        filedialog.InitialDirectory = Environment.SpecialFolder.UserProfile
        filedialog.ShowDialog()
    End Sub
    Private Sub FolderBrowserDialog2_Disposed(sender As Object, e As EventArgs) Handles Button3.Click
        TextBox2.Text = FileDialog.SelectedPath.ToString
        Label7.Text = GetDestDriveSize(TextBox2.Text)
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
                If File.Exists(roboPath) Then
                    PrepareNotif(roboPath)
                End If
                If ComboBox1.Text = "Anytime" Then
                    uiTrimSrc = TextBox1.Text
                    uiTrimDest = TextBox2.Text
                    If Directory.Exists(uiTrimSrc) Then
                        If Directory.Exists(uiTrimDest) Then
                            CheckFileExist(uiSrcPath, uiTrimSrc)
                            CheckFileExist(uiDestPath, uiTrimDest)
                            PrepareNotif(lastResult)
                            PrepareNotif(lastErr)
                            ManualBackup("bat/MigrateToGDrive_AT_MN.bat")
                            WriteFrRobo()
                        Else
                            CheckFileExist(lastResult, "err")
                            CheckFileExist(lastErr, "Destination drive not exist !")
                        End If
                    Else
                        CheckFileExist(lastResult, "err")
                        CheckFileExist(lastErr, "Source drive not exist !")
                    End If
                ElseIf ComboBox1.Text = "Today" Then
                    If File.Exists(uiReDatePath) Then
                        GC.Collect()
                        GC.WaitForPendingFinalizers()
                        File.Delete(uiReDatePath)
                        File.Create(uiReDatePath).Dispose()
                        Dim destWriter As New StreamWriter(uiReDatePath, True)
                        Dim dt As Date = Today
                        destWriter.WriteLine(dt.ToString("yyyyMMdd"))
                        destWriter.Close()
                    Else
                        File.Create(uiReDatePath).Dispose()
                        Dim destWriter As New StreamWriter(uiReDatePath, True)
                        Dim dt As Date = Today
                        destWriter.WriteLine(dt.ToString("yyyyMMdd"))
                        destWriter.Close()
                    End If
                    uiTrimSrc = TextBox1.Text
                    uiTrimDest = TextBox2.Text
                    If Directory.Exists(uiTrimSrc) Then
                        If Directory.Exists(uiTrimDest) Then
                            CheckFileExist(uiSrcPath, uiTrimSrc)
                            CheckFileExist(uiDestPath, uiTrimDest)
                            PrepareNotif(lastResult)
                            PrepareNotif(lastErr)
                            ManualBackup("bat/MigrateToGDrive_TD_MN.bat")
                            WriteFrRobo()
                        Else
                            CheckFileExist(lastResult, "err")
                            CheckFileExist(lastErr, "Destination drive not exist !")
                        End If
                    Else
                        CheckFileExist(lastResult, "err")
                        CheckFileExist(lastErr, "Source drive not exist !")
                    End If
                ElseIf ComboBox1.Text = "From Date" Then
                    If File.Exists(uiFrDatePath) Then
                        GC.Collect()
                        GC.WaitForPendingFinalizers()
                        File.Delete(uiFrDatePath)
                        File.Create(uiFrDatePath).Dispose()
                        Dim destWriter As New StreamWriter(uiFrDatePath, True)
                        Dim dt As Date = DateTimePicker1.Value.ToShortDateString
                        destWriter.WriteLine(dt.ToString("yyyyMMdd"))
                        destWriter.Close()
                    Else
                        File.Create(uiFrDatePath).Dispose()
                        Dim destWriter As New StreamWriter(uiFrDatePath, True)
                        Dim dt As Date = DateTimePicker1.Value.ToShortDateString
                        destWriter.WriteLine(dt.ToString("yyyyMMdd"))
                        destWriter.Close()
                    End If
                    If File.Exists(uiToDatePath) Then
                        GC.Collect()
                        GC.WaitForPendingFinalizers()
                        File.Delete(uiToDatePath)
                        File.Create(uiToDatePath).Dispose()
                        Dim destWriter As New StreamWriter(uiToDatePath, True)
                        Dim dt As Date = DateTimePicker2.Value.ToShortDateString
                        Dim newDate As Integer = Integer.Parse(dt.ToString("dd") + 1)
                        If newDate < 10 Then
                            Dim newAffixDate = "0" + newDate.ToString
                            Dim newMonthYear As String = dt.ToString("yyyyMM")
                            destWriter.WriteLine(newMonthYear + newAffixDate.ToString)
                        Else
                            Dim newMonthYear As String = dt.ToString("yyyyMM")
                            destWriter.WriteLine(newMonthYear + newDate.ToString)
                        End If
                        destWriter.Close()
                    Else
                        File.Create(uiToDatePath).Dispose()
                        Dim destWriter As New StreamWriter(uiToDatePath, True)
                        Dim dt As Date = DateTimePicker2.Value.ToShortDateString
                        Dim newDate As Integer = Integer.Parse(dt.ToString("dd") + 1)
                        If newDate < 10 Then
                            Dim newAffixDate = "0" + newDate.ToString
                            Dim newMonthYear As String = dt.ToString("yyyyMM")
                            destWriter.WriteLine(newMonthYear + newAffixDate.ToString)
                        Else
                            Dim newMonthYear As String = dt.ToString("yyyyMM")
                            destWriter.WriteLine(newMonthYear + newDate.ToString)
                        End If
                        destWriter.Close()
                    End If
                    uiTrimSrc = TextBox1.Text
                    uiTrimDest = TextBox2.Text
                    If Directory.Exists(uiTrimSrc) Then
                        If Directory.Exists(uiTrimDest) Then
                            CheckFileExist(uiSrcPath, uiTrimSrc)
                            CheckFileExist(uiDestPath, uiTrimDest)
                            PrepareNotif(lastResult)
                            PrepareNotif(lastErr)
                            ManualBackup("bat/MigrateToGDrive_FD_MN.bat")
                            WriteFrRobo()
                        Else
                            CheckFileExist(lastResult, "err")
                            CheckFileExist(lastErr, "Destination drive not exist !")
                        End If
                    Else
                        CheckFileExist(lastResult, "err")
                        CheckFileExist(lastErr, "Source drive not exist !")
                    End If
                End If
                ShowNotif()
            End If
        End If
    End Sub
    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.Text = "From Date" Then
            DateTimePicker1.Visible = True
            DateTimePicker2.Visible = True
            Label4.Visible = True
        Else
            DateTimePicker1.Visible = False
            DateTimePicker2.Visible = False
            Label4.Visible = False
        End If
    End Sub
    Private Sub ShowNotif()
        If File.Exists(lastResult) Then
            Dim lastRest As String = PathVal(lastResult, 0)
            If PathVal(lastResult, 0).Equals("success") Then
                MsgBox("Backup success !", MsgBoxStyle.Information, "MigrateToGDrive")
                ShowLog("Backup", logPath)
            ElseIf PathVal(lastResult, 0).Equals("err") Then
                MsgBox("Backup error !", MsgBoxStyle.Critical, "MigrateToGDrive")
                RichTextBox1.Text = ""
                If File.Exists(lastErr) Then
                    If PathVal(lastErr, 0).Equals("") Then
                        MsgBox("Unknown error reason !", MsgBoxStyle.Critical, "MigrateToGDrive")
                        RichTextBox1.Text = ""
                    Else
                        MsgBox(PathVal(lastErr, 0), MsgBoxStyle.Critical, "MigrateToGDrive")
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
    Private Sub ShowLog(log As String, path As String)
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
    Private Sub WriteFrRobo()
        Dim destWriter As New StreamWriter(logPath, True)
        destWriter.WriteLine(PathVal(roboPath, 1))
        destWriter.WriteLine(PathVal(roboPath, 2))
        destWriter.WriteLine(PathVal(roboPath, 3))
        destWriter.WriteLine(PathVal(roboPath, CInt(File.ReadAllLines(roboPath).Length) - 11))
        destWriter.WriteLine(PathVal(roboPath, CInt(File.ReadAllLines(roboPath).Length) - 10))
        destWriter.WriteLine(PathVal(roboPath, CInt(File.ReadAllLines(roboPath).Length) - 9))
        destWriter.WriteLine(PathVal(roboPath, CInt(File.ReadAllLines(roboPath).Length) - 8))
        destWriter.WriteLine(PathVal(roboPath, CInt(File.ReadAllLines(roboPath).Length) - 7))
        destWriter.WriteLine(PathVal(roboPath, CInt(File.ReadAllLines(roboPath).Length) - 6))
        destWriter.WriteLine(PathVal(roboPath, CInt(File.ReadAllLines(roboPath).Length) - 5))
        destWriter.WriteLine(PathVal(roboPath, CInt(File.ReadAllLines(roboPath).Length) - 4))
        destWriter.WriteLine(PathVal(roboPath, CInt(File.ReadAllLines(roboPath).Length) - 3))
        destWriter.WriteLine(PathVal(roboPath, CInt(File.ReadAllLines(roboPath).Length) - 2))
        destWriter.Close()
    End Sub
End Class