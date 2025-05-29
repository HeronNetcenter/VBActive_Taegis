Imports System.Data.SqlClient
Imports System.IO
Imports Microsoft.VisualBasic.FileIO
Imports VBActive_Taegis_DLL.ActiveTaegisDLL
Imports OfficeOpenXml
Imports System.Net.Http
Imports System.Text.RegularExpressions
Imports System.Threading.Tasks
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class Form1
    'Sistema Active - VS2022
    'Módulo: Acionamento dos alertas Taegis
    'Obs: There is so much info about how to execute pything script, but none of them saying one simple thing:
    '     that the script must be with the same directory as your project.
    'Heron Jr
    '08/02/23-21/03/23-31/03/23
    '07/03/24
    '13/03/25
    '17/03/25-18/03/25
    '==========================================================================================================
    Private fd As New FolderBrowserDialog
    Private _strPastaProj As New String("")
    Private _strSaida As New String("")
    Private _strPastaPython As New String("")
    Private _intLinhasSQL_Inc As Integer = 0
    Private _intLinhasSQL_Alt As Integer = 0
    Private _intLinhasLidas As Integer = 0
    Private _datExtracao1 As Date
    Private _datExtracao2 As Date
    Private _today As Date = Date.Today
    Private _aTenantId As New List(Of String)
    Private _aClientId As New List(Of String)
    Private _aClientSecret As New List(Of String)
    Private _intClientIndex As Integer = 0
    Private _intAlerts As Integer = 0
    Private _intAttachTT As Integer = 0
    Private _intAlertId As Integer = 0
    Private _blnJsonOK As Boolean = False
    Private _intLinhasJson As Integer = 0

    '17/03/25: COLETAS - MENSAL, QUINZENAL E DE 10 EM 10 DIAS
    Private _datFirstDayLastMonth As Date = New Date(_today.Year, _today.Month, 1).AddMonths(-1)
    Private _strMesAnterior As New String(Strings.Left(MonthName(_datFirstDayLastMonth.Month), 3).ToUpper & "/" & _datFirstDayLastMonth.Year)
    Private _strDiasProcesso As New String("0") 'OPÇÃO DEFAULT = PROCCESSO PARA O MÊS INTEIRO
    Private _strMensagemProcesso As New String("Processando o mês inteiro... Aguarde" & vbCrLf)

    Public Value As String

#Region "Main"

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Carga do form
        '09/02/23-21/03/23-30/03/23-12/09/23-17/10/23
        '30/12/24
        '18/03/25
        '============================================
        Dim strPastas As New String(LeConfig())

        Me.Text = Proj.VersaoAuto1()
        ToolStripStatusLabel1.Text = CopyRight()

        CargaComboClientes()
        _intClientIndex = cboClientes.Items.IndexOf("NETCENTER")
        cboClientes.SelectedIndex = _intClientIndex

        _strPastaPython = strPastas.Split(";")(0)
        '================================================================================================================
        _strPastaProj = strPastas.Split(";")(1) '.Replace("Release", "Debug")   'Replace para ser somente usado em testes
        '================================================================================================================
        txtPastaPython.Text = _strPastaPython
        txtPastaProj.Text = _strPastaProj
        _datExtracao1 = DateAdd(DateInterval.Day, -1, dtpDataExtracao.Value.Date)
        _datExtracao2 = dtpDataExtracao.Value.Date
        grpMesAnterior.Text = "Opções para " & _strMesAnterior
        ExcluiArquivosAnteriores()
        btnAlerts1.Enabled = False
        txtMensagens.Text = Space(30) & "****** SELECIONE UM CLIENTE ******" & vbCrLf
        cboClientes.Select()
        txtMensagens.ForeColor = Color.Red

    End Sub

    Private Sub CargaComboClientes()
        'Carga do combo de clientes cboClientes
        '30/03/23
        '======================================
        Dim oCon As New SqlConnection(ConnectionString)
        Dim oCmd As New SqlCommand
        Dim oDrd As SqlDataReader = Nothing

        oCon.Open()

        With oCmd
            .Connection = oCon
            .CommandType = CommandType.Text
            .CommandText = "select client_name, tenant_id, client_id, client_secret from t_taegis_tab_clients"
            .CommandTimeout = 3600
            oDrd = .ExecuteReader
        End With

        With oDrd

            While .Read
                cboClientes.Items.Add(.Item("client_name"))
                _aTenantId.Add(.Item("tenant_id"))
                _aClientId.Add(.Item("client_id"))
                _aClientSecret.Add(.Item("client_secret"))
            End While

            .Close()
        End With

        oCon.Close()

    End Sub

#End Region

