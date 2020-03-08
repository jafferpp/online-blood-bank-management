Imports System.Data.SqlClient
Imports System.Text.RegularExpressions
Imports System.IO
Imports System.Drawing.Image
Public Class Institutelogin
    Inherits System.Windows.Forms.Form
    Dim sqlconn As New SqlConnection("Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\HP\Desktop\jaffer\new\online blood bank management\online blood bank management\bloodbank.mdf;Integrated Security=True;User Instance=True")
    Dim sqlconn2 As New SqlConnection("Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\HP\Desktop\jaffer\new\online blood bank management\online blood bank management\bloodbank.mdf;Integrated Security=True;User Instance=True")
    Dim myImage As Image
    Dim imgMemoryStream As IO.MemoryStream = New IO.MemoryStream()
    Dim reader As SqlDataReader
    Dim command As New SqlCommand
    Dim query As String
    Public institutename, id, city, address, contact, email As String

    Private Sub Institutelogin_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        start.Show()
    End Sub

    Private Sub Institutelogin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        PictureBox2.Image = My.Resources.ResourceManager.GetObject("678086-institution-512")
        Me.Width = 455
        Me.Height = 325
        TextBox10.UseSystemPasswordChar = True
        TextBox11.UseSystemPasswordChar = True
        TextBox2.UseSystemPasswordChar = True
        GroupBox2.Visible = False

    End Sub


    Private Sub LinkLabel1_LinkClicked_1(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Me.Width = 908
        Me.Height = 440
        GroupBox1.Visible = False
        GroupBox2.Visible = True
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        GroupBox2.Visible = False
        GroupBox1.Visible = True
        Me.Width = 455
        Me.Height = 325
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        sqlconn.Open()
        query = "select * from register where userid= '" & TextBox1.Text & "' and password='" & TextBox2.Text & "' "
        command = New SqlCommand(query, sqlconn)
        reader = command.ExecuteReader
        Dim count As Integer = 0
        While (reader.Read)
            count = count + 1
            institutename = reader("institutename").ToString
            id = reader("userid").ToString
            city = reader("city").ToString
            address = reader("address").ToString
            email = reader("email").ToString
            contact = reader("contact").ToString

        End While
        If count = 1 Then
            institutepage.Show()
            Me.Hide()
        Else
            MessageBox.Show("invalid user name or password")
        End If
        sqlconn.Close()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        start.Show()
        Me.Close()

    End Sub


    Private Sub LinkLabel2_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked

        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            PictureBox2.Image = Image.FromFile(OpenFileDialog1.FileName)
            Labelimage.Text = OpenFileDialog1.FileName.Substring(0, 15)
            PictureBox2.Image.Save(imgMemoryStream, PictureBox2.Image.RawFormat)
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        PictureBox2.Image.Save(imgMemoryStream, PictureBox2.Image.RawFormat)
        If TextBox3.Text = "" Or TextBox4.Text = "" Or TextBox12.Text = "" Or TextBox6.Text = "" Or TextBox9.Text = "" Or TextBox10.Text = "" Then
            MessageBox.Show("fill the required fields")
            Dim txt As Label
            For i = 14 To 19
                txt = CType(Me.GroupBox2.Controls("Label" & i), Label)
                txt.Visible = True
            Next
        ElseIf TextBox10.Text <> TextBox11.Text Then
            MessageBox.Show("password not matching")
        ElseIf Regex.IsMatch(TextBox3.Text, "^[A-Za-z0-9\s]+$") And Regex.IsMatch(TextBox4.Text, "^[A-Za-z0-9]+$") And Regex.IsMatch(TextBox12.Text, "^[0-9\s,]+$") Then

            Try
                Dim sqlquery As New SqlCommand("INSERT INTO register VALUES('" & TextBox3.Text.Trim() & "','" & TextBox4.Text.Trim() & "','" & TextBox5.Text.Trim() & "','" & TextBox12.Text.Trim() & "' ,'" & TextBox6.Text.Trim() & "','" & TextBox7.Text.Trim() & "','" & TextBox8.Text.Trim() & "','" & TextBox9.Text.Trim() & "','" & TextBox10.Text.Trim() & "',@image)", sqlconn)
                sqlconn.Open()
                sqlquery.Parameters.Add("@image", SqlDbType.Image).Value = imgMemoryStream.ToArray()

                sqlquery.ExecuteNonQuery()
                sqlconn.Close()
                MessageBox.Show("your account has been created" & vbNewLine & "user id= " & TextBox4.Text & vbNewLine & "Password= " & TextBox10.Text)
                Me.Width = 455
                Me.Height = 325
                GroupBox2.Visible = False
                GroupBox1.Visible = True
            Catch ex As Exception
                sqlconn.Close()
                MessageBox.Show("error!" & vbNewLine & "possible cases:user name already exists or invalid data you have entered")
            End Try
            Try

                Dim sqlquery2 As New SqlCommand("insert into blood values('" & TextBox4.Text & "', '0','0','0','0','0','0','0','0')", sqlconn2)
                sqlconn2.Open()
                sqlquery2.ExecuteNonQuery()
                sqlconn2.Close()
            Catch ex As Exception
                sqlconn2.Close()
                MessageBox.Show(ex.Message)
            End Try

        Else
            MessageBox.Show("you entered a invalid uesr id or institute name or contact number (special symbols are not allowed)")

        End If

    End Sub


    Private Sub TextBox5_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox9.TextChanged, TextBox6.TextChanged, TextBox10.TextChanged, TextBox5.TextChanged, TextBox4.TextChanged, TextBox3.TextChanged
        Dim txt As Label
        For i = 14 To 19
            txt = CType(Me.GroupBox2.Controls("Label" & i), Label)
            txt.Visible = False
        Next
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            TextBox10.UseSystemPasswordChar = False
            TextBox11.UseSystemPasswordChar = False
        Else
            TextBox10.UseSystemPasswordChar = True
            TextBox11.UseSystemPasswordChar = True

        End If
    End Sub




End Class