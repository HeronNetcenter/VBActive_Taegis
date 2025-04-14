Imports System.Data.SqlClient
Imports Microsoft.VisualBasic.FileIO
Imports VBActive_Taegis_DLL.ActiveTaegisDLL

''' <summary>
''' Processa o promeiro passo da extração de Assets da Taegis para a geração do arquivo assets01_TenantId_yyyymmdd.json - VS2022
''' Autor: Heron Domingues Jr
''' Data:  16/03/23
''' Alteração Data: 17/04/23-07/03/24-21/03/24
''' 06/07/23: Inclusão de campos na tabela t_taegis_assets para a emissão do book do NOC
''' Obs: Nas propriedades do projeto desmarcar a opção "enable application framework" e mudar a caixa "Startup Object" para a Sub Main deste módulo
''' </summary>
''' <remarks></remarks>

Module Modulo_Active_Taegis_Assets_Auto1
    '16/03/23-17/04/23
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

    'Parâmetros para enviar aos módulos Python - 17/04/23
    Private _aTenantId As New List(Of String)
    Private _aClientId As New List(Of String)
    Private _aClientSecret As New List(Of String)
    Private _aClientName As New List(Of String)

    Public Value As String

    Public Sub Main()
        '14/03/23-17/04/23-19/10/23
        'Chamada principal do Módulo de extração 1
        '=========================================
        _strPastaPython = LeConfigPasta("pasta_python")
        _strPastaProj = LeConfigPasta("pasta_proj_assets_auto")

        ExcluiArquivosAnteriores()
        CargaSQLClientes()

        For i As Integer = 0 To _aTenantId.Count - 1
            Gera_Asset1_Json(_aTenantId(i), _aClientId(i), _aClientSecret(i), _aClientName(i))
            Parse_Asset2_Json(_aTenantId(i))
            Gera_Assets2_CSV(_aTenantId(i))
            Gera_SQL(_aTenantId(i), _aClientId(i), _aClientSecret(i), _aClientName(i))
            GravaLogAsset(_aTenantId(i), _aClientId(i), _aClientSecret(i), _aClientName(i))
        Next i

    End Sub

    Private Sub CargaSQLClientes()
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

    Private Sub Gera_Asset1_Json(ByVal strTenantId As String, ByVal strClientId As String, ByVal strClientSecret As String, ByVal strClientName As String)
        '14/03/23-17/04/23
        'Gera o arquivo assets01_TenantId_yyyymmdd.json
        '==============================================
        Dim oProcess As Process = New Process()
        Dim oStartInfo As New ProcessStartInfo(_strPastaPython, "assets01.py " &
                                               strTenantId & " " & strClientId & " " & strClientSecret)
        Dim strSaida As New String("assets01_" & strTenantId & "_" & DateTime.Now.AddDays(-1).ToString("yyyyMMdd") & ".json")
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

    Private Sub GravaLogAsset(ByVal strTenantId As String, ByVal strClientId As String, ByVal strClientSecret As String, ByVal strClientName As String)
        '16/03/23-17/04/23
        'Grava o log de geração dos arquivos de assets na tabela t_taegis_log
        '=====================================================================
        Dim oCon As New SqlConnection(ConnectionString)
        Dim oCmd As New SqlCommand

        oCon.Open()

        With oCmd
            .Connection = oCon
            .CommandType = CommandType.Text
            .CommandText = "insert into t_taegis_log(tenant_id, client_name, data_log, qtde_inc, tipo_log, texto_log)" &
            " values('" & strTenantId & "', '" & strClientName & "', getdate(), " & _intLinhasSQL_Inc & ", 'Assets', '" & _strLog & "')"
            .CommandTimeout = 3600
            .ExecuteNonQuery()
        End With

        oCon.Close()

    End Sub

    Private Sub Parse_Asset2_Json(ByVal strTenantId As String)
        'Parse Json - Grava arquivo assets02_TenantId_aaaammdd.json
        '16/03/23-17/04/23
        '==========================================================
        Dim oProcess As Process = New Process()
        Dim oStartInfo As New ProcessStartInfo(_strPastaPython, "parsejson.py " &
                                               strTenantId)
        Dim strSaida As New String("assets02_" & strTenantId & "_" & DateTime.Now.ToString("yyyyMMdd") & ".json")
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

        _strLog &= "2) Arquivo JSON " & strSaida & " gravado com sucesso - Linhas: " & strLinhas.Length & vbCrLf

    End Sub

    Private Sub Gera_Assets2_CSV(ByVal strTenantId As String)
        'Grava arquivo assets02_TenantId_aaaammdd.csv
        '16/03/23-17/04/23
        '============================================
        Dim oProcess As Process = New Process()
        Dim oStartInfo As New ProcessStartInfo(_strPastaPython, "assets02.py " &
                                               strTenantId)
        Dim strSaida As New String("assets02_" & strTenantId & "_" & DateTime.Now.ToString("yyyyMMdd") & ".csv")
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

        _strLog &= "3) Arquivo CSV " & strSaida & " gravado com sucesso - Linhas: " & strLinhas.Length & vbCrLf

    End Sub

    Private Sub Gera_SQL(ByVal strTenantId As String, ByVal strClientId As String, ByVal strClientSecret As String, ByVal strClientName As String)
        'Grava tabela SQL t_taegis_assets a partir do arquivo assets02_TenantId_aaaammdd.csv
        '16/03/23-17/04/23-06/07/23
        '===================================================================================
        '00 - asset_num
        '01 - endpoint_type
        '02 - host_id
        '03 - hostname
        '04 - id
        '05 - os_version
        '06 - sensor_id
        '07 - sensor_tenant
        '08 - sensor_version
        '09 - os_family
        '===================> Inclusão de campos em 06/07/23
        '10 - osRelease
        '11 - systemType
        '12 - status
        '13 - osDistributor
        Dim strData As New String(DateTime.Now.ToString("yyyyMMdd"))
        Dim strSaida As New String("\assets02_" & strTenantId & "_" & strData & ".csv")

        Dim tfp As New TextFieldParser(_strPastaProj & strSaida)
        tfp.Delimiters = New String() {";"}
        tfp.TextFieldType = FieldType.Delimited

        tfp.ReadLine() ' skip header

        While tfp.EndOfData = False
            Dim fields = tfp.ReadFields()

            If Not ExisteAssetSQL(fields(4)) Then
                GravaSQL_Inclui(fields, strTenantId, strClientName)
                _intLinhasSQL_Inc += 1
            Else
                GravaSQL_Altera(fields)
                _intLinhasSQL_Alt += 1
            End If
            _intLinhasLidas += 1
        End While

        _strLog &= "Linhas incluídas na tabela SQL t_taegis_assets: " & _intLinhasSQL_Inc & vbCrLf &
                   "Linhas alteradas na tabela SQL t_taegis_assets: " & _intLinhasSQL_Alt & vbCrLf &
                   "Total de linhas CSV lidas: " & _intLinhasLidas

    End Sub

    Private Function ExisteAssetSQL(strId As String) As Boolean
        'Verifica se o asset existe na tabela SQL t_taegis_assets
        '16/03/23
        '========================================================
        Dim oCon As New SqlConnection(ConnectionString)
        Dim oCmd As New SqlCommand
        Dim intQtd As Integer = 0

        oCon.Open()

        With oCmd
            .Connection = oCon
            .CommandType = CommandType.Text
            .CommandText = "select count(*) from t_taegis_assets " &
            "where id = '" & strId & "'"
            .CommandTimeout = 3600
            intQtd = .ExecuteScalar
        End With

        oCon.Close()

        Return (intQtd > 0)

    End Function

    Private Sub GravaSQL_Inclui(fields As String(), ByVal strTenantID As String, ByVal strClientName As String)
        'Inclui linhas na tabela SQL t_taegis_assets
        '16/03/23-17/04/23-06//07/23
        '===========================================
        Dim oCon As New SqlConnection(ConnectionString)
        Dim oCmd As New SqlCommand

        oCon.Open()

        Try

            With oCmd
                .Connection = oCon
                .CommandType = CommandType.Text
                .CommandText = "insert into t_taegis_assets(" &
                    "endpoint_type, " &
                    "asset_tenant_id," &
                    "asset_client," &
                    "host_id, " &
                    "hostname, " &
                    "id, " &
                    "os_version, " &
                    "sensor_id, " &
                    "sensor_tenant, " &
                    "sensor_version, " &
                    "os_family, " &
                    "osRelease, " &
                    "systemType, " &
                    "status, " &
                    "osDistributor) " &
                    "values('" &
                    fields(1) & "', '" &
                    strTenantID & "', '" &
                    strClientName & "', '" &
                    fields(2) & "', '" &
                    fields(3) & "', '" &
                    fields(4) & "', '" &
                    fields(5) & "', '" &
                    fields(6) & "', '" &
                    fields(7) & "', '" &
                    fields(8) & "', '" &
                    fields(9) & "', '" &
                    fields(10) & "', '" &
                    fields(11) & "', '" &
                    fields(12) & "', '" &
                    fields(13) & "')"
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

    Private Sub GravaSQL_Altera(fields As String())
        'Altera linhas na tabela SQL t_taegis_assets
        '06/07/23
        '===========================================
        Dim oCon As New SqlConnection(ConnectionString)
        Dim oCmd As New SqlCommand

        oCon.Open()

        Try

            With oCmd
                .Connection = oCon
                .CommandType = CommandType.Text
                .CommandText = "UPDATE t_taegis_assets set " &
                    "os_version = '" & fields(5) & "', " &        '5
                    "os_family = '" & fields(9) & "', " &         '9
                    "osRelease = '" & fields(10) & "', " &        '10
                    "status = '" & fields(12) & "', " &           '12
                    "osDistributor = '" & fields(13) & "', " &    '13
                    "alt_data = GETDATE(), " &
                    "alt_user = SUSER_NAME() " &
                    "WHERE id = '" & fields(4) & "'"
                .CommandTimeout = 3600
                .ExecuteNonQuery()
            End With

            oCon.Close()

        Catch ex As SqlException
            _strLog &= vbCrLf & "Erro SQL: " & ex.ToString

        Catch ex1 As Exception
            _strLog &= vbCrLf & "Altera SQL => Erro linha " & fields(0) & ": " & ex1.ToString

        End Try

    End Sub

    Private Sub ExcluiArquivosAnteriores()
        '16/03/23
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

End Module
