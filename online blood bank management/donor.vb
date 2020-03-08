Imports System.Data.SqlClient
Imports System.Text.RegularExpressions
Imports System.IO
Imports System.Drawing.Image
Public Class donor
    Dim query As String
    Dim myImage As Image
    Dim imgMemoryStream As IO.MemoryStream = New IO.MemoryStream()
    Public username, id, dob, gender, contact, type, city, email, status As String

    Dim reader As SqlDataReader
    Dim command As New SqlCommand
    Dim sqlconn2 As New SqlConnection("Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\HP\Desktop\jaffer\new\online blood bank management\online blood bank management\bloodbank.mdf;Integrated Security=True;User Instance=True")
    Dim sqlconn As New SqlConnection("Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\HP\Desktop\jaffer\new\online blood bank management\online blood bank management\bloodbank.mdf;Integrated Security=True;User Instance=True")


    Private Sub donor_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        TextBox2.UseSystemPasswordChar = True
        TextBox11.UseSystemPasswordChar = False

        TextBox10.UseSystemPasswordChar = True
        TextBox11.UseSystemPasswordChar = True
        Me.Width = 422
        Me.Height = 322
        GroupBox22.Visible = False
        PictureBox2.Image = My.Resources.ResourceManager.GetObject("user")

    End Sub
    Private Sub donor_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        start.Show()
    End Sub


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        
        sqlconn.Open()
        query = "select * from donor where userid= '" & TextBox1.Text & "' and password='" & TextBox2.Text & "' "
        command = New SqlCommand(query, sqlconn)
        reader = command.ExecuteReader
        Dim count As Integer = 0
        While (reader.Read)
            count = count + 1
            username = reader("name").ToString
            id = reader("userid").ToString
            dob = reader("dob").ToString
            gender = reader("gender").ToString
            contact = reader("contact").ToString
            type = reader("type").ToString
            city = reader("city").ToString
            email = reader("email").ToString
            status = reader("status").ToString
        End While
        If count = 1 Then

            donor_page.Show()
        Else
            MessageBox.Show("invalid user name or password")
        End If
        sqlconn.Close()
    End Sub

    Private Sub Label3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        GroupBox11.Visible = False
        GroupBox22.Visible = True
        Me.Height = 480
        Me.Width = 785
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.Close()
        start.Show()
    End Sub

    Private Sub LinkLabel2_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            PictureBox2.Image = Image.FromFile(OpenFileDialog1.FileName)
            PictureBox2.Image.Save(imgMemoryStream, PictureBox2.Image.RawFormat)
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim myage As Integer = DateTime.Today.Year - DateTimePicker1.Value.Year
        PictureBox2.Image.Save(imgMemoryStream, PictureBox2.Image.RawFormat)
        If TextBox3.Text = "" Or TextBox4.Text = "" Or TextBox5.Text = "" Or TextBox6.Text = "" Or TextBox10.Text = "" Or ComboBox1.Text = "" Then
            MessageBox.Show("fill the required fields")
            Dim txt As Label
            For i = 15 To 20
                txt = CType(Me.GroupBox22.Controls("Label" & i), Label)
                txt.Visible = True
            Next
        ElseIf myage < 21 Then
            MessageBox.Show("you are minor")
        ElseIf TextBox10.Text <> TextBox11.Text Then
            MessageBox.Show("password not matching")
        ElseIf Regex.IsMatch(TextBox3.Text, "^[A-Za-z0-9\s]+$") And Regex.IsMatch(TextBox4.Text, "^[A-Za-z0-9]+$") Then
            Try
                Dim btype As String = ComboBox1.SelectedItem

                Dim gender As String
                If RadioButton2.Checked = True Then
                    gender = "female"
                Else
                    gender = "male"
                End If


                Dim sqlquery As New SqlCommand("INSERT INTO donor VALUES('" & TextBox3.Text.Trim() & "','" & TextBox4.Text.Trim() & "', @dob,'" & gender & "','" & TextBox5.Text.Trim() & "' ,'" & btype & "', '" & TextBox6.Text.Trim() & "','" & TextBox7.Text.Trim() & "','" & TextBox8.Text.Trim() & "','" & TextBox9.Text.Trim() & "','" & TextBox10.Text.Trim() & "',@image,'Available','" & myage & "')", sqlconn)
                sqlconn.Open()
                sqlquery.Parameters.Add("@image", SqlDbType.Image).Value = imgMemoryStream.ToArray()
                sqlquery.Parameters.Add("@dob", SqlDbType.Date).Value = DateTimePicker1.Value

                sqlquery.ExecuteNonQuery()
                sqlconn.Close()
                MessageBox.Show("your account has been created succes fully" & vbNewLine & "user id:" & TextBox4.Text & "password:" & TextBox10.Text)
                Me.Width = 422
                Me.Height = 322
                GroupBox22.Visible = False
                GroupBox11.Visible = True
            Catch ex As Exception
                sqlconn.Close()
                MessageBox.Show("error!" & vbNewLine & "possible cases:user name already exists or invalid data you have entered")
            End Try


        Else
            MessageBox.Show("you entered a invalid uesr id or institute name (special symbols are not allowed)")

        End If

    End Sub

   
    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If TextBox10.UseSystemPasswordChar = True And TextBox11.UseSystemPasswordChar = True Then
            TextBox10.UseSystemPasswordChar = False
            TextBox11.UseSystemPasswordChar = False
        Else
            TextBox10.UseSystemPasswordChar = True
            TextBox11.UseSystemPasswordChar = True

        End If
    End Sub
End Class