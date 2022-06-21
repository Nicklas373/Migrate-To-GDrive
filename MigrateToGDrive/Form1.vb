Public Class Form1
    Private Sub StandardBackupToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StandardBackupToolStripMenuItem.Click
        Dim menu_nrmbackup = New normal_backup With {
            .MdiParent = Me
        }
        menu_nrmbackup.Show()
    End Sub
    Private Sub AdvancedBackupToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AdvancedBackupToolStripMenuItem.Click
        Dim menu_advbackup = New advanced_backup With {
            .MdiParent = Me
        }
        menu_advbackup.Show()
    End Sub
    Private Sub RestoreBackupToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RestoreBackupToolStripMenuItem.Click
        Dim menu_resbackup = New advanced_restore With {
            .MdiParent = Me
         }
        menu_resbackup.Show()
    End Sub
    Private Sub HistoryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HistoryToolStripMenuItem.Click
        Dim menu_history = New Log With {
            .MdiParent = Me
        }
        menu_history.Show()
    End Sub
    Private Sub SettingsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SettingsToolStripMenuItem.Click
        Dim menu_setting = New Settings With {
            .MdiParent = Me
        }
        menu_setting.Show()
    End Sub
    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        Dim menu_about = New About With {
            .MdiParent = Me
        }
        menu_about.Show()
    End Sub
    Private Sub PDFToolToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PDFToolToolStripMenuItem.Click
        Dim menu_pdf = New pdf_compress With {
            .MdiParent = Me
        }
        menu_pdf.Show()
    End Sub
End Class