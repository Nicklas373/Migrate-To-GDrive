Imports Syncfusion.Pdf
Imports Syncfusion.Pdf.Parsing
Public Class pdf_compress
    Dim fileDialog As New OpenFileDialog
    Dim saveDialog As New SaveFileDialog
    Private Sub pdf_compress_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AllowTransparency = False
        Button5.Visible = False
        Button2.Visible = False
        TextBox1.ReadOnly = True
        TextBox2.ReadOnly = True
    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        TextBox1.ReadOnly = False
        TextBox2.ReadOnly = False
        Button4.Visible = False
        Button5.Visible = True
        Button2.Visible = True
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        TextBox1.ReadOnly = True
        TextBox2.ReadOnly = True
        Button5.Visible = False
        Button2.Visible = False
        Button4.Visible = True
        Label8.Text = getFileSize(TextBox1.Text)
    End Sub
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox1.ReadOnly = True
        TextBox2.ReadOnly = True
        Button5.Visible = False
        Button4.Visible = True
        Button2.Visible = False
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.ReadOnly = True Then
            MsgBox("Configuration menu is locked, Please click edit !", MsgBoxStyle.Information, "MigrateToGDrive")
        Else
            fileDialog.DefaultExt = ".pdf"
            fileDialog.Filter = "PDF File | *.pdf"
            fileDialog.InitialDirectory = Environment.SpecialFolder.UserProfile
            fileDialog.ShowDialog()
        End If
    End Sub
    Private Sub OpenFileDialog_Disposed(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.ReadOnly = False Then
            TextBox1.Text = Path.GetFullPath(fileDialog.FileName.ToString)
        End If
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If TextBox2.ReadOnly = True Then
            MsgBox("Configuration menu is locked, Please click edit !", MsgBoxStyle.Information, "MigrateToGDrive")
        Else
            saveDialog.DefaultExt = ".pdf"
            saveDialog.Filter = "PDF File | *.pdf"
            saveDialog.InitialDirectory = Environment.SpecialFolder.UserProfile
            saveDialog.ShowDialog()
        End If
    End Sub
    Private Sub SaveFileDialog_Disposed(sender As Object, e As EventArgs) Handles Button3.Click
        If TextBox1.ReadOnly = False Then
            TextBox2.Text = Path.GetFullPath(saveDialog.FileName.ToString)
        End If
    End Sub
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        If Button2.Visible = True And Button5.Visible = True Then
            MsgBox("Please save PDF file configuration before compress !", MsgBoxStyle.Critical, "MigrateToGDrive")
        Else
            If TextBox1.Text = "" Then
                MsgBox("No PDF file was selected !, please select PDF file first !", MsgBoxStyle.Critical, "MigrateToGDrive")
            Else
                If TextBox2.Text = "" Then
                    MsgBox("Destination PDF file location was not selected !, please select destination location first !", MsgBoxStyle.Critical, "MigrateToGDrive")
                Else
                    If ComboBox2.Text = "" Then
                        MsgBox("Please select compression level !", MsgBoxStyle.Critical, "MigrateToGDrive")
                    Else
                        CompressPDF(TextBox1.Text, TextBox2.Text, True, imgCompLvlVal, pdfFoOptVal, pdfOpcOptVal, pdfMtOptVal, pdfIncUpdVal)
                    End If
                End If
            End If
        End If
    End Sub
    Private Async Sub CompressPDF(pdfPathIn As String, pdfPathOut As String, pdfCompOpt As Boolean, pdfImgQtyOpt As Integer, pdfOfOpt As Boolean, pdfOpcOpt As Boolean, pdfRmOpt As Boolean, pdfIncUpd As Boolean)
        ProgressBar1.Style = ProgressBarStyle.Marquee
        ProgressBar1.MarqueeAnimationSpeed = 40
        ProgressBar1.Refresh()
        Dim ldoc As New PdfLoadedDocument(pdfPathIn)
        Dim options As PdfCompressionOptions = New PdfCompressionOptions
        options.CompressImages = pdfCompOpt
        options.ImageQuality = pdfImgQtyOpt
        options.OptimizeFont = pdfOfOpt
        options.OptimizePageContents = pdfOpcOpt
        options.RemoveMetadata = pdfRmOpt
        ldoc.FileStructure.IncrementalUpdate = pdfIncUpd
        ldoc.CompressionOptions = options
        Await Task.Run(Sub() ldoc.Save(pdfPathOut))
        ProgressBar1.Style = ProgressBarStyle.Blocks
        ProgressBar1.Value = 100
        ldoc.Close(True)
        If File.Exists(TextBox2.Text) Then
            MsgBox("Compress PDF success !", MsgBoxStyle.Information, "MigrateToGDrive")
            Label10.Text = getFileSize(TextBox2.Text)
        Else
            MsgBox("Compress PDF failed !", MsgBoxStyle.Critical, "MigrateToGDrive")
        End If
    End Sub
    Private Function imgCompLvlVal() As Integer
        Dim value As Integer
        If ComboBox2.Text = "Highest" Then
            value = 20
        ElseIf ComboBox2.Text = "High" Then
            value = 40
        ElseIf ComboBox2.Text = "Normal" Then
            value = 60
        ElseIf ComboBox2.Text = "Low" Then
            value = 80
        ElseIf ComboBox2.Text = "Lowest" Then
            value = 100
        Else
            value = 0
        End If

        Return value
    End Function
    Private Function pdfIncUpdVal() As Boolean
        Dim value As Boolean
        If CheckBox5.Checked Then
            value = True
        Else
            value = False
        End If

        Return value
    End Function
    Private Function pdfMtOptVal() As Boolean
        Dim value As Boolean
        If CheckBox4.Checked Then
            value = True
        Else
            value = False
        End If

        Return value
    End Function
    Private Function pdfFoOptVal() As Boolean
        Dim value As Boolean
        If CheckBox2.Checked Then
            value = True
        Else
            value = False
        End If

        Return value
    End Function
    Private Function pdfOpcOptVal() As Boolean
        Dim value As Boolean
        If CheckBox3.Checked Then
            value = True
        Else
            value = False
        End If

        Return value
    End Function
End Class