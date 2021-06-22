Namespace Elements
    Public Class Water
        Inherits Powder
        Public Pressure As Boolean

        Sub New(sb As Sandbox)
            MyBase.New(sb)
            Me.Color = Color.SkyBlue
        End Sub

        Public Overrides Sub Update()
            If Me.Sandbox.Powders.Exists(Function(c) c.Y = Y - 1 AndAlso c.X = X) Then
                If (OnSolid()) Then Pressure = True
            End If

            If (Pressure) Then
                Me.Move(Direction.Horizontal, Me.Sandbox.Random.Next(-1, 2))
            End If

            Dim neighbours As IEnumerable(Of Powder) = Me.GetNeighbors(1)

            For Each liquid In neighbours.OfType(Of Water)
                If liquid.OnSolid() Then liquid.Pressure = True
            Next

            If Not neighbours.Any() AndAlso Not Me.Sandbox.Powders.Exists(Function(c) c.Y = Y - 1 AndAlso c.X = X) Then
                Pressure = False
            End If

            Me.Move(Direction.Vertical, 1)
        End Sub
        Public Overrides ReadOnly Property Name As String
            Get
                Return "Water"
            End Get
        End Property
    End Class
End Namespace