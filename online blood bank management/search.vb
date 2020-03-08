Imports System.Data.SqlClient
Imports System.Text.RegularExpressions
Imports System.IO
Imports System.Drawing.Image
Imports System.Windows.Forms.Padding

Public Class search
    Dim name, contact, type, city, status, age As String

    Dim reader As SqlDataReader
    Dim img() As Byte
    Dim image As Image

    Dim sqlconn As New SqlConnection("Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\HP\Desktop\jaffer\new\online blood bank management\online blood bank management\bloodbank.mdf;Integrated Security=True;User Instance=True")



    Private Sub search_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        start.Show()

    End Sub


    Private Sub search_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim command As New SqlCommand("select distinct city from donor ", sqlconn)
        sqlconn.Open()
        reader = command.ExecuteReader()
        While (reader.Read)
            ComboBox1.AutoCompleteCustomSource.Add(reader("city"))
        End While
        sqlconn.Close()
    End Sub


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        FlowLayoutPanel1.Controls.Clear()
        Dim command As New SqlCommand("select * from donor ", sqlconn)
        If ComboBox1.Text <> "" And ComboBox2.Text <> "" Then
            command = New SqlCommand("select * from donor where type = '" & ComboBox2.SelectedItem & "' and city = '" & ComboBox1.Text & "' order by status ", sqlconn)
            sqlconn.Open()
            reader = command.ExecuteReader()
            Dim count As Integer
            While (reader.Read)
                count = count + 1
                name = reader("name")
                age = reader("age")
                contact = reader("contact")
                type = reader("type")
                city = reader("city")
                status = reader("status")
                img = DirectCast(reader("image"), Byte())
                Dim imgMemoryStream As New MemoryStream(img, 0, img.Length)
                imgMemoryStream.Write(img, 0, img.Length)
                image = image.FromStream(imgMemoryStream, True)


                Dim pic As New PictureBox
                pic.Width = 70
                pic.Height = 80
                pic.SizeMode = PictureBoxSizeMode.StretchImage
                pic.Image = My.Resources.ResourceManager.GetObject("user")
                pic.Image = image
                Dim lab, lab2, lab3, lab4, __, lab5 As New Label
                lab.AutoSize = True
                lab2.AutoSize = True
                lab3.AutoSize = True
                lab4.AutoSize = True
                lab5.AutoSize = True
                lab5.Font = New Font("Comic Sans MS", 12, FontStyle.Bold)
                If status = "Available" Then
                    lab5.ForeColor = Color.Green
                    lab5.Text = status
                Else
                    lab5.ForeColor = Color.Red
                    lab5.Text = status
                End If
                __.Text = " "
                lab.Font = New Font("Comic Sans MS", 12, FontStyle.Bold)
                lab.Text = name & vbNewLine
                lab2.Text = "Age : " & age
                lab3.Text = "Contact : " & contact



                lab4.Font = New Font("Franklin Gothic", 8.5)

                lab4.Text = "Type : " & type
                Dim flo As New FlowLayoutPanel
                flo.BorderStyle = BorderStyle.FixedSingle
                flo.FlowDirection = FlowDirection.LeftToRight
                flo.Width = 300
                flo.Height = 165
                Dim flow_ima As New FlowLayoutPanel
                flow_ima.Width = 80
                flow_ima.Height = 90
                flow_ima.Controls.Add(pic)
                Dim flow_txt As New FlowLayoutPanel

                flow_txt.Width = 200
                flow_txt.Height = 150
                flow_txt.FlowDirection = FlowDirection.TopDown


                flow_txt.Controls.Add(lab)
                flow_txt.Controls.Add(__)
                flow_txt.Controls.Add(lab2)
                flow_txt.Controls.Add(lab3)
                flow_txt.Controls.Add(lab4)
                flow_txt.Controls.Add(lab5)
                flo.Controls.Add(flow_ima)
                flo.Controls.Add(flow_txt)
                FlowLayoutPanel1.Controls.Add(flo)
            End While
            Label3.Text = count & " results found"


            sqlconn.Close()

        Else
            MessageBox.Show("select blood type and city")
        End If
    End Sub

    Private Sub FlowLayoutPanel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles FlowLayoutPanel1.Paint

    End Sub
End Class