#Region "Alerts1"

    Private Sub btnAlerts1_Click(sender As Object, e As EventArgs) Handles btnAlerts1.Click
        'Botão Alerts1 - Grava arquivo alerts01_aaaammdd.json
        '08/02/23-13/02/23-21/03/23-31/03/23
        '13/03/25: AMPLIAÇÃO DO PERÍODO DE COLETA - SUBSTITUI "yesterday" POR UM PERÍODO ANTERIOR DE 3 MESES _datfirst_day_3_months_ago
        '16/03/25: CONTINUAM OS TESTES COM CHATGPT-IA PARA PERÍODOS MENORES DO QUE 3 MESES - coleta de 10 em 10 dias no mês anterior
        '17/03/25: COLETAS - MENSAL, QUINZENAL E DE 10 EM 10 DIAS
        '==============================================================================================================================
        Cursor = Cursors.WaitCursor
        Dim oProcess As Process = New Process()
        Dim oStartInfo As New ProcessStartInfo(_strPastaPython, If(_datExtracao2 = Now.Date, "alerts01.py ", "alerts_par01.py " &
                                               _datExtracao1.Year & "-" & _datExtracao1.Month.ToString("00") & "-" & _datExtracao1.Day.ToString("00") &
                                               " " & _datExtracao2.Year & "-" & _datExtracao2.Month.ToString("00") & "-" & _datExtracao2.Day.ToString("00") & " ") &
                                               _aTenantId(_intClientIndex) & " " & _aClientId(_intClientIndex) & " " & _aClientSecret(_intClientIndex) &
                                               " " & _strDiasProcesso) 'ESTE PARÂMETRO É O DIA INICIAL DO MÊS ANTERIOR
        'Dim strSaida As New String("alerts01_" & _aTenantId(_intClientIndex) & "_" & _datExtracao1.ToString("yyyyMMdd") & ".json")
        Dim strSaida As New String("alerts01_" & _aTenantId(_intClientIndex) & "_" & _datFirstDayLastMonth.ToString("yyyyMMdd") & ".json")
        Dim strLinhas() As String

        txtMensagens.Text &= _strMensagemProcesso
        Application.DoEvents()

        'VARIÁVEL PARA DEBUG - 16/03/25
        'txtMensagens.Text = _strPastaPython & " " & If(_datExtracao2 = Now.Date, "alerts01.py ", "alerts_par01.py " &
        '                                       _datExtracao1.Year & "-" & _datExtracao1.Month.ToString("00") & "-" & _datExtracao1.Day.ToString("00") &
        '                                       " " & _datExtracao2.Year & "-" & _datExtracao2.Month.ToString("00") & "-" & _datExtracao2.Day.ToString("00") & " ") &
        '                                       _aTenantId(_intClientIndex) & " " & _aClientId(_intClientIndex) & " " & _aClientSecret(_intClientIndex) &
        '                                       " 1" & vbCrLf 'ESTE PARÂMETRO É O DIA INICIAL DO MÊS ANTERIOR

        oStartInfo.UseShellExecute = False
        oStartInfo.CreateNoWindow = True
        oStartInfo.RedirectStandardOutput = True
        oProcess.StartInfo = oStartInfo
        oProcess.Start()

        Dim sOutput As String

        Using oStreamReader As System.IO.StreamReader = oProcess.StandardOutput
            sOutput = oStreamReader.ReadToEnd
            strLinhas = sOutput.Split(New String() {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries)
        End Using

        'txtMensagens.Text = sOutput
        Cursor = Cursors.Default
        'txtMensagens.Text &= "1) Arquivo JSON " & strSaida & " gravado com sucesso - Linhas: " & strLinhas.Length & vbCrLf
        'btnAlerts2.Enabled = System.IO.File.Exists(strSaida)

        CountJsonLines(strSaida)
        If _blnJsonOK Then
            txtMensagens.Text &= "1) Arquivo JSON " & strSaida & " gravado com sucesso - Linhas: " & _intLinhasJson & vbCrLf
            btnAlerts2.Enabled = True
            btnAlerts1.Enabled = False
            btnReinicio.Enabled = True
        End If
        oProcess.Dispose()
        oProcess = Nothing

        'Salta para o final de txtMensagens
        SaltaParaFimDaMensagem()

    End Sub

#End Region

#Region "Alerts2"

    Private Sub btnAlerts2_Click(sender As Object, e As EventArgs) Handles btnAlerts2.Click
        'Botão Alerts2 - Grava arquivo alerts02_aaaammdd.csv
        '13/02/23-21/03/23-31/03/23-08/12/23
        '13/03/25: AMPLIAÇÃO DO PERÍODO DE COLETA - SUBSTITUI "yesterday" POR UM PERÍODO ANTERIOR DE 3 MESES first_day_3_months_ago 
        '16/03/25: CONTINUAM OS TESTES COM CHATGPT-IA PARA PERÍODOS MENORES DO QUE 3 MESES - coleta de 10 em 10 dias no mês anterior
        '17/03/25: COLETAS - MENSAL, QUINZENAL E DE 10 EM 10 DIAS
        '===========================================================================================================================
        Cursor = Cursors.WaitCursor
        Dim oProcess As Process = New Process()
        Dim oStartInfo As New ProcessStartInfo(_strPastaPython, If(_datExtracao2 = Now.Date, "alerts02.py ", "alerts_par02.py " &
                                               _datExtracao1.Year & "-" & _datExtracao1.Month.ToString("00") & "-" & _datExtracao1.Day.ToString("00") & " ") &
                                               _aTenantId(_intClientIndex) &
                                               " " & _strDiasProcesso) 'ESTE PARÂMETRO É O DIA INICIAL DO MÊS ANTERIOR
        'Dim strSaida As New String("alerts02_" & _aTenantId(_intClientIndex) & "_" & _datExtracao1.ToString("yyyyMMdd") & ".csv")
        Dim strSaida As New String("alerts02_" & _aTenantId(_intClientIndex) & "_" & _datFirstDayLastMonth.ToString("yyyyMMdd") & ".csv")
        Dim strLinhas() As String

        Dim strStartInfo As New String(_strPastaPython & " " & If(_datExtracao2 = Now.Date, "alerts02.py ", "alerts_par02.py " &
                                               _datExtracao1.Year & "-" & _datExtracao1.Month.ToString("00") & "-" & _datExtracao1.Day.ToString("00") & " ") &
                                               _aTenantId(_intClientIndex) &
                                               " 1") 'ESTE PARÂMETRO É O DIA INICIAL DO MÊS ANTERIOR

        oStartInfo.UseShellExecute = False
        oStartInfo.CreateNoWindow = True
        oStartInfo.RedirectStandardOutput = True
        oProcess.StartInfo = oStartInfo
        oProcess.Start()

        Dim sOutput As String

        Using oStreamReader As System.IO.StreamReader = oProcess.StandardOutput
            sOutput = oStreamReader.ReadToEnd
            strLinhas = sOutput.Split(New String() {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries)
        End Using

        'txtMensagens.Text = sOutput
        Cursor = Cursors.Default
        'txtMensagens.Text &= "2) Arquivo CSV " & strSaida & " gravado com sucesso - Linhas: " & strLinhas.Length & vbCrLf
        txtMensagens.Text &= "2) Arquivo CSV " & strSaida & " gravado com sucesso - Linhas: " & File.ReadAllLines(_strPastaProj & "\" & strSaida).Length & vbCrLf
        btnSQL.Enabled = System.IO.File.Exists(strSaida)
        btnAlerts2.Enabled = False
        oProcess.Dispose()
        oProcess = Nothing

        'Salta para o final de txtMensagens
        SaltaParaFimDaMensagem()

    End Sub

#End Region

#Region "SQL"

    Private Sub btnSQL_Click(sender As Object, e As EventArgs) Handles btnSQL.Click
        'Botão SQL - Grava tabela SQL t_taegis_alerts a partir do arquivo alerts02_aaaammdd.csv
        '13/02/23-17/02/23-27/02/23-21/03/23-22/03/23-31/03/23-04/04/23-14/10/23-11/12/23
        '27/12/24
        '13/03/25: AMPLIAÇÃO DO PERÍODO DE COLETA - SUBSTITUI "yesterday" POR UM PERÍODO ANTERIOR DE 3 MESES first_day_3_months_ago
        '17/03/25: AUTOMATIZAÇÃO DO PROCESSO QUINZENAL
        '27/03/25: INCLUSÃO DO CAMPO metadata_first_investigated_at NA TABELA SQL t_taegis_alerts
        '==========================================================================================================================
        '01 - attack_technique
        '02 - entities
        '03 - ent_relationships
        '04 - id
        '05 - investigation_ids
        '06 - metadata_confidence
        '07 - metadata_created_at
        '08 - metadata_creator_detector_id
        '09 - metadata_creator_detector_version
        '10 - metadata_creator_rule_id
        '11 - metadata_creator_rule_version
        '12 - metadata_engine_name
        '13 - metadata_severity
        '14 - metadata_title
        '15 - sensor_types
        '16 - status
        '17 - suppressed
        '18 - suppressed_rules
        '19 - tactics_technique
        '20 - metadata_first_resolved_at
        '21 - metadata_first_investigated_at

        Cursor = Cursors.WaitCursor
        'Dim strData As New String(_datExtracao1.ToString("yyyyMMdd"))
        'Dim strSaida As New String("\alerts02_" & _aTenantId(_intClientIndex) & "_" & _datExtracao1.ToString("yyyyMMdd") & ".csv")
        Dim strData As String = _datFirstDayLastMonth.ToString("yyyyMMdd")
        Dim strSaida As String = "\alerts02_" & _aTenantId(_intClientIndex) & "_" & _datFirstDayLastMonth.ToString("yyyyMMdd") & ".csv"

        Dim tfp As New TextFieldParser(_strPastaProj & strSaida)
        tfp.Delimiters = New String() {";"}
        tfp.TextFieldType = FieldType.Delimited

        tfp.ReadLine() ' skip header

        While tfp.EndOfData = False
            Dim fields = tfp.ReadFields()
            'Dim fields2 = tfp.ReadFields()  'Teste

            If ExisteAlertaSQL(_aTenantId(_intClientIndex), fields(4)) Then
                GravaSQL_Altera(fields, Proj.UnixTimestampToDate(fields(7)).ToString("yyyyMMdd"))
                _intLinhasSQL_Alt += 1
            Else
                GravaSQL_Inclui(fields)
                _intLinhasSQL_Inc += 1
            End If
            _intLinhasLidas += 1
        End While

        Application.DoEvents()
        Cursor = Cursors.Default
        txtMensagens.Text &= "Linhas alteradas na tabela SQL t_taegis_alerts: " & _intLinhasSQL_Alt & vbCrLf &
                        "Linhas incluídas na tabela SQL t_taegis_alerts: " & _intLinhasSQL_Inc & vbCrLf &
                        "Total de linhas CSV lidas: " & _intLinhasLidas
        txtMensagens.Text &= vbCrLf & "===================================================================================" & vbCrLf
        _intLinhasSQL_Alt = 0
        _intLinhasSQL_Inc = 0
        _intLinhasLidas = 0

        'btnReinicio.Enabled = True
        btnSQL.Enabled = False

        'Salta para o final de txtMensagens
        SaltaParaFimDaMensagem()

        '17/03/25: AUTOMATIZAÇÃO DO PROCESSO QUINZENAL
        If _strDiasProcesso = "1_a_15" Then
            opt_16_fim.Checked = True
            Application.DoEvents()
            btnReinicio.PerformClick()
            Application.DoEvents()
            btnAlerts1.PerformClick()
        End If

    End Sub

    Private Function ExisteAlertaSQL(strTenantId As String, strId As String) As Boolean
        'Verifica se o alerta existe na tabela SQL t_taegis_alerts
        '14/02/23-17/02/23-04/04/23
        '=========================================================
        Dim oCon As New SqlConnection(ConnectionString)
        Dim oCmd As New SqlCommand
        Dim intQtd As Integer = 0

        oCon.Open()

        With oCmd
            .Connection = oCon
            .CommandType = CommandType.Text
            .CommandText = "select count(*) from t_taegis_alerts " &
                           "where id = '" & strId & "' AND alert_tenant_id = '" & strTenantId & "'"
            .CommandTimeout = 3600
            intQtd = .ExecuteScalar
        End With

        oCon.Close()

        Return (intQtd > 0)

    End Function

    Private Sub GravaSQL_Inclui(fields As String())
        'Inclui linhas na tabela SQL t_taegis_alerts
        '13/02/23-14/02/23-21/03/23-29/03/23-11/12/23
        '27/12/24
        '13/03/25
        '27/05/25 - Inclusão do campo metadata_first_investigated_at na tabela SQL t_taegis_alerts
        '=========================================================================================
        Application.DoEvents()
        Dim oCon As New SqlConnection(ConnectionString)
        Dim oCmd As New SqlCommand
        Dim quote As String = """"    'Uma aspa dupla como string

        oCon.Open()

        'Try

        With oCmd
            .Connection = oCon
            .CommandType = CommandType.Text
            .CommandText = "insert into t_taegis_alerts(" &
                        "alert_date, " &
                        "alert_num, " &
                        "alert_tenant_id," &
                        "alert_client," &
                        "attack_technique_ids, " &
                        "entities, " &
                        "ent_relationships, " &
                        "id, " &
                        "investigation_ids, " &
                        "metadata_confidence, " &
                        "metadata_created_at, " &
                        "metadata_creator_detector_id, " &
                        "metadata_creator_detector_version, " &
                        "metadata_creator_rule_id, " &
                        "metadata_creator_rule_version, " &
                        "metadata_engine_name, " &
                        "metadata_severity, " &
                        "metadata_title, " &
                        "sensor_types, " &
                        "status, " &
                        "suppressed, " &
                        "suppressed_rules, " &
                        "tactics_technique_id, " &
                        "metadata_first_resolved_at, " &
                        "metadata_first_investigated_at) " &
                        "values('" & Proj.UnixTimestampToDate(fields(7)).ToString("yyyyMMdd") & "', " &
                        fields(0) & ", '" &
                        _aTenantId(_intClientIndex) & "', '" &
                        cboClientes.Text & "', '" &
                        fields(1).Replace("'", "|") & "', '" &
                        fields(2).Replace("'", "|") & "', '" &
                        fields(3).Replace("'", "|") & "', '" &
                        fields(4).Replace("'", "|") & "', '" &
                        fields(5).Replace("'", "|") & "', '" &
                        fields(6).Replace("'", "|") & "', '" &
                        fields(7).Replace("'", "|") & "', '" &
                        fields(8).Replace("'", "|") & "', '" &
                        fields(9).Replace("'", "|") & "', '" &
                        fields(10).Replace("'", "|") & "', '" &
                        fields(11).Replace("'", "|") & "', '" &
                        fields(12).Replace("'", "|") & "', '" &
                        fields(13).Replace("'", "|") & "', '" &
                        fields(14).Replace("'", "|") & "', '" &
                        fields(15).Replace("'", "|") & "', '" &
                        fields(16).Replace("'", "|") & "', '" &
                        fields(17).Replace("'", "|") & "', '" &
                        fields(18).Replace("'", "|") & "', '" &
                        fields(19).Replace("'", "|") & "', '" &
                        fields(20).Replace("'", "|") & "', '" &
                        fields(21).Replace("'", quote) & "')"
            .CommandTimeout = 3600
            .ExecuteNonQuery()
        End With

        oCon.Close()

        'Catch ex As SqlException
        '    txtMensagens.Text &= vbCrLf & "Erro SQL: " & ex.ToString
        '    txtMensagens.Text &= vbCrLf & "==================================================================================="

        'Catch ex1 As Exception
        '    txtMensagens.Text &= vbCrLf & "Inclui SQL => Erro linha " & fields(0) & ": " & ex1.ToString
        '    txtMensagens.Text &= vbCrLf & "==================================================================================="

        'End Try

    End Sub

    Private Sub GravaSQL_Altera(fields As String(), strAlertDate As String)
        'Altera linhas na tabela SQL t_taegis_alerts
        '13/02/23-14/02/23-27/02/23-11/12/23
        '27/12/24
        '13/03/25
        '27/05/25 - Inclusão do campo metadata_first_investigated_at na tabela SQL t_taegis_alerts
        '=========================================================================================
        Application.DoEvents()
        Dim oCon As New SqlConnection(ConnectionString)
        Dim oCmd As New SqlCommand
        Dim quote As String = """"    'Uma aspa dupla como string

        oCon.Open()

        'Try

        With oCmd
            .Connection = oCon
            .CommandType = CommandType.Text
            .CommandText = "update t_taegis_alerts " &
                    "set alert_date = '" & strAlertDate & "', " &
                    "attack_technique_ids = '" & fields(1).Replace("'", "|") & "', " &
                    "tactics_technique_id = '" & fields(19).Replace("'", quote) & "', " &
                    "entities = '" & fields(2).Replace("'", "|") & "', " &
                    "ent_relationships = '" & fields(3).Replace("'", "|") & "', " &
                    "id = '" & fields(4).Replace("'", "|") & "', " &
                    "investigation_ids = '" & fields(5).Replace("'", "|") & "', " &
                    "metadata_confidence = '" & fields(6).Replace("'", "|") & "', " &
                    "metadata_first_resolved_at = '" & fields(20).Replace("'", "|") & "', " &
                    "metadata_first_investigated_at = '" & fields(21).Replace("'", "|") & "', " &
                    "metadata_created_at = '" & fields(7).Replace("'", "|") & "', " &
                    "metadata_creator_detector_id = '" & fields(8).Replace("'", "|") & "', " &
                    "metadata_creator_detector_version = '" & fields(9).Replace("'", "|") & "', " &
                    "metadata_creator_rule_id = '" & fields(10).Replace("'", "|") & "', " &
                    "metadata_creator_rule_version = '" & fields(11).Replace("'", "|") & "', " &
                    "metadata_engine_name = '" & fields(12).Replace("'", "|") & "', " &
                    "metadata_severity = '" & fields(13).Replace("'", "|") & "', " &
                    "metadata_title = '" & fields(14).Replace("'", "|") & "', " &
                    "sensor_types = '" & fields(15).Replace("'", "|") & "', " &
                    "status = '" & fields(16).Replace("'", "|") & "', " &
                    "suppressed = '" & fields(17).Replace("'", "|") & "', " &
                    "suppressed_rules = '" & fields(18).Replace("'", "|") & "', " &
                    "alt_data = getdate(), " &
                    "alt_user = suser_name() " &
                    "where id = '" & fields(4) & "'"
            .ExecuteNonQuery()
            .CommandTimeout = 3600
        End With

        oCon.Close()

        'Catch ex As SqlException
        '    txtMensagens.Text &= vbCrLf & "Erro SQL => Linha " & fields(0) & ": " & ex.ToString
        '    txtMensagens.Text &= vbCrLf & "==================================================================================="


        'Catch ex1 As Exception
        '    txtMensagens.Text &= vbCrLf & "Altera SQL => Erro linha " & fields(0) & ": " & ex1.ToString
        '    txtMensagens.Text &= vbCrLf & "==================================================================================="

        'End Try

    End Sub

#End Region

#Region "Métodos e funções gerais"

    Private Sub SaltaParaFimDaMensagem()
        'Salta para o fim do texto de mensagens txtMensagens
        '23/03/23
        '===================================================
        txtMensagens.SelectionStart = txtMensagens.Text.Length
        txtMensagens.ScrollToCaret()

    End Sub

    Private Sub txtPastaPython_TextChanged(sender As Object, e As EventArgs) Handles txtPastaPython.TextChanged
        'Texto alterado
        '09/02/23-14/02/23
        '==============
        Dim strSaida1 As New String("alerts01_" & _aTenantId(_intClientIndex) & "_" & DateTime.Now.AddDays(-1).ToString("yyyyMMdd") & ".json")
        Dim strSaida2 As New String("alerts02_" & _aTenantId(_intClientIndex) & "_" & DateTime.Now.AddDays(-1).ToString("yyyyMMdd") & ".csv")

        btnAlerts1.Enabled = Len(Trim(txtPastaProj.Text)) > 0 And Len(Trim(txtPastaPython.Text)) > 0
        btnAlerts2.Enabled = System.IO.File.Exists(strSaida1)
        btnSQL.Enabled = System.IO.File.Exists(strSaida2)

    End Sub

    Private Sub txtPastaProj_TextChanged(sender As Object, e As EventArgs) Handles txtPastaProj.TextChanged
        'Texto alterado
        '09/02/23-14/02/23
        '==============
        Dim strSaida1 As New String("alerts01_" & _aTenantId(_intClientIndex) & "_" & DateTime.Now.AddDays(-1).ToString("yyyyMMdd") & ".json")
        Dim strSaida2 As New String("alerts02_" & _aTenantId(_intClientIndex) & "_" & DateTime.Now.AddDays(-1).ToString("yyyyMMdd") & ".csv")

        btnAlerts1.Enabled = Len(Trim(txtPastaProj.Text)) > 0 And Len(Trim(txtPastaPython.Text)) > 0
        btnAlerts2.Enabled = System.IO.File.Exists(strSaida1)
        btnSQL.Enabled = System.IO.File.Exists(strSaida2)

    End Sub

    Private Sub dtpDataExtracao_ValueChanged(sender As Object, e As EventArgs) Handles dtpDataExtracao.ValueChanged
        'Alteração de valor
        '21/03/23-22/03/23
        '=================
        Dim intAlertas As Integer = DataJaExtraida()

        btnAlerts1.Enabled = True
        With dtpDataExtracao
            If .Value.Date > Now.Date Then
                MessageBox.Show("Datas futuras não são permitidas", "Erro de Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ElseIf .Value.Date = Now.Date Then
                _datExtracao1 = DateAdd(DateInterval.Day, -1, dtpDataExtracao.Value.Date)
                _datExtracao2 = dtpDataExtracao.Value.Date
            Else
                _datExtracao1 = dtpDataExtracao.Value.Date
                _datExtracao2 = DateAdd(DateInterval.Day, 1, dtpDataExtracao.Value.Date)
                btnAlerts2.Enabled = False
                btnSQL.Enabled = False
                If intAlertas > 0 Then
                    Dim strMsg As New String("Data selecionada " & .Value.ToString("dd/MM/yyyy") & " já existe no banco de dados para este cliente com " & intAlertas & " alertas. Continua?")
                    Dim strTit As New String("Seleção de Data para Alertas")
                    If MessageBox.Show(strMsg, strTit, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                        btnAlerts1.Enabled = False
                    End If
                End If
            End If

        End With

    End Sub

    Private Function DataJaExtraida() As Integer
        'Retorna True se a data selecionada já consta na tabela t_taegis_alerts no banco de dados
        '22/03/23-31/03/23
        '========================================================================================
        Dim oCon As New SqlConnection(ConnectionString)
        Dim oCmd As New SqlCommand
        Dim intQtd As Integer = 0
        Dim strDataSel As New String(dtpDataExtracao.Value.ToString("yyyyMMdd"))

        oCon.Open()

        With oCmd
            .Connection = oCon
            .CommandType = CommandType.Text
            .CommandText = "select count(*) from t_taegis_alerts " &
                           "where alert_date = '" & strDataSel & "' and alert_tenant_id = '" & _aTenantId(_intClientIndex) & "'"
            .CommandTimeout = 3600
            intQtd = .ExecuteScalar
        End With

        oCon.Close()

        Return intQtd

    End Function

    Private Sub ExcluiArquivosAnteriores()
        '15/03/23-14/10/23
        'Exclusão de arquivos de trabalho *.json e *.csv
        '===============================================
        Dim strJson As New String(_strPastaProj & "\*.json")
        Dim strCsv As New String(_strPastaProj & "\*.csv")

        Application.DoEvents()

        For Each fileFound As String In IO.Directory.GetFiles(_strPastaProj).Where(Function(fi) IO.Path.GetFileName(fi) Like "*.json").ToArray
            IO.File.Delete(fileFound)
        Next

        For Each fileFound As String In IO.Directory.GetFiles(_strPastaProj).Where(Function(fi) IO.Path.GetFileName(fi) Like "*.csv").ToArray
            IO.File.Delete(fileFound)
        Next

    End Sub

    Private Function ExistemDadosNoMes() As Boolean
        'Retorna True se a tabela t_taegis_alerts no banco de dados possuir alertas no mês
        '24/03/23-03/04/23
        '=================================================================================
        Dim oCon As New SqlConnection(ConnectionString)
        Dim oCmd As New SqlCommand
        Dim intQtd As Integer = 0
        Dim strMesSel As New String(dtpDataExtracao.Value.ToString("MM"))
        Dim strAnoSel As New String(dtpDataExtracao.Value.ToString("yyyy"))

        oCon.Open()

        With oCmd
            .Connection = oCon
            .CommandType = CommandType.Text
            .CommandText = "select count(*) from t_taegis_alerts " &
                           "where substring(alert_date, 1, 4) = '" & strAnoSel & "' and substring(alert_date, 5, 2) = '" & strMesSel & "' " &
                           "AND alert_client = '" & cboClientes.Text & "'"
            .CommandTimeout = 3600
            intQtd = .ExecuteScalar
        End With

        oCon.Close()

        Return (intQtd > 0)

    End Function

    Private Function ExistemLogsNoMes() As Boolean
        'Retorna True se a tabela t_taegis_log no banco de dados possuir entradas no mês
        '20/04/23
        '===============================================================================
        Dim oCon As New SqlConnection(ConnectionString)
        Dim oCmd As New SqlCommand
        Dim intQtd As Integer = 0
        Dim intMesSel As Integer = dtpDataExtracao.Value.ToString("MM")
        Dim intAnoSel As Integer = dtpDataExtracao.Value.ToString("yyyy")

        oCon.Open()

        With oCmd
            .Connection = oCon
            .CommandType = CommandType.Text
            .CommandText = "select count(*) from t_taegis_log " &
                               "where YEAR(data_log) = " & intAnoSel & " and MONTH(data_log) = " & intMesSel
            .CommandTimeout = 3600
            intQtd = .ExecuteScalar
        End With

        oCon.Close()

        Return (intQtd > 0)

    End Function

#End Region

#Region "Menus e botões"

    Private Sub ConfiguraçãoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ConfiguraçãoToolStripMenuItem.Click
        'Menu Arquivo/Configuração F2
        '09/02/23-15/09/23
        '============================
        Dim F As New VBActive_Taegis_DLL.frmConfig

        F.ShowDialog()
        btnAtualizaPastas.Visible = True
        btnAlerts1.Enabled = False

    End Sub

    Private Sub btnAtualizaPastas_Click(sender As Object, e As EventArgs) Handles btnAtualizaPastas.Click
        'Botão atualiza pastas
        '03/04/23
        '=====================
        RecargaPastas()
        btnAtualizaPastas.Visible = False
        btnAlerts1.Enabled = True

    End Sub

    Private Sub RecargaPastas()
        'Atualiza as pastas Python e Projeto
        '03/04/23-12/09/23-19/10/23
        '===================================
        txtPastaPython.Text = LeConfigPasta("pasta_python")
        txtPastaProj.Text = LeConfigPasta("pasta_proj_alerts")

    End Sub

    Private Sub btnFecha_Click(sender As Object, e As EventArgs) Handles btnFecha.Click
        'Botão Fecha
        '06/03/23
        '===========
        Me.Close()

    End Sub

    Private Sub FechaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FechaToolStripMenuItem.Click
        'Menu: Arquivo/Fecha
        '06/03/23
        '===================
        btnFecha.PerformClick()

    End Sub

    Private Sub AlertasMensaisToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AlertasMensaisToolStripMenuItem.Click
        'Chama o quadro de alertas do mês
        '22/03/23
        '================================
        Dim strMes As New String(dtpDataExtracao.Value.ToString("MM"))
        Dim strAno As New String(dtpDataExtracao.Value.ToString("yyyy"))

        If ExistemDadosNoMes() Then
            frmAlertasMes.strMes = strMes
            frmAlertasMes.strAno = strAno
            frmAlertasMes.strClientName = cboClientes.Text
            frmAlertasMes.ShowDialog()
        Else
            Dim strMsg As New String("Não existem alertas no mês " & strMes & "/" & strAno)
            Dim strTit As New String("Lista Mensal de Alertas")
            MessageBox.Show(strMsg, strTit, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

    End Sub

    Private Sub AlertasMensaisToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles AlertasMensaisToolStripMenuItem1.Click
        'Chama o quadro de alertas do mês - geral
        '06/04/23
        '========================================
        Dim strMes As New String(dtpDataExtracao.Value.ToString("MM"))
        Dim strAno As New String(dtpDataExtracao.Value.ToString("yyyy"))

        If ExistemDadosNoMes() Then
            frmAlertasMesGeral.strMesG = strMes
            frmAlertasMesGeral.strAnoG = strAno
            frmAlertasMesGeral.ShowDialog()
        Else
            Dim strMsg As New String("Não existem alertas no mês " & strMes & "/" & strAno)
            Dim strTit As New String("Lista Mensal de Alertas")
            MessageBox.Show(strMsg, strTit, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

    End Sub

    Private Sub TabelaDeClientesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TabelaDeClientesToolStripMenuItem.Click
        'Menu Arquivo: Tabela de Clientes
        '29/03/23-15/09/23
        '================================
        Dim f As New VBActive_Taegis_DLL.frmClientes
        f.ShowDialog()

    End Sub

    Private Sub cboClientes_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboClientes.SelectedIndexChanged
        'Alteração do combo
        '31/03/23
        '18/03/25
        '==================
        _intClientIndex = cboClientes.SelectedIndex
        btnAlerts1.Enabled = True
        txtMensagens.Text = ""
        txtMensagens.ForeColor = Color.Blue

    End Sub

    Private Sub LogMensalToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LogMensalToolStripMenuItem.Click
        'Chama o quadro de logs do mês - geral
        '20/04/23
        '=====================================
        Dim strMes As New String(dtpDataExtracao.Value.ToString("MM"))
        Dim strAno As New String(dtpDataExtracao.Value.ToString("yyyy"))

        If ExistemLogsNoMes() Then
            frmLogsMesGeral.strMesG = strMes
            frmLogsMesGeral.strAnoG = strAno
            frmLogsMesGeral.ShowDialog()
        Else
            Dim strMsg As New String("Não existem logs no mês " & strMes & "/" & strAno)
            Dim strTit As New String("Lista Mensal de Logs")
            MessageBox.Show(strMsg, strTit, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

    End Sub

    Private Sub btnReinicio_Click(sender As Object, e As EventArgs) Handles btnReinicio.Click
        'Botão reinício
        '14/10/23
        '18/10/23
        '==============
        btnAlerts1.Enabled = True
        btnAlerts2.Enabled = False
        btnSQL.Enabled = False
        'ExcluiArquivosAnteriores()
        If _strDiasProcesso <> "16_a_fim" Then
            txtMensagens.Text = ""
        End If

    End Sub

#End Region

#Region "Tabela alerts_id"

    Private Sub CriaTabelaAlertsIdToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CriaTabelaAlertsIdToolStripMenuItem.Click
        'Menu Arquivo/Cria tabela Alerts_Id
        '04/12/23
        '==================================
        Cursor = Cursors.WaitCursor
        Dim Reader As StreamReader = My.Computer.FileSystem.OpenTextFileReader("c:\DWNetcenter\_Desenv\Python\Test_Alerts\alerts03_id.txt")
        Dim strLinha As String

        Do
            strLinha = Reader.ReadLine
            GravaTabelaAlertsId(strLinha)
        Loop Until strLinha Is Nothing

        txtMensagens.Text &= "Linhas gravadas em t_taegis_alerts_id: " & _intAlertId & vbCrLf
        Cursor = Cursors.Default

    End Sub

    Private Sub GravaTabelaAlertsId(ByVal strLinha As String)
        'Grava uma linha strLinha na tabela Alerts_Id
        '04/12/23
        '============================================
        Dim oCon As New SqlConnection(ConnectionString)
        Dim oCmd As New SqlCommand

        oCon.Open()

        With oCmd
            .Connection = oCon
            .CommandType = CommandType.Text
            .CommandText = "INSERT INTO t_taegis_alerts_id(alert_id) VALUES('" & strLinha & "')"
            .CommandTimeout = 3600
            .ExecuteNonQuery()
        End With

        oCon.Close()
        _intAlertId += 1

    End Sub

#End Region

#Region "Tactics & Techniques"

    Private Sub btnSQLTechTact_Click(sender As Object, e As EventArgs) Handles btnSQLTechTact.Click
        'Botão SQLTechTact - Tratamento dos campos de Técnicas e Táticas
        '29/11/23
        '===============================================================
        SQLTechTact()

    End Sub

    Private Sub SQLTechTact()
        'Tratamento dos campos de Técnicas e Táticas
        '29/11/23
        '===========================================
        Cursor = Cursors.WaitCursor
        TruncaTab("t_taegis_alerts_tt")
        Dim oCon As New SqlConnection(ConnectionString)
        Dim oCmd As New SqlCommand
        Dim oDrd As SqlDataReader
        _intAlerts = 0

        oCon.Open()

        With oCmd
            .Connection = oCon
            .CommandType = CommandType.Text
            .CommandText = "SELECT * FROM dbo.t_taegis_alerts 
                           WHERE tactics_technique_id <> 'None' 
                                AND tactics_technique_id <> '[]'
                                AND tactics_technique_id <> '[{""mitre_attack_info"": None}]'
                                AND tactics_technique_id IS NOT NULL"
            .CommandTimeout = 3600
            oDrd = .ExecuteReader
        End With

        With oDrd

            While .Read
                'MessageBox.Show("Alert date/num: " & .Item("alert_date") & "/" & .Item("alert_num"))

                Dim aTactics As New ArrayList(OcorrenciasTactics(.Item("tactics_technique_id"), .Item("alert_tenant_id"), .Item("alert_date"), .Item("alert_num")))
                Dim aTechniqueId As New ArrayList(OcorrenciasTechnique_Id(.Item("tactics_technique_id"), .Item("id")))
                Dim aTact() As String = {"", "", "", "", "", "", "", "", "", "", "", "", ""}
                Dim aTech() As String = {"", "", "", "", "", "", "", "", "", "", "", "", ""}
                Dim i As Integer = -1

                For Each item In aTactics
                    i += 1
                    If i > 12 Then
                        Exit For
                    End If
                    aTact(i) = item
                Next

                i = -1
                For Each item In aTechniqueId
                    i += 1
                    aTech(i) = item
                Next

                GravaAlertsTT(
                .Item("alert_date"),
                .Item("alert_num"),
                .Item("alert_tenant_id"),
                .Item("alert_client"),
                SX(aTact(0)), SX(aTact(1)), SX(aTact(2)), SX(aTact(3)), SX(aTact(4)), SX(aTact(5)), SX(aTact(6)), SX(aTact(7)), SX(aTact(8)), SX(aTact(9)), SX(aTact(10)), SX(aTact(11)), SX(aTact(12)),
                aTech(SN(aTact(0), 0)), aTech(SN(aTact(1), 1)), aTech(SN(aTact(2), 2)), aTech(SN(aTact(3), 3)), aTech(SN(aTact(4), 4)), aTech(SN(aTact(5), 5)),
                aTech(SN(aTact(6), 6)), aTech(SN(aTact(7), 7)), aTech(SN(aTact(8), 8)), aTech(SN(aTact(9), 9)), aTech(SN(aTact(10), 10)), aTech(SN(aTact(11), 11)), aTech(SN(aTact(12), 12)),
                .Item("id"))
                _intAlerts += 1
            End While
        End With

        Cursor = Cursors.Default
        txtMensagens.Text &= vbCrLf & "Tabela t_taegis_alerts_ linhas lidas: " & _intAlerts
        txtMensagens.Text &= vbCrLf & "Tabela t_taegis_alerts_tt linhas gravadas: " & _intAttachTT
        oCon.Close()

    End Sub

    Private Function SX(ByVal strTact As String) As String
        'Retorna o campo Tática sem os últimos 4 ou 5 caracteres " [n]" ou " [nn]"
        '15/12/23
        '=========================================================================
        If Len(strTact) > 0 Then
            Return strTact.Substring(0, strTact.IndexOf(" ["))
        Else
            Return ""
        End If

    End Function

    Private Function SN(ByVal strTact As String, ByVal intInd As Integer) As Integer
        'Retorna o campo numérico da Tática " [n]" ou " [nn]"
        '15/12/23
        '====================================================
        If Len(strTact) > 0 Then
            Return strTact.Substring(strTact.IndexOf(" [") + 1, 3).Replace("[", "").Replace("]", "") - 1
        Else
            Return intInd
        End If

    End Function

    Private Sub GravaAlertsTT(
                                strAlert_date As String,
                                intAlert_num As Integer,
                                strAlert_tenant_id As String,
                                strAlert_client As String,
                                strTactics_1 As String,
                                strTactics_2 As String,
                                strTactics_3 As String,
                                strTactics_4 As String,
                                strTactics_5 As String,
                                strTactics_6 As String,
                                strTactics_7 As String,
                                strTactics_8 As String,
                                strTactics_9 As String,
                                strTactics_10 As String,
                                strTactics_11 As String,
                                strTactics_12 As String,
                                strTactics_13 As String,
                                strTechniqueId_1 As String,
                                strTechniqueId_2 As String,
                                strTechniqueId_3 As String,
                                strTechniqueId_4 As String,
                                strTechniqueId_5 As String,
                                strTechniqueId_6 As String,
                                strTechniqueId_7 As String,
                                strTechniqueId_8 As String,
                                strTechniqueId_9 As String,
                                strTechniqueId_10 As String,
                                strTechniqueId_11 As String,
                                strTechniqueId_12 As String,
                                strTechniqueId_13 As String,
                                strId As String)
        'Grava a tabela t_taegis_alerts_TT
        '29/11/23-12/12/23
        '=================================
        Dim oCon As New SqlConnection(ConnectionString)
        Dim oCmd As New SqlCommand

        'Preparo dos campos
        'strTechniqueId_2 = If(strTechniqueId_2 = "" And strTactics_2 <> "", strTechniqueId_1, strTechniqueId_2)
        'strTechniqueId_3 = If(strTechniqueId_3 = "" And strTactics_3 <> "", strTechniqueId_2, strTechniqueId_3)
        'strTechniqueId_4 = If(strTechniqueId_4 = "" And strTactics_4 <> "", strTechniqueId_3, strTechniqueId_4)
        'strTechniqueId_5 = If(strTechniqueId_5 = "" And strTactics_5 <> "", strTechniqueId_4, strTechniqueId_5)
        'strTechniqueId_6 = If(strTechniqueId_6 = "" And strTactics_6 <> "", strTechniqueId_5, strTechniqueId_6)
        'strTechniqueId_7 = If(strTechniqueId_7 = "" And strTactics_7 <> "", strTechniqueId_6, strTechniqueId_7)
        'strTechniqueId_8 = If(strTechniqueId_8 = "" And strTactics_8 <> "", strTechniqueId_7, strTechniqueId_8)
        'strTechniqueId_9 = If(strTechniqueId_9 = "" And strTactics_9 <> "", strTechniqueId_8, strTechniqueId_9)
        'strTechniqueId_10 = If(strTechniqueId_10 = "" And strTactics_10 <> "", strTechniqueId_9, strTechniqueId_10)
        'strTechniqueId_11 = If(strTechniqueId_11 = "" And strTactics_11 <> "", strTechniqueId_10, strTechniqueId_11)
        'strTechniqueId_12 = If(strTechniqueId_12 = "" And strTactics_12 <> "", strTechniqueId_11, strTechniqueId_12)
        'strTechniqueId_13 = If(strTechniqueId_13 = "" And strTactics_13 <> "", strTechniqueId_12, strTechniqueId_13)

        oCon.Open()

        With oCmd
            .Connection = oCon
            .CommandType = CommandType.Text
            .CommandText = "INSERT INTO t_taegis_alerts_tt(
                                alert_date,
                                alert_num,
                                alert_tenant_id,
                                alert_client,
                                attack_tactics_id_1,
                                attack_technique_id_1,
                                attack_tactics_id_2,
                                attack_technique_id_2,
                                attack_tactics_id_3,
                                attack_technique_id_3,
                                attack_tactics_id_4,
                                attack_technique_id_4,
                                attack_tactics_id_5,
                                attack_technique_id_5,
                                attack_tactics_id_6,
                                attack_technique_id_6,
                                attack_tactics_id_7,
                                attack_technique_id_7,
                                attack_tactics_id_8,
                                attack_technique_id_8,
                                attack_tactics_id_9,
                                attack_technique_id_9,
                                attack_tactics_id_10,
                                attack_technique_id_10,
                                attack_tactics_id_11,
                                attack_technique_id_11,
                                attack_tactics_id_12,
                                attack_technique_id_12,
                                attack_tactics_id_13,
                                attack_technique_id_13,
                                id)
                            VALUES('" &
                                strAlert_date & "', " &
                                intAlert_num & ", '" &
                                strAlert_tenant_id & "', '" &
                                strAlert_client & "', '" &
                                strTactics_1 & "', '" &
                                strTechniqueId_1 & "', '" &
                                strTactics_2 & "', '" &
                                strTechniqueId_2 & "', '" &
                                strTactics_3 & "', '" &
                                strTechniqueId_3 & "', '" &
                                strTactics_4 & "', '" &
                                strTechniqueId_4 & "', '" &
                                strTactics_5 & "', '" &
                                strTechniqueId_5 & "', '" &
                                strTactics_6 & "', '" &
                                strTechniqueId_6 & "', '" &
                                strTactics_7 & "', '" &
                                strTechniqueId_7 & "', '" &
                                strTactics_8 & "', '" &
                                strTechniqueId_8 & "', '" &
                                strTactics_9 & "', '" &
                                strTechniqueId_9 & "', '" &
                                strTactics_10 & "', '" &
                                strTechniqueId_10 & "', '" &
                                strTactics_11 & "', '" &
                                strTechniqueId_11 & "', '" &
                                strTactics_12 & "', '" &
                                strTechniqueId_12 & "', '" &
                                strTactics_13 & "', '" &
                                strTechniqueId_13 & "', '" &
                                strId & "')"
            .CommandTimeout = 3600
            .ExecuteNonQuery()
        End With

        _intAttachTT += 1
        oCon.Close()

    End Sub

    Private Sub TruncaTab(ByVal strTabela As String)
        'Trunca uma tabela strTabela
        '29/11/23
        '===========================
        Dim oCon As New SqlConnection(ConnectionString)
        Dim oCmd As New SqlCommand

        oCon.Open()

        With oCmd
            .Connection = oCon
            .CommandType = CommandType.Text
            .CommandText = "TRUNCATE TABLE " & strTabela
            .CommandTimeout = 3600
            .ExecuteNonQuery()
        End With

        oCon.Close()

    End Sub

    ''' <summary>
    ''' Retorna a quantidade de ocorrências do substring "tactics" no campo tactics_technique_id 
    ''' </summary>
    ''' <param name="strTactics"></param>
    ''' <returns></returns>
    Private Function OcorrenciasTacticsAnt(ByVal strTactics As String, ByVal strTenant As String, ByVal strDate As String, ByVal intNum As Integer) As ArrayList
        '[{|mitre_attack_info|: {|tactics|: [|defense-evasion|], |technique_id|: |T1140|}}]
        '12/12/23
        '========
        Dim regex As New System.Text.RegularExpressions.Regex("tactics")
        Dim aTactics As New ArrayList
        Dim quote As String = """"
        Dim plick As String = "'"
        Dim intMach As Integer = 0

        'Percorre o campo tactics_technique_id para encontrar a palavra "tactics" e recuperar a tática
        For Each match As System.Text.RegularExpressions.Match In regex.Matches(strTactics)

            'Try

            intMach += 1
            aTactics.Add(strTactics.Substring(match.Index + 11).Replace(quote, plick).Split(plick)(1) & " [" & intMach & "]")

            For i = 0 To strTactics.Substring(match.Index + 11).Replace(quote, plick).Split(plick).Length - 3
                If strTactics.Substring(match.Index + 11).Replace(quote, plick).Split(plick)(i + 2) = ", " Then
                    aTactics.Add(strTactics.Substring(match.Index + 11).Replace(quote, plick).Split(plick)(i + 3) & " [" & intMach & "]")
                ElseIf strTactics.Substring(match.Index + 11).Replace(quote, plick).Split(plick)(i + 2) = "], " Then
                    Exit For
                End If
            Next i

            'Catch ex As Exception
            '    MessageBox.Show(ex.Message & "Tenant: " & strTenant & " Data: " & strDate & " Alert Num: " & intNum)

            'End Try

        Next

        Return aTactics

    End Function

    Private Function OcorrenciasTactics(ByVal strTactics As String, ByVal strTenant As String, ByVal strDate As String, ByVal intNum As Integer) As ArrayList
        'Varre o campo de ocorrências táticas
        'Apoio: ChatGPT
        '30/12/24
        '====================================
        ' Constants for better readability
        Const OFFSET As Integer = 11
        Dim regex As New System.Text.RegularExpressions.Regex("tactics")
        Dim aTactics As New ArrayList
        Dim quote As String = """"
        Dim pipe As String = "|"
        Dim intMach As Integer = 0

        ' Iterate through matches
        For Each match As System.Text.RegularExpressions.Match In regex.Matches(strTactics)
            intMach += 1

            ' Extract and process substring
            Dim startIndex As Integer = match.Index + OFFSET
            If startIndex < strTactics.Length Then
                Dim extractedString As String = strTactics.Substring(startIndex).Replace(quote, pipe)
                Dim splitParts() As String = extractedString.Split(pipe)

                ' Ensure there are enough parts
                If splitParts.Length > 1 Then
                    aTactics.Add(splitParts(1) & " [" & intMach & "]")
                End If

                ' Iterate through subsequent parts
                For i = 2 To splitParts.Length - 1 Step 2
                    If i + 1 < splitParts.Length Then
                        Dim separator As String = splitParts(i)
                        If separator = ", " Then
                            aTactics.Add(splitParts(i + 1) & " [" & intMach & "]")
                        ElseIf separator = "], " Then
                            Exit For
                        End If
                    End If
                Next
            End If
        Next

        Return aTactics

    End Function

    ''' <summary>
    ''' Retorna a quantidade de ocorrências do substring "technique_id" no campo tactics_technique_id 
    ''' </summary>
    ''' <param name="strTechniqueId"></param>
    ''' <returns></returns>
    Private Function OcorrenciasTechnique_Id(ByVal strTechniqueId As String, ByVal strId As String) As ArrayList
        '12/12/23
        '========
        Dim regex As New System.Text.RegularExpressions.Regex("technique_id")
        Dim aTechnique_id As New ArrayList
        Dim quote As String = """"

        'Percorre o campo tactics_technique_id para encontrar a palavra "technique_id" e recuperar a técnica
        For Each match As System.Text.RegularExpressions.Match In regex.Matches(strTechniqueId)
            aTechnique_id.Add(strTechniqueId.Substring(match.Index + 16).Split(quote)(0))
        Next

        Return aTechnique_id

    End Function

#End Region

    Private Sub TacticsTechniquesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TacticsTechniquesToolStripMenuItem.Click
        'Menu Listas/Tactics/Techniques
        '15/12/23
        '==============================
        LeTactics()

    End Sub

    Private Sub LeTactics()
        'Leitura da tabela t_taegis_alerts_tactics
        '15/12/23
        '=========================================
        Dim oCon As New SqlConnection(ConnectionString)
        Dim oCmd As New SqlCommand
        Dim oDrd As SqlDataReader
        Dim intQtd As Integer = 0

        oCon.Open()

        With oCmd
            .Connection = oCon
            .CommandType = CommandType.Text
            .CommandText = "SELECT name FROM t_taegis_alerts_tactics
                            ORDER BY ordem"
            .CommandTimeout = 3600
            oDrd = .ExecuteReader
        End With

        With oDrd

            While .Read
                intQtd = LeTechniques(.Item("name").ToString.ToLower)
                txtMensagens.Text &= vbCrLf & .Item("name") & ": " & intQtd
            End While

        End With
        oCon.Close()

    End Sub

    Private Function LeTechniques(ByVal strTactics As String) As Integer
        'Leitura e contagem de techniques desta tactics
        '15/12/23
        '==============================================
        Dim oCon As New SqlConnection(ConnectionString)
        Dim oCmd As New SqlCommand
        Dim oDrd As SqlDataReader
        Dim strMesSel As New String(dtpDataExtracao.Value.ToString("MM"))
        Dim strAnoSel As New String(dtpDataExtracao.Value.ToString("yyyy"))
        Dim intQtd As Integer = 0

        oCon.Open()

        With oCmd
            .Connection = oCon
            .CommandType = CommandType.Text
            .CommandText = "SELECT * FROM t_taegis_alerts_tt
                           where substring(alert_date, 1, 4) = '" & strAnoSel & "' and substring(alert_date, 5, 2) = '" & strMesSel & "' " &
                           "AND alert_client = '" & cboClientes.Text & "'"
            .CommandTimeout = 3600
            oDrd = .ExecuteReader
        End With

        With oDrd

            While .Read

                For i As Integer = 4 To 28 Step 2
                    If .Item(i).ToString.ToLower.Replace("-", " ") = strTactics Then
                        intQtd += 1
                    End If
                Next

            End While
        End With

        oCon.Close()

        Return intQtd

    End Function

    Private Sub ImportaTechniquesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImportaTechniquesToolStripMenuItem.Click

        ImportaTechniquesParaSQL()

    End Sub

    Sub ImportaTechniquesParaSQL()
        '12/01/25
        '=========================
        Dim folderPath As String = "c:\DWNetcenter\ACTIVE\ALERTS\"
        Dim sqlConnectionString As String = ConnectionString
        Dim strUltID As String = ""
        Dim intLinhas As Integer = 0

        ' Set EPPlus to non-commercial license
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial

        TruncaTab("t_taegis_alerts_techniques")

        ' Open File Dialog to select spreadsheet
        Dim openFileDialog As New OpenFileDialog With {
            .InitialDirectory = folderPath,
            .Filter = "Excel Files|*.xlsx",
            .Title = "Select an Excel file to import"
        }

        If openFileDialog.ShowDialog() <> DialogResult.OK Then
            MessageBox.Show("No file selected. Exiting...")
            Return
        End If

        Dim selectedFile As String = openFileDialog.FileName
        txtMensagens.Text = ($"Início da importação planilha {Path.GetFileName(selectedFile)} para SQL Server")
        Cursor = Cursors.WaitCursor
        Application.DoEvents()

        ' Open the selected Excel file
        Using package As New ExcelPackage(New FileInfo(selectedFile))
            Dim worksheet As ExcelWorksheet = package.Workbook.Worksheets(0) ' Read first sheet
            If worksheet Is Nothing Then
                MessageBox.Show("Worksheet not found. Exiting...")
                Return
            End If

            Dim rowCount As Integer = worksheet.Dimension.Rows

            ' Insert data into SQL Server
            Using sqlConn As New SqlConnection(sqlConnectionString)
                sqlConn.Open()
                For row As Integer = 2 To rowCount ' Assuming row 1 is header
                    Dim sql As String = "INSERT INTO t_taegis_alerts_techniques VALUES (" &
                                        "@col1, @col2, @col3, @col4, @col5, @col6, @col7, @col8, @col9, @col10, " &
                                        "@col11, @col12, @col13, @col14, @col15, @col16, @col17, @col18, @col19, @col20, " &
                                        "@col21, @col22, @col23)"
                    Using sqlCmd As New SqlCommand(sql, sqlConn)
                        For col As Integer = 1 To 23
                            sqlCmd.Parameters.AddWithValue("@col" & col, worksheet.Cells(row, col).Text)
                            'Caso haja um estouro de campo na tabela, fazer este teste - 12/01/25
                            'If col = 1 Then
                            '    strUltID = worksheet.Cells(row, col).Text
                            'End If
                        Next
                        sqlCmd.ExecuteNonQuery()
                        intLinhas += 1
                    End Using
                Next
            End Using
        End Using

        Cursor = Cursors.Default
        txtMensagens.Text &= vbCrLf & "Importação de " & intLinhas & " linhas da planilha para a tabela t_taegis_alerts_techniques executada com sucesso!" & vbCrLf

    End Sub

    Private Sub ImportaPlanilhaDeTechniquesGenéricaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImportaPlanilhaDeTechniquesGenéricaToolStripMenuItem.Click
        'Importa a planilha https://attack.mitre.org/docs/enterprise-attack-v16.1/enterprise-attack-v16.1-techniques.xlsx
        'diretamente do site Mitre-Attack. As atualizações ocorrem poucas vezes por ano.
        'A planilha é baixada na pasta C:\DWNetcenter\ACTIVE\ALERTS\TESTE\
        '13/02/25
        '=====================================================================================================
        DownloadLatestExcelAsync()

    End Sub

    ''' <summary>
    ''' Faz o download da planilha Excel
    ''' </summary>
    Private Async Sub DownloadLatestExcelAsync()
        '13/02/25
        '=======================================
        Dim savePath As String = "C:\DWNetcenter\ACTIVE\ALERTS\"

        Try
            Dim excelUrl As String = Await GetLatestExcelUrlAsync()

            If Not String.IsNullOrEmpty(excelUrl) Then
                ' Obtém apenas o nome do arquivo a partir da URL
                Dim fileName As String = Path.GetFileName(New Uri(excelUrl).AbsolutePath)
                Dim fullSavePath As String = Path.Combine(savePath, fileName)

                ' Baixa o arquivo
                Using client As New HttpClient()
                    Dim data As Byte() = Await client.GetByteArrayAsync(excelUrl)

                    ' Escreve os bytes no arquivo
                    File.WriteAllBytes(fullSavePath, data)
                End Using

                MessageBox.Show("Download concluído: " & fullSavePath, "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("Nenhuma planilha encontrada.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If

        Catch ex As Exception
            MessageBox.Show("Erro ao baixar a planilha: " & ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    ''' <summary>
    ''' Monta o URL da planilha Excel de Techniques
    ''' </summary>
    ''' <returns></returns>
    Private Async Function GetLatestExcelUrlAsync() As Task(Of String)
        '13/02/25
        '=============================================================
        Dim baseUrl As String = "https://attack.mitre.org/resources/attack-data-and-tools/"
        Dim baseDocsUrl As String = "https://attack.mitre.org"

        ' Regex corrigido para capturar o padrão correto do link da planilha
        Dim excelPattern As String = "href\s*=\s*""(/docs/(enterprise-attack-v[0-9]+\.[0-9]+)/\2-techniques\.xlsx)"""

        Try
            Using client As New HttpClient()
                ' Baixa o conteúdo da página de recursos do MITRE ATT&CK
                Dim pageContent As String = Await client.GetStringAsync(baseUrl)

                ' Procura um link da planilha usando a expressão regular correta
                Dim match As Match = Regex.Match(pageContent, excelPattern, RegexOptions.IgnoreCase)

                If match.Success Then
                    ' Concatena a URL base com o caminho relativo encontrado
                    Dim fullUrl As String = baseDocsUrl & match.Groups(1).Value
                    Return fullUrl
                Else
                    MessageBox.Show("Nenhuma planilha encontrada. Verifique se a estrutura do site mudou.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return Nothing
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show("Erro ao buscar a URL da planilha: " & ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try

    End Function

    Private Sub DebugToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DebugToolStripMenuItem.Click
        Dim psi As New ProcessStartInfo()
        psi.FileName = "c:\Users\ADM_HERON.DOMINGUES\AppData\Local\Programs\Python\Python311\python.exe"
        psi.Arguments = "alerts01_chatgpt.py 137287 SGDErpvNZHWbG5hhRVTQ1uJ3Tl8TExMg ZVvI3BUTRlpgkjs9D9e4wgex9T6_FcZrmVzUVYMJIwJnx9LjzRIqb5hJwtyVRZxx 21"
        psi.UseShellExecute = False
        psi.RedirectStandardOutput = True
        psi.RedirectStandardError = True
        psi.CreateNoWindow = True

        Using process As Process = Process.Start(psi)
            Dim output As String = process.StandardOutput.ReadToEnd()
            Dim errors As String = process.StandardError.ReadToEnd()
            process.WaitForExit()
            txtMensagens.Text &= ("Output: " & output)
            txtMensagens.Text &= ("Errors: " & errors)
        End Using
    End Sub

    Private Function CountJsonLinesBak(ByVal strJsonFile As String) As Integer
        Dim intLinhas As Integer = 0

        Try
            ' ... (your existing code to run the Python script) ...

            ' Path to the JSON file generated by the Python script
            'Dim jsonFilePath As String = Path.Combine("C:\DWNetcenter\_Desenv\Python\Taegis Alerts", "alerts.json")
            Dim jsonFilePath As String = Path.Combine(_strPastaProj, strJsonFile)

            ' Read the JSON file
            Dim jsonString As String = File.ReadAllText(jsonFilePath)

            ' Parse the JSON
            Dim jsonObject As JObject = JObject.Parse(jsonString)

            ' Get the "alerts" array and count the items (adjust based on your JSON structure)
            Dim alertsArray As JArray = jsonObject("alerts")
            Dim alertCount As Integer = alertsArray.Count

            ' Display the result
            'Console.WriteLine($"Number of alerts collected: {alertCount}")
            intLinhas = alertCount

        Catch ex As Exception
            Console.WriteLine("An error occurred while reading the JSON file: " & ex.Message)
        End Try

        Return intLinhas

    End Function

    Private Sub CountJsonLines(ByVal strJsonFile As String)
        ' Define the JSON file path (adjust based on your setup)
        Dim jsonFile As String = Path.Combine(_strPastaProj, strJsonFile)

        Try
            ' Read the JSON file
            Dim jsonText As String = File.ReadAllText(jsonFile)
            Dim jsonObject As JObject = JObject.Parse(jsonText)

            ' Navigate to the "list" array
            Dim data As JObject = jsonObject("data")
            If data Is Nothing Then
                txtMensagens.Text &= ("Error: 'data' key not found in JSON.")
                Exit Sub
            End If

            Dim alertsServiceSearch As JObject = data("alertsServiceSearch")
            If alertsServiceSearch Is Nothing Then
                txtMensagens.Text &= ("Error: 'alertsServiceSearch' key not found in JSON.")
                Exit Sub
            End If

            Dim alerts As JObject = alertsServiceSearch("alerts")
            If alerts Is Nothing Then
                txtMensagens.Text &= ("Error: 'alerts' key not found in JSON.")
                Exit Sub
            End If

            Dim alertsArray As JArray = alerts("list")
            If alertsArray Is Nothing Then
                txtMensagens.Text &= ("Error: 'list' key not found or not an array in JSON.")
                Exit Sub
            End If

            ' Count the alerts
            Dim alertCount As Integer = alertsArray.Count
            txtMensagens.Text &= ($"Total number of alerts: {alertCount}") & vbCrLf
            _blnJsonOK = True
            _intLinhasJson = alertCount

        Catch ex As FileNotFoundException
            txtMensagens.Text &= ($"Error: JSON file '{jsonFile}' not found.")
        Catch ex As JsonException
            txtMensagens.Text &= ($"Error: Invalid JSON format in '{jsonFile}'. Details: {ex.Message}")
        Catch ex As Exception
            txtMensagens.Text &= ($"Unexpected error: {ex.Message}")
        End Try

    End Sub

    Private Sub optMes_CheckedChanged(sender As Object, e As EventArgs) Handles optMes.CheckedChanged
        'Opção para processar o mês inteiro
        '17/03/25
        '==================================
        _strDiasProcesso = "Mes"
        _datFirstDayLastMonth = New Date(_today.Year, _today.Month, 1).AddMonths(-1)
        _strMensagemProcesso = "Período: " & _datFirstDayLastMonth.Month & "/" & _datFirstDayLastMonth.Year & " - Processando o mês inteiro... Aguarde" & vbCrLf

    End Sub

    Private Sub opt_1_a_15_CheckedChanged(sender As Object, e As EventArgs) Handles opt_1_a_15.CheckedChanged
        'Opção para processar a primeira quinzena
        '17/03/25
        '========================================
        _strDiasProcesso = "1_a_15"
        _datFirstDayLastMonth = New Date(_today.Year, _today.Month, 1).AddMonths(-1)
        _strMensagemProcesso = "Período: " & _datFirstDayLastMonth.Month & "/" & _datFirstDayLastMonth.Year & " - Processando a 1ª quinzena... Aguarde" & vbCrLf

    End Sub

    Private Sub opt_16_fim_CheckedChanged(sender As Object, e As EventArgs) Handles opt_16_fim.CheckedChanged
        'Opção para processar a segunda quinzena
        '17/03/25
        '========================================
        _strDiasProcesso = "16_a_fim"
        _datFirstDayLastMonth = New Date(_today.Year, _today.Month, 15).AddMonths(-1)
        _strMensagemProcesso = "Período: " & _datFirstDayLastMonth.Month & "/" & _datFirstDayLastMonth.Year & " - Processando a 2ª quinzena... Aguarde" & vbCrLf

    End Sub

    Private Sub opt_1_a_10_CheckedChanged(sender As Object, e As EventArgs) Handles opt_1_a_10.CheckedChanged
        'Opção para processar do dia 1 ao dia 10
        '17/03/25
        '=======================================
        _strDiasProcesso = "1_a_10"
        _datFirstDayLastMonth = New Date(_today.Year, _today.Month, 1).AddMonths(-1)
        _strMensagemProcesso = "Período: " & _datFirstDayLastMonth.Month & "/" & _datFirstDayLastMonth.Year & " - Processando do dia 1 ao dia 10... Aguarde" & vbCrLf

    End Sub

    Private Sub opt_11_a_20_CheckedChanged(sender As Object, e As EventArgs) Handles opt_11_a_20.CheckedChanged
        'Opção para processar do dia 11 ao dia 20
        '17/03/25
        '========================================
        _strDiasProcesso = "11_a_20"
        _datFirstDayLastMonth = New Date(_today.Year, _today.Month, 10).AddMonths(-1)
        _strMensagemProcesso = "Período: " & _datFirstDayLastMonth.Month & "/" & _datFirstDayLastMonth.Year & " - Processando do dia 11 ao dia 20... Aguarde" & vbCrLf

    End Sub

    Private Sub opt_21_a_fim_CheckedChanged(sender As Object, e As EventArgs) Handles opt_21_a_fim.CheckedChanged
        'Opção para processar do dia 21 ao fim do mês
        '17/03/25
        '============================================
        _strDiasProcesso = "21_a_fim"
        _datFirstDayLastMonth = New Date(_today.Year, _today.Month, 20).AddMonths(-1)
        _strMensagemProcesso = "Período: " & _datFirstDayLastMonth.Month & "/" & _datFirstDayLastMonth.Year & " - Processando do dia 1 ao dia " &
                                DateTime.DaysInMonth(_datFirstDayLastMonth.Year, _datFirstDayLastMonth.Month) & "... Aguarde" & vbCrLf

    End Sub

End Class
