Imports System.Data.SqlClient
Public Class category
    Dim con As New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\BHARATH KUMARA\Documents\inventorydb.mdf;Integrated Security=True;Connect Timeout=30")
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim home = New home
        home.Show()
        Me.Hide()

    End Sub
    Public Sub populate()
        con.Open()
        Dim sql = "select * from Categorytbl"
        Dim adapter As SqlDataAdapter
        adapter = New SqlDataAdapter(sql, con)
        Dim builder As SqlCommandBuilder
        builder = New SqlCommandBuilder(adapter)
        Dim ds As DataSet
        ds = New DataSet
        adapter.Fill(ds)
        catdgv.DataSource = ds.Tables(0)
        con.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            con.Open()
            Dim query As String
            query = "insert into Categorytbl values('" + catid.Text + "','" + catname.Text + "')"
            Dim cmd As SqlCommand
            cmd = New SqlCommand(query, con)
            cmd.ExecuteNonQuery()
            MsgBox(" CATEGORY ADDED SUCCESSFULLY")
            con.Close()
            populate()
        Catch ex As Exception
            MsgBox(ex.Message)
            con.Close()
        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If catid.Text = "" Then
            MsgBox("ENTER THE PRODUCT TO BE DELETED")
        Else
            con.Open()
            Dim query As String
            query = "delete from Categorytbl where Catid = " & catid.Text & ""
            Dim cmd As SqlCommand
            cmd = New SqlCommand(query, con)
            cmd.ExecuteNonQuery()
            MsgBox(" PRODUCT DELETED SUCCESSFULLY")
            con.Close()
            populate()
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If catid.Text = "" Or catname.Text = "" Then
            MsgBox("INCOMPLETE DATA")
        Else
            con.Open()
            Dim sql = "update Categorytbl set Catname = '" + catname.Text + "' where Catid = " + catid.Text + ""
            Dim cmd As SqlCommand
            cmd = New SqlCommand(sql, con)
            cmd.ExecuteNonQuery()
            MsgBox(" PRODUCT UPDATED SUCCESSFULLY")
            con.Close()
            populate()
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        catid.Text = ""
        catname.Text = ""
    End Sub

    Private Sub category_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        populate()
    End Sub
End Class