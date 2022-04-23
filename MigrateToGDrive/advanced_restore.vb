Public Class advanced_restore
    Dim openfiledialog As New OpenFileDialog
    Dim openfolderdialog As New FolderBrowserDialog
    Dim savefiledialog As New SaveFileDialog
    Dim logPath As String = "log/reslog"
    Dim errPath As String = "log/reserr"
    Dim lastResult As String = "log/lastResult"
    Dim lastErr As String = "log/lastErr"
    Dim resSrcPath As String = "conf/res_backup/resSrcPath"
    Dim resDestPath As String = "conf/res_backup/resDestPath"
    Dim resInstPath As String = "conf/res_backup/resInstPath"
    Dim resTempPass As String = "conf/res_backup/resTempKey"
    Dim resDecResult As String = "conf/res_backup/resDecResult"
    Dim resZipLog As String = "conf/res_backup/resZipLog"
    Dim resDecMtd As String = "conf/res_backup/resDecType"
    Private Sub Advanced_restore_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AllowTransparency = False
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Button4.Enabled = False Then
            MsgBox("Password has been set, please cancel to change this option !", vbExclamation, "MigrateToGDrive")
        Else
            openfiledialog.InitialDirectory = Environment.SpecialFolder.UserProfile
            openfiledialog.DefaultExt = ".7z"
            openfiledialog.Filter = "7-ZIP Supported Format|*.7z;*.zip"
            openfiledialog.ShowDialog()
        End If
    End Sub
    Private Sub FileBrowserDialog1_Disposed(sender As Object, e As EventArgs) Handles Button1.Click
        TextBox1.Text = openfiledialog.FileName.ToString
        Label4.Text = GetSrcDriveSize(TextBox1.Text)
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If Button4.Enabled = False Then
            MsgBox("Password has been set, please cancel to change this option !", vbExclamation, "MigrateToGDrive")
        Else
            openfolderdialog.InitialDirectory = Environment.SpecialFolder.UserProfile
            openfolderdialog.ShowDialog()
        End If
    End Sub
    Private Sub FolderBrowserDialog1_Disposed(sender As Object, e As EventArgs) Handles Button2.Click
        TextBox3.Text = openfolderdialog.SelectedPath.ToString
        Label6.Text = GetDestDriveSize(TextBox3.Text)
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If Button4.Enabled = False Then
            MsgBox("Password has been set, please cancel to change this option !", vbExclamation, "MigrateToGDrive")
        Else
            If ComboBox1.SelectedIndex = 0 Then
                MsgBox("Encryption method set as no encryption, no key file needed !", vbExclamation, "MigrateToGDrive")
            Else
                openfiledialog.InitialDirectory = Environment.SpecialFolder.UserProfile
                openfiledialog.DefaultExt = ".mtg"
                openfiledialog.Filter = "MigrateToGDrive Encrypted Key|*.mtg"
                openfiledialog.ShowDialog()
            End If
        End If
    End Sub
    Private Sub FileSaveDialog1_Disposed(sender As Object, e As EventArgs) Handles Button3.Click
        If ComboBox1.SelectedIndex = 1 Then
            If openfiledialog.FileName.ToString.Remove(0, openfiledialog.FileName.ToString.Length - 3).Equals("mtg") Then
                TextBox2.Text = openfiledialog.FileName.ToString
            Else
                MsgBox("Please select a valid MigrateToGDrive encrypted key file !", vbCritical, "MigrateToGDrive")
            End If
        End If
    End Sub
    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If Button4.Enabled = False Then
            MsgBox("Password has been set, please cancel to change this option !", vbExclamation, "MigrateToGDrive")
        Else
            If ComboBox1.SelectedIndex = 0 Then
                TextBox2.Enabled = False
                TextBox2.Text = ""
            Else
                TextBox2.Enabled = True
                TextBox2.Text = ""
            End If
        End If
    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If TextBox4.Text = "" Then
            MsgBox("Please fill your password !")
        Else
            If TextBox5.Text = "" Then
                MsgBox("Please fill your password !")
            Else
                If TextBox4.Text = TextBox5.Text Then
                    TextBox1.ReadOnly = True
                    TextBox2.ReadOnly = True
                    TextBox3.ReadOnly = True
                    TextBox4.ReadOnly = True
                    TextBox5.ReadOnly = True
                    ComboBox1.Enabled = False
                    Button4.Enabled = False
                    MsgBox("Password locked !")
                Else
                    MsgBox("Password not match !")
                End If
            End If
        End If
    End Sub
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        TextBox1.ReadOnly = False
        TextBox2.ReadOnly = False
        TextBox3.ReadOnly = False
        TextBox4.Text = ""
        TextBox4.ReadOnly = False
        TextBox5.ReadOnly = False
        TextBox5.Text = ""
        ComboBox1.Enabled = True
        Button4.Enabled = True
    End Sub
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim srcFile As String
        Dim destFolder As String
        Dim encKeyPath As String
        Dim encPass As String
        Dim decKeyPass As String
        Dim decAesKey As String
        RichTextBox1.Text = "Processing ..."
        If TextBox1.Text = "" Then
            MsgBox("Archive file path is empty !, please select file", MsgBoxStyle.Critical, "MigrateToGDrive")
            RichTextBox1.Text = ""
        Else
            If TextBox3.Text = "" Then
                MsgBox("Destination folder is empty !, please fill destination folder location", MsgBoxStyle.Critical, "MigrateToGDrive")
                RichTextBox1.Text = ""
            Else
                srcFile = TextBox1.Text
                destFolder = TextBox3.Text
                encKeyPath = TextBox2.Text
                encPass = TextBox4.Text
                InitComp()
                If ComboBox1.SelectedIndex = 0 Then
                    If File.Exists(srcFile) Then
                        If Directory.Exists(destFolder) Then
                            CheckFileExist(resSrcPath, srcFile)
                            CheckFileExist(resDestPath, destFolder)
                            CheckFileExist(resInstPath, My.Application.Info.DirectoryPath.ToString)
                            CheckFileExist(resDecMtd, "No Decryption")
                            CheckFileExist(resTempPass, TextBox4.Text)
                            PrepareNotif(lastResult)
                            PrepareNotif(lastErr)
                            ManualBackup("bat\MigrateToGDrive_RB.bat")
                            decKey = File.ReadAllText(resZipLog)
                            decIndKey = decKey.IndexOf("Archives with Errors: 1")
                            If decIndKey >= 0 Then
                                PrepareNotif(lastResult)
                                PrepareNotif(lastErr)
                                CheckFileExist(lastResult, "err")
                                CheckFileExist(lastErr, "Extract error, password not match !")
                                Dim writer As New StreamWriter(errPath, True)
                                writer.WriteLine("# Migrate To GDrive v1.1")
                                writer.WriteLine("Extract Result               : Error")
                                writer.WriteLine("Extract Time                 : " & DateTime.Now)
                                writer.WriteLine("Extract Filename          : " & TextBox1.Text)
                                writer.WriteLine("Extract Location          : " & TextBox3.Text)
                                writer.WriteLine("Error Reason                 : Password Error !")
                                writer.WriteLine("Decryption Method    : No Decryption")
                                writer.WriteLine(vbCrLf)
                                writer.Close()
                            Else
                                decIndKey = decKey.IndexOf("Everything is Ok")
                                If decIndKey >= 0 Then
                                    PrepareNotif(lastResult)
                                    PrepareNotif(lastErr)
                                    CheckFileExist(lastResult, "success")
                                    Dim writer As New StreamWriter(logPath, True)
                                    writer.WriteLine("# Migrate To GDrive v1.1")
                                    writer.WriteLine("Extract Result               : Success")
                                    writer.WriteLine("Extract Time                 : " & DateTime.Now)
                                    writer.WriteLine("Extract Filename          : " & TextBox1.Text)
                                    writer.WriteLine("Extract Location          : " & TextBox3.Text)
                                    writer.WriteLine("Decryption Method    : No Decryption")
                                    writer.WriteLine(vbCrLf)
                                    writer.Close()
                                End If
                            End If
                        Else
                            CheckFileExist(lastResult, "err")
                            CheckFileExist(lastErr, "Destination folder not exist !")
                        End If
                    Else
                        CheckFileExist(lastResult, "err")
                        CheckFileExist(lastErr, "Source file not exist !")
                    End If
                Else
                    If TextBox2.Text = "" Then
                        MsgBox("Encryption key path is empty, please select encryption key file !", MsgBoxStyle.Critical, "MigrateToGDrive")
                        RichTextBox1.Text = ""
                    Else
                        RichTextBox1.Text = ""
                        If Button4.Visible = True Then
                            If ComboBox1.SelectedIndex = 1 Then
                                DecryptFile(encPass, encKeyPath, encKeyPath.Remove(0, encKeyPath.Length - 3))
                                If PathVal(resDecResult, 0).Equals("KEY_ERR") Then
                                    CheckFileExist(lastResult, "err")
                                    CheckFileExist(lastErr, "Key decryption failed, password not match !")
                                Else
                                    decKeyPass = PathVal(encKeyPath.Remove(0, encKeyPath.Length - 3), 0)
                                    If AESDecryptBase64ToString(decKeyPass, MyKey).Equals("SHA256_ERR") Then
                                        CheckFileExist(lastResult, "err")
                                        CheckFileExist(lastErr, "SHA256 decryption failure !")
                                    Else
                                        decAesKey = AESDecryptBase64ToString(decKeyPass, MyKey)
                                        If File.Exists(srcFile) Then
                                            If Directory.Exists(destFolder) Then
                                                CheckFileExist(resSrcPath, srcFile)
                                                CheckFileExist(resDestPath, destFolder)
                                                CheckFileExist(resInstPath, My.Application.Info.DirectoryPath.ToString)
                                                CheckFileExist(resTempPass, decAesKey)
                                                CheckFileExist(resDecMtd, "SHA-256")
                                                PrepareNotif(lastResult)
                                                PrepareNotif(lastErr)
                                                ManualBackup("bat\MigrateToGDrive_RB.bat")
                                                File.Delete(destFolder & "\" & encKeyPath.Remove(0, encKeyPath.Length - 3))
                                                decKey = File.ReadAllText(resZipLog)
                                                decIndKey = decKey.IndexOf("Archives with Errors: 1")
                                                If decIndKey >= 0 Then
                                                    PrepareNotif(lastResult)
                                                    PrepareNotif(lastErr)
                                                    CheckFileExist(lastResult, "err")
                                                    CheckFileExist(lastErr, "Extract error, password not match !")
                                                    Dim writer As New StreamWriter(errPath, True)
                                                    writer.WriteLine("# Migrate To GDrive v1.1")
                                                    writer.WriteLine("Extract Result               : Error")
                                                    writer.WriteLine("Extract Time                 : " & DateTime.Now)
                                                    writer.WriteLine("Extract Filename          : " & TextBox1.Text)
                                                    writer.WriteLine("Extract Location          : " & TextBox3.Text)
                                                    writer.WriteLine("Error Reason                 : Password Error !")
                                                    writer.WriteLine("Decryption Method    : SHA-256")
                                                    writer.WriteLine(vbCrLf)
                                                    writer.Close()
                                                Else
                                                    decIndKey = decKey.IndexOf("Everything is Ok")
                                                    If decIndKey >= 0 Then
                                                        PrepareNotif(lastResult)
                                                        PrepareNotif(lastErr)
                                                        CheckFileExist(lastResult, "success")
                                                        Dim writer As New StreamWriter(logPath, True)
                                                        writer.WriteLine("# Migrate To GDrive v1.1")
                                                        writer.WriteLine("Extract Result               : Success")
                                                        writer.WriteLine("Extract Time                 : " & DateTime.Now)
                                                        writer.WriteLine("Extract Filename          : " & TextBox1.Text)
                                                        writer.WriteLine("Extract Location          : " & TextBox3.Text)
                                                        writer.WriteLine("Decryption Method    : SHA-256")
                                                        writer.WriteLine(vbCrLf)
                                                        writer.Close()
                                                    End If
                                                End If
                                            Else
                                                CheckFileExist(lastResult, "err")
                                                CheckFileExist(lastErr, "Source drive not exist !")
                                            End If
                                        Else
                                            CheckFileExist(lastResult, "err")
                                            CheckFileExist(lastErr, "Source file not exist !")
                                        End If
                                        PrepareNotif(resTempPass)
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
                ShowNotif()
            End If
        End If
    End Sub
    Private Sub ShowNotif()
        If File.Exists(lastResult) Then
            Dim lastRest As String = PathVal(lastResult, 0)
            If PathVal(lastResult, 0).Equals("success") Then
                MsgBox("Restore backup success !", MsgBoxStyle.Information, "MigrateToGDrive")
                ShowLog("Restore", logPath)
            ElseIf PathVal(lastResult, 0).Equals("err") Or PathVal(resDecResult, 0).Equals("KEY_ERR") Then
                MsgBox("Restore backup error !", MsgBoxStyle.Critical, "MigrateToGDrive")
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
                ShowLog("Error", errPath)
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
    Private Sub InitComp()
        PrepareNotif(resDestPath)
        PrepareNotif(resInstPath)
        PrepareNotif(resSrcPath)
        PrepareNotif(resTempPass)
        PrepareNotif(resDecResult)
        PrepareNotif(resZipLog)
        PrepareNotif(resDecMtd)
    End Sub
End Class