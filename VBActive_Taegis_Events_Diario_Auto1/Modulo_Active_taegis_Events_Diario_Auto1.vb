Imports System.Data.SqlClient
Imports Microsoft.VisualBasic.FileIO
Imports VBActive_Taegis_DLL.ActiveTaegisDLL

''' <summary>
''' Sistema Active - VS2022
''' Programa: Active_taegis_Events_Diario_Auto1
''' Processa o promeiro passo da extração de Events da Taegis para a geração do arquivo events_count_tenantId_aaaammdd_aaaammdd
''' Autor: Heron Domingues Jr
''' Data:  23/12/24
''' Alteração Data: 
''' Obs: Nas propriedades do projeto desmarcar a opção "enable application framework" e mudar a caixa "Startup Object" para a Sub Main deste módulo
''' </summary>
''' <remarks></remarks>

Module Modulo_Active_taegis_Events_Diario_Auto1
    '29/08/23-11/09/23
    'Módulo de extração 1
    '====================
    Private _strPastaProj As New String("")
    Private _strSaida As New String("")
    Private _strLog As New String("")
    Private _strPastaPython As New String("")
    Private _intLinhasSQL_Inc As Integer = 0
    Private _intLinhasSQL_Alt As Integer = 0
    Private _intLinhasLidas As Integer = 0
    Private _datExtracao As Date = Now()

    Private _aClientName As New List(Of String)
    Private _aTenantId As New List(Of String)
    Private _aClientId As New List(Of String)
    Private _aClientSecret As New List(Of String)

    Public Value As String

    Public Sub Main()
        '29/08/23-30/08/23
        '24/12/24
        'Chamada principal do Módulo de extração 1
        '=========================================
        _strPastaPython = LeConfigPasta("pasta_python")
        'APÓS HOMOLOGAR O PROGRAMA COMENTAR A FUNÇÃO .Replace("\Release", "\Debug") 
        _strPastaProj = LeConfigPasta("pasta_proj_events_diario_auto")  '.Replace("\Release", "\Debug")

        ExcluiArquivosAnteriores()
        CargaSQLClientes()

        For i As Integer = 0 To _aTenantId.Count - 1
            Gera_Events_CSV(_aClientName(i), "delta", _aTenantId(i), _aClientId(i), _aClientSecret(i))
            Gera_SQL(_aClientName(i), _aTenantId(i))
            GravaLogAsset(_aTenantId(i), _aClientId(i), _aClientSecret(i), _aClientName(i))
        Next i

    End Sub

    Private Sub CargaSQLClientes()
        'Carga dos clientes do banco SQL Active
        '29/08/23
        '23/12/24
        '======================================
        Dim oCon As New SqlConnection(ConnectionString)
        Dim oCmd As New SqlCommand
        Dim oDrd As SqlDataReader

        oCon.Open()

        With oCmd
            .Connection = oCon
            .CommandType = CommandType.Text
            .CommandText = "USE ACTIVE
                            SELECT ac.client_name client_name, ac.tenant_id tenant_id, ac.client_id client_id, ac.client_secret client_secret 
                                FROM t_taegis_tab_clients ac
                            INNER JOIN DWNetcenter.dbo.t_abrev_clientes dc ON ac.client_name = dc.nome
                            WHERE dc.cliente_active = 1"
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

    Private Sub Gera_Events_CSV(ByVal strClientName As String, ByVal strEnvironment As String, ByVal strTenantId As String, ByVal strClientId As String, ByVal strClientSecret As String)
        'Grava arquivo "events_count_tenentId_aaaammdd_aaaammdd.csv"
        '23/12/24
        '===========================================================
        Dim oProcess As Process = New Process()
        Dim oStartInfo As New ProcessStartInfo(_strPastaPython,
                                                   "events_count_oauth2_dia_pars.py " & _datExtracao.Year & "-" & _datExtracao.Month.ToString("00") &
                                                   "-" & _datExtracao.Day.ToString("00") & " " &
                                               strClientName & " " & strEnvironment & " " & strTenantId & " " & strClientId & " " & strClientSecret)
        'Exemplo: python events_query.py 2024-12-23 142779 delta
        Dim strSaida As New String("events_count_" & strTenantId & "_" & Ontem() & ".csv")
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

    Private Sub Gera_SQL(ByVal strClientName As String, ByVal strTenantId As String)
        'Botão SQL - Grava tabela SQL t_taegis_events a partir do arquivo events_count_[tenant_id]_[yyyymm01]_[yyyymm31].csv
        '29/08/23-30/08/23-11/09/23
        '24/12/24
        '===================================================================================================================
        Dim strSaida As New String("\events_count_" & strTenantId & "_" & Ontem() & ".csv")

        Dim tfp As New TextFieldParser(_strPastaProj & strSaida)
        tfp.Delimiters = New String() {";"}
        tfp.TextFieldType = FieldType.Delimited

        While tfp.EndOfData = False
            Dim fields = tfp.ReadFields()

            If Not ExisteEventoSQL(strTenantId, fields(0), fields(1)) Then
                GravaSQL_Inclui(strClientName, strTenantId, fields)
                _intLinhasSQL_Inc += 1
            Else
                GravaSQL_Altera(strTenantId, fields)
                _intLinhasSQL_Alt += 1
            End If
            _intLinhasLidas += 1
        End While

        _strLog &= "Linhas alteradas na tabela SQL t_taegis_events_diario: " & _intLinhasSQL_Alt & vbCrLf &
                        "Linhas incluídas na tabela SQL t_taegis_events_diario: " & _intLinhasSQL_Inc & vbCrLf &
                        "Total de linhas CSV lidas: " & _intLinhasLidas
        _strLog &= vbCrLf & "===================================================================================" & vbCrLf
        _intLinhasSQL_Alt = 0
        _intLinhasSQL_Inc = 0
        _intLinhasLidas = 0

    End Sub

    Private Sub GravaSQL_Inclui(ByVal strClientName As String, ByVal strTenantId As String, ByVal fields As String())
        'Inclui linhas na tabela SQL t_taegis_events
        '29/08/23-30/08/23-11/09/23-16/10/23
        '23/12/24
        '===========================================
        Dim oCon As New SqlConnection(ConnectionString)
        Dim oCmd As New SqlCommand

        oCon.Open()

        Try

            With oCmd
                .Connection = oCon
                .CommandType = CommandType.Text
                .CommandText = "insert into t_taegis_events_diario(" &
                            "event_date_start, " &
                            "event_date_end, " &
                            "event_tenant_id," &
                            "event_client, " &
                            "event_qty) " &
                            "values('" & fields(0) & "', '" &
                            fields(1) & "', '" &
                            strTenantId & "', '" &
                            strClientName & "', " &
                            fields(2) & ")"
                .CommandTimeout = 3600
                .ExecuteNonQuery()
            End With

            oCon.Close()

        Catch ex As SqlException
            _strLog &= vbCrLf & "Erro SQL: " & ex.ToString
            _strLog &= vbCrLf & "==================================================================================="

        Catch ex1 As Exception
            _strLog &= vbCrLf & "Inclui SQL => Erro linha " & fields(0) & ": " & ex1.ToString
            _strLog &= vbCrLf & "==================================================================================="

        End Try

    End Sub

    Private Sub GravaSQL_Altera(ByVal strTenantId As String, ByVal fields As String())
        'Altera linhas na tabela SQL t_taegis_events
        '11/09/23-20/10/23
        '23/12/24
        '===========================================
        Dim oCon As New SqlConnection(ConnectionString)
        Dim oCmd As New SqlCommand

        oCon.Open()

        Try

            With oCmd
                .Connection = oCon
                .CommandType = CommandType.Text
                .CommandText = "update t_taegis_events_diario " &
                    "set event_qty = " & fields(2) & ", " &
                    "alt_data = getdate(), " &
                    "alt_user = suser_name() " &
                    "where event_tenant_id = '" & strTenantId & "' and event_date_start = '" & fields(0) & "'"
                .CommandTimeout = 3600
                .ExecuteNonQuery()
            End With

            oCon.Close()

        Catch ex As SqlException
            _strLog &= vbCrLf & "Erro SQL: " & ex.ToString
            _strLog &= vbCrLf & "==================================================================================="

        Catch ex1 As Exception
            _strLog &= vbCrLf & "Inclui SQL => Erro linha " & fields(0) & ": " & ex1.ToString
            _strLog &= vbCrLf & "==================================================================================="

        End Try

    End Sub


    Private Function ExisteEventoSQL(strTenantId As String, strDateStart As String, strDateEnd As String) As Boolean
        'Verifica se o eventa existe na tabela SQL t_taegis_events
        '28/08/23
        '=========================================================
        Dim oCon As New SqlConnection(ConnectionString)
        Dim oCmd As New SqlCommand
        Dim intQtd As Integer = 0

        oCon.Open()

        With oCmd
            .Connection = oCon
            .CommandType = CommandType.Text
            .CommandText = "select count(*) from t_taegis_events_diario " &
                           "where event_date_start = '" & strDateStart & "' AND event_date_end = '" & strDateEnd & "' AND event_tenant_id = '" & strTenantId & "'"
            .CommandTimeout = 3600
            intQtd = .ExecuteScalar
        End With

        oCon.Close()

        Return (intQtd > 0)

    End Function

    Private Function Ontem() As String
        'Retorna o dia de ontem
        '19/12/24-20/12/24
        '======================
        'Dim strFirstDay As New String(DateTime.Now.AddDays(-1).ToString("yyyyMMdd"))
        Dim strFirstDay As New String(_datExtracao.AddDays(-1).ToString("yyyyMMdd"))

        Return strFirstDay & "_" & strFirstDay

    End Function

    Private Sub ExcluiArquivosAnteriores()
        '29/08/23
        'Exclusão de arquivos de trabalho *.csv
        '======================================
        For Each fileFound As String In IO.Directory.GetFiles(_strPastaProj).Where(Function(fi) IO.Path.GetFileName(fi) Like "*.csv").ToArray
            IO.File.Delete(fileFound)
        Next

    End Sub

    Private Sub GravaLogAsset(ByVal strTenantId As String, ByVal strClientId As String, ByVal strClientSecret As String, ByVal strClientName As String)
        '29/08/23
        'Grava o log de geração dos arquivos de events na tabela t_taegis_log
        '=====================================================================
        Dim oCon As New SqlConnection(ConnectionString)
        Dim oCmd As New SqlCommand

        oCon.Open()

        With oCmd
            .Connection = oCon
            .CommandType = CommandType.Text
            .CommandText = "insert into t_taegis_log(tenant_id, client_name, data_log, qtde_inc, tipo_log, texto_log)" &
            " values('" & strTenantId & "', '" & strClientName & "', getdate(), " & _intLinhasSQL_Inc & ", 'Events Diario', '" & _strLog & "')"
            .CommandTimeout = 3600
            .ExecuteNonQuery()
        End With

        oCon.Close()

    End Sub

End Module
