Imports System.Data.SqlClient
Imports VBActive_Taegis_DLL.ActiveTaegisDLL


Public Class frmAlertasMes
    'Lista de alertas do mês
    '22/03/23-03/04/23
    '=======================
    Private _strMes As New String("")
    Private _strAno As New String("")
    Private _strClientName As New String("")

    Public Property strMes As String
        Get
            Return _strMes
        End Get

        Set(value As String)
            _strMes = value
        End Set

    End Property
    Public Property strAno As String
        Get
            Return _strAno
        End Get

        Set(value As String)
            _strAno = value
        End Set

    End Property

    Public Property strClientName As String
        Get
            Return _strClientName
        End Get

        Set(value As String)
            _strClientName = value
        End Set

    End Property

    Private Sub frmAlertasMes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Carga do form
        '22/03/23-15/09/23
        '=============
        ToolStripStatusLabel1.Text = CopyRight()
        CargaAlertasMes()

    End Sub

    Private Sub CargaAlertasMes()
        'Carga dos alertas do mês em lvwAlertasMes
        '22/03/23-23/03/23-10/04/23
        '=========================================
        Dim oCon As New SqlConnection(ConnectionString)
        Dim oCmd As New SqlCommand
        Dim oDrd As SqlDataReader
        Dim intMaxAlertas As Integer = Proj.MaiorQtdeAlertasMes(strMes, strAno, _strClientName)

        Me.Text = "Alertas do mês " & _strMes & "/" & _strAno & " - " & _strClientName

        Try
            oCon.Open()

            With oCmd
                .Connection = oCon
                .CommandType = CommandType.Text
                .CommandText = "SELECT count(*) Alertas, SUBSTRING(tta.alert_date, 7, 2) Alert_Date " &
                               "from dbo.t_taegis_alerts tta " &
                               "WHERE SUBSTRING(tta.alert_date, 5, 2) = '" & _strMes & "' AND SUBSTRING(tta.alert_date, 1, 4) = '" & _strAno & "' " &
                               "AND alert_client = '" & strClientName & "' " &
                               "GROUP BY tta.alert_date " &
                               "ORDER BY tta.alert_date"
                oDrd = .ExecuteReader
            End With

            lvwAlertasMes.Items.Clear()
            lvwAlertasMes.BeginUpdate()

            With oDrd
                Dim intAcum As Integer = 0
                Dim maxValue As Double = 0
                Dim intDias As Integer = 0
                Dim intLinha As Integer = 1

                While .Read
                    If intLinha = .Item("Alert_Date") Then
                        Dim oItem As New ListViewItem(.Item("Alert_Date").ToString)
                        oItem.SubItems.Add(FormatNumber(.Item("Alertas"), 0,,, GroupDigits:=TriState.True))
                        intAcum += .Item("Alertas")
                        oItem.SubItems.Add(FormatNumber(intAcum, 0,,, GroupDigits:=TriState.True))
                        If .Item("Alertas") = intMaxAlertas Then
                            oItem.ForeColor = Color.Red
                        End If
                        lvwAlertasMes.Items.AddRange(New ListViewItem() {oItem})
                    Else
                        Dim oItem As New ListViewItem(intLinha.ToString("00"))
                        oItem.SubItems.Add("-")
                        oItem.SubItems.Add("-")
                        lvwAlertasMes.Items.AddRange(New ListViewItem() {oItem})

                        'Linha em branco para dias sem valor
                        Dim oItem1 As New ListViewItem(.Item("Alert_Date").ToString)
                        oItem1.SubItems.Add(FormatNumber(.Item("Alertas"), 0,,, GroupDigits:=TriState.True))
                        intAcum += .Item("Alertas")
                        oItem1.SubItems.Add(FormatNumber(intAcum, 0,,, GroupDigits:=TriState.True))
                        If .Item("Alertas") = intMaxAlertas Then
                            oItem.ForeColor = Color.Red
                        End If
                        lvwAlertasMes.Items.AddRange(New ListViewItem() {oItem1})
                        intLinha += 1
                    End If
                    intLinha += 1
                End While

                intDias = lvwAlertasMes.Items.Count
                ToolStripStatusLabel2.Text = "Média de " & intDias & " dias: " & FormatNumber(intAcum / intDias, 0,,, GroupDigits:=TriState.True)
                .Close()
            End With

            lvwAlertasMes.EndUpdate()

        Catch ex As SqlException
            MessageBox.Show(ex.ToString, "Erro SQL", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Catch ex1 As Exception
            MessageBox.Show(ex1.ToString, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            oCon.Close()

        End Try

    End Sub

End Class