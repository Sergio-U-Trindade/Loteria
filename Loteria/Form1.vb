Imports Loteria
Public Class Form1

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim obj As New Loteria.Loterias.Loteria(Loterias.Loteria.Tipo_Loteria.MegaSena, {1, 2, 3, 4, 5, 6})
        Dim sng_preco As Single
        Dim tst As Integer
        Dim bolJogoOtimizado As Boolean
        Dim Arr_Sorteados() As Integer

        obj.Minimo = 1
        obj.Maximo = 60
        obj.Quantidade_Num_Aposta = 6
        obj.Quantidade_Aposta = 2
        sng_preco = obj.Preco_Aposta(3, 6)
        tst = obj.Arr_Numeros_Sorteio_Anterior(5)
        bolJogoOtimizado = obj.Jogo_Otimizado
        Arr_Sorteados = obj.Calculate_random

        MessageBox.Show(sng_preco)
        MessageBox.Show(tst)
        MessageBox.Show(bolJogoOtimizado).ToString()
        Dim i As Integer

        For i = 0 To UBound(Arr_Sorteados)
            MessageBox.Show(Arr_Sorteados(i))
        Next
    End Sub
End Class