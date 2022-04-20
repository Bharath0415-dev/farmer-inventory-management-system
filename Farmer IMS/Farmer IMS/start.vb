Public Class start
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        ProgressBar1.Increment(1)
        If (ProgressBar1.Value = 100) Then
            Timer1.Stop()
            Dim form1 = New Form1
            form1.Show()
            Me.Hide()
        End If
    End Sub

    Private Sub start_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Enabled = True
        Timer1.Start()
        Timer1.Interval = 5
    End Sub
End Class