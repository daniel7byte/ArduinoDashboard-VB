Imports System.IO.Ports

Public Class Form1

    Dim txt As String
    Dim s As System.Media.SoundPlayer

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        s = New System.Media.SoundPlayer(My.Application.Info.DirectoryPath + "/Alarm.wav")

        Windows.Forms.Control.CheckForIllegalCrossThreadCalls = False
        Try
            For Each port As String In SerialPort.GetPortNames()
                ComboBox1.Items.Add(port)
            Next
            ComboBox1.SelectedIndex = 0
            ComboBox2.SelectedItem = "9600"
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Button1.Text = "CONECTAR" Then
            SerialPort1.BaudRate = Val(ComboBox2.SelectedItem)
            SerialPort1.PortName = ComboBox1.SelectedItem
            Try
                SerialPort1.Open()
                Button1.Text = "DESCONECTAR"
                TextBox1.Enabled = True
                GroupBox1.Enabled = False
            Catch ex As Exception

            End Try
        ElseIf Button1.Text = "DESCONECTAR" Then
            s.Stop()
            SerialPort1.Close()
            TextBox1.Enabled = False
            GroupBox1.Enabled = True
            Button1.Text = "CONECTAR"
        End If
    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            SerialPort1.Write(TextBox1.Text)
            TextBox1.Clear()
        End If
    End Sub

    Private Sub SerialPort1_DataReceived(sender As Object, e As SerialDataReceivedEventArgs) Handles SerialPort1.DataReceived
        RichTextBox1.Text &= SerialPort1.ReadExisting()

        If CheckBox1.Checked Then

            'FORMATEO

            txt = (RichTextBox1.Text).Trim()

            'MsgBox(txt)

            txt = Replace(Replace(RichTextBox1.Text, Chr(10), " "), Chr(13), " ")

            'MsgBox("-" & txt & "-")

            txt = Replace(txt, " ", "")

            'MsgBox("-" & txt & "-")

            txt = txt.Substring(txt.Length - 1, 1)

            'MsgBox("-" & pir & "-")

            If txt = "P" Then
                TextBox2.Text = "DANGER"
                PictureBox1.Visible = True
                s.PlayLooping()
            ElseIf txt = "p" Then
                TextBox2.Text = "NO DANGER"
                PictureBox1.Visible = False
                s.Stop()
            End If

            txt = ""

        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        SerialPort1.Write("13 1")
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        SerialPort1.Write("13 0")
    End Sub
End Class
