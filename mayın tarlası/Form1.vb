Public Class Form1
    'Korh@n GeriÞ....2005
#Region "global deðiþkenler"
    Dim tarla(8, 8, 1) As Char
    Dim mayýnsayýsý As Integer = 10
    Dim oyunbitmedi As Boolean = False
    Dim but() As Button
    Dim baþlamazamaný As DateTime
    Dim bulunanmayýnsayýsý, konulanünlemsayýsý As Integer
#End Region

#Region "prosedürler ve oluþturulan eventler"
    ' tarlayý oluþturan butonlarý oluþturan ve form'a dizer
    Private Sub tarlaoluþtur(ByVal x As Integer, ByVal y As Integer)

        Dim tarlayukarýdanuzaklýk As Integer = 70
        Dim tarlasoldanuzaklýk As Integer = 20
        Dim kareyüksekliði As Integer = 20
        Dim karegeniþliði As Integer = 21
        Dim i, j, sütunno As Integer, satýrno As Integer = 0
        ReDim but((x * y) - 1)

        For i = 0 To x * y - 1
            but(i) = New Button()
            but(i).Name = "but" & i
            Me.Controls.Add(but(i))
            but(i).BackgroundImage = Image.FromFile("boþ.bmp")
            but(i).Width = karegeniþliði
            but(i).Height = kareyüksekliði
            sütunno = i Mod y
            but(i).Top = tarlayukarýdanuzaklýk + satýrno * kareyüksekliði
            but(i).Left = tarlasoldanuzaklýk + sütunno * karegeniþliði
            If (sütunno = y - 1) Then
                satýrno += 1
            End If
            AddHandler but(i).MouseUp, New MouseEventHandler(AddressOf týkla)
        Next

        For i = 0 To x - 1

            For j = 0 To y - 1

                tarla(i, j, 1) = CChar("b")
            Next

        Next
    End Sub

    'yeni oyun baþladýðýnda kareleri ve tarlayý temizleyen kýsým
    Private Sub temizle()
        Me.Refresh()
        Dim i, j As Integer
        For i = 0 To (tarla.Length / 2) - 1
            Me.Controls.Remove(but(i))
        Next

        For i = 0 To tarla.GetUpperBound(0)
            For j = 0 To tarla.GetUpperBound(1)
                tarla(i, j, 0) = CChar(" ")
            Next
        Next
        Me.Refresh()
    End Sub


    'mayýnlarýn etrafýndaki sayýlarý hesaplayan kýsým
    Private Function mayýnsay(ByVal x As Integer, ByVal y As Integer) As String
        Dim xbaþ, xbit, ybaþ, ybit, satýr, sütun As Integer
        mayýnsayýsý = 0

        xbaþ = x - 1
        xbit = xbaþ + 2
        If (xbaþ < 0) Then

            xbaþ = 0
        End If
        If (xbit > tarla.GetUpperBound(0)) Then
            xbit = tarla.GetUpperBound(0)
        End If

        ybaþ = y - 1
        ybit = ybaþ + 2
        If (ybaþ < 0) Then
            ybaþ = 0
        End If

        If (ybit > tarla.GetUpperBound(1)) Then
            ybit = tarla.GetUpperBound(1)
        End If

        For satýr = xbaþ To xbit
            For sütun = ybaþ To ybit
                If (tarla(satýr, sütun, 0) = CChar("M")) Then
                    mayýnsayýsý += 1
                End If
            Next
        Next

        Return mayýnsayýsý.ToString()
    End Function
    Dim a
    'mayýnlarýn etrafýndaki sayýlarý yazan kýsým
    Private Sub mayýnla(ByVal mayýnsayýsý As Integer)

        Dim koyulanmayýnsayýsý As Integer = 0
        Dim satýr, sütun As Integer

        Do
            satýr = Rnd() * (tarla.GetUpperBound(0) - 1)
            sütun = Rnd() * (tarla.GetUpperBound(1) - 1)

            If (tarla(satýr, sütun, 0) <> CChar("M")) Then
                tarla(satýr, sütun, 0) = CChar("M")
                koyulanmayýnsayýsý += 1
            End If

        Loop While (koyulanmayýnsayýsý <> 10)
        '********************************************

        Dim x, y, xbak, ybak As Integer
        For x = 0 To tarla.GetUpperBound(0)

            For y = 0 To tarla.GetUpperBound(0)

                If (tarla(x, y, 0) <> CChar("M")) Then

                    tarla(x, y, 0) = Convert.ToChar(mayýnsay(x, y))
                End If
            Next
        Next
        '*********************************

    End Sub

    'bir kutu açýlacaðýnda açýlanyede gösterilmesi gerekeni gösteriyor. 
    Private Sub kutuaç(ByVal x As Integer, ByVal y As Integer)

        Dim butonno As Integer = x * tarla.GetLength(0) + y

        If (tarla(x, y, 0) = CChar("M")) Then

            but(butonno).Image = Image.FromFile("mayýn.bmp")
            If (oyunbitmedi = True) Then

                booom()
            End If

        ElseIf (tarla(x, y, 0) = CChar(" ")) Then

            but(butonno).Visible = False

        ElseIf (tarla(x, y, 0) = CChar("0")) Then

            boþaç(x, y)

        Else

            but(butonno).Text = tarla(x, y, 0).ToString()
        End If

    End Sub

    'boþ kutuya týkl=ýðýnda yanýndaki diðer boþlarý açýyor
    Private Sub boþaç(ByVal x As Integer, ByVal y As Integer)

        If (tarla(x, y, 0) = CChar("0")) Then

            tarla(x, y, 0) = CChar(" ")
            kutuaç(x, y)

            Dim xbaþ, xbit, ybaþ, ybit, satýr, sütun As Integer

            xbaþ = x - 1
            xbit = xbaþ + 2
            If (xbaþ < 0) Then

                xbaþ = 0
            End If

            If (xbit > tarla.GetUpperBound(0)) Then

                xbit = tarla.GetUpperBound(0)
            End If

            ybaþ = y - 1
            ybit = ybaþ + 2
            If (ybaþ < 0) Then

                ybaþ = 0
            End If

            If (ybit > tarla.GetUpperBound(1)) Then

                ybit = tarla.GetUpperBound(1)
            End If

            For satýr = xbaþ To xbit

                For sütun = ybaþ To ybit

                    If (tarla(satýr, sütun, 0) = CChar("0")) Then

                        boþaç(satýr, sütun)

                    Else

                        kutuaç(satýr, sütun)
                    End If

                Next
            Next

        End If
    End Sub

    'herhengi bir mouseup da çalýþan kýsým
    Private Sub týkla(ByVal sender As Object, ByVal e As MouseEventArgs)

        If (oyunbitmedi = True) Then

            Dim butonno As String = sender.Name.ToString()
            butonno = butonno.Remove(0, 3)
            Dim x, y As Integer
            x = Math.Floor(Convert.ToDouble(butonno) / tarla.GetLength(0))
            y = Convert.ToInt32(Convert.ToInt32(butonno) Mod Convert.ToInt32(tarla.GetLength(1)))
            Me.Text = (x + y).ToString()
            If (e.Button = MouseButtons.Left) Then

                kutuaç(x, y)
            End If
            If (e.Button = MouseButtons.Right) Then

                If (tarla(x, y, 1) = CChar("b")) Then

                    tarla(x, y, 1) = CChar("ü")
                    sender.BackgroundImage = Image.FromFile("ünlem.bmp")
                    konulanünlemsayýsý += 1
                    If (tarla(x, y, 0) = CChar("M")) Then

                        bulunanmayýnsayýsý += 1
                    End If
                    If ((bulunanmayýnsayýsý = mayýnsayýsý) And (mayýnsayýsý = konulanünlemsayýsý)) Then

                        tebrikler()
                    End If

                ElseIf (tarla(x, y, 1) = CChar("ü")) Then

                    tarla(x, y, 1) = CChar("s")
                    sender.BackgroundImage = Image.FromFile("soru.bmp")
                    konulanünlemsayýsý -= 1
                    If (tarla(x, y, 0) = CChar("M")) Then

                        bulunanmayýnsayýsý -= 1
                    End If

                ElseIf (tarla(x, y, 1) = CChar("s")) Then

                    tarla(x, y, 1) = CChar("b")
                    sender.BackgroundImage = Image.FromFile("boþ.bmp")
                    If ((bulunanmayýnsayýsý = mayýnsayýsý) And (mayýnsayýsý = konulanünlemsayýsý)) Then

                        tebrikler()
                    End If

                End If
            End If
        End If
    End Sub

    'oyun baþarýlý bittiðinde yapýlacaklar
    Private Sub tebrikler()

        Me.BackColor = Color.Blue
        Timer1.Enabled = False
        oyunbitmedi = False
    End Sub

    'mayýna basýldýðýnda yapýlacaklar
    Private Sub booom()

        oyunbitmedi = False
        Dim x, y As Integer
        For x = 0 To tarla.GetUpperBound(0)

            For y = 0 To tarla.GetUpperBound(1)

                kutuaç(x, y)
            Next
        Next
        Timer1.Enabled = False
        Me.BackColor = Color.Red
    End Sub
#End Region

#Region "eventler"
    ''form1 yüklenirken yapýlacaklar :)))
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.BackColor = Color.LightGray
    End Sub


    'yeni oyun butonu
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Refresh()
        Label1.Text = "00:00"
        konulanünlemsayýsý = 0
        bulunanmayýnsayýsý = 0
        Me.BackColor = Color.LightGray
        If (Button1.Text = "Yeni Oyun") Then
            temizle()

        End If
        Button1.Text = "Yeni Oyun"
        tarlaoluþtur(tarla.GetLength(0), tarla.GetLength(1))

        mayýnla(mayýnsayýsý)

        oyunbitmedi = True
        baþlamazamaný = DateTime.Now
        Timer1.Enabled = True
        Me.Refresh()

    End Sub

    'oyunsýrasýnda geçen zamaný gösteren kýsým
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Dim geçensaniye As Integer
        geçensaniye = Convert.ToInt32(DateAndTime.DateDiff(DateInterval.Second, baþlamazamaný, DateTime.Now, FirstDayOfWeek.System, FirstWeekOfYear.System))
        Dim dakika As String
        Dim saniye As String
        saniye = (geçensaniye Mod 60).ToString().PadLeft(2, CChar("0"))
        dakika = Convert.ToString(Math.Floor(Convert.ToDouble(geçensaniye / 60)).ToString().PadLeft(2, CChar("0")))
        Label1.Text = dakika + ":" + saniye
    End Sub
#End Region

   
End Class
