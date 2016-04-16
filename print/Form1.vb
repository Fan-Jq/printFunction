Imports System.Drawing.Drawing2D
Public Class Form1
    Dim X_LeftTop As Single, Y_LeftTop As Single, X_RightDown As Single, Y_RightDown As Single
    Dim Last_X As Single, Last_Y As Single, x2, y2
    Dim s As Integer
    Dim px1 As Integer, py1 As Integer
    Dim g1 As Graphics
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        X_LeftTop = -20
        Y_LeftTop = 20
        X_RightDown = 20
        Y_RightDown = -20
        g1 = Me.CreateGraphics()
        Scale(X_LeftTop, Y_LeftTop, X_RightDown, Y_RightDown, g1)
    End Sub

    Private Sub Form1_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
        Last_X = e.X
        Last_Y = e.Y
    End Sub
    Private Sub Form1_MouseUp(sender As Object, e As MouseEventArgs) Handles Me.MouseUp
        g1 = Me.CreateGraphics()
        X_LeftTop += (Last_X - e.X) / (Me.Width / (Math.Abs(X_RightDown - X_LeftTop)))
        Y_LeftTop += -(Last_Y - e.Y) / (Me.Height / (Math.Abs(Y_LeftTop - Y_RightDown)))
        X_RightDown += (Last_X - e.X) / (Me.Width / (Math.Abs(X_RightDown - X_LeftTop)))
        Y_RightDown += -(Last_Y - e.Y) / (Me.Width / (Math.Abs(Y_LeftTop - Y_RightDown)))
            Scale(X_LeftTop, Y_LeftTop, X_RightDown, Y_RightDown, g1)
            print(g1)
    End Sub

    Private Sub Form1_MouseWheel(sender As Object, e As MouseEventArgs) Handles Me.MouseWheel
        g1 = Me.CreateGraphics()
        If e.Delta > 0 Then
            X_LeftTop *= 0.9
            Y_LeftTop *= 0.9
            X_RightDown *= 0.9
            Y_RightDown *= 0.9
        ElseIf e.Delta < 0 Then
            X_LeftTop *= 1.1
            Y_LeftTop *= 1.1
            X_RightDown *= 1.1
            Y_RightDown *= 1.1
        End If
        Scale(X_LeftTop, Y_LeftTop, X_RightDown, Y_RightDown, g1)
        print(g1)
    End Sub
    Private Sub Form1_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        print(g1)
    End Sub
    Public Function func1(ByRef x As Single)
        Dim y As Single
        y = Math.Sqrt(1 + 1 / (x ^ 2)) * 8 * x / (x ^ 2 + 4)
        Return y
    End Function
    Public Function func(ByRef x As Single)
        Dim y As Single
        y = Math.Sqrt(1 + x ^ 2) * 8 * x / (1 + 4 * x ^ 2)
        Return y
    End Function
    Public Sub print(ByRef g As Graphics)
        Dim myPen As New Pen(Color.Black, 0.01)
        g.Clear(Color.White)
        g.DrawLine(myPen, X_LeftTop, 0, X_RightDown, 0)
        g.DrawLine(myPen, 0, Y_LeftTop, 0, Y_RightDown)
        For x As Integer = X_LeftTop To X_RightDown Step 1
            Dim x1 As Single = x
            Dim a As Single = 0.05
            g.DrawLine(myPen, x1, -a, x1, a)
        Next
        For y As Integer = Y_RightDown To Y_LeftTop Step 1
            Dim y1 As Single = y
            Dim a As Single = 0.05
            g.DrawLine(myPen, -a, y1, a, y1)
        Next
        For x As Single = X_LeftTop To X_RightDown Step 0.1
            Dim y As Single = func(x)
            Dim x2 As Single = x + 0.1
            Dim y2 As Single = func(x2)
            g.DrawLine(myPen, x, y, x2, y2)
        Next
    End Sub
    Private Sub Form1_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged
        g1 = Me.CreateGraphics()
        g1.Clear(Color.White)
        print(g1)
    End Sub
    Private Sub Scale(ByRef X_LeftTop As Single, ByRef Y_LeftTop As Single, ByRef X_RightDown As Single, ByRef Y_RightDown As Single, ByRef g As Graphics)
        g.ScaleTransform(Me.Width / Math.Abs(X_RightDown - X_LeftTop), -(Me.Height / Math.Abs(Y_LeftTop - Y_RightDown)))
        g.TranslateTransform(-X_LeftTop, -Y_LeftTop)
    End Sub
End Class