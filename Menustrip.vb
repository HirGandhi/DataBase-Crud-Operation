Public Class Form3
    Private Sub PRODUCTToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PRODUCTToolStripMenuItem.Click
        Form1.MdiParent = Me
        Form1.Show()
    End Sub

    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Form2.MdiParent = Me
        Form2.Show()
    End Sub

    Private Sub CATEGORYToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CATEGORYToolStripMenuItem.Click
        Category.MdiParent = Me
        Category.Show()
    End Sub
End Class
