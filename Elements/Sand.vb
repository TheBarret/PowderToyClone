Namespace Elements
    Public Class Sand
        Inherits Powder
        Sub New(sb As Sandbox)
            MyBase.New(sb)
            Me.Color = Color.SandyBrown
        End Sub

        Public Overrides Sub Update()
            If (Me.Sandbox.Powders.Exists(Function(c) c.X = X AndAlso c.Y = Y - 1)) Then
                If (Me.Sandbox.Powders.Exists(Function(c) c.X = X AndAlso c.Y = Y + 1) Or Y + 1 = Me.Sandbox.Size.Height) Then
                    MoveX(Me.Sandbox.Random.Next(-1, 2))
                End If
            End If
            Me.Move(Direction.Vertical, 2)
        End Sub

        Public ReadOnly Property Flammability() As Double
            Get
                Return 0.5
            End Get
        End Property

        Public Overrides ReadOnly Property Name As String
            Get
                Return "Sand"
            End Get
        End Property
    End Class
End Namespace