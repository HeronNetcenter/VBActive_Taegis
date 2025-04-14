Imports System.Data.SqlClient
Imports VBActive_Taegis_DLL.ActiveTaegisDLL

Public Class frmAlertasMesGeral
    'Lista de alertas do mês - geral - todos os clientes
    '06/04/23
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
    Private Sub frmAlertasMesGeral_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Carga do form
        '06/04/23
        '=============
        ToolStripStatusLabel1.Text = CopyRight()
        CargaAlertasMes()

    End Sub

    Private Sub CargaAlertasMes()
        'Carga dos alertas do mês em lvwAlertasMes
        '06/04/23
        '=========================================
        Dim oCon As New SqlConnection(ConnectionString)
        Dim oCmd As New SqlCommand
        Dim oDrd As SqlDataReader
        Dim intMaxAlertas As Integer = Proj.MaiorQtdeAlertasMesGeral(strMesG, strAnoG)

        Me.Text = "Alertas do mês " & _strMes & "/" & _strAno & " - Geral"

        Try
            oCon.Open()

            With oCmd
                .Connection = oCon
                .CommandType = CommandType.Text
                .CommandText = "SELECT SUBSTRING(tta.alert_date, 7, 2) + alert_client Alert_Date, count(*) Alertas " &
                                   "from dbo.t_taegis_alerts tta " &
                                   "WHERE SUBSTRING(tta.alert_date, 5, 2) = " & _strMes & " AND SUBSTRING(tta.alert_date, 1, 4) = " & _strAno & " " &
                                   "GROUP BY SUBSTRING(tta.alert_date, 7, 2) + alert_client"
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
                    Dim strDia As String = .Item("Alert_Date").ToString.Substring(0, 2)
                    Dim strClient As String = .Item("Alert_Date").ToString.Substring(2)

                    Dim oItem As New ListViewItem(strDia)
                    oItem.SubItems.Add(strClient)
                    oItem.SubItems.Add(FormatNumber(.Item("Alertas"), 0,,, GroupDigits:=TriState.True))
                    intAcum += .Item("Alertas")
                    oItem.SubItems.Add(FormatNumber(intAcum, 0,,, GroupDigits:=TriState.True))
                    If (strDia Mod 2) > 0 Then
                        oItem.BackColor = SystemColors.GradientInactiveCaption
                    Else
                        oItem.BackColor = SystemColors.Info
                    End If
                    If .Item("alertas") = intMaxAlertas Then
                        oItem.ForeColor = Color.Red
                    End If
                    lvwAlertasMes.Items.AddRange(New ListViewItem() {oItem})
                    If strDia <> strDiaAnt Then
                        intDias += 1
                        strDiaAnt = strDia
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

        Catch ex As SqlException
            MessageBox.Show(ex.ToString, "Erro SQL", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Catch ex1 As Exception
            MessageBox.Show(ex1.ToString, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            oCon.Close()

        End Try

    End Sub

End Class