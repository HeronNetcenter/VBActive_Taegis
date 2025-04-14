Imports System.Data.SqlClient
Imports VBActive_Taegis_DLL.ActiveTaegisDLL

Public Class frmLogsMesGeral
    'Lista de logs do mês - geral - todos os clientes
    '20/04/23
    '===================================================
    Private _strMes As New String("")
    Private _strAno As New String("")

    Public Property strMesG As String
        Get
            Return _strMes
        End Get

        Set(value As String)
            _strMes = value
        End Set

    End Property

    Public Property strAnoG As String
        Get
            Return _strAno
        End Get

        Set(value As String)
            _strAno = value
        End Set

    End Property

    Private Sub frmLogssMesGeral_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Carga do form
        '20/04/23
        '=============
        ToolStripStatusLabel1.Text = CopyRight()
        CargaLogsMes()

    End Sub

    Private Sub CargaLogsMes()
        'Carga dos logs do mês em lvwAlertasMes
        '20/04/23
        '======================================
        Dim oCon As New SqlConnection(ConnectionString)
        Dim oCmd As New SqlCommand
        Dim oDrd As SqlDataReader
        Dim intMaxAlertas As Integer = Proj.MaiorQtdeAlertasMesGeral(strMesG, strAnoG)

        Me.Text = "Logs do mês " & _strMes & "/" & _strAno & " - Geral"

        'Try
        oCon.Open()

            With oCmd
                .Connection = oCon
                .CommandType = CommandType.Text
                .CommandText = "SELECT DISTINCT FORMAT(DAY(log.data_log), 'd2') + ';' + client_name + ';' + tipo_log data_log, qtde_inc Quantidade " &
                                       "from dbo.t_taegis_log log " &
                                       "WHERE MONTH(log.data_log) = " & CInt(_strMes) & " AND " &
                                       "YEAR(log.data_log) = " & CInt(_strAno)
                oDrd = .ExecuteReader
            End With

            lvwAlertasMes.Items.Clear()
            lvwAlertasMes.BeginUpdate()

            With oDrd
                Dim intAcum As Integer = 0
                Dim maxValue As Double = 0
                Dim intDias As Integer = 0
                Dim strDiaAnt As New String("")

                While .Read
                    Dim strDia As String = .Item("data_log").ToString.Split(";")(0)
                    Dim strClient As String = .Item("data_log").ToString.Split(";")(1)
                    Dim strTipo As String = .Item("data_log").ToString.Split(";")(2)
                    If .Item("Quantidade") > 0 And ((chkAlerts.Checked And strTipo = "Alerts" Or
                                                     chkAssets.Checked And strTipo = "Assets") Or
                                                     chkCollectors.Checked And strTipo = "Collectors" Or
                                                     chkInvestigations.Checked And strTipo = "Investigations") Then
                        Dim oItem As New ListViewItem(strDia)
                        oItem.SubItems.Add(strClient)
                        oItem.SubItems.Add(strTipo)
                        oItem.SubItems.Add(FormatNumber(.Item("Quantidade"), 0,,, GroupDigits:=TriState.True))
                        intAcum += .Item("Quantidade")
                        oItem.SubItems.Add(FormatNumber(intAcum, 0,,, GroupDigits:=TriState.True))
                        If (strDia Mod 2) > 0 Then
                            oItem.BackColor = SystemColors.GradientInactiveCaption
                        Else
                            oItem.BackColor = SystemColors.Info
                        End If
                        If .Item("Quantidade") = intMaxAlertas Then
                            oItem.ForeColor = Color.Red
                        End If
                        lvwAlertasMes.Items.AddRange(New ListViewItem() {oItem})
                        If strDia <> strDiaAnt Then
                            intDias += 1
                            strDiaAnt = strDia
                        End If
                    End If
                End While

                ToolStripStatusLabel2.Text = "Média de " & intDias & " dias: " & FormatNumber(intAcum / intDias, 0,,, GroupDigits:=TriState.True)
                .Close()
            End With

            lvwAlertasMes.EndUpdate()

            For i As Integer = 0 To lvwAlertasMes.Items.Count - 1
                If lvwAlertasMes.Items(i).ForeColor = Color.Red Then
                    lvwAlertasMes.Items(i).EnsureVisible()
                End If
            Next

        'Catch ex As SqlException
        '    MessageBox.Show(ex.ToString, "Erro SQL", MessageBoxButtons.OK, MessageBoxIcon.Error)

        'Catch ex1 As Exception
        '    MessageBox.Show(ex1.ToString, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)

        'Finally
        oCon.Close()

        'End Try

    End Sub

    Private Sub chkAlerts_CheckedChanged(sender As Object, e As EventArgs) Handles chkAlerts.CheckedChanged
        'Alteração de campo
        '24/04/23
        '==================
        ListaLogsMes()

    End Sub

    Private Sub chkAssets_CheckedChanged(sender As Object, e As EventArgs) Handles chkAssets.CheckedChanged
        'Alteração de campo
        '24/04/23
        '==================
        ListaLogsMes()

    End Sub

    Private Sub chkCollectors_CheckedChanged(sender As Object, e As EventArgs) Handles chkCollectors.CheckedChanged
        'Alteração de campo
        '24/04/23
        '==================
        ListaLogsMes()

    End Sub

    Private Sub chkInvestigations_CheckedChanged(sender As Object, e As EventArgs) Handles chkInvestigations.CheckedChanged
        'Alteração de campo
        '24/04/23
        '==================
        ListaLogsMes()

    End Sub

    Private Sub ListaLogsMes()
        'Lista dos logs do mês se um ou mais checkbox estiverem marcados
        '24/04/23
        '===============================================================
        If IsNumeric(_strAno) And (chkAlerts.Checked Or chkAssets.Checked Or chkCollectors.Checked Or chkInvestigations.Checked) Then
            CargaLogsMes()
        Else
            lvwAlertasMes.Items.Clear()
        End If

    End Sub

End Class