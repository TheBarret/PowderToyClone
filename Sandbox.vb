Imports Alchemy.Elements
Imports Timer = System.Timers.Timer

Public Class Sandbox
    Delegate Sub Complete(Output As Image)
    Public Property Buffer As Bitmap
    Public Property Size As Size

    Public ReadOnly Property Random As Random
    Public Property Timer As Timer

    Public Event Completed As Complete

    Public Property Powders As List(Of Powder)
    Public Property Deleted As Queue(Of Powder)
    Public Property Added As Queue(Of Powder)

    Public Property Mouse As Point
    Public Property Keys As List(Of Keys)

    Public Property RTime As Integer
    Public Property STime As Integer

    Public Property PLock As Object
    Public Property RLock As Object

    Public Property Delta As Stopwatch

    Public Property DeltaT As Integer
    Public Property Running As Boolean

    Sub New()
        Me.RTime = 0
        Me.STime = 0
        Me.DeltaT = 0
        Me.Delta = New Stopwatch
        Me.Running = False
        Me.PLock = New Object
        Me.RLock = New Object
        Me.Size = New Size(64, 64)
        Me.Powders = New List(Of Powder)
        Me.Deleted = New Queue(Of Powder)
        Me.Added = New Queue(Of Powder)
        Me.Keys = New List(Of Keys)
        Me.Mouse = Point.Empty
        Me.Random = New Random(Environment.TickCount)
        Me.Timer = New Timer(20)
        AddHandler Timer.Elapsed, AddressOf Me.Elapsed
    End Sub

    Public Sub Start()
        Me.Running = True
        Me.Timer.Start()
    End Sub

    Public Function Render() As Image
        Me.ClearBuffer()
        Me.Draw()
        Me.DrawUI()
        Return Buffer
    End Function
    Private Sub Elapsed(sender As Object, e As Timers.ElapsedEventArgs)
        SyncLock Me.RLock
            DeltaT = CInt(Me.Delta.ElapsedMilliseconds)
            Me.Delta.Restart()
            Dim sw As New Stopwatch()
            sw.Start()
            If (Me.Running) Then Simulate()
            Me.STime = CInt(sw.ElapsedMilliseconds)
            sw.Restart()
            Me.Render()
            RaiseEvent Completed(Me.Buffer)
            Me.RTime = CInt(sw.ElapsedMilliseconds)
            sw.Stop()
        End SyncLock
    End Sub

    Private Sub ClearBuffer()
        Me.Buffer = New Bitmap(Me.Size.Width, Me.Size.Height)
        SyncLock Buffer
            Using g As Graphics = Graphics.FromImage(Buffer)
                g.Clear(Color.Black)
            End Using
        End SyncLock
    End Sub

    Private Sub Draw()
        SyncLock Me.Buffer
            SyncLock Me.PLock
                For Each p In Me.Powders.ToList()
                    Me.Buffer.SetPixel(p.X, p.Y, p.Color)
                Next
            End SyncLock
        End SyncLock
    End Sub

    Private Sub DrawUI()
        Try
            SyncLock Me.Buffer
                Me.Buffer.SetPixel(Me.Mouse.X, Me.Mouse.Y, Color.White)
            End SyncLock
        Catch
            '//ignore exception for now
        End Try
    End Sub

    Private Sub Simulate()
        SyncLock Me.PLock
            For Each p In Powders
                p.Tick()
            Next
            While Me.Deleted.Count > 0
                Powders.Remove(Me.Deleted.Dequeue())
            End While
            While Me.Added.Count > 0
                Powders.Add(Me.Added.Dequeue)
            End While
        End SyncLock
    End Sub

    Public Sub Add(p As Powder)
        If p.Y >= 0 AndAlso p.X >= 0 AndAlso p.Y < Me.Size.Width AndAlso p.X < Me.Size.Height Then
            SyncLock Me.PLock
                If Not Powders.Any(Function(c) c.X = p.X AndAlso c.Y = p.Y) Then
                    If Not Added.Any(Function(c) c.X = p.X AndAlso c.Y = p.Y) Then Added.Enqueue(p)
                ElseIf Deleted.Any(Function(c) c.X = p.X AndAlso c.Y = p.Y) Then
                    If Not Added.Any(Function(c) c.X = p.X AndAlso c.Y = p.Y) Then Added.Enqueue(p)
                End If
            End SyncLock
        End If
    End Sub

    Public Sub Remove(x As Integer, y As Integer)
        SyncLock Me.PLock
            Dim powder As Powder = Powders.FirstOrDefault(Function(p) p.X = x AndAlso p.Y = y)
            If powder IsNot Nothing Then Deleted.Enqueue(powder)
        End SyncLock
    End Sub

    Public Sub ClearAll()
        SyncLock Me.PLock
            Powders.Clear()
        End SyncLock
    End Sub
End Class
