Imports System.Data.SqlClient
Imports Microsoft.VisualBasic.FileIO
Imports VBActive_Taegis_DLL.ActiveTaegisDLL

Module Modulo_Active_Taegis_DataSources_Auto1
    'Módulo de extração 1 - VS2022
    '17/10/23
    '07/03/24-21/03/24
    '=============================
    Private _strPastaProj As New String("")
    Private _strSaida As New String("")
    Private _strLog As New String("")
    Private _strPastaPython As New String("")
    Private _intLinhasSQL_Inc As Integer = 0
    Private _intLinhasSQL_Alt As Integer = 0
    Private _intLinhasLidas As Integer = 0

    Private _aClientName As New List(Of String)
    Private _aTenantId As New List(Of String)
    Private _aClientId As New List(Of String)
    Private _aClientSecret As New List(Of String)

    Public Value As String

    Public Sub Main()
        '17/10/23-19/10/23
        'Chamada principal do Módulo de extração 1
        '=========================================
        _strPastaPython = LeConfigPasta("pasta_python")
        _strPastaProj = LeConfigPasta("pasta_proj_datasources_auto")

        ExcluiArquivosAnteriores()
        CargaSQLClientes()

        For i As Integer = 0 To _aTenantId.Count - 1
            Gera_DataSources_CSV(_aClientName(i), "delta", _aTenantId(i), _aClientId(i), _aClientSecret(i))
            Gera_SQL(_aClientName(i), _aTenantId(i))
            GravaLogAsset(_aTenantId(i), _aClientId(i), _aClientSecret(i), _aClientName(i))
        Next i

    End Sub

    Private Sub CargaSQLClientes()
        'Carga dos clientes do banco SQL Active
        '17/10/23
        '======================================
        Dim oCon As New SqlConnection(ConnectionString)
        Dim oCmd As New SqlCommand
        Dim oDrd As SqlDataReader

        oCon.Open()

        With oCmd
            .Connection = oCon
            .CommandType = CommandType.Text
            .CommandText = "SELECT client_name, tenant_id, client_id, client_secret from t_taegis_tab_clients"
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

    Private Sub ExcluiArquivosAnteriores()
        '17/10/23
        'Exclusão de arquivos de trabalho *.csv
        '======================================
        For Each fileFound As String In IO.Directory.GetFiles(_strPastaProj).Where(Function(fi) IO.Path.GetFileName(fi) Like "*.csv").ToArray
            IO.File.Delete(fileFound)
        Next

    End Sub

    Private Sub GravaLogAsset(ByVal strTenantId As String, ByVal strClientId As String, ByVal strClientSecret As String, ByVal strClientName As String)
        '17/10/23
        'Grava o log de geração dos arquivos de DataSources na tabela t_taegis_log
        '=====================================================================
        Dim oCon As New SqlConnection(ConnectionString)
        Dim oCmd As New SqlCommand

        oCon.Open()

        With oCmd
            .Connection = oCon
            .CommandType = CommandType.Text
            .CommandText = "insert into t_taegis_log(tenant_id, client_name, data_log, qtde_inc, tipo_log, texto_log)" &
            " values('" & strTenantId & "', '" & strClientName & "', getdate(), " & _intLinhasSQL_Inc & ", 'DataSources', '" & _strLog & "')"
            .CommandTimeout = 3600
            .ExecuteNonQuery()
        End With

        oCon.Close()

    End Sub

    Private Sub Gera_DataSources_CSV(ByVal strClientName As String, ByVal strEnvironment As String, ByVal strTenantId As String, ByVal strClientId As String, ByVal strClientSecret As String)
        '17/10/23
        'Gera o arquivo assets01_TenantId_yyyymmdd.json
        '==============================================
        Dim oProcess As Process = New Process()
        Dim oStartInfo As New ProcessStartInfo(_strPastaPython, "data_sources_count_oauth2_pars.py " & strClientName & " " & strEnvironment & " " & strTenantId & " " & strClientId & " " & strClientSecret)
        'Exemplo: python data_sources_query.py 142779 delta

        Dim strSaida As New String("data_sources_count_" & strTenantId & "_" & MesPassado() & ".csv")
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

        oProcess.Dispose()
        oProcess = Nothing
        _strLog &= "1) Arquivo CSV " & strSaida & " gravado com sucesso - Linhas: " & strLinhas.Length & vbCrLf

    End Sub

    Private Sub Gera_SQL(ByVal strClientName As String, ByVal strTenantId As String)
        'Botão SQL - Grava tabela SQL t_taegis_data_sources a partir do arquivo data_sources_count_<tenant_id>_yyyyMM01_yyyyMM30.csv
        '17/10/23
        '===========================================================================================================================
        Dim strSaida As New String("\data_sources_count_" & strTenantId & "_" & MesPassado() & ".csv")
        Dim tfp As New TextFieldParser(_strPastaProj & strSaida)

        tfp.Delimiters = New String() {";"}
        tfp.TextFieldType = FieldType.Delimited

        While tfp.EndOfData = False
            Dim fields = tfp.ReadFields()

            If Not ExisteDataSourcesQL(strTenantId, fields(0), fields(1)) Then
                GravaSQL_Inclui(strClientName, strTenantId, fields)
                _intLinhasSQL_Inc += 1
            Else
                GravaSQL_Altera(strTenantId, fields)
                _intLinhasSQL_Alt += 1
            End If
            _intLinhasLidas += 1
        End While

        _strLog &= "Linhas alteradas na tabela SQL t_taegis_data_sources: " & _intLinhasSQL_Alt & vbCrLf &
                        "Linhas incluídas na tabela SQL t_taegis_data_sources: " & _intLinhasSQL_Inc & vbCrLf &
                        "Total de linhas CSV lidas: " & _intLinhasLidas
        _strLog &= vbCrLf & "===================================================================================" & vbCrLf
        _intLinhasSQL_Alt = 0
        _intLinhasSQL_Inc = 0
        _intLinhasLidas = 0

    End Sub

    Private Function ExisteDataSourcesQL(strTenantId As String, strDateStart As String, strDateEnd As String) As Boolean
        'Verifica se o eventa existe na tabela SQL t_taegis_data_sources
        '17/10/23
        '===============================================================
        Dim oCon As New SqlConnection(ConnectionString)
        Dim oCmd As New SqlCommand
        Dim intQtd As Integer = 0

        oCon.Open()

        With oCmd
            .Connection = oCon
            .CommandType = CommandType.Text
            .CommandText = "select count(*) from t_taegis_data_sources " &
                           "where event_tenant_id = '" & strTenantId & "'"
            .CommandTimeout = 3600
            intQtd = .ExecuteScalar
        End With

        oCon.Close()

        Return (intQtd > 0)

    End Function

    Private Sub GravaSQL_Inclui(ByVal strClientName As String, ByVal strTenantId As String, ByVal fields As String())
        'Inclui linhas na tabela SQL t_taegis_data_sources
        '17/10/23
        '=================================================
        Dim oCon As New SqlConnection(ConnectionString)
        Dim oCmd As New SqlCommand

        oCon.Open()

        Try

            With oCmd
                .Connection = oCon
                .CommandType = CommandType.Text
                .CommandText = "insert into t_taegis_data_sources(" &
                    "event_date, " &
                    "event_tenant_id," &
                    "event_client, " &
                    "event_qty_healthy, " &
                    "event_qty_warning, " &
                    "event_qty_nodata) " &
                    "values('" & fields(1) & "', '" &
                    strTenantId & "', '" &
                    strClientName & "', " &
                    fields(2) & ", " &
                    fields(3) & ", " &
                    fields(4) & ")"
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
        'Altera linhas na tabela SQL t_taegis_data_sources
        '17/10/23
        '=================================================
        Dim oCon As New SqlConnection(ConnectionString)
        Dim oCmd As New SqlCommand

        oCon.Open()

        Try

            With oCmd
                .Connection = oCon
                .CommandType = CommandType.Text
                .CommandText = "update t_taegis_data_sources " &
                    "set event_date = " & fields(1) & ", " &
                    "event_qty_healthy = " & fields(2) & ", " &
                    "event_qty_warning = " & fields(3) & ", " &
                    "event_qty_nodata = " & fields(4) & ", " &
                    "alt_data = getdate(), " &
                    "alt_user = suser_name() " &
                    "where event_tenant_id = '" & strTenantId & "'"
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

    Private Function MesPassado() As String
        'Retorna o primeiro e o último dia do mês passado
        '24/08/23
        '================================================
        Dim intLastMonth As Integer = 0
        Dim intYear As Integer = 0

        If Not Date.Today.Month = 1 Then
            intLastMonth = Date.Today.Month - 1
            intYear = Date.Today.Year
        Else
            intLastMonth = 12
            intYear = Date.Today.Year - 1
        End If

        Dim intDaysInMonth As Integer = Date.DaysInMonth(intYear, intLastMonth)
        Dim d As New Date(intYear, intLastMonth, 1)
        Dim strFirstDay As New String(d.Date.ToString("yyyyMMdd"))
        Dim strLastDay As New String(d.AddDays(intDaysInMonth - 1).Date.ToString("yyyyMMdd"))

        Return strFirstDay & "_" & strLastDay

    End Function

End Module
