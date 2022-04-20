Imports System.Data.SqlClient
Public Class order
    Dim con As New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\BHARATH KUMARA\Documents\inventorydb.mdf;Integrated Security=True;Connect Timeout=30")
    Private Sub Fillproduct()
        con.Open()
        Dim sql = "select * from Producttbl"
        Dim cmd As New SqlCommand(sql, con)
        Dim adapter As New SqlDataAdapter(cmd)
        Dim tbl As New DataTable()
        adapter.Fill(tbl)
        prodidCb.DataSource = tbl
        prodidCb.DisplayMember = "Prodid"
        prodidCb.ValueMember = "Prodid"
        con.Close()
    End Sub
    Private Sub Fillcustomer()
        con.Open()
        Dim sql = "select * from Customertbl"
        Dim cmd As New SqlCommand(sql, con)
        Dim adapter As New SqlDataAdapter(cmd)
        Dim tbl As New DataTable()
        adapter.Fill(tbl)
        custid.DataSource = tbl
        custid.DisplayMember = "Custid"
        custid.ValueMember = "Custid"
        con.Close()

    End Sub
    Private Sub fetchname()
        con.Open()
        Dim query = "select * from Customertbl where Custid =" & custid.SelectedValue.ToString() & ""
        Dim cmd As New SqlCommand(query, con)
        Dim dt As New DataTable
        Dim reader As SqlDataReader
        reader = cmd.ExecuteReader()
        While reader.Read
            custname.Text = reader(1).ToString
        End While
        con.Close()

    End Sub
    Dim prodname As String
    Dim prodprice As Integer
    Dim availqty As Integer
    Private Sub fetchdata()
        con.Open()
        Dim query = "select * from Producttbl where Prodid=" & prodidCb.SelectedValue.ToString() & ""
        Dim cmd As New SqlCommand(query, con)
        Dim dt As New DataTable
        Dim reader As SqlDataReader
        reader = cmd.ExecuteReader()
        While reader.Read
            'custname.Text = reader(2).ToString
            prodname = reader(1).ToString()
            prodprice = Convert.ToInt32(reader(3).ToString())
            availqty = Convert.ToInt32(reader(2).ToString())
            prodnameT.Text = prodname
        End While
        con.Close()

    End Sub



    Dim newqty
    Private Sub updateprod()
        newqty = availqty - Convert.ToInt32(quantity.Text)
        con.Open()
        Dim sql = "update Producttbl set Prodqty = " + quantity.Text + " where Prodid =" + prodidCb.SelectedItem.ToString + ""
        Dim cmd As SqlCommand
        cmd = New SqlCommand(sql, con)
        cmd.ExecuteNonQuery()
        con.Close()

    End Sub
    Dim Grtot = 0, i = 0, total = 0
    Public Sub populate()
        con.Open()
        Dim sql = "select * from Ordertbl"
        Dim adapter As SqlDataAdapter
        adapter = New SqlDataAdapter(sql, con)
        Dim builder As SqlCommandBuilder
        builder = New SqlCommandBuilder(adapter)
        Dim ds As DataSet
        ds = New DataSet
        adapter.Fill(ds)
        orderdgv.DataSource = ds.Tables(0)
        con.Close()
    End Sub
    Private Sub insertorder()
        Try
            con.Open()
            Dim query As String
            query = "insert into Ordertbl values('" + orderid.Text + "','" + custid.SelectedValue.ToString + "','" + custname.Text + "','" + amtlbl.Text + "')"
            Dim cmd As SqlCommand
            cmd = New SqlCommand(query, con)
            cmd.ExecuteNonQuery()
            MsgBox(" ORDER ADDED SUCCESSFULLY")
            con.Close()
            populate()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub PrintDocument1_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        e.Graphics.DrawString("****************************", New Font("Century Gothic", 24), Brushes.BlueViolet, 250, 40)
        e.Graphics.DrawString("************* YOUR ORDER ***************", New Font("Century Gothic", 20), Brushes.BlueViolet, 250, 60)
        Dim bm As New Bitmap(Me.billdgv.Width, Me.billdgv.Height)
        billdgv.DrawToBitmap(bm, New Rectangle(0, 0, Me.billdgv.Width, Me.billdgv.Height))
        e.Graphics.DrawImage(bm, 110, 90)
        insertorder()
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        Application.Exit()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim home = New home
        home.Show()
        Me.Hide()
    End Sub

    Private Sub prodidCb_SelectionChangeCommitted_1(sender As Object, e As EventArgs) Handles prodidCb.SelectionChangeCommitted
        fetchdata()
    End Sub

    Private Sub custid_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles custid.SelectionChangeCommitted
        fetchname()
    End Sub

    Private Sub order_Load_1(sender As Object, e As EventArgs) Handles MyBase.Load
        Fillproduct()
        Fillcustomer()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        PrintPreviewDialog1.Show()
    End Sub



    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If quantity.Text = "" Then
            MsgBox("ENTER THE PRODUCT QUANTITY")
        Else
            Dim rnum As Integer = billdgv.Rows.Add()
            i = i + 1
            total = prodprice * Convert.ToInt32(quantity.Text)
            billdgv.Rows.Item(rnum).Cells("Column1").Value = i
            billdgv.Rows.Item(rnum).Cells("Column2").Value = prodnameT.Text
            billdgv.Rows.Item(rnum).Cells("Column3").Value = prodprice
            billdgv.Rows.Item(rnum).Cells("Column4").Value = quantity.Text
            billdgv.Rows.Item(rnum).Cells("Column5").Value = total
            Grtot = Grtot + total
            amtlbl.Text = Grtot
        End If
    End Sub
End Class