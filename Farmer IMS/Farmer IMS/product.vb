Imports System.Data.SqlClient
Public Class product
    Dim con As New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\BHARATH KUMARA\Documents\inventorydb.mdf;Integrated Security=True;Connect Timeout=30")
    Public Sub populate()
        con.Open()
        Dim sql = "select * from Producttbl"
        Dim adapter As SqlDataAdapter
        adapter = New SqlDataAdapter(sql, con)
        Dim builder As SqlCommandBuilder
        builder = New SqlCommandBuilder(adapter)
        Dim ds As DataSet
        ds = New DataSet
        adapter.Fill(ds)
        productdgv.DataSource = ds.Tables(0)
        con.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            con.Open()
            Dim query As String
            query = "insert into Producttbl values('" + prodid.Text + "','" + prodname.Text + "','" + prodqty.Text + "','" + prodprice.Text + "','" + prodcat.SelectedItem.ToString() + "')"
            Dim cmd As SqlCommand
            cmd = New SqlCommand(query, con)
            cmd.ExecuteNonQuery()
            MsgBox(" PRODUCT ADDED SUCCESSFULLY")
            con.Close()
            populate()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub fillcategory()
        con.Open()
        Dim sql = "select * from Categorytbl"
        Dim cmd As New SqlCommand(sql, con)
        Dim adapter As New SqlDataAdapter(cmd)
        Dim tbl As New DataTable()
        adapter.Fill(tbl)
        prodcat.DataSource = tbl
        prodcat.DisplayMember = "Catname"
        prodcat.ValueMember = "Catname"
        con.Close()

    End Sub

    Private Sub product_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        populate()
        fillcategory()
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        Application.Exit()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If prodid.Text = "" Then
            MsgBox("ENTER THE PRODUCT TO BE DELETED")
        Else
            con.Open()
            Dim query As String
            query = "delete from Producttbl where Prodid = " & prodid.Text & ""
            Dim cmd As SqlCommand
            cmd = New SqlCommand(query, con)
            cmd.ExecuteNonQuery()
            MsgBox(" PRODUCT DELETED SUCCESSFULLY")
            con.Close()
            populate()
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        prodid.Text = ""
        prodname.Text = ""
        prodqty.Text = ""
        prodprice.Text = ""
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If prodid.Text = "" Or prodname.Text = "" Or prodqty.Text = "" Or prodprice.Text = "" Then
            MsgBox("INCOMPLETE DATA")
        Else
            con.Open()
            Dim sql = "update Producttbl set Prodname = '" + prodname.Text + "',Prodqty = " + prodqty.Text + ",Prodprice=" + prodprice.Text + ",Prodcat ='" + prodcat.SelectedItem.ToString() + "' where Prodid =" + prodid.Text + ""
            Dim cmd As SqlCommand
            cmd = New SqlCommand(sql, con)
            cmd.ExecuteNonQuery()
            MsgBox(" PRODUCT UPDATED SUCCESSFULLY")
            con.Close()
            populate()
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim home = New home
        home.Show()
        Me.Hide()
    End Sub


End Class