Namespace Loterias
    Public Class Loteria
#Region "Variáveis públicas"
        Public Arr_Num() As Integer
        Public Arr_Numeros_Sorteio_Anterior() As Integer
#End Region
#Region "Construtores"
        Public Sub New()
            MyBase.New()
        End Sub
        Public Sub New(ByVal Tipo As Tipo_Loteria, Optional Arr_Sort_Ant() As Integer = Nothing)
            m_Tipo = Tipo
            If Not IsNothing(Arr_Sort_Ant) Then
                Arr_Numeros_Sorteio_Anterior = Arr_Sort_Ant
                m_jogoOtimizado = True
            Else
                m_jogoOtimizado = False
            End If
        End Sub
#End Region
#Region "Enumerações"
        Public Enum Tipo_Loteria
            MegaSena
            Quina
            Lotofacil
            Lotomania
            DuplaSena
        End Enum
#End Region
#Region "Propriedades"
        Private m_iMinimo As Integer
        Private m_iMaximo As Integer
        Private m_iFormulario As Form
        Private m_Quantidade_Aposta As Integer
        Private m_Quantidade_Num_Aposta As Integer
        Private m_jogoOtimizado As Boolean
        Private m_Tipo As Tipo_Loteria
        ''' <summary>
        ''' Propriedade que define o tipo de loteria
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Tipo() As Tipo_Loteria
            Get
                Return m_Tipo
            End Get
            Set(ByVal value As Tipo_Loteria)
                m_Tipo = value
            End Set
        End Property
        Public Property Jogo_Otimizado() As Boolean
            Get
                Return m_jogoOtimizado
            End Get
            Set(ByVal value As Boolean)
                m_jogoOtimizado = value
            End Set
        End Property
        ''' <summary>
        ''' Utilizada uma propriedade auto implementada para a definição do formulário
        ''' Uma vez que a mesma não necessita de lógica adicional
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Property Formulario As Form
        ''' <summary>
        ''' Propriedade responsável pela quantidade de apostas a serem sorteadas
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Quantidade_Aposta() As Integer
            Get
                Return m_Quantidade_Aposta
            End Get
            Set(ByVal value As Integer)
                If Not IsNumeric(value) Or value = 0 Then
                    Throw New Exception("O Valor deve ser um inteiro maior ou igual a 1")
                Else
                    Me.m_Quantidade_Aposta = value
                End If
                m_Quantidade_Aposta = value
            End Set
        End Property
        ''' <summary>
        ''' Propriedade responsável por controlar o menor número a ser levado em consideração para o sorteio
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Minimo() As Integer
            Get
                Return m_iMinimo
            End Get
            Set(ByVal value As Integer)
                If Not IsNumeric(value) Or value = 0 Then
                    Throw New Exception("O Valor deve ser um inteiro maior ou igual a 1")
                Else
                    Me.m_iMinimo = value
                End If
                m_iMinimo = value
            End Set
        End Property
        ''' <summary>
        ''' Propriedade responsável por controlar o maior número a ser levado em consideração para o sorteio
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Maximo() As String
            Get
                Return m_iMaximo
            End Get
            Set(ByVal value As String)
                If Not IsNumeric(value) Or value = 0 Then
                    Throw New Exception("O Valor deve ser um inteiro maior ou igual a 1")
                Else
                    m_iMaximo = value
                End If
            End Set
        End Property
        ''' <summary>
        ''' Propriedade responsável por controlar a quantidade de números por aposta
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Quantidade_Num_Aposta() As Integer
            Get
                Return m_Quantidade_Num_Aposta
            End Get
            Set(ByVal value As Integer)
                If Not IsNumeric(value) Or value = 0 Then
                    Throw New Exception("O Valor deve ser um inteiro maior ou igual a 1")
                Else
                    Me.m_Quantidade_Num_Aposta = value
                End If
                m_Quantidade_Num_Aposta = value
            End Set
        End Property
#End Region
#Region "Métodos - Funções"
        ''' <summary>
        ''' Método responsável pelo sorteio dos números levando em consideração o número mínimo e o número máximo
        ''' Leva em consideração também, caso seja informado, os números do sorteio anterior
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Calculate_random() As Integer()
            'Public Function Calculate_random(m_Quantidade_Num_Aposta As Integer) As Integer()
            'Define o array de acordo com a quantidade de números escolhidos
            ReDim Preserve Arr_Num(m_Quantidade_Num_Aposta - 1)
            Dim j As Integer = 0
            For i As Integer = 0 To m_Quantidade_Num_Aposta - 1
                j = Calc_Random(Minimo, Maximo + 1)
                Arr_Num(i) = j
                j = 0
            Next
            Return Arr_Num
        End Function
        Private Function Calc_Random(Min As Integer, Max As Integer) As Integer
            Dim int_calculado As Integer
            Dim objForm As New Form
            objForm = Formulario
            'Chama a função que retornará o numero sorteado passando como parâmetros o número mínimo e o máximo
            int_calculado = GetRandom(Min, Max + 1)
            'Verifica se no array já existe um número igual ao sorteado ou se o número sorteado é igual a zero ou 
            'se o número é igual a um dos números sorteados no sorteio anterior, se sim chama recursivamente a função para sortear um novo número
            If Not IsNothing(Arr_Numeros_Sorteio_Anterior) Then
                If Arr_Num.Contains(int_calculado) Or int_calculado = 0 Or Arr_Numeros_Sorteio_Anterior.Contains(int_calculado) Then
                    int_calculado = Calc_Random(Min, Max + 1)
                End If
            Else
                If Arr_Num.Contains(int_calculado) Or int_calculado = 0 Then
                    int_calculado = Calc_Random(Min, Max + 1)
                End If
            End If
            Return int_calculado
        End Function
        Private Function GetRandom(ByVal Min As Integer, ByVal Max As Integer) As Integer
            Static Generator As System.Random = New System.Random()
            Return Generator.Next(Min, Max)
        End Function
        Public Function Preco_Aposta(m_Quantidade_Aposta As Integer, m_Quantidade_Num_Aposta As Integer) As Single
            Dim Sng_preco As Single
            Select Case m_Tipo
                Case Is = Tipo_Loteria.Lotofacil
                    If m_Quantidade_Num_Aposta = 15 Then
                        Sng_preco = m_Quantidade_Aposta * 1.5
                    ElseIf m_Quantidade_Num_Aposta = 16 Then
                        Sng_preco = m_Quantidade_Aposta * 24
                    ElseIf m_Quantidade_Num_Aposta = 17 Then
                        Sng_preco = m_Quantidade_Aposta * 204
                    ElseIf m_Quantidade_Num_Aposta = 18 Then
                        Sng_preco = m_Quantidade_Aposta * 1224
                    End If
                Case Is = Tipo_Loteria.Lotomania
                    Sng_preco = m_Quantidade_Aposta * 1.5
                Case Is = Tipo_Loteria.MegaSena
                    If m_Quantidade_Num_Aposta = 6 Then
                        Sng_preco = m_Quantidade_Aposta * 2.5
                    ElseIf m_Quantidade_Num_Aposta = 7 Then
                        Sng_preco = m_Quantidade_Aposta * 17.5
                    ElseIf m_Quantidade_Num_Aposta = 8 Then
                        Sng_preco = m_Quantidade_Aposta * 70
                    ElseIf m_Quantidade_Num_Aposta = 9 Then
                        Sng_preco = m_Quantidade_Aposta * 210
                    ElseIf m_Quantidade_Num_Aposta = 10 Then
                        Sng_preco = m_Quantidade_Aposta * 525
                    ElseIf m_Quantidade_Num_Aposta = 11 Then
                        Sng_preco = m_Quantidade_Aposta * 1155
                    ElseIf m_Quantidade_Num_Aposta = 12 Then
                        Sng_preco = m_Quantidade_Aposta * 2310
                    ElseIf m_Quantidade_Num_Aposta = 13 Then
                        Sng_preco = m_Quantidade_Aposta * 4290
                    ElseIf m_Quantidade_Num_Aposta = 14 Then
                        Sng_preco = m_Quantidade_Aposta * 7507
                    ElseIf m_Quantidade_Num_Aposta = 15 Then
                        Sng_preco = m_Quantidade_Aposta * 12512
                    End If
                Case Is = Tipo_Loteria.Quina
                    If m_Quantidade_Num_Aposta = 5 Then
                        Sng_preco = m_Quantidade_Aposta * 1
                    ElseIf m_Quantidade_Num_Aposta = 6 Then
                        Sng_preco = m_Quantidade_Aposta * 4
                    ElseIf m_Quantidade_Num_Aposta = 7 Then
                        Sng_preco = m_Quantidade_Aposta * 10
                    End If
                Case Is = Tipo_Loteria.DuplaSena
                    If m_Quantidade_Num_Aposta = 6 Then
                        Sng_preco = m_Quantidade_Aposta * 1
                    ElseIf m_Quantidade_Num_Aposta = 7 Then
                        Sng_preco = m_Quantidade_Aposta * 7
                    ElseIf m_Quantidade_Num_Aposta = 8 Then
                        Sng_preco = m_Quantidade_Aposta * 28
                    ElseIf m_Quantidade_Num_Aposta = 9 Then
                        Sng_preco = m_Quantidade_Aposta * 84
                    ElseIf m_Quantidade_Num_Aposta = 10 Then
                        Sng_preco = m_Quantidade_Aposta * 210
                    ElseIf m_Quantidade_Num_Aposta = 11 Then
                        Sng_preco = m_Quantidade_Aposta * 462
                    ElseIf m_Quantidade_Num_Aposta = 12 Then
                        Sng_preco = m_Quantidade_Aposta * 924
                    ElseIf m_Quantidade_Num_Aposta = 13 Then
                        Sng_preco = m_Quantidade_Aposta * 1716
                    ElseIf m_Quantidade_Num_Aposta = 14 Then
                        Sng_preco = m_Quantidade_Aposta * 3003
                    ElseIf m_Quantidade_Num_Aposta = 15 Then
                        Sng_preco = m_Quantidade_Aposta * 5005
                    End If
            End Select
            Return Sng_preco
        End Function
#End Region
#Region "Métodos - SubRotinas"
        Public Shared Sub ResetAllControlsBackColor(control As Control)
            'Determina a propriedade BackColor do controle
            control.BackColor = Color.White
            'Determina a propriedade Forecolor do controle
            control.ForeColor = SystemColors.HotTrack
            'Verifica se o controle passado como parâmetro possui um ou mais filhos
            If control.HasChildren Then
                'Chama o método recursivamente para cada controle filho. 
                Dim childControl As Control
                For Each childControl In control.Controls
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
        Public Shared Sub Refresh_Numbers_Select(Formulario As Form, Int_Number As Integer)
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