﻿Public Class normal_backup
    Dim filedialog As New FolderBrowserDialog
    Dim logPath As String = "log/log"
    Dim lastResult As String = "log/lastResult"
    Dim lastErr As String = "log/lastErr"
    Dim uiSrcPath As String = "conf/nrm_backup/nrmSrcPath"
    Dim uiDestPath As String = "conf/nrm_backup/nrmDestPath"
    Dim uiDatePath As String = "conf/nrm_backup/nrmDatePath"
    Private Sub normal_backup_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DateTimePicker1.Visible = False
        DateTimePicker1.Format = DateTimePickerFormat.Custom
        DateTimePicker1.CustomFormat = "MM-dd-yyyy"
        Label4.Visible = False
        Dim dt As Date = Today
        TextBox3.Text = dt.ToString("MM-dd-yyyy")
        TextBox3.Enabled = False
        TextBox3.Visible = False
        TextBox1.ReadOnly = True
        TextBox2.ReadOnly = True
        AllowTransparency = False
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
                        Else
                            CheckFileExist(lastResult, "err")
                            CheckFileExist(lastErr, "Destination drive not exist !")
                        End If
                    Else
                        CheckFileExist(lastResult, "err")
                        CheckFileExist(lastErr, "Source drive not exist !")
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
                            CheckFileExist(uiSrcPath, uiTrimSrc)
                            CheckFileExist(uiDestPath, uiTrimDest)
                            PrepareNotif(lastResult)
                            PrepareNotif(lastErr)
                            ManualBackup("bat/MigrateToGDrive_TD_MN.bat")
                        Else
                            CheckFileExist(lastResult, "err")
                            CheckFileExist(lastErr, "Destination drive not exist !")
                        End If
                    Else
                        CheckFileExist(lastResult, "err")
                        CheckFileExist(lastErr, "Source drive not exist !")
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
                            CheckFileExist(uiSrcPath, uiTrimSrc)
                            CheckFileExist(uiDestPath, uiTrimDest)
                            PrepareNotif(lastResult)
                            PrepareNotif(lastErr)
                            ManualBackup("bat/MigrateToGDrive_FD_MN.bat")
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
            Label4.Visible = True
            TextBox3.Visible = True
        Else
            DateTimePicker1.Visible = False
            Label4.Visible = False
            TextBox3.Visible = False
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
End Class