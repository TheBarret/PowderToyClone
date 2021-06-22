<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMain
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
        Me.lblFps = New System.Windows.Forms.Label()
        Me.R1 = New System.Windows.Forms.RadioButton()
        Me.R2 = New System.Windows.Forms.RadioButton()
        Me.Renderer = New Alchemy.IMPictureBox()
        Me.btnClear = New System.Windows.Forms.Button()
        CType(Me.Renderer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblFps
        '
        Me.lblFps.AutoSize = True
        Me.lblFps.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFps.Location = New System.Drawing.Point(12, 9)
        Me.lblFps.Name = "lblFps"
        Me.lblFps.Size = New System.Drawing.Size(31, 13)
        Me.lblFps.TabIndex = 1
        Me.lblFps.Text = "Fps:"
        '
        'R1
        '
        Me.R1.AutoSize = True
        Me.R1.Checked = True
        Me.R1.Location = New System.Drawing.Point(530, 34)
        Me.R1.Name = "R1"
        Me.R1.Size = New System.Drawing.Size(54, 17)
        Me.R1.TabIndex = 2
        Me.R1.TabStop = True
        Me.R1.Text = "Water"
        Me.R1.UseVisualStyleBackColor = True
        '
        'R2
        '
        Me.R2.AutoSize = True
        Me.R2.Location = New System.Drawing.Point(530, 57)
        Me.R2.Name = "R2"
        Me.R2.Size = New System.Drawing.Size(50, 17)
        Me.R2.TabIndex = 3
        Me.R2.Text = "Sand"
        Me.R2.UseVisualStyleBackColor = True
        '
        'Renderer
        '
        Me.Renderer.BackColor = System.Drawing.Color.Black
        Me.Renderer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Renderer.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
        Me.Renderer.Location = New System.Drawing.Point(12, 34)
        Me.Renderer.Name = "Renderer"
        Me.Renderer.Size = New System.Drawing.Size(512, 512)
        Me.Renderer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.Renderer.TabIndex = 0
        Me.Renderer.TabStop = False
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(530, 516)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(120, 30)
        Me.btnClear.TabIndex = 4
        Me.btnClear.Text = "Reset"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(662, 561)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.R2)
        Me.Controls.Add(Me.R1)
        Me.Controls.Add(Me.lblFps)
        Me.Controls.Add(Me.Renderer)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Alchemy"
        CType(Me.Renderer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Renderer As IMPictureBox
    Friend WithEvents lblFps As Label
    Friend WithEvents R1 As RadioButton
    Friend WithEvents R2 As RadioButton
    Friend WithEvents btnClear As Button
End Class
