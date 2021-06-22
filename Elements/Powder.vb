Imports Alchemy.Elements.Properties

Namespace Elements
    Public Enum Direction
        Vertical
        Horizontal
    End Enum
    Public MustInherit Class Powder
        Public Property X As Integer
        Public Property Y As Integer

        Public Property Solid As Boolean
        Public Property Color As Color

        Public Property Sandbox As Sandbox

        Sub New(sb As Sandbox)
            Me.X = 0
            Me.Y = 0
            Me.Solid = False
            Me.Sandbox = sb
            Me.Color = Color.Gray
        End Sub

        Public Overridable Sub Update()
            Me.MoveY(2)
        End Sub

        Public Sub Tick()
            Me.Update()
        End Sub

        Public Sub Move(d As Direction, distance As Integer)
            If (d = Direction.Horizontal) Then
                Me.MoveX(distance)
            ElseIf (d = Direction.Vertical) Then
                Me.MoveY(distance)
            End If
        End Sub

        Public Sub MoveY(distance As Integer)
            Try
                If (distance = 0) Then Return
                Dim Objects As IEnumerable(Of Powder)

                If (distance > 0) Then
                    Objects = Me.Sandbox.Powders.Where(Function(c) c.X = X AndAlso Y < c.Y AndAlso Y + distance >= c.Y AndAlso Not c.Equals(Me))
                Else
                    Objects = Me.Sandbox.Powders.Where(Function(c) c.X = X AndAlso Y > c.Y AndAlso Y + distance <= c.Y AndAlso Not c.Equals(Me))
                End If

                If Objects.Any() Then

                    If distance > 0 Then
                        Dim nearest As Powder = Objects.OrderBy(Function(c) c.Y).ToList.First
                        Y = nearest.Y - 1 - distance
                    Else
                        Dim nearest = Objects.OrderByDescending(Function(c) c.Y).ToList()(0)
                        Y = nearest.Y + 1 + (distance * -1)
                    End If
                End If

                If (distance > 0) Then
                    For i As Integer = 0 To distance - 1
                        If (Y + i >= Me.Sandbox.Size.Height - 1) Then
                            Y = Me.Sandbox.Size.Height - 1
                            Return
                        End If
                    Next
                Else
                    For i As Integer = 0 To distance + 1
                        If (Y + i <= 0) Then
                            Y = 0
                            Return
                        End If
                    Next
                End If

                Y = Y + distance
            Catch ex As Exception
                Debug.WriteLine(String.Format("Could not move on Y, distance {0}", distance))
            End Try
        End Sub
        Public Sub MoveX(distance As Integer)
            Try
                If distance = 0 Then Return
                Dim Objects As IEnumerable(Of Powder) = Nothing

                If (distance > 0) Then
                    Objects = Me.Sandbox.Powders.Where(Function(c) c.Y = Y AndAlso Not c.Equals(Me) AndAlso X < c.X AndAlso X + distance >= c.X)
                Else
                    Objects = Me.Sandbox.Powders.Where(Function(c) c.Y = Y AndAlso Not c.Equals(Me) AndAlso X > c.X AndAlso X + distance <= c.X)
                End If

                If (Objects.Any) Then
                    If (distance > 0) Then
                        Dim nearest As Powder = Objects.OrderBy(Function(c) c.X).ToList().First
                        X = nearest.X - 1 - distance
                    Else
                        Dim nearest As Powder = Objects.OrderByDescending(Function(c) c.X).ToList().First
                        X = nearest.X + 1 + (distance * -1)
                    End If
                End If

                If (distance > 0) Then
                    For i As Integer = 0 To distance - 1
                        If (X + i >= Me.Sandbox.Size.Width - 1) Then
                            X = Me.Sandbox.Size.Width - 1
                            Return
                        End If
                    Next
                Else
                    For i As Integer = 0 To distance + 1
                        If (X + i <= 0) Then
                            X = 0
                            Return
                        End If
                    Next
                End If

                X = X + distance
            Catch ex As Exception
                Debug.WriteLine(String.Format("Could not move on X, distance {0}", distance))
            End Try
        End Sub

        Public Function OnSolid() As Boolean
            If (Y + 1 >= Me.Sandbox.Size.Height - 1) Then Return True

            Dim powder As Powder = Me.Sandbox.Powders.FirstOrDefault(Function(c) c.Y = Y + 1)

            If (powder IsNot Nothing) Then
                If TypeOf powder Is Locked Then Return True
                Return Me.Sandbox.Powders.First(Function(c) c.Y = Y + 1).OnSolid()
            End If
            Return False
        End Function

        Public Function GetNeighbors(r As Integer) As IEnumerable(Of Powder)
            Return Me.Sandbox.Powders.Where(Function(p) (p.Y = Y AndAlso p.X = X - r) OrElse (p.Y = Y AndAlso p.X = X + r))
        End Function

        Public Shared Function Square(position As Point, radius As Integer) As List(Of Point)
            Dim points As New List(Of Point)
            For x As Integer = position.X - radius To position.X + radius
                For y As Integer = position.Y - radius To position.Y + radius
                    points.Add(New Point(x, y))
                Next
            Next
            Return points
        End Function

        Public MustOverride ReadOnly Property Name As String

        Public Overrides Function ToString() As String
            Return String.Format("{0} [{1},{2}]", Me.Name, Me.X, Me.Y)
        End Function

    End Class

End Namespace