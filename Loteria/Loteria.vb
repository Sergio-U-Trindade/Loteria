Namespace Loteria
    Namespace Funcoes
        Public Class Loteria
#Region "Variáveis públicas"
            Public Arr_Num() As Integer
            Public Quant_Apostas As Integer = 1
#End Region
#Region "Construtores"
            Public Sub New()
                MyBase.New()
            End Sub
#End Region
#Region "Enumerações"
            Public Enum Tipo_Loteria
                MegaSena = 1
                Quina = 2
                Lotofacil = 3
                Lotomania = 4
                DuplaSena = 5
            End Enum
#End Region
#Region "Propriedades"
            Private _Min As Integer
            Public Property Min() As Integer
                Get
                    Return _Min
                End Get
                Set(ByVal value As Integer)
                    _Min = value
                End Set
            End Property
            Private _Max As Integer
            Public Property Max() As Integer
                Get
                    Return _Max
                End Get
                Set(ByVal value As Integer)
                    _Max = value
                End Set
            End Property
            Private _Quant_Aposta As Integer
            Public Property Quant_Aposta() As Integer
                Get
                    Return _Quant_Aposta
                End Get
                Set(ByVal value As Integer)
                    _Quant_Aposta = value
                End Set
            End Property
            Private _Quant_Dezenas As Integer
            Public Property Quant_Dezenas() As Integer
                Get
                    Return _Quant_Dezenas
                End Get
                Set(ByVal value As Integer)
                    _Quant_Dezenas = value
                End Set
            End Property
            Private _Formulario As Form
            Public Property Formulario() As Form
                Get
                    Return _Formulario
                End Get
                Set(ByVal value As Form)
                    _Formulario = value
                End Set
            End Property

