Imports System.Data.OleDb
Public Class Category

    Dim cn As New OleDbConnection("Data Source=XE;Password=Heer;User Id=system;Provider=OraOLEDB.Oracle")
    Private Sub Category_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cn.Open()
        showgrid()
    End Sub
    Sub showgrid()
        Dim ds As New DataSet
        Dim adp As New OleDbDataAdapter("select * from Category", cn)
        adp.Fill(ds)
        DataGridView1.DataSource = ds.Tables(0)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim cmd As New OleDbCommand("Insert into Category values(" & t1.Text & ",'" & t2.Text & "')", cn)
        cmd.ExecuteNonQuery()
        MsgBox("Inserted")
        showgrid()
        clear()
    End Sub
    Sub clear()
        t1.Clear()
        t2.Clear()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim id As Integer
        id = InputBox("Enter the category id")
        Dim cmd As New OleDbCommand("select * from Category where Category_id=" & id, cn)
        Dim dr As OleDbDataReader
        dr = cmd.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            t1.Text = dr("Category_id")
            t2.Text = dr("Category_name")
        Else
            MsgBox("No Such Product")
            showgrid()
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim cmd As New OleDbCommand("update Category set Category_name='" & t2.Text & "' where Category_id=" + t1.Text, cn)
        cmd.ExecuteNonQuery()
        MsgBox("updated")
        showgrid()
        clear()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim ans As String
        ans = MsgBox("sure to Delete", MsgBoxStyle.YesNo)
        If ans = DialogResult.Yes Then
            Dim cmd As New OleDbCommand("Delete from Category where Category_id=" & t1.Text, cn)
            cmd.ExecuteNonQuery()
            MsgBox("deleted")
            showgrid()
            clear()
        End If
    End Sub
End Class
