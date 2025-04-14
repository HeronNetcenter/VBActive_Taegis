Imports System.Data.SqlClient
Imports Microsoft.VisualBasic.FileIO
Imports VBActive_Taegis_DLL.ActiveTaegisDLL

''' <summary>
''' Processa o promeiro passo da extração de alertas da Taegis para a geração do arquivo alerts01_yyyymmdd.json - VS2022
''' Autor: Heron Domingues Jr
''' Data:  14/03/23
''' Alteração Data: 04/04/23-07/03/24-21/03/24
''' 27/12/24: Inclusão do campo metadata_resolved_at
''' Obs: Nas propriedades do projeto desmarcar a opção "enable application framework" e mudar a caixa "Startup Object" para a Sub Main deste módulo
''' </summary>
''' <remarks></remarks>

Module Modulo_Active_Taegis_Alerts_Auto1
    '14/03/23-04/04/23-30/11/23
    'Módulo de extração 1
    '====================
    Private fd As New FolderBrowserDialog
    Private _strPastaProj As New String("")
    Private _strSaida As New String("")
    Private _strLog As New String("")
    Private _strPastaPython As New String("")
    Private _intLinhasSQL_Inc As Integer = 0
    Private _intLinhasSQL_Alt As Integer = 0
    Private _intLinhasLidas As Integer = 0
    Private _aTenantId As New List(Of String)
    Private _aClientId As New List(Of String)
    Private _aClientSecret As New List(Of String)
    Private _aClientName As New List(Of String)
    Private _blnClienteComAlertas As Boolean = False
    Private _intAlerts As Integer = 0
    Private _intAttachTT As Integer = 0
    Public Value As String

    Public Sub Main()
        'Chamada principal do Módulo de extração 1
        '14/03/23-30/08/23
        '=========================================
        _strPastaPython = LeConfigPasta("pasta_python")
        _strPastaProj = LeConfigPasta("pasta_proj_alerts_auto")

        ExcluiArquivosAnteriores()
        CargaSQLClintes()

        For i As Integer = 0 To _aTenantId.Count - 1
            Gera_Alert1_Json(_aTenantId(i), _aClientId(i), _aClientSecret(i), _aClientName(i))
            Gera_Alert2_CSV(_aTenantId(i), _aClientId(i), _aClientSecret(i), _aClientName(i))
            Dim paths() = IO.Directory.GetFiles(_strPastaProj, "alerts*.csv")
            _blnClienteComAlertas = (paths.Length > 0)
            Gera_SQL(_aTenantId(i), _aClientId(i), _aClientSecret(i), _aClientName(i))
            If i = _aTenantId.Count - 1 Then
                SQLTechTact()
            End If
            GravaLogAlert(_aTenantId(i), _aClientId(i), _aClientSecret(i), _aClientName(i))
            _intLinhasSQL_Inc = 0
            _intLinhasSQL_Alt = 0
            _strLog = ""
        Next



    End Sub

    Private Sub CargaSQLClintes()
        'Carga dos clientes do banco SQL Active
        '04/04/23
        '======================================
        Dim oCon As New SqlConnection(ConnectionString)
        Dim oCmd As New SqlCommand
        Dim oDrd As SqlDataReader

        oCon.Open()

        With oCmd
            .Connection = oCon
            .CommandType = CommandType.Text
            .CommandText = "SELECT tenant_id, client_id, client_secret, client_name from t_taegis_tab_clients"
            .CommandTimeout = 3600
            oDrd = .ExecuteReader
        End With

        With oDrd

            While .Read
                _aTenantId.Add(.Item("tenant_id"))
                _aClientId.Add(.Item("client_id"))
                _aClientSecret.Add(.Item("client_secret"))
                _aClientName.Add(.Item("client_name"))
            End While

            .Close()
        End With

        oCon.Close()

    End Sub

    Private Sub Gera_Alert1_Json(ByVal strTenantId As String, ByVal strClientId As String, ByVal strClientSecret As String, ByVal strClientName As String)
        '14/03/23-04/04/23
        'Gera o arquivo alerts01_yyyymmdd.json
        '=====================================
        Dim oProcess As Process = New Process()
        Dim oStartInfo As New ProcessStartInfo(_strPastaPython, "alerts01.py " &
                                               strTenantId & " " & strClientId & " " & strClientSecret)
        Dim strSaida As New String("alerts01_" & strTenantId & "_" & DateTime.Now.AddDays(-1).ToString("yyyyMMdd") & ".json")
        Dim strLinhas() As String

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

        _strLog &= "1) Arquivo JSON " & strSaida & " gravado com sucesso - Linhas: " & strLinhas.Length & vbCrLf

    End Sub

    Private Sub Gera_Alert2_CSV(ByVal strTenantId As String, ByVal strClientId As String, ByVal strClientSecret As String, ByVal strClientName As String)
        '15/03/23-04/04/23
        'Gera o arquivo alerts02_yyyymmdd.csv
        '====================================
        Dim oProcess As Process = New Process()
        Dim oStartInfo As New ProcessStartInfo(_strPastaPython, "alerts02.py " &
                                               strTenantId)
        Dim strSaida As New String("alerts02_" & strTenantId & "_" & DateTime.Now.AddDays(-1).ToString("yyyyMMdd") & ".csv")
        Dim strLinhas() As String

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

        _strLog &= "2) Arquivo CSV " & strSaida & " gravado com sucesso - Linhas: " & strLinhas.Length & vbCrLf

    End Sub

    Private Sub Gera_SQL(ByVal strTenantId As String, ByVal strClientId As String, ByVal strClientSecret As String, ByVal strClientName As String)
        'Grava tabela SQL t_taegis_alerts a partir do arquivo alerts02_aaaammdd.csv
        '15/03/23-04/04/23-11/12/23
        '27/12/24
        '==========================================================================
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
        Dim strData As New String(DateTime.Now.AddDays(-1).ToString("yyyyMMdd"))
        Dim strSaida As New String("\alerts02_" & strTenantId & "_" & strData & ".csv")

        If _blnClienteComAlertas Then
            Dim tfp As New TextFieldParser(_strPastaProj & strSaida)
            tfp.Delimiters = New String() {";"}
            tfp.TextFieldType = FieldType.Delimited

            tfp.ReadLine() ' skip header

            While tfp.EndOfData = False
                Dim fields = tfp.ReadFields()
                'Dim fields2 = tfp.ReadFields()  'Teste

                If ExisteAlertaSQL(strTenantId, fields(4)) Then
                    GravaSQL_Altera(fields, strTenantId)
                    _intLinhasSQL_Alt += 1
                Else
                    GravaSQL_Inclui(fields, strTenantId, strClientName, strData)
                    _intLinhasSQL_Inc += 1
                End If
                _intLinhasLidas += 1
            End While

            _strLog &= "Linhas alteradas na tabela SQL t_taegis_alerts: " & _intLinhasSQL_Alt & vbCrLf &
                            "Linhas incluídas na tabela SQL t_taegis_alerts: " & _intLinhasSQL_Inc & vbCrLf &
                            "Total de linhas CSV lidas: " & _intLinhasLidas & vbCrLf
        Else
            _strLog &= "Cliente não possui alertas neste período" & vbCrLf
        End If

    End Sub

    Private Function ExisteAlertaSQL(strTenantId As String, strId As String) As Boolean
        'Verifica se o alerta existe na tabela SQL t_taegis_alerts
        '15/03/23
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

    Private Sub GravaSQL_Inclui(fields As String(), ByVal strTenantID As String, ByVal strClientName As String, ByVal strData As String)
        'Inclui linhas na tabela SQL t_taegis_alerts
        '15/03/23-04/04/23-11/12/23
        '27/12/24
        '===========================================
        Dim oCon As New SqlConnection(ConnectionString)
        Dim oCmd As New SqlCommand
        Dim quote As String = """"    'Uma aspa dupla como string

        oCon.Open()

        Try

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
                    "metadata_first_resolved_at) " &
                    "values('" & strData & "', " &
                    fields(0) & ", '" &
                    strTenantID & "', '" &
                    strClientName & "', '" &
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
                    fields(20).Replace("'", quote) & "')"
                .CommandTimeout = 3600
                .ExecuteNonQuery()
            End With

            oCon.Close()

        Catch ex As SqlException
            _strLog &= vbCrLf & "Erro SQL: " & ex.ToString

        Catch ex1 As Exception
            _strLog &= vbCrLf & "Inclui SQL => Erro linha " & fields(0) & ": " & ex1.ToString

        End Try

    End Sub

    Private Sub GravaSQL_Altera(fields As String(), ByVal strTenantId As String)
        'Altera linhas na tabela SQL t_taegis_alerts
        '15/03/23-04/04/23-14/10/23-11/12/23
        '27/12/24
        '===========================================
        Dim oCon As New SqlConnection(ConnectionString)
        Dim oCmd As New SqlCommand
        Dim quote As String = """"    'Uma aspa dupla como string

        oCon.Open()

        Try

            With oCmd
                .Connection = oCon
                .CommandType = CommandType.Text
                .CommandText = "update t_taegis_alerts " &
                    "set attack_technique_ids = '" & fields(1).Replace("'", "|") & "', " &
                    "tactics_technique_id = '" & fields(19).Replace("'", quote) & "', " &
                    "entities = '" & fields(2).Replace("'", "|") & "', " &
                    "ent_relationships = '" & fields(3).Replace("'", "|") & "', " &
                    "id = '" & fields(4).Replace("'", "|") & "', " &
                    "investigation_ids = '" & fields(5).Replace("'", "|") & "', " &
                    "metadata_confidence = '" & fields(6).Replace("'", "|") & "', " &
                    "metadata_first_resolved_at = '" & fields(20).Replace("'", "|") & "', " &
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
                    "where id = '" & fields(4) & "'AND alert_tenant_id = '" & strTenantId & "'"
                .ExecuteNonQuery()
                .CommandTimeout = 3600
            End With

            oCon.Close()

        Catch ex As SqlException
            _strLog &= vbCrLf & "Erro SQL => Linha " & fields(0) & ": " & ex.ToString


        Catch ex1 As Exception
            _strLog &= vbCrLf & "Altera SQL => Erro linha " & fields(0) & ": " & ex1.ToString

        End Try

    End Sub

    Private Sub GravaLogAlert(ByVal strTenantId As String, ByVal strClientId As String, ByVal strClientSecret As String, ByVal strClientName As String)
        '15/03/23-04/04/23-14/10/23
        'Grava o log de geração dos arquivos de alertas na tabela t_taegis_log
        '=====================================================================
        Dim oCon As New SqlConnection(ConnectionString)
        Dim oCmd As New SqlCommand

        oCon.Open()

        With oCmd
            .Connection = oCon
            .CommandType = CommandType.Text
            .CommandText = "insert into t_taegis_log(tenant_id, client_name, data_log, qtde_inc, tipo_log, texto_log)" &
            " values('" & strTenantId & "', '" & strClientName & "', getdate(), " & _intLinhasSQL_Inc & ", 'Alerts', '" & _strLog.Replace("'", "|") & "')"
            .CommandTimeout = 3600
            .ExecuteNonQuery()
        End With

        oCon.Close()

    End Sub

    Private Sub ExcluiArquivosAnteriores()
        '15/03/23
        'Exclusão de arquivos de trabalho *.json e *.csv
        '===============================================
        Dim strJson As New String(_strPastaProj & "\*.json")
        Dim strCsv As New String(_strPastaProj & "\*.csv")

        For Each fileFound As String In System.IO.Directory.GetFiles(_strPastaProj).Where(Function(fi) System.IO.Path.GetFileName(fi) Like "*.json").ToArray
            System.IO.File.Delete(fileFound)
        Next

        For Each fileFound As String In System.IO.Directory.GetFiles(_strPastaProj).Where(Function(fi) System.IO.Path.GetFileName(fi) Like "*.csv").ToArray
            System.IO.File.Delete(fileFound)
        Next

    End Sub

#Region "Tactics & Techniques"

    Private Sub SQLTechTact()
        'Tratamento dos campos de Técnicas e Táticas
        '29/11/23
        '===========================================
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

        _strLog &= vbCrLf & "Tabela t_taegis_alerts_ linhas lidas: " & _intAlerts
        _strLog &= vbCrLf & "Tabela t_taegis_alerts_tt linhas gravadas: " & _intAttachTT
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
        '12/12/23
        '========
        Dim regex As New System.Text.RegularExpressions.Regex("tactics")
        Dim aTactics As New ArrayList
        Dim quote As String = """"
        Dim plick As String = "'"
        Dim intMach As Integer = 0

        'Percorre o campo tactics_technique_id para encontrar a palavra "tactics" e recuperar a tática
        For Each match As System.Text.RegularExpressions.Match In regex.Matches(strTactics)
            intMach += 1
            aTactics.Add(strTactics.Substring(match.Index + 11).Replace(quote, plick).Split(plick)(1) & " [" & intMach & "]")

            For i = 0 To strTactics.Substring(match.Index + 11).Replace(quote, plick).Split(plick).Length - 3
                If strTactics.Substring(match.Index + 11).Replace(quote, plick).Split(plick)(i + 2) = ", " Then
                    aTactics.Add(strTactics.Substring(match.Index + 11).Replace(quote, plick).Split(plick)(i + 3) & " [" & intMach & "]")
                ElseIf strTactics.Substring(match.Index + 11).Replace(quote, plick).Split(plick)(i + 2) = "], " Then
                    Exit For
                End If
            Next i

        Next

        If strTenant = "137287" And strDate = "20231103" And intNum = 43 Then
            Dim k = 0
        End If

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

        If strId = "alert://priv:event-filter:137287:1699028329024:c9e42212-73b5-5bf6-b21f-c806ebfdfab5" Then
            Dim k = 0
        End If

        Return aTechnique_id

    End Function

#End Region

End Module
