Imports Alchemy.Elements

Public Class frmMain
    Private Sandbox As Sandbox
    Private Delegate Sub SetFPS()
    Private FPSdelegate As SetFPS
    Private Current As Type
    Private MouseTimer As Timer
    Private MouseMode As MouseMode = MouseMode.Create

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.Current = GetType(Water)

        Me.Sandbox = New Sandbox
        AddHandler Me.Sandbox.Completed, AddressOf Me.Completed

        FPSdelegate = AddressOf Me.SetFpsText


        Me.MouseTimer = New Timer
        MouseTimer.Interval = 10
        AddHandler MouseTimer.Tick, AddressOf MouseTimer_Tick

        Me.Sandbox.Start()

    End Sub

    Private Sub MouseTimer_Tick(sender As Object, e As EventArgs)
        Select Case Me.MouseMode
            Case MouseMode.Create : Me.Create()
            Case MouseMode.Destroy : Me.Destroy()
        End Select
    End Sub

    Private Sub Renderer_MouseDown(sender As Object, e As MouseEventArgs) Handles Renderer.MouseDown
        Select Case e.Button
            Case MouseButtons.Left
                Me.Create()
                Me.MouseTimer.Start()

            Case MouseButtons.Right
                Me.Destroy()
                Me.MouseTimer.Start()

        End Select
    End Sub

    Private Sub Renderer_MouseUp(sender As Object, e As MouseEventArgs) Handles Renderer.MouseUp
        Me.MouseTimer.Stop()
    End Sub

    Private Sub Renderer_MouseMove(sender As Object, e As MouseEventArgs) Handles Renderer.MouseMove
        Dim xoffset As Double = Me.Renderer.Width / Me.Sandbox.Size.Width
        Dim yoffset As Double = Me.Renderer.Height / Me.Sandbox.Size.Height
        Me.Sandbox.Mouse = New Point(e.X / xoffset, e.Y / yoffset)
    End Sub

    Private Sub Completed(Output As Image)
        Me.Renderer.BackgroundImage = Output
        Me.lblFps.Invoke(FPSdelegate)
    End Sub

    Private Sub SetFpsText()
        Me.lblFps.Text = String.Format("Render: {0}ms Simulate: {1}ms Powders: {2} Delta: {3}", Me.Sandbox.RTime, Me.Sandbox.STime, Me.Sandbox.Powders.Count, Me.Sandbox.DeltaT)
    End Sub

    Private Sub Create()
        Dim square As List(Of Point) = Powder.Square(Me.Sandbox.Mouse, 1)
        For Each point As Point In square
            Me.Sandbox.Remove(point.X, point.Y)
            Dim powder As Powder = CType(Activator.CreateInstance(Me.Current, Me.Sandbox), Powder)
            powder.X = point.X
            powder.Y = point.Y
            Me.Sandbox.Add(powder)
        Next
    End Sub

    Private Sub Destroy()
        Me.Sandbox.Remove(Me.Sandbox.Mouse.X, Me.Sandbox.Mouse.Y)
    End Sub

    Private Sub R1_CheckedChanged(sender As Object, e As EventArgs) Handles R1.CheckedChanged
        If (R1.Checked) Then Me.Current = GetType(Water)
    End Sub
    Private Sub R2_CheckedChanged(sender As Object, e As EventArgs) Handles R2.CheckedChanged
        If (R2.Checked) Then Me.Current = GetType(Sand)
    End Sub
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Me.Sandbox.ClearAll()
    End Sub
End Class
