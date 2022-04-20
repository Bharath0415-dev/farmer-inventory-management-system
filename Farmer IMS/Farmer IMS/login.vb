Imports System.Data.SqlClient

Public Class Form1
        Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
            If (CheckBox1.Checked = False) Then
                PassTB.UseSystemPasswordChar = True
            Else
                PassTB.UseSystemPasswordChar = False
            End If
        End Sub

        Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click
            UsernameTB.Text = ""
            PassTB.Text = ""
        End Sub

    Dim con As New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\BHARATH KUMARA\Documents\inventorydb.mdf;Integrated Security=True;Connect Timeout=30")

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If UsernameTB.Text = "" Then
            MsgBox("ENTER THE USER NAME")
        ElseIf PassTB.Text = "" Then
            MsgBox("ENTER THE PASSWORD")
        Else
            con.Open()
            Dim query = "select * from Usertb where Uname='" & UsernameTB.Text & "' and Upassword ='" & PassTB.Text & "'"
            Dim cmd As New SqlCommand
            cmd = New SqlCommand(query, con)
            Dim da As SqlDataAdapter = New SqlDataAdapter(cmd)
            Dim ds As DataSet = New DataSet()
            da.Fill(ds)
            Dim a As Integer
            a = ds.Tables(0).Rows.Count
            If a = 0 Then
                MsgBox("WRONG USERNAME OR PASSWORD..")
                con.Close()
            Else
                Dim home = New home
                home.Show()
                Me.Hide()
            End If

        End If
    End Sub

    Private Sub Label11_Click(sender As Object, e As EventArgs) Handles Label11.Click
        Application.Exit()
    End Sub
End Class
