Imports System.Data.SqlClient
Public Class customer
    Dim con As New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\BHARATH KUMARA\Documents\inventorydb.mdf;Integrated Security=True;Connect Timeout=30")
    Public Sub populate()
        con.Open()
        Dim sql = "select * from Customertbl"
        Dim adapter As SqlDataAdapter
        adapter = New SqlDataAdapter(sql, con)
        Dim builder As SqlCommandBuilder
        builder = New SqlCommandBuilder(adapter)
        Dim ds As DataSet
        ds = New DataSet
        adapter.Fill(ds)
        custdgv.DataSource = ds.Tables(0)
        con.Close()
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            con.Open()
            Dim query As String
            query = "insert into Customertbl values('" + custid.Text + "','" + custname.Text + "','" + custphone.Text + "')"
            Dim cmd As SqlCommand
            cmd = New SqlCommand(query, con)
            cmd.ExecuteNonQuery()
            MsgBox(" CUSTOMER ADDED SUCCESSFULLY")
            con.Close()
            populate()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If custid.Text = "" Then
            MsgBox("ENTER THE CUSTOMER TO BE DELETED")
        Else
            con.Open()
            Dim query As String
            query = "delete from Customertbl where Custid = " & custid.Text & ""
            Dim cmd As SqlCommand
            cmd = New SqlCommand(query, con)
            cmd.ExecuteNonQuery()
            MsgBox(" CUSTOMER DELETED SUCCESSFULLY")
            con.Close()
            populate()
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If custid.Text = "" Or custname.Text = "" Or custphone.Text = "" Then
            MsgBox("INCOMPLETE DATA")
        Else
            con.Open()
            Dim sql = "update Customertbl set Custname = '" + custname.Text + "',Custphone = " + custphone.Text + " where Custid =" + custid.Text + ""
            Dim cmd As SqlCommand
            cmd = New SqlCommand(sql, con)
            cmd.ExecuteNonQuery()
            MsgBox(" INFORMATION UPDATED SUCCESSFULLY")
            con.Close()
            populate()
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        custid.Text = ""
        custname.Text = ""
        custphone.Text = ""
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim home = New home
        home.Show()
        Me.Hide()
    End Sub
End Class