#End Region
#Region "Funções"
            Private Function calculate_random(Quant_Dezenas As Integer) As Integer()
                'Define o array de acordo com a quantidade de números escolhidos
                ReDim Preserve Arr_Num(Quant_Dezenas - 1)
                Dim j As Integer = 0
                For i As Integer = 0 To Quant_Dezenas - 1
                    j = Calc_Random(Min, Max + 1)
                    Arr_Num(i) = j
                    j = 0
                Next
                Return Arr_Num
            End Function
            Public Function Calc_Random(Min As Integer, Max As Integer) As Integer
                Dim int_calculado As Integer
                Dim objForm As New Form
                objForm = Formulario
                'Chama a função que retornará o numero sorteado passando como parâmetros o número mínimo e o máximo
                int_calculado = GetRandom(Min, Max + 1)
                'Verifica de no array já existe um número igual ao sorteado ou se o número sorteado é igual a zero, se sim chama recursivamente
                'a função para sortear um novo número
                If Arr_Num.Contains(int_calculado) Or int_calculado = 0 Then
                    int_calculado = Calc_Random(Min, Max + 1)
                End If
                'verfica se a quantidade de apostas é igual a 1 caso seja, atualiza os labels com os números das apostas
                If Quant_Apostas = 1 Then
                    Refresh_Numbers_Select(objForm, int_calculado)
                End If
                'Retorna o número sorteado 
                Return int_calculado
            End Function
            Public Shared Function GetRandom(ByVal Min As Integer, ByVal Max As Integer) As Integer
                Static Generator As System.Random = New System.Random()
                Return Generator.Next(Min, Max)
            End Function
            Public Shared Function Preco_Aposta(Tipo As Tipo_Loteria, Quant_Aposta As Integer, Quant_Dezenas As Integer)
                Dim Sng_preco As Single
                Select Case Tipo
                    Case Is = Tipo_Loteria.Lotofacil
                        If Quant_Dezenas = 15 Then
                            Sng_preco = Quant_Aposta * 1.5
                        ElseIf Quant_Dezenas = 16 Then
                            Sng_preco = Quant_Aposta * 24
                        ElseIf Quant_Dezenas = 17 Then
                            Sng_preco = Quant_Aposta * 204
                        ElseIf Quant_Dezenas = 18 Then
                            Sng_preco = Quant_Aposta * 1224
                        End If
                    Case Is = Tipo_Loteria.Lotomania
                        Sng_preco = Quant_Aposta * 1.5
                    Case Is = Tipo_Loteria.MegaSena
                        If Quant_Dezenas = 6 Then
                            Sng_preco = Quant_Aposta * 2.5
                        ElseIf Quant_Dezenas = 7 Then
                            Sng_preco = Quant_Aposta * 17.5
                        ElseIf Quant_Dezenas = 8 Then
                            Sng_preco = Quant_Aposta * 70
                        ElseIf Quant_Dezenas = 9 Then
                            Sng_preco = Quant_Aposta * 210
                        ElseIf Quant_Dezenas = 10 Then
                            Sng_preco = Quant_Aposta * 525
                        ElseIf Quant_Dezenas = 11 Then
                            Sng_preco = Quant_Aposta * 1155
                        ElseIf Quant_Dezenas = 12 Then
                            Sng_preco = Quant_Aposta * 2310
                        ElseIf Quant_Dezenas = 13 Then
                            Sng_preco = Quant_Aposta * 4290
                        ElseIf Quant_Dezenas = 14 Then
                            Sng_preco = Quant_Aposta * 7507
                        ElseIf Quant_Dezenas = 15 Then
                            Sng_preco = Quant_Aposta * 12512
                        End If
                    Case Is = Tipo_Loteria.Quina
                        If Quant_Dezenas = 5 Then
                            Sng_preco = Quant_Aposta * 1
                        ElseIf Quant_Dezenas = 6 Then
                            Sng_preco = Quant_Aposta * 4
                        ElseIf Quant_Dezenas = 7 Then
                            Sng_preco = Quant_Aposta * 10
                        End If
                    Case Is = Tipo_Loteria.DuplaSena
                        If Quant_Dezenas = 6 Then
                            Sng_preco = Quant_Aposta * 1
                        ElseIf Quant_Dezenas = 7 Then
                            Sng_preco = Quant_Aposta * 7
                        ElseIf Quant_Dezenas = 8 Then
                            Sng_preco = Quant_Aposta * 28
                        ElseIf Quant_Dezenas = 9 Then
                            Sng_preco = Quant_Aposta * 84
                        ElseIf Quant_Dezenas = 10 Then
                            Sng_preco = Quant_Aposta * 210
                        ElseIf Quant_Dezenas = 11 Then
                            Sng_preco = Quant_Aposta * 462
                        ElseIf Quant_Dezenas = 12 Then
                            Sng_preco = Quant_Aposta * 924
                        ElseIf Quant_Dezenas = 13 Then
                            Sng_preco = Quant_Aposta * 1716
                        ElseIf Quant_Dezenas = 14 Then
                            Sng_preco = Quant_Aposta * 3003
                        ElseIf Quant_Dezenas = 15 Then
                            Sng_preco = Quant_Aposta * 5005
                        End If
                End Select
                Return Sng_preco
            End Function
#End Region
#Region "SubRotinas"
            Public Shared Sub ResetAllControlsBackColor(control As Control)
                'Determina a propriedade BackColor do controle
                Control.BackColor = Color.White
                'Determina a propriedade Forecolor do controle
                Control.ForeColor = SystemColors.HotTrack
                'Verifica se o controle passado como parâmetro possui um ou mais filhos
                If Control.HasChildren Then
                    'Chama o método recursivamente para cada controle filho. 
                    Dim childControl As Control
                    For Each childControl In Control.Controls
                        'Verifica se o tipo de controle é um Label
                        If TypeOf childControl Is Label Then
                            'Verifica se o nome do controle se inicia com a expressão Lbl
                            If Mid(childControl.Name, 1, 3) = "Lbl" Then
                                'Retorna o controle para o BackColor e ForeColor especificádos
                                ResetAllControlsBackColor(childControl)
                            End If
                        End If
                    Next childControl
                End If
            End Sub
            Private Sub Refresh_Numbers_Select(Formulario As Form, Int_Number As Integer)
                'Altera a propriedade Backcolor dos labels de acordo com o parâmetro

                For Each ctrl As Control In Formulario.Controls
                    If TypeOf (ctrl) Is Label And ctrl.Name = "Lbl" & Int_Number.ToString Then
                        ctrl.BackColor = Color.Red
                        ctrl.ForeColor = Color.White
                    End If
                Next
                Formulario.Refresh()
            End Sub
#End Region
        End Class
    End Namespace
End Namespace