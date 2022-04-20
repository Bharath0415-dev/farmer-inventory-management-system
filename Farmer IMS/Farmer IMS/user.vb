Imports System.Data.SqlClient
Public Class user
    Dim con As New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\BHARATH KUMARA\Documents\inventorydb.mdf;Integrated Security=True;Connect Timeout=30")
    Public Sub populate()
        con.Open()
        Dim sql = "select * from Usertb"
        Dim adapter As SqlDataAdapter
        adapter = New SqlDataAdapter(sql, con)
        Dim builder As SqlCommandBuilder
        builder = New SqlCommandBuilder(adapter)
        Dim ds As DataSet
        ds = New DataSet
        adapter.Fill(ds)
        userdgv.DataSource = ds.Tables(0)
        con.Close()
    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If uid.Text = "" Or uname.Text = "" Or upassword.Text = "" Or uphoneno.Text = "" Then
            MsgBox("INCOMPLETE DATA")
        Else
            con.Open()
            Dim sql = "update Usertb set Uid = '" + uid.Text + "',Uname = " + uname.Text + ",Upassword=" + upassword.Text + " where Uphone =" + uphoneno.Text + ""
            Dim cmd As SqlCommand
            cmd = New SqlCommand(sql, con)
            cmd.ExecuteNonQuery()
            MsgBox(" ADMIN DATA UPDATED SUCCESSFULLY")
            con.Close()
            populate()
        End If
    End Sub


    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        uid.Text = ""
        uname.Text = ""
        upassword.Text = ""
        uphoneno.Text = ""
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim home = New home
        home.Show()
        Me.Hide()
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        Application.Exit()
    End Sub

    Private Sub user_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        populate()
    End Sub


End Class