﻿Public Class About
    Dim changelog As String = "changelog.txt"
    Private Sub About_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.AllowTransparency = False
        TextBox1.Text = My.Application.Info.ProductName
        TextBox2.Text = My.Application.Info.Version.ToString
        TextBox3.Text = "April, 16 2022"
        TextBox4.Text = My.Application.Info.Copyright
        TextBox5.Text = My.Application.Info.DirectoryPath
        RichTextBox1.Text = "Description: " & vbCrLf & vbCrLf & "Migrate To GDrive is a " & My.Application.Info.Description
        readLog("changelog", changelog)
        TextBox6.Text = "PLACEHOLDER"
        TextBox7.Text = My.Computer.Name.ToString
        TextBox8.Text = My.Computer.Info.OSFullName.ToString
        TextBox9.Text = My.Computer.Info.OSVersion.ToString
    End Sub

    Private Sub readLog(log As String, path As String)
        RichTextBox2.Text = ""
        If File.Exists(path) Then
            If New FileInfo(path).Length.Equals(0) Then
                MsgBox(log & " is empty !", MsgBoxStyle.Critical, "MigrateToGDrive")
            Else
                RichTextBox2.Text = File.ReadAllText(path)
            End If
        Else
            MsgBox(log & " does not exist !", MsgBoxStyle.Critical, "MigrateToGDrive")
        End If
    End Sub
End Class