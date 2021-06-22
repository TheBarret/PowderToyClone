Imports System.Drawing.Drawing2D

Public Class IMPictureBox
    Inherits PictureBox
    Public Property InterpolationMode As InterpolationMode
    Protected Overrides Sub OnPaint(ByVal paintEventArgs As PaintEventArgs)
        If (paintEventArgs.Graphics.InterpolationMode <> InterpolationMode) Then
            paintEventArgs.Graphics.InterpolationMode = InterpolationMode
        End If
        MyBase.OnPaint(paintEventArgs)
    End Sub
End Class