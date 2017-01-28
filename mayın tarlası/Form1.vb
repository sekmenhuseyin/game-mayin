Public Class Form1
    'Korh@n Geri�....2005
#Region "global de�i�kenler"
    Dim tarla(8, 8, 1) As Char
    Dim may�nsay�s� As Integer = 10
    Dim oyunbitmedi As Boolean = False
    Dim but() As Button
    Dim ba�lamazaman� As DateTime
    Dim bulunanmay�nsay�s�, konulan�nlemsay�s� As Integer
#End Region

#Region "prosed�rler ve olu�turulan eventler"
    ' tarlay� olu�turan butonlar� olu�turan ve form'a dizer
    Private Sub tarlaolu�tur(ByVal x As Integer, ByVal y As Integer)

        Dim tarlayukar�danuzakl�k As Integer = 70
        Dim tarlasoldanuzakl�k As Integer = 20
        Dim karey�ksekli�i As Integer = 20
        Dim karegeni�li�i As Integer = 21
        Dim i, j, s�tunno As Integer, sat�rno As Integer = 0
        ReDim but((x * y) - 1)

        For i = 0 To x * y - 1
            but(i) = New Button()
            but(i).Name = "but" & i
            Me.Controls.Add(but(i))
            but(i).BackgroundImage = Image.FromFile("bo�.bmp")
            but(i).Width = karegeni�li�i
            but(i).Height = karey�ksekli�i
            s�tunno = i Mod y
            but(i).Top = tarlayukar�danuzakl�k + sat�rno * karey�ksekli�i
            but(i).Left = tarlasoldanuzakl�k + s�tunno * karegeni�li�i
            If (s�tunno = y - 1) Then
                sat�rno += 1
            End If
            AddHandler but(i).MouseUp, New MouseEventHandler(AddressOf t�kla)
        Next

        For i = 0 To x - 1

            For j = 0 To y - 1

                tarla(i, j, 1) = CChar("b")
            Next

        Next
    End Sub

    'yeni oyun ba�lad���nda kareleri ve tarlay� temizleyen k�s�m
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


    'may�nlar�n etraf�ndaki say�lar� hesaplayan k�s�m
    Private Function may�nsay(ByVal x As Integer, ByVal y As Integer) As String
        Dim xba�, xbit, yba�, ybit, sat�r, s�tun As Integer
        may�nsay�s� = 0

        xba� = x - 1
        xbit = xba� + 2
        If (xba� < 0) Then

            xba� = 0
        End If
        If (xbit > tarla.GetUpperBound(0)) Then
            xbit = tarla.GetUpperBound(0)
        End If

        yba� = y - 1
        ybit = yba� + 2
        If (yba� < 0) Then
            yba� = 0
        End If

        If (ybit > tarla.GetUpperBound(1)) Then
            ybit = tarla.GetUpperBound(1)
        End If

        For sat�r = xba� To xbit
            For s�tun = yba� To ybit
                If (tarla(sat�r, s�tun, 0) = CChar("M")) Then
                    may�nsay�s� += 1
                End If
            Next
        Next

        Return may�nsay�s�.ToString()
    End Function
    Dim a
    'may�nlar�n etraf�ndaki say�lar� yazan k�s�m
    Private Sub may�nla(ByVal may�nsay�s� As Integer)

        Dim koyulanmay�nsay�s� As Integer = 0
        Dim sat�r, s�tun As Integer

        Do
            sat�r = Rnd() * (tarla.GetUpperBound(0) - 1)
            s�tun = Rnd() * (tarla.GetUpperBound(1) - 1)

            If (tarla(sat�r, s�tun, 0) <> CChar("M")) Then
                tarla(sat�r, s�tun, 0) = CChar("M")
                koyulanmay�nsay�s� += 1
            End If

        Loop While (koyulanmay�nsay�s� <> 10)
        '********************************************

        Dim x, y, xbak, ybak As Integer
        For x = 0 To tarla.GetUpperBound(0)

            For y = 0 To tarla.GetUpperBound(0)

                If (tarla(x, y, 0) <> CChar("M")) Then

                    tarla(x, y, 0) = Convert.ToChar(may�nsay(x, y))
                End If
            Next
        Next
        '*********************************

    End Sub

    'bir kutu a��laca��nda a��lanyede g�sterilmesi gerekeni g�steriyor. 
    Private Sub kutua�(ByVal x As Integer, ByVal y As Integer)

        Dim butonno As Integer = x * tarla.GetLength(0) + y

        If (tarla(x, y, 0) = CChar("M")) Then

            but(butonno).Image = Image.FromFile("may�n.bmp")
            If (oyunbitmedi = True) Then

                booom()
            End If

        ElseIf (tarla(x, y, 0) = CChar(" ")) Then

            but(butonno).Visible = False

        ElseIf (tarla(x, y, 0) = CChar("0")) Then

            bo�a�(x, y)

        Else

            but(butonno).Text = tarla(x, y, 0).ToString()
        End If

    End Sub

    'bo� kutuya t�kl=���nda yan�ndaki di�er bo�lar� a��yor
    Private Sub bo�a�(ByVal x As Integer, ByVal y As Integer)

        If (tarla(x, y, 0) = CChar("0")) Then

            tarla(x, y, 0) = CChar(" ")
            kutua�(x, y)

            Dim xba�, xbit, yba�, ybit, sat�r, s�tun As Integer

            xba� = x - 1
            xbit = xba� + 2
            If (xba� < 0) Then

                xba� = 0
            End If

            If (xbit > tarla.GetUpperBound(0)) Then

                xbit = tarla.GetUpperBound(0)
            End If

            yba� = y - 1
            ybit = yba� + 2
            If (yba� < 0) Then

                yba� = 0
            End If

            If (ybit > tarla.GetUpperBound(1)) Then

                ybit = tarla.GetUpperBound(1)
            End If

            For sat�r = xba� To xbit

                For s�tun = yba� To ybit

                    If (tarla(sat�r, s�tun, 0) = CChar("0")) Then

                        bo�a�(sat�r, s�tun)

                    Else

                        kutua�(sat�r, s�tun)
                    End If

                Next
            Next

        End If
    End Sub

    'herhengi bir mouseup da �al��an k�s�m
    Private Sub t�kla(ByVal sender As Object, ByVal e As MouseEventArgs)

        If (oyunbitmedi = True) Then

            Dim butonno As String = sender.Name.ToString()
            butonno = butonno.Remove(0, 3)
            Dim x, y As Integer
            x = Math.Floor(Convert.ToDouble(butonno) / tarla.GetLength(0))
            y = Convert.ToInt32(Convert.ToInt32(butonno) Mod Convert.ToInt32(tarla.GetLength(1)))
            Me.Text = (x + y).ToString()
            If (e.Button = MouseButtons.Left) Then

                kutua�(x, y)
            End If
            If (e.Button = MouseButtons.Right) Then

                If (tarla(x, y, 1) = CChar("b")) Then

                    tarla(x, y, 1) = CChar("�")
                    sender.BackgroundImage = Image.FromFile("�nlem.bmp")
                    konulan�nlemsay�s� += 1
                    If (tarla(x, y, 0) = CChar("M")) Then

                        bulunanmay�nsay�s� += 1
                    End If
                    If ((bulunanmay�nsay�s� = may�nsay�s�) And (may�nsay�s� = konulan�nlemsay�s�)) Then

                        tebrikler()
                    End If

                ElseIf (tarla(x, y, 1) = CChar("�")) Then

                    tarla(x, y, 1) = CChar("s")
                    sender.BackgroundImage = Image.FromFile("soru.bmp")
                    konulan�nlemsay�s� -= 1
                    If (tarla(x, y, 0) = CChar("M")) Then

                        bulunanmay�nsay�s� -= 1
                    End If

                ElseIf (tarla(x, y, 1) = CChar("s")) Then

                    tarla(x, y, 1) = CChar("b")
                    sender.BackgroundImage = Image.FromFile("bo�.bmp")
                    If ((bulunanmay�nsay�s� = may�nsay�s�) And (may�nsay�s� = konulan�nlemsay�s�)) Then

                        tebrikler()
                    End If

                End If
            End If
        End If
    End Sub

    'oyun ba�ar�l� bitti�inde yap�lacaklar
    Private Sub tebrikler()

        Me.BackColor = Color.Blue
        Timer1.Enabled = False
        oyunbitmedi = False
    End Sub

    'may�na bas�ld���nda yap�lacaklar
    Private Sub booom()

        oyunbitmedi = False
        Dim x, y As Integer
        For x = 0 To tarla.GetUpperBound(0)

            For y = 0 To tarla.GetUpperBound(1)

                kutua�(x, y)
            Next
        Next
        Timer1.Enabled = False
        Me.BackColor = Color.Red
    End Sub
#End Region

#Region "eventler"
    ''form1 y�klenirken yap�lacaklar :)))
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.BackColor = Color.LightGray
    End Sub


    'yeni oyun butonu
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Refresh()
        Label1.Text = "00:00"
        konulan�nlemsay�s� = 0
        bulunanmay�nsay�s� = 0
        Me.BackColor = Color.LightGray
        If (Button1.Text = "Yeni Oyun") Then
            temizle()

        End If
        Button1.Text = "Yeni Oyun"
        tarlaolu�tur(tarla.GetLength(0), tarla.GetLength(1))

        may�nla(may�nsay�s�)

        oyunbitmedi = True
        ba�lamazaman� = DateTime.Now
        Timer1.Enabled = True
        Me.Refresh()

    End Sub

    'oyuns�ras�nda ge�en zaman� g�steren k�s�m
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Dim ge�ensaniye As Integer
        ge�ensaniye = Convert.ToInt32(DateAndTime.DateDiff(DateInterval.Second, ba�lamazaman�, DateTime.Now, FirstDayOfWeek.System, FirstWeekOfYear.System))
        Dim dakika As String
        Dim saniye As String
        saniye = (ge�ensaniye Mod 60).ToString().PadLeft(2, CChar("0"))
        dakika = Convert.ToString(Math.Floor(Convert.ToDouble(ge�ensaniye / 60)).ToString().PadLeft(2, CChar("0")))
        Label1.Text = dakika + ":" + saniye
    End Sub
#End Region

   
End Class
