Imports System.Data.SqlClient
Imports System.Text.RegularExpressions
Imports System.IO
Imports System.Drawing.Image
Public Class donor_page
    Dim messagesta As String
    Dim reader As SqlDataReader
    Dim sqlconn As New SqlConnection("Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\HP\Desktop\jaffer\new\online blood bank management\online blood bank management\bloodbank.mdf;Integrated Security=True;User Instance=True")
    Dim command As New SqlCommand
    Dim id1 As String = donor.id
    Dim status As String = donor.status
    Dim contact As String = donor.contact
    Dim username, name1, dob, gender, type, city, email, senderid, senderid2 As String

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Button6.Focus()
        GroupBox1.Visible = False
        GroupBox2.Visible = True

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        GroupBox1.Visible = True
        GroupBox2.Visible = False
        GroupBox3.Visible = False
    End Sub



    Private Sub donor_page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Button1.BackColor = System.Drawing.SystemColors.ActiveCaption
        Button5.BackColor = System.Drawing.SystemColors.ActiveCaption
        Button6.BackColor = System.Drawing.SystemColors.Control
        GroupBox2.Visible = True
        GroupBox1.Visible = False
        GroupBox3.Visible = False
        Panelsend.Visible = False
        Label14.Visible = False
        Button6.Focus()
        Name = donor.username
        Dim dob As String = donor.dob.Substring(0, 10)
        Dim gender As String = donor.gender

        Dim type As String = donor.type
        Dim city As String = donor.city
        Dim email As String = donor.email
        Label1.Text = Name
        Label3.Text = "User id : " & id1 & vbNewLine & vbNewLine & "Date of Birth :" & dob & vbNewLine & "gender : " & gender & vbNewLine & "contact number : " & contact & vbNewLine & "Blood Type : " & type & vbNewLine & "city : " & city & vbNewLine & "email : " & email
        If status = "Available" Then
            Label9.ForeColor = Color.Green
        Else
            Label9.ForeColor = Color.Red
        End If
        Label9.Text = status

        Dim str As String = "select image from donor where userid='" & id1 & "'"
        command = New SqlCommand(str, sqlconn)
        sqlconn.Open()
        Dim table As New DataTable
        Dim adapter As New SqlDataAdapter(command)

        Try
            adapter.Fill(table)
            Dim img() As Byte
            img = table.Rows(0)(0)
            Dim imgMemoryStream As New MemoryStream(img)
            PictureBox1.Image = FromStream(imgMemoryStream)
            sqlconn.Close()
        Catch ex As Exception
            sqlconn.Close()
        End Try
        sqlconn.Close()
        sqlconn.Open()
        command = New SqlCommand("select * from messages where status='new' ", sqlconn)
        Dim tabl As New DataTable
        Dim ada As New SqlDataAdapter(command)
        ada.Fill(tabl)
        If messagesta = 1 Then
            command = New SqlCommand("update messages set status='old' ", sqlconn)
            command.ExecuteNonQuery()
            messagesta = 0
        End If
        sqlconn.Close()
        If tabl.Rows.Count > 0 Then
            Label14.Visible = True
        End If
    End Sub

    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton1.CheckedChanged
        sqlconn.Open()
        Dim str As String = "update donor set status='Available' where userid='" & id1 & "'"
        command = New SqlCommand(str, sqlconn)
        command.ExecuteNonQuery()
        sqlconn.Close()
        sqlconn.Open()
        str = "select * from donor where userid= '" & id1 & "'"
        command = New SqlCommand(str, sqlconn)
        reader = command.ExecuteReader
        Dim count As Integer = 0
        While (reader.Read)
            status = reader("status").ToString
        End While
        sqlconn.Close()
        donor_page_Load(e, e)
    End Sub

    Private Sub RadioButton2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton2.CheckedChanged

        sqlconn.Open()
        Dim str As String = "update donor set status='Not Available' where userid='" & id1 & "'"
        command = New SqlCommand(str, sqlconn)
        command.ExecuteNonQuery()
        sqlconn.Close()
        sqlconn.Open()
        str = "select * from donor where userid= '" & id1 & "'"
        command = New SqlCommand(str, sqlconn)
        reader = command.ExecuteReader
        While (reader.Read)
            status = reader("status").ToString
        End While
        sqlconn.Close()
        donor_page_Load(e, e)

    End Sub


    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If TextBox1.Text = "" And TextBox2.Text = "" Then
            MessageBox.Show("enter passwords")
        ElseIf TextBox1.Text <> TextBox2.Text Then
            MessageBox.Show("passwords not match")
        Else
            sqlconn.Open()
            Dim str As String = "update donor set password= '" & TextBox1.Text().Trim() & "' where userid='" & id1 & "'"
            command = New SqlCommand(str, sqlconn)
            command.ExecuteNonQuery()
            sqlconn.Close()
            MessageBox.Show("password changed your new password is: " & TextBox1.Text)

        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim passwords As String

        sqlconn.Open()
        Dim str As String = "select password from donor where userid='" & id1 & "'"
        command = New SqlCommand(str, sqlconn)
        reader = command.ExecuteReader

        Dim count As Integer = 0
        While (reader.Read)
            count = count + 1
            passwords = reader("password").ToString
        End While
        sqlconn.Close()
        If passwords = TextBox3.Text.Trim() Then
            sqlconn.Open()
            str = "delete  from donor where userid= '" & id1 & "'"
            command = New SqlCommand(str, sqlconn)
            command.ExecuteNonQuery()
            MessageBox.Show("your account deleted" & count)
            Me.Close()
        Else
            MessageBox.Show("password is not correct")
        End If
        sqlconn.Close()

    End Sub



    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        If TextBox4.Text = "" Then
            MessageBox.Show("enter number")
        ElseIf Regex.IsMatch(TextBox4.Text, "^[0-9,\s]+$") Then
            sqlconn.Open()
            Dim str As String = "update donor set contact= '" & TextBox4.Text().Trim() & "' where userid='" & id1 & "'"
            command = New SqlCommand(str, sqlconn)
            command.ExecuteNonQuery()
            sqlconn.Close()
            MessageBox.Show("Contact changed your new contact is: " & TextBox4.Text)
            contact = TextBox4.Text.Trim()
            donor_page_Load(e, e)

        End If
    End Sub



    Private Sub Button5_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Enter
        GroupBox1.Visible = False
        GroupBox2.Visible = False
        GroupBox3.Visible = True
        Button5.BackColor = System.Drawing.SystemColors.Control
        Button6.BackColor = System.Drawing.SystemColors.ActiveCaption
        Button1.BackColor = System.Drawing.SystemColors.ActiveCaption
        Button5_Click(e, e)
    End Sub
    Private Sub Button1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Enter
        Button1.BackColor = System.Drawing.SystemColors.Control
        Button5.BackColor = System.Drawing.SystemColors.ActiveCaption
        Button6.BackColor = System.Drawing.SystemColors.ActiveCaption
        GroupBox2.Visible = False
        GroupBox1.Visible = True
        GroupBox3.Visible = False
    End Sub


    Private Sub Button6_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        GroupBox2.Visible = True
        GroupBox1.Visible = False
        GroupBox3.Visible = False

    End Sub

    Private Sub Button6_Enter_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Enter
        Button6.BackColor = System.Drawing.SystemColors.Control
        Button1.BackColor = System.Drawing.SystemColors.ActiveCaption
        Button5.BackColor = System.Drawing.SystemColors.ActiveCaption
        GroupBox2.Visible = True
        GroupBox1.Visible = False
        GroupBox3.Visible = False
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        GroupBox1.Visible = False
        GroupBox2.Visible = False
        GroupBox3.Visible = True
        messagesta = 1
        FlowLayoutPanel2.Controls.Clear()
        sqlconn.Open()
        command = New SqlCommand("select * from messages where userid= '" & id1 & "' order by id desc", sqlconn)
        reader = command.ExecuteReader()

        While (reader.Read)
            Dim mesg, name, line As New Label
            mesg.AutoSize = True
            senderid = reader("senderid")
            mesg.Text = reader("message")
            mesg.Font = New Font("Comic Sans MS", 9, FontStyle.Bold)
            name.Text = reader("name")
            name1 = reader("name")
            Dim butt As New Button
            AddHandler butt.Click, AddressOf clickme
            butt.Font = New Font("Comic Sans MS", 9)
            butt.Text = "replay"
            butt.Tag = senderid
            butt.AutoSize = True
            name.AutoSize = True
            mesg.ForeColor = Color.Blue

            FlowLayoutPanel2.Controls.Add(name)
            FlowLayoutPanel2.Controls.Add(mesg)
            FlowLayoutPanel2.Controls.Add(butt)
            line.AutoSize = True
            line.Text = "_______________________________________________"
            FlowLayoutPanel2.Controls.Add(line)


        End While
        sqlconn.Close()

    End Sub

    Private Sub clickme(ByVal sender As Object, ByVal e As EventArgs)
        Panelsend.Visible = False
        Dim btn As Button
        btn = CType(sender, Button)
        senderid2 = btn.Tag
        Panelsend.Visible = True
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click

        sqlconn.Open()

        Dim Str As String = "insert into messages values('" & senderid2 & "','" & id1 & "','hello this is from : " & Name & " " & vbNewLine & " " & TextBox5.Text.Trim() & "','new','" & name1 & "') "

        command = New SqlCommand(Str, sqlconn)
        command.ExecuteNonQuery()

        sqlconn.Close()
        Panelsend.Visible = False
    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        donor_page_Load(e, e)

    End Sub
End Class