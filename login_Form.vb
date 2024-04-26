Imports System.Data.OleDb
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Public Class Form2

    Dim cn As New OleDbConnection("Data Source=XE;Password=Heer;User Id=system;Provider=OraOLEDB.Oracle")
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cn.Open()
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim cmd As New OleDbCommand("select * from logining where username='" & TextBox1.Text & "' and password = '" & TextBox2.Text & "'", cn)
        Dim dr As OleDbDataReader
        dr = cmd.ExecuteReader
        If dr.HasRows Then
            MsgBox("welcome")
            dr.Close()
            Me.Close()
        Else
            MsgBox("sorry")
        End If
        dr.Close()
    End Sub
End Class
