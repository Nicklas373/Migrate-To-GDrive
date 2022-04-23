<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Log
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Log))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.RichTextBox1 = New System.Windows.Forms.RichTextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.Button7 = New System.Windows.Forms.Button()
        Me.Button8 = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.RichTextBox1)
        Me.GroupBox1.Location = New System.Drawing.Point(163, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(795, 519)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "History"
        '
        'RichTextBox1
        '
        Me.RichTextBox1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.RichTextBox1.Location = New System.Drawing.Point(6, 22)
        Me.RichTextBox1.Name = "RichTextBox1"
        Me.RichTextBox1.ReadOnly = True
        Me.RichTextBox1.Size = New System.Drawing.Size(784, 492)
        Me.RichTextBox1.TabIndex = 0
        Me.RichTextBox1.Text = "Backup history will show here..."
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.Button1.Image = CType(resources.GetObject("Button1.Image"), System.Drawing.Image)
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button1.Location = New System.Drawing.Point(6, 22)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(142, 42)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "Backup History"
        Me.Button1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.Button2.Image = CType(resources.GetObject("Button2.Image"), System.Drawing.Image)
        Me.Button2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button2.Location = New System.Drawing.Point(5, 20)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(142, 42)
        Me.Button2.TabIndex = 2
        Me.Button2.Text = "Backup Error"
        Me.Button2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.Button3.Image = CType(resources.GetObject("Button3.Image"), System.Drawing.Image)
        Me.Button3.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button3.Location = New System.Drawing.Point(6, 22)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(142, 42)
        Me.Button3.TabIndex = 3
        Me.Button3.Text = "Clear History"
        Me.Button3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.Button4.Image = CType(resources.GetObject("Button4.Image"), System.Drawing.Image)
        Me.Button4.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button4.Location = New System.Drawing.Point(6, 70)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(142, 42)
        Me.Button4.TabIndex = 4
        Me.Button4.Text = "Export History"
        Me.Button4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button4.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Button7)
        Me.GroupBox2.Controls.Add(Me.Button5)
        Me.GroupBox2.Controls.Add(Me.Button1)
        Me.GroupBox2.Location = New System.Drawing.Point(3, 12)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(154, 169)
        Me.GroupBox2.TabIndex = 5
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Backup History"
        '
        'Button5
        '
        Me.Button5.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.Button5.Image = CType(resources.GetObject("Button5.Image"), System.Drawing.Image)
        Me.Button5.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button5.Location = New System.Drawing.Point(6, 70)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(142, 42)
        Me.Button5.TabIndex = 3
        Me.Button5.Text = "Archive History"
        Me.Button5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button5.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Button3)
        Me.GroupBox3.Controls.Add(Me.Button4)
        Me.GroupBox3.Location = New System.Drawing.Point(3, 359)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(154, 124)
        Me.GroupBox3.TabIndex = 1
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Tools"
        '
        'Button6
        '
        Me.Button6.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.Button6.Image = CType(resources.GetObject("Button6.Image"), System.Drawing.Image)
        Me.Button6.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button6.Location = New System.Drawing.Point(5, 68)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(142, 42)
        Me.Button6.TabIndex = 4
        Me.Button6.Text = "Archive Error"
        Me.Button6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button6.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.Button8)
        Me.GroupBox4.Controls.Add(Me.Button6)
        Me.GroupBox4.Controls.Add(Me.Button2)
        Me.GroupBox4.Location = New System.Drawing.Point(3, 186)
        Me.GroupBox4.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupBox4.Size = New System.Drawing.Size(154, 168)
        Me.GroupBox4.TabIndex = 6
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Error History"
        '
        'Button7
        '
        Me.Button7.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.Button7.Image = CType(resources.GetObject("Button7.Image"), System.Drawing.Image)
        Me.Button7.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button7.Location = New System.Drawing.Point(6, 118)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(142, 42)
        Me.Button7.TabIndex = 4
        Me.Button7.Text = "Restore History"
        Me.Button7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button7.UseVisualStyleBackColor = True
        '
        'Button8
        '
        Me.Button8.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.Button8.Image = CType(resources.GetObject("Button8.Image"), System.Drawing.Image)
        Me.Button8.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button8.Location = New System.Drawing.Point(6, 116)
        Me.Button8.Name = "Button8"
        Me.Button8.Size = New System.Drawing.Size(142, 42)
        Me.Button8.TabIndex = 5
        Me.Button8.Text = "Restore Error"
        Me.Button8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button8.UseVisualStyleBackColor = True
        '
        'Log
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.AutoSize = True
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ClientSize = New System.Drawing.Size(969, 541)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.HelpButton = True
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Log"
        Me.Text = "MigrateToGDrive | History"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents RichTextBox1 As RichTextBox
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents Button4 As Button
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents Button5 As Button
    Friend WithEvents Button6 As Button
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents Button7 As Button
    Friend WithEvents Button8 As Button
End Class
