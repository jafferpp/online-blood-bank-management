Imports System.Data.SqlClient
Imports System.Text.RegularExpressions
Imports System.IO
Imports System.Drawing.Image
Public Class institutepage

    Dim id1 As String = Institutelogin.id
    Dim messagesta As Integer
    Dim command, command1 As New SqlCommand
    Dim reader, reader1 As SqlDataReader
    Dim strd, name1, senderid2 As String
    Dim sqlconn As New SqlConnection("Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\HP\Desktop\jaffer\new\online blood bank management\online blood bank management\bloodbank.mdf;Integrated Security=True;User Instance=True")

    Private Sub institutepage_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        start.Show()

    End Sub


    Private Sub institutepage_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label37.Visible = False
        Panelsend2.Visible = False
        GroupBox1.BringToFront()
        Button3.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Button4.BackColor = System.Drawing.SystemColors.HotTrack
        Button5.BackColor = System.Drawing.SystemColors.HotTrack
        Button6.BackColor = System.Drawing.SystemColors.HotTrack
        Button7.BackColor = System.Drawing.SystemColors.HotTrack
        Button8.BackColor = System.Drawing.SystemColors.HotTrack
        Dim name As String = Institutelogin.institutename
        name1 = name
        Dim city As String = Institutelogin.city
        Dim address As String = Institutelogin.address
        Dim email As String = Institutelogin.email
        Dim contact As String = Institutelogin.contact
        Label1.Text = name
        Label31.Text = "User id : " & id1
        Label32.Text = "contact : " & contact
        Label33.Text = "email : " & email & vbNewLine & "city : " & city
        Label34.Text = address

        Dim str As String = "select image from register where userid='" & id1 & "'"
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
        Catch ex As Exception

        End Try
        





        str = "select * from blood where userid='" & id1 & "'"
        command = New SqlCommand(str, sqlconn)
        reader = command.ExecuteReader()
        Dim ap, an, bp, bn, abp, abn, op, onegative As Single
        Dim app, ann, bpp, bnn, abpp, abnn, onegativee, opp As Integer
        While (reader.Read)
            ap = reader("APOSITIVE")
            an = reader("ANEGATIVE")
            bp = reader("BPOSITIVE")
            bn = reader("BNEGATIVE")
            abp = reader("ABPOSITIVE")
            abn = reader("ABNEGATIVE")
            op = reader("OPOSITIVE")
            onegative = reader("ONEGATIVE")
            app = ap
            ann = an
            bpp = bp
            bnn = bn
            abpp = abp
            abnn = abn
            onegativee = onegative
            opp = op
            ProgressBar1.Value = ann
            Label2.Text = Convert.ToInt32(an * 35) & " ml"
            ProgressBar2.Value = app
            Label6.Text = Convert.ToInt32(ap * 35) & " ml"
            ProgressBar3.Value = bpp
            Label22.Text = Convert.ToInt32(bp * 35) & " ml"
            ProgressBar4.Value = bnn
            Label3.Text = Convert.ToInt32(bn * 35) & " ml"
            ProgressBar7.Value = abpp
            Label23.Text = Convert.ToInt32(abp * 35) & " ml"
            ProgressBar8.Value = abnn
            Label4.Text = Convert.ToInt32(abn * 35) & " ml"
            ProgressBar5.Value = opp
            Label24.Text = Convert.ToInt32(op * 35) & " ml"
            ProgressBar6.Value = onegativee
            Label5.Text = Convert.ToInt32(onegative * 35) & " ml"
        End While

        sqlconn.Close()
        sqlconn.Open()
        strd = "select * from record  where userid= '" & id1 & "' order by id desc"
        Dim sqlcom As New SqlCommand(strd, sqlconn)
        reader = sqlcom.ExecuteReader()
        recordtext.Clear()
        While (reader.Read)
            Dim st As String
            st = reader("record")
            recordtext.Text += st
        End While
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
            Label37.Visible = True
        End If

    End Sub
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        GroupBox1.BringToFront()
        Button3.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Button4.BackColor = System.Drawing.SystemColors.HotTrack
        Button5.BackColor = System.Drawing.SystemColors.HotTrack
        Button6.BackColor = System.Drawing.SystemColors.HotTrack
        Button7.BackColor = System.Drawing.SystemColors.HotTrack
        Button8.BackColor = System.Drawing.SystemColors.HotTrack
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        GroupBox2.BringToFront()
        Button3.BackColor = System.Drawing.SystemColors.HotTrack
        Button4.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Button5.BackColor = System.Drawing.SystemColors.HotTrack
        Button6.BackColor = System.Drawing.SystemColors.HotTrack
        Button7.BackColor = System.Drawing.SystemColors.HotTrack
        Button8.BackColor = System.Drawing.SystemColors.HotTrack
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        GroupBox3.BringToFront()
        Button3.BackColor = System.Drawing.SystemColors.HotTrack
        Button4.BackColor = System.Drawing.SystemColors.HotTrack
        Button5.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Button6.BackColor = System.Drawing.SystemColors.HotTrack
        Button7.BackColor = System.Drawing.SystemColors.HotTrack
        Button8.BackColor = System.Drawing.SystemColors.HotTrack
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        messagesta = 1
        Groupbox4.BringToFront()
        Button3.BackColor = System.Drawing.SystemColors.HotTrack
        Button4.BackColor = System.Drawing.SystemColors.HotTrack
        Button5.BackColor = System.Drawing.SystemColors.HotTrack
        Button6.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Button7.BackColor = System.Drawing.SystemColors.HotTrack
        Button8.BackColor = System.Drawing.SystemColors.HotTrack
        FlowLayoutPanel2.HorizontalScroll.Enabled = True


        sqlconn.Open()
        command = New SqlCommand("select * from messages where userid= '" & id1 & "' order by id desc", sqlconn)
        reader = command.ExecuteReader()

        While (reader.Read)
            Dim mesg, name, line, senderid As New Label
            mesg.AutoSize = True
            Dim senderid1 As String = reader("senderid")
            mesg.Text = reader("message")
            mesg.Font = New Font("Comic Sans MS", 9, FontStyle.Bold)
            name.Text = reader("name")
            Dim butt As New Button
            AddHandler butt.Click, AddressOf clickme
            butt.Font = New Font("Comic Sans MS", 9)
            butt.Text = "replay"
            butt.Tag = senderid1
            butt.AutoSize = True
            name.AutoSize = True
            mesg.ForeColor = Color.Blue

            FlowLayoutPanel2.Controls.Add(name)
            FlowLayoutPanel2.Controls.Add(mesg)
            FlowLayoutPanel2.Controls.Add(butt)
            line.AutoSize = True
            line.Text = "__________________"
            FlowLayoutPanel2.Controls.Add(line)


        End While
        sqlconn.Close()
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        GroupBox5.BringToFront()
        Button3.BackColor = System.Drawing.SystemColors.HotTrack
        Button4.BackColor = System.Drawing.SystemColors.HotTrack
        Button5.BackColor = System.Drawing.SystemColors.HotTrack
        Button6.BackColor = System.Drawing.SystemColors.HotTrack
        Button7.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Button8.BackColor = System.Drawing.SystemColors.HotTrack
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        GroupBox6.BringToFront()
        Button3.BackColor = System.Drawing.SystemColors.HotTrack
        Button4.BackColor = System.Drawing.SystemColors.HotTrack
        Button5.BackColor = System.Drawing.SystemColors.HotTrack
        Button6.BackColor = System.Drawing.SystemColors.HotTrack
        Button7.BackColor = System.Drawing.SystemColors.HotTrack
        Button8.BackColor = System.Drawing.SystemColors.GradientActiveCaption
    End Sub

    ''add blood''


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        If Regex.IsMatch(TextBox1.Text.Trim(), "^[A-Za-z0-9\s]+$") And Regex.IsMatch(TextBox2.Text.Trim(), "^[0-9]+$") And Regex.IsMatch(TextBox3.Text.Trim(), "^[0-9]+$") Then
            MessageBox.Show("are u sure? " & TextBox3.Text & "ml blood will be added")

            Dim strd As String = "select * from blood where userid= '" & id1 & "'"
            sqlconn.Open()
            command = New SqlCommand(strd, sqlconn)
            reader = command.ExecuteReader()
            Dim ap, an, bp, bn, abp, abn, op, onegative As Single
            While (reader.Read)
                ap = reader("APOSITIVE")
                an = reader("ANEGATIVE")
                bp = reader("BPOSITIVE")
                bn = reader("BNEGATIVE")
                abp = reader("ABPOSITIVE")
                abn = reader("ABNEGATIVE")
                op = reader("OPOSITIVE")
                onegative = reader("ONEGATIVE")
            End While
            sqlconn.Close()
            Dim bloodname As String = "A Negative"
            Dim bloodtype As String
            Dim ml As Single

            ml = (CInt(TextBox3.Text) / 3500) * 100


            If RadioButton1.Checked Then
                ml = ml + an
                If ml > 100 Then
                    MessageBox.Show("over flow")
                    ml = 100
                End If
                bloodname = "A Negative"
                bloodtype = "update blood set ANEGATIVE= " & ml & " where userid='" & id1 & "'"
            End If
            If RadioButton2.Checked Then
                ml = ml + ap
                If ml > 100 Then
                    ml = 100
                    MessageBox.Show("over flow")
                End If
                bloodname = "A Positive"
                bloodtype = "update blood set APOSITIVE= " & ml & " where userid='" & id1 & "'"
            End If
            If RadioButton6.Checked Then
                ml = ml + bn
                If ml > 100 Then
                    ml = 100
                    MessageBox.Show("over flow")
                End If
                bloodname = "B Negative"
                bloodtype = "update blood set BNEGATIVE= " & ml & " where userid='" & id1 & "'"
            End If
            If RadioButton3.Checked Then
                ml = ml + bp
                If ml > 100 Then
                    ml = 100
                    MessageBox.Show("over flow")
                End If
                bloodname = "B Positive"
                bloodtype = "update blood set BPOSITIVE= " & ml & " where userid='" & id1 & "'"
            End If
            If RadioButton7.Checked Then
                ml = ml + abn
                If ml > 100 Then
                    ml = 100
                    MessageBox.Show("over flow")
                    MessageBox.Show("over flow")
                End If
                bloodname = "AB Negative"
                bloodtype = "update blood set ABNEGATIVE= " & ml & " where userid='" & id1 & "'"
            End If
            If RadioButton4.Checked Then
                ml = ml + abp
                If ml > 100 Then
                    ml = 100
                    MessageBox.Show("over flow")
                End If
                bloodname = "AB Posoitive"
                bloodtype = "update blood set ABPOSITIVE= " & ml & " where userid='" & id1 & "'"
            End If
            If RadioButton8.Checked Then
                ml = ml + onegative
                If ml > 100 Then
                    ml = 100
                    MessageBox.Show("over flow")
                End If
                bloodname = "O Negative"
                bloodtype = "update blood set ONEGATIVE= " & ml & " where userid='" & id1 & "'"
            End If
            If RadioButton5.Checked Then
                ml = ml + op
                If ml > 100 Then
                    ml = 100
                    MessageBox.Show("over flow")
                End If
                bloodname = "O Positive"
                bloodtype = "update blood set OPOSITIVE= " & ml & " where userid='" & id1 & "'"
            End If



            Dim sqlquery2 As New SqlCommand(bloodtype, sqlconn)
            sqlconn.Open()

            sqlquery2.ExecuteNonQuery()
            sqlconn.Close()



            sqlconn.Open()
            Dim regDate As Date = Date.Now()
            Dim strDate As String = regDate.ToString("dd/MMM/yyyy")
            Dim records As String = "date:" & strDate & " Donor_name :" & TextBox1.Text & " blood_type :" & bloodname & " Age=" & TextBox2.Text & " Donated_blood:" & TextBox3.Text & "ml _______________________"
            strd = "insert into record values('" & id1 & "','" & records & "')"
            Dim sqlq As New SqlCommand(strd, sqlconn)
            sqlq.ExecuteNonQuery()
            sqlconn.Close()
            sqlconn.Open()
            strd = "select * from record  where userid= '" & id1 & "' order by id desc"
            Dim sqlcom As New SqlCommand(strd, sqlconn)
            reader = sqlcom.ExecuteReader
            recordtext.Clear()
            While (reader.Read)
                Dim st As String
                st = reader("record")
                recordtext.Text += st
            End While
            sqlconn.Close()
            institutepage_Load(e, e)



        Else
            MessageBox.Show("please enter valid details")
        End If
    End Sub

    ''take blood''
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim regDate As Date = Date.Now()
        Dim strDate As String = regDate.ToString("dd/MMM/yyyy")

        If Regex.IsMatch(TextBox5.Text.Trim(), "^[A-Za-z0-9\s]+$") And Regex.IsMatch(TextBox4.Text.Trim(), "^[0-9]+$") Then
            MessageBox.Show("are u sure? " & TextBox4.Text & "ml blood will be deducted")
            Dim bloodtype As String
            Dim ml As Single
            ml = (CInt(TextBox4.Text) / 3500) * 100

            Dim strd As String = "select * from blood  where userid= '" & id1 & "' "
            sqlconn.Open()
            command = New SqlCommand(strd, sqlconn)
            reader = command.ExecuteReader()

            Dim ap, an, bp, bn, abp, abn, op, onegative As Single
            While (reader.Read)
                ap = reader("APOSITIVE")
                an = reader("ANEGATIVE")
                bp = reader("BPOSITIVE")
                bn = reader("BNEGATIVE")
                abp = reader("ABPOSITIVE")
                abn = reader("ABNEGATIVE")
                op = reader("OPOSITIVE")
                onegative = reader("ONEGATIVE")
            End While
            sqlconn.Close()

            Dim bloodname As String = "A positive"
            Dim lo As Integer

            If RadioButton111.Checked Then
                bloodname = "A negative"
                lo = 35 * an
                ml = an - ml
                If ml < 0 Then
                    ml = 0
                    MessageBox.Show("only " & lo & " ml available")
                End If
                bloodtype = "update blood set ANEGATIVE= " & ml & " where userid='" & id1 & "'"
            End If
            If RadioButton222.Checked Then
                bloodname = "A positive"
                lo = 35 * ap
                ml = ap - ml
                If ml < 0 Then
                    ml = 0
                    MessageBox.Show("only " & lo & " ml available")
                End If
                bloodtype = "update blood set APOSITIVE= " & ml & " where userid='" & id1 & "'"
            End If
            If RadioButton333.Checked Then
                bloodname = "B negative"
                lo = 35 * bn
                ml = bn - ml
                If ml < 0 Then
                    ml = 0
                    MessageBox.Show("only " & lo & " ml available")
                End If
                bloodtype = "update blood set BNEGATIVE= " & ml & " where userid='" & id1 & "'"
            End If
            If RadioButton444.Checked Then
                bloodname = "B positive"
                lo = 35 * bp
                ml = bp - ml
                If ml < 0 Then
                    ml = 0
                    MessageBox.Show("only " & lo & " ml available")
                End If
                bloodtype = "update blood set BPOSITIVE= " & ml & " where userid='" & id1 & "'"
            End If
            If RadioButton555.Checked Then
                bloodname = "AB negative"
                lo = 35 * abn
                ml = abn - ml
                If ml < 0 Then
                    ml = 0
                    MessageBox.Show("only " & lo & " ml available")
                End If
                bloodtype = "update blood set ABNEGATIVE= " & ml & " where userid='" & id1 & "'"
            End If
            If RadioButton666.Checked Then
                bloodname = "AB positive"
                lo = 35 * abp
                ml = abp - ml
                If ml < 0 Then
                    ml = 0
                    MessageBox.Show("only " & lo & " ml available")
                End If
                bloodtype = "update blood set ABPOSITIVE= " & ml & " where userid='" & id1 & "'"
            End If
            If RadioButton777.Checked Then
                bloodname = "O negative"
                lo = 35 * onegative
                ml = onegative - ml
                If ml < 0 Then
                    ml = 0
                    MessageBox.Show("only " & lo & " ml available")
                End If
                bloodtype = "update blood set ONEGATIVE= " & ml & " where userid='" & id1 & "'"
            End If
            If RadioButton888.Checked Then
                bloodname = "O positive"
                lo = 35 * op
                ml = op - ml
                If ml < 0 Then
                    ml = 0
                    MessageBox.Show("only " & lo & " ml available")
                End If
                bloodtype = "update blood set OPOSITIVE= " & ml & " where userid='" & id1 & "'"
            End If



            Dim sqlquery2 As New SqlCommand(bloodtype, sqlconn)
            sqlconn.Open()
            sqlquery2.ExecuteNonQuery()
            Dim records As String = "date:" & strDate & vbNewLine & "reciever name: " & TextBox5.Text & vbNewLine & "blood type: " & bloodname & vbNewLine & "Blood Amount Taken: " & TextBox4.Text & " _______________________"

            strd = "insert into record values('" & id1 & "','" & records & "')"
            Dim sqlq As New SqlCommand(strd, sqlconn)
            sqlq.ExecuteNonQuery()
            sqlconn.Close()
            sqlconn.Open()
            strd = "select * from record  where userid='" & id1 & "' order by id desc"
            Dim sqlcom As New SqlCommand(strd, sqlconn)
            reader = sqlcom.ExecuteReader
            recordtext.Clear()
            While (reader.Read)
                Dim st As String
                st = reader("record")
                recordtext.Text += st
            End While
            sqlconn.Close()
            institutepage_Load(e, e)
        Else
            MessageBox.Show("please enter valid details")

        End If
    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        If TextBox6.Text = "" And TextBox7.Text = "" Then
            MessageBox.Show("enter passwords")
        ElseIf TextBox6.Text <> TextBox7.Text Then
            MessageBox.Show("passwords not match")
        Else
            sqlconn.Open()
            Dim str As String = "update register set password= '" & TextBox6.Text().Trim() & "' where userid='" & id1 & "'"
            command = New SqlCommand(str, sqlconn)
            command.ExecuteNonQuery()
            sqlconn.Close()
            MessageBox.Show("password changed your new password is: " & TextBox6.Text)

        End If
    End Sub

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        Dim passwords As String

        sqlconn.Open()
        Dim str As String = "select password from register where userid='" & id1 & "'"
        command = New SqlCommand(str, sqlconn)
        reader = command.ExecuteReader

        Dim count As Integer = 0
        While (reader.Read)
            count = count + 1
            passwords = reader("password").ToString
        End While
        sqlconn.Close()
        If passwords = TextBox8.Text.Trim() Then
            sqlconn.Open()
            str = "delete  from register where userid= '" & id1 & "'"
            command = New SqlCommand(str, sqlconn)
            command.ExecuteNonQuery()
            MessageBox.Show("your account deleted")
            Me.Close()
        Else
            MessageBox.Show("password id not correct")
        End If
        sqlconn.Close()
    End Sub

    Private Sub Label30_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label30.Click
        Dim result As Integer = MessageBox.Show("Are you sure Do you want to clear all records", "clear records", MessageBoxButtons.YesNo)

        If result = DialogResult.Yes Then
            sqlconn.Open()
            Dim str As String = "delete from record where userid='" & id1 & "'"
            command = New SqlCommand(str, sqlconn)
            reader = command.ExecuteReader
            sqlconn.Close()
            recordtext.Clear()
        End If

    End Sub

    Private Sub Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button11.Click
        ''request for A Negative blood
        Dim sendstatus As String = ""
        If ProgressBar1.Value = 0 Then
            sendstatus += " A Negative" & vbNewLine
            sqlconn.Open()
            Dim str As String = "select userid from donor where type = 'A Negative'"
            command = New SqlCommand(str, sqlconn)
            Dim table As New DataTable
            Dim adapter As New SqlDataAdapter(command)
            adapter.Fill(table)

            For i = 0 To (table.Rows.Count) - 1
                Dim tab As String = table.Rows(i)(0)
                str = "insert into messages values('" & tab & "','" & id1 & "','hello this is from : " & name1 & " , we need A Negative blood to our blood bank please contact us if you have intrest','new','" & name1 & "') "
                command = New SqlCommand(str, sqlconn)
                command.ExecuteNonQuery()
            Next

            sqlconn.Close()
        End If
        '' request for A Positive blood
        If ProgressBar2.Value = 0 Then
            sendstatus += " A Positive" & vbNewLine
            sqlconn.Open()
            Dim str As String = "select userid from donor where type = 'A Positive'"
            command = New SqlCommand(str, sqlconn)
            Dim table As New DataTable
            Dim adapter As New SqlDataAdapter(command)
            adapter.Fill(table)

            For i = 0 To (table.Rows.Count) - 1
                Dim tab As String = table.Rows(i)(0)
                str = "insert into messages values('" & tab & "','" & id1 & "','hello this is from : " & name1 & " , we need A Positive blood to our blood bank please contact us, if you have intrest','new','" & name1 & "') "
                command = New SqlCommand(str, sqlconn)
                command.ExecuteNonQuery()
            Next
            sqlconn.Close()
        End If

        If ProgressBar4.Value = 0 Then
            sendstatus += " B Negative" & vbNewLine
            sqlconn.Open()
            Dim str As String = "select userid from donor where type = 'B Negative'"
            command = New SqlCommand(str, sqlconn)
            Dim table As New DataTable
            Dim adapter As New SqlDataAdapter(command)
            adapter.Fill(table)
            For i = 0 To (table.Rows.Count) - 1
                Dim tab As String = table.Rows(i)(0)
                str = "insert into messages values('" & tab & "','" & id1 & "','hello this is from : " & name1 & " , we need B Negative blood to our blood bank please contact us, if you have intrest','new','" & name1 & "') "
                command = New SqlCommand(str, sqlconn)
                command.ExecuteNonQuery()
            Next
            sqlconn.Close()
        End If

        If ProgressBar3.Value = 0 Then
            sendstatus += " B Positive" & vbNewLine
            sqlconn.Open()
            Dim str As String = "select userid from donor where type = 'B Positive'"
            command = New SqlCommand(str, sqlconn)
            Dim table As New DataTable
            Dim adapter As New SqlDataAdapter(command)
            adapter.Fill(table)

            For i = 0 To (table.Rows.Count) - 1
                Dim tab As String = table.Rows(i)(0)
                str = "insert into messages values('" & tab & "','" & id1 & "','hello this is from : " & name1 & " , we need B Positive blood to our blood bank please contact us, if you have intrest','new','" & name1 & "') "
                command = New SqlCommand(str, sqlconn)
                command.ExecuteNonQuery()
            Next
            sqlconn.Close()
        End If

        If ProgressBar8.Value = 0 Then
            sqlconn.Open()
            sendstatus += " AB Negative" & vbNewLine
            Dim str As String = "select userid from donor where type = 'AB Negative'"
            command = New SqlCommand(str, sqlconn)
            Dim table As New DataTable
            Dim adapter As New SqlDataAdapter(command)
            adapter.Fill(table)


            For i = 0 To (table.Rows.Count) - 1
                Dim tab As String = table.Rows(i)(0)
                str = "insert into messages values('" & tab & "','" & id1 & "','hello this is from : " & name1 & " , we need AB Negative blood to our blood bank please contact us, if you have intrest','new','" & name1 & "') "
                command = New SqlCommand(str, sqlconn)
                command.ExecuteNonQuery()
            Next
            sqlconn.Close()
        End If

        If ProgressBar7.Value = 0 Then
            sqlconn.Open()
            sendstatus += " AB Positive" & vbNewLine
            Dim str As String = "select userid from donor where type = 'AB Positive'"
            command = New SqlCommand(str, sqlconn)
            Dim table As New DataTable
            Dim adapter As New SqlDataAdapter(command)
            adapter.Fill(table)
            For i = 0 To (table.Rows.Count) - 1
                Dim tab As String = table.Rows(i)(0)
                str = "insert into messages values('" & tab & "','" & id1 & "','hello this is from : " & name1 & " , we need AB Positive blood to our blood bank please contact us, if you have intrest','new','" & name1 & "') "
                command = New SqlCommand(str, sqlconn)
                command.ExecuteNonQuery()
            Next
            sqlconn.Close()
        End If

        If ProgressBar6.Value = 0 Then
            sendstatus += " O Negative" & vbNewLine
            sqlconn.Open()
            Dim str As String = "select userid from donor where type = 'O Negative'"
            command = New SqlCommand(str, sqlconn)
            Dim table As New DataTable
            Dim adapter As New SqlDataAdapter(command)
            adapter.Fill(table)
            For i = 0 To (table.Rows.Count) - 1
                Dim tab As String = table.Rows(i)(0)
                str = "insert into messages values('" & tab & "','" & id1 & "','hello this is from : " & name1 & " , we need O Negative blood to our blood bank please contact us, if you have intrest','new','" & name1 & "') "
                command = New SqlCommand(str, sqlconn)
                command.ExecuteNonQuery()
            Next
            sqlconn.Close()
        End If

        If ProgressBar5.Value = 0 Then
            sqlconn.Open()
            sendstatus += " O Positive" & vbNewLine
            Dim str As String = "select userid from donor where type = 'O Positive'"
            command = New SqlCommand(str, sqlconn)
            Dim table As New DataTable
            Dim adapter As New SqlDataAdapter(command)
            adapter.Fill(table)
            For i = 0 To (table.Rows.Count) - 1
                Dim tab As String = table.Rows(i)(0)
                str = "insert into messages values('" & tab & "','" & id1 & "','hello this is from : " & name1 & " , we need O Positive blood to our blood bank please contact us, if you have intrest','new','" & name1 & "') "
                command = New SqlCommand(str, sqlconn)
                command.ExecuteNonQuery()
            Next
            sqlconn.Close()
        End If
        If String.Equals(sendstatus, " ") Then

        Else
            MessageBox.Show("Message send to donors:" & vbNewLine & sendstatus)
        End If

    End Sub
    Private Sub clickme(ByVal sender As Object, ByVal e As EventArgs)
        Dim btn As Button
        btn = CType(sender, Button)
        senderid2 = btn.Tag
        Panelsend2.Visible = True
    End Sub

    Private Sub Button122_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button122.Click
        sqlconn.Open()

        Dim Str As String = "insert into messages values('" & senderid2 & "','" & id1 & "','hello this is from : " & name1 & " " & vbNewLine & "...............  " & vbNewLine & " " & TextBox99.Text.Trim() & " ','new','" & name1 & "') "

        command = New SqlCommand(Str, sqlconn)
        command.ExecuteNonQuery()

        sqlconn.Close()
        Panelsend2.Visible = False
    End Sub
    Private Sub Button12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button12.Click
        institutepage_Load(e, e)
    End Sub
End Class
