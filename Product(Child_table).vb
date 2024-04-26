Imports System.Data.OleDb
Imports System.Data
Imports System.Security.Cryptography
Imports System.Runtime.ConstrainedExecution
Public Class Form1
    Dim cn As New OleDbConnection("Data Source=XE;Password=Heer;User Id=system;Provider=OraOLEDB.Oracle")

    Dim adp As New OleDbDataAdapter("select * from Category", cn)
    Dim cmb As New OleDbCommandBuilder(adp)
    Dim ds As New DataSet

    Sub showgrid()
        Dim ds As New DataSet
        Dim adp As New OleDbDataAdapter("select * from Produts", cn)
        adp.Fill(ds)
        DataGridView1.DataSource = ds.Tables(0)
    End Sub


    Sub filldata()
        Dim adp As New OleDbDataAdapter("select * from Category", cn)

        Dim cmb As New OleDbCommandBuilder(adp)

        Dim ds As New DataSet
        adp.Fill(ds)
        cmbcat.DataSource = ds.Tables(0)
        cmbcat.DisplayMember = "category_name"
        cmbcat.ValueMember = "category_id"

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim tax As String
        If rby.Checked Then
            tax = "yes"
        Else
            tax = "no"
        End If

        Dim strreg As String
        strreg = ""
        If cbg.Checked Then
            strreg = strreg + cbg.Text + ","
        End If
        If cbm.Checked Then
            strreg = strreg + cbm.Text + ","
        End If
        If cbr.Checked Then
            strreg = strreg + cbr.Text + ","
        End If
        strreg = strreg.Substring(0, strreg.Length - 1)
        MsgBox("Insert into Produts(product_id,product_name,product_price,tax,category_id,region) values('" & t1.Text & "','" & t2.Text & "'," & t3.Text & ",'" & tax & "'," & cmbcat.SelectedValue & ",'" & strreg & "')")
        Dim cmd As New OleDbCommand("Insert into Produts(product_id,product_name,product_price,tax,category_id,region) values('" & t1.Text & "','" & t2.Text & "'," & t3.Text & ",'" & tax & "'," & cmbcat.SelectedValue & ",'" & strreg & "')", cn)

        cmd.ExecuteNonQuery()
        MsgBox("Inserted")
        showgrid()
        clear()
    End Sub


    Sub clear()
        t1.Clear()
        t2.Clear()
        t3.Clear()

        rby.Checked = False
        rbn.Checked = False
        cbg.Checked = False
        cbm.Checked = False
        cbr.Checked = False
        cmbcat.SelectedIndex = 0
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cn.Open()
        filldata()
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim id As Integer
        id = InputBox("Enter Product id")
        Dim cmd As New OleDbCommand("Select * From Produts where product_id=" & id, cn)
        Dim dr As OleDbDataReader
        dr = cmd.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            t1.Text = dr("product_id")
            t2.Text = dr("product_name")
            t3.Text = dr("product_price")

            If dr("tax") = "Yes" Then
                rby.Checked = True
            Else
                rbn.Checked = True
            End If
            cmbcat.SelectedValue = dr("category_id")
            cbg.Checked = False
            cbm.Checked = False
            cbr.Checked = False
            If (dr("region").ToString().Contains("Gujarat")) Then
                cbg.Checked = True
            End If

            If (dr("region").ToString().Contains("Maharashtra")) Then
                cbm.Checked = True
            End If

            If (dr("region").ToString().Contains("Gujarat")) Then
                cbr.Checked = True
            End If

            dr.Close()
        Else
            MsgBox("No Such Product")
            showgrid()
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim tax As String
        If rby.Checked Then
            tax = "Yes"
        Else
            tax = "No"
        End If
        Dim strreg As String
        strreg = ""
        If cbg.Checked Then
            strreg = strreg + cbg.Text + ","
        End If
        If cbm.Checked Then
            strreg = strreg + cbm.Text + ","
        End If
        If cbr.Checked Then
            strreg = strreg + cbr.Text + ","
        End If
        strreg = strreg.Substring(0, strreg.Length - 1)

        Dim cmd As New OleDbCommand("update Produts set product_name='" & t2.Text & "',product_price=" & t3.Text & ",tax='" & tax & "',category_id=" & cmbcat.SelectedValue & ",region='" & strreg & "' where product_id=" + t1.Text, cn)

        cmd.ExecuteNonQuery()
        MsgBox("Updated")
        clear()
        showgrid()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim ans As String
        ans = MsgBox("Sure to Delete ", MsgBoxStyle.YesNo)
        If ans = DialogResult.Yes Then
            Dim cmd As New OleDbCommand("Delete from Produts where product_id=" & t1.Text, cn)
            cmd.ExecuteNonQuery()
            MsgBox("Deleted")
            clear()
            showgrid()
        End If
    End Sub

End Class
