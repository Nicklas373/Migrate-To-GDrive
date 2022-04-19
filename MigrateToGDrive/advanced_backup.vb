Public Class advanced_backup
    Dim filedialog As New FolderBrowserDialog
    Dim logPath As String = "log/advlog"
    Dim errPath As String = "log/adverr"
    Dim lastResult As String = "log/lastResult"
    Dim lastErr As String = "log/lastErr"
    Dim advSrcPath As String = "conf/adv_backup/advSrcPath"
    Dim advDestPath As String = "conf/adv_backup/advDestPath"
    Dim advCompExt As String = "conf/adv_backup/advCompExt"
    Dim advCompLevel As String = "conf/adv_backup/advCompLvl"
    Dim advCompType As String = "conf/adv_backup/advCompType"
    Dim advEncType As String = "conf/adv_backup/advEncType"
    Dim advInstPath As String = "conf/adv_backup/advInstPath"
    Dim advRanStrg As String = "conf/adv_backup/advRandomStrg"
    Dim advTempPass As String = "conf/adv_backup/advTempPass"
    Dim compressLevel As String
    Dim compressType As String
    Dim compressExt As String
    Dim key As String = RandomString(10)
    Dim MyKey As String = "YOUR_KEY_HERE"
    Private Sub advanced_backup_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim dt As Date = Today
        TextBox1.ReadOnly = True
        TextBox2.ReadOnly = True
        GroupBox1.Enabled = False
        Me.AllowTransparency = False
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        filedialog.InitialDirectory = Environment.SpecialFolder.UserProfile
        filedialog.ShowDialog()
    End Sub
    Private Sub FolderBrowserDialog1_Disposed(sender As Object, e As EventArgs) Handles Button1.Click
        TextBox1.Text = filedialog.SelectedPath.ToString
        Label3.Text = GlobalModul.getSrcDriveSize(TextBox1.Text)
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        filedialog.InitialDirectory = Environment.SpecialFolder.UserProfile
        filedialog.ShowDialog()
    End Sub
    Private Sub FolderBrowserDialog2_Disposed(sender As Object, e As EventArgs) Handles Button3.Click
        TextBox2.Text = filedialog.SelectedPath.ToString
        Label1.Text = GlobalModul.getDestDriveSize(TextBox2.Text)
    End Sub
    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        If ComboBox2.Text = "Archive" Then
            GroupBox1.Enabled = False
            ComboBox5.Enabled = False
            Label10.Enabled = False
        Else
            GroupBox1.Enabled = True
            ComboBox5.Enabled = True
            Label10.Enabled = True
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
                    TextBox4.ReadOnly = True
                    TextBox5.ReadOnly = True
                    Button4.Enabled = False
                    MsgBox("Password locked !")
                Else
                    MsgBox("Password not match !")
                End If
            End If
        End If
    End Sub
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        TextBox4.Text = ""
        TextBox4.ReadOnly = True
        TextBox5.ReadOnly = True
        TextBox5.Text = ""
        Button4.Enabled = True
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
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
            Else
                If ComboBox2.Text = "" Then
                    MsgBox("Backup type is empty !, Please select backup type first !", MsgBoxStyle.Critical, "MigrateToGDrive")
                    RichTextBox1.Text = ""
                ElseIf ComboBox2.Text = "Archive" Then
                    If ComboBox3.Text = "" Then
                        MsgBox("Compression level is empty, please select compression type !", MsgBoxStyle.Critical, "MigrateToGDrive")
                    Else
                        compressLevel = CompLevel(ComboBox3.Text)
                        CheckFileExist(advCompLevel, compressLevel)
                        If ComboBox4.Text = "" Then
                            MsgBox("Compression type is empty, please select compression type !", MsgBoxStyle.Critical, "MigrateToGDrive")
                        Else
                            uiTrimSrc = TextBox1.Text
                            uiTrimDest = TextBox2.Text
                            compressType = CompType(ComboBox4.Text)
                            compressExt = CompExt(ComboBox4.Text)
                            CheckFileExist(advCompType, compressType)
                            If Directory.Exists(uiTrimSrc) Then
                                Dim key As String = RandomString(10)
                                CheckFileExist(advSrcPath, uiTrimSrc)
                                CheckFileExist(advDestPath, uiTrimDest)
                                CheckFileExist(advInstPath, My.Application.Info.DirectoryPath.ToString)
                                CheckFileExist(advRanStrg, key)
                                CheckFileExist(advCompExt, compressExt)
                                PrepareNotif(lastResult)
                                PrepareNotif(lastErr)
                                ManualBackup("bat\MigrateToGDrive_AR_NP.bat")
                            Else
                                CheckFileExist(lastResult, "err")
                                CheckFileExist(lastErr, "Source drive not exist !")
                            End If
                            ShowNotif()
                        End If
                    End If
                ElseIf ComboBox2.Text = "Archive + Password" Then
                    If ComboBox3.Text = "" Then
                        MsgBox("Compression level is empty, please select compression type !", MsgBoxStyle.Critical, "MigrateToGDrive")
                    Else
                        compressLevel = CompLevel(ComboBox3.Text)
                        If ComboBox4.Text = "" Then
                            MsgBox("Compression type is empty, please select compression type !", MsgBoxStyle.Critical, "MigrateToGDrive")
                        Else
                            compressType = CompType(ComboBox4.Text)
                            If ComboBox5.Text = "" Then
                                MsgBox("Password type is empty, please select password type !", MsgBoxStyle.Critical, "MigrateToGDrive")
                            Else
                                If ComboBox5.SelectedIndex = 0 Then
                                    If Button4.Enabled = False Then
                                        MsgBox("No Encrypt")
                                        uiTrimSrc = TextBox1.Text
                                        uiTrimDest = TextBox2.Text
                                        compressType = CompType(ComboBox4.Text)
                                        compressExt = CompExt(ComboBox4.Text)
                                        CheckFileExist(advCompType, compressType)
                                        CheckFileExist(advCompType, "No Encryption")
                                        If Directory.Exists(uiTrimSrc) Then
                                            Dim key As String = RandomString(10)
                                            CheckFileExist(advSrcPath, uiTrimSrc)
                                            CheckFileExist(advDestPath, uiTrimDest)
                                            CheckFileExist(advInstPath, My.Application.Info.DirectoryPath.ToString)
                                            CheckFileExist(advRanStrg, key)
                                            CheckFileExist(advCompExt, compressExt)
                                            CheckFileExist(advTempPass, TextBox4.Text)
                                            PrepareNotif(lastResult)
                                            PrepareNotif(lastErr)
                                            ManualBackup("bat\MigrateToGDrive_AR_P.bat")
                                        Else
                                            CheckFileExist(lastResult, "err")
                                            CheckFileExist(lastErr, "Source drive not exist !")
                                        End If
                                        ShowNotif()
                                        PrepareNotif(advTempPass)
                                    End If
                                ElseIf ComboBox5.SelectedIndex = 1 Then
                                    If Button4.Enabled = False Then
                                        uiTrimSrc = TextBox1.Text
                                        uiTrimDest = TextBox2.Text
                                        compressType = CompType(ComboBox4.Text)
                                        compressExt = CompExt(ComboBox4.Text)
                                        CheckFileExist(advCompType, compressType)
                                        CheckFileExist(advEncType, "SHA-256")
                                        If Directory.Exists(uiTrimSrc) Then
                                            CheckFileExist(advSrcPath, uiTrimSrc)
                                            CheckFileExist(advDestPath, uiTrimDest)
                                            CheckFileExist(advInstPath, My.Application.Info.DirectoryPath.ToString)
                                            CheckFileExist(advRanStrg, key)
                                            CheckFileExist(advCompExt, compressExt)
                                            PrepareNotif(lastResult)
                                            PrepareNotif(lastErr)
                                            ManualBackup("bat\MigrateToGDrive_AR_P.bat")
                                            EncryptFile(TextBox4.Text, advTempPass, TextBox2.Text & "\MigrateToGDrive_" & key & "_KEY.enc")
                                        Else
                                            CheckFileExist(lastResult, "err")
                                            CheckFileExist(lastErr, "Source drive not exist !")
                                        End If
                                        ShowNotif()
                                        PrepareNotif(advTempPass)
                                    End If
                                Else
                                    If Button4.Enabled = False Then
                                        uiTrimSrc = TextBox1.Text
                                        uiTrimDest = TextBox2.Text
                                        compressType = CompType(ComboBox4.Text)
                                        compressExt = CompExt(ComboBox4.Text)
                                        CheckFileExist(advCompType, compressType)
                                        CheckFileExist(advEncType, "HMAC SHA-256")
                                        If Directory.Exists(uiTrimSrc) Then
                                            CheckFileExist(advSrcPath, uiTrimSrc)
                                            CheckFileExist(advDestPath, uiTrimDest)
                                            CheckFileExist(advInstPath, My.Application.Info.DirectoryPath.ToString)
                                            CheckFileExist(advRanStrg, key)
                                            CheckFileExist(advCompExt, compressExt)
                                            PrepareNotif(lastResult)
                                            PrepareNotif(lastErr)
                                            ManualBackup("bat\MigrateToGDrive_AR_P.bat")
                                            EncryptFile(TextBox4.Text, advTempPass, TextBox2.Text & "\MigrateToGDrive_" & key & "_KEY.enc")
                                        Else
                                            CheckFileExist(lastResult, "err")
                                            CheckFileExist(lastErr, "Source drive not exist !")
                                        End If
                                        ShowNotif()
                                        PrepareNotif(advTempPass)
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        End If
    End Sub
    Private Sub ShowNotif()
        If File.Exists(lastResult) Then
            Dim lastRest As String = PathVal(lastResult, 0)
            If PathVal(lastResult, 0).Equals("success") Then
                MsgBox("Backup success !", MsgBoxStyle.Information, "MigrateToGDrive")
                showLog("Backup", logPath)
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
    Private Sub showLog(log As String, path As String)
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