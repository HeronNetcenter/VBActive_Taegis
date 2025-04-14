Imports System.Data.SqlClient
Imports Microsoft.VisualBasic.FileIO
Imports VBActive_Taegis_DLL.ActiveTaegisDLL

''' <summary>
''' Processa o promeiro passo da extração de collectors da Taegis para a geração do arquivo collectors01_yyyymmdd.json - VS2022
''' Autor: Heron Domingues Jr
''' Data:  14/03/23
''' Alteração Data: 19/04/23-07/03/24-21/03/24
''' Obs: Nas propriedades do projeto desmarcar a opção "enable application framework" e mudar a caixa "Startup Object" para a Sub Main deste módulo
''' </summary>
''' <remarks></remarks>
Module Modulo_Active_Taegis_Collectors_Auto1
    'Módulo de extração 1
    '17/03/23
    '27/01/25
    '====================
    Private fd As New FolderBrowserDialog
    Private _strPastaProj As New String("")
    Private _strSaida As New String("")
    Private _strLog As New String("")
    Private _strPastaPython As New String("")
    Private _intLinhasSQL_Inc As Integer = 0
    Private _intLinhasSQL_Alt As Integer = 0
    Private _intLinhasLidas As Integer = 0
    Private _intLinhas_Collectors01 As Integer = 0

    'Parâmetros para enviar aos módulos Python - 19/04/23
    Private _aTenantId As New List(Of String)
    Private _aClientId As New List(Of String)
    Private _aClientSecret As New List(Of String)
    Private _aClientName As New List(Of String)

    Public Value As String

    Public Sub Main()
        'Chamada principal do Módulo de extração 1
        '17/03/23-19/04/23
        '27/01/25
        '=========================================
        _strPastaPython = LeConfigPasta("pasta_python")
        _strPastaProj = LeConfigPasta("pasta_proj_collectors_auto") '.Replace("\Release", "\Debug") 'Troca de pasta para testes no modo Debug

        ExcluiArquivosAnteriores()
        CargaSQLClientes()

        For i As Integer = 0 To _aTenantId.Count - 1
            GeraCollectors1_Json(_aTenantId(i), _aClientId(i), _aClientSecret(i), _aClientName(i))
            If _intLinhas_Collectors01 > 0 Then
                GeraParse_Json(_aTenantId(i))
                GeraCollectors2_CSV(_aTenantId(i))
                GeraSQL(_aTenantId(i), _aClientId(i), _aClientSecret(i), _aClientName(i))
                GravaLogAlert(_aTenantId(i), _aClientId(i), _aClientSecret(i), _aClientName(i))
            End If
        Next i

    End Sub

    Private Sub ExcluiArquivosAnteriores()
        '17/03/23
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

    Private Sub GeraCollectors1_Json(ByVal strTenantId As String, ByVal strClientId As String, ByVal strClientSecret As String, ByVal strClientName As String)
        'Gera collectors1 - Grava arquivo collectors01_TenantId_aaaammdd.json
        '10/03/23-19/04/23
        '27/01/25
        '====================================================================
        Dim oProcess As Process = New Process()
        Dim oStartInfo As New ProcessStartInfo(_strPastaPython, "collectors01.py " &
                                               strTenantId & " " & strClientId & " " & strClientSecret)
        Dim strSaida As New String("collectors01_" & strTenantId & "_" & DateTime.Now.ToString("yyyyMMdd") & ".json")
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

        'txtMensagens.Text = sOutput
        _strLog &= "1) Arquivo JSON " & strSaida & " gravado com sucesso - Linhas: " & strLinhas.Length & vbCrLf
        _intLinhas_Collectors01 = strLinhas.Length  'Se este contador de linhas JSON for zero o cliente (Tenant_Id) não segue o processo (27/01/25)

    End Sub

    Private Sub GeraParse_Json(ByVal strTenantId As String)
        'Gera Parse Json - Grava arquivo collectors02_TenantId_aaaammdd.json
        '10/03/23-19/04/23
        '===================================================================
        Dim oProcess As Process = New Process()
        Dim oStartInfo As New ProcessStartInfo(_strPastaPython, "parsejson.py " &
                                               strTenantId)
        Dim strSaida As New String("collectors02_" & strTenantId & "_" & DateTime.Now.ToString("yyyyMMdd") & ".json")
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

        'txtMensagens.Text = sOutput
        _strLog &= "1) Arquivo JSON " & strSaida & " gravado com sucesso - Linhas: " & strLinhas.Length & vbCrLf

    End Sub

    Private Sub GeraCollectors2_CSV(ByVal strTenantId As String)
        'Gera collectors2 - Grava arquivo collectors02_TenantId_aaaammdd.csv
        '10/03/23-19/04/23
        '===================================================================
        Dim oProcess As Process = New Process()
        Dim oStartInfo As New ProcessStartInfo(_strPastaPython, "collectors02.py " &
                                               strTenantId)
        Dim strSaida As New String("collectors02_" & strTenantId & "_" & DateTime.Now.ToString("yyyyMMdd") & ".csv")
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

    Private Sub GeraSQL(ByVal strTenantId As String, ByVal strClientId As String, ByVal strClientSecret As String, ByVal strClientName As String)
        'Gera SQL - Grava tabela SQL t_taegis_collectors a partir do arquivo collectors02_TenentId_aaaammdd.csv
        '10/03/23-19/04/23-21/06/23
        '======================================================================================================
        Dim strData As New String(DateTime.Now.ToString("yyyyMMdd"))
        Dim strSaida As New String("\collectors02_" & strTenantId & "_" & strData & ".csv")

        Dim tfp As New TextFieldParser(_strPastaProj & strSaida)
        tfp.Delimiters = New String() {";"}
        tfp.TextFieldType = FieldType.Delimited

        tfp.ReadLine() ' skip header

        While tfp.EndOfData = False
            Dim fields = tfp.ReadFields()

            If Not ExisteCollectorsQL(fields(6)) Then
                GravaSQL_Inclui(fields, strTenantId, strClientName)
                _intLinhasSQL_Inc += 1
            Else
                GravaSQL_Altera(fields)
                _intLinhasSQL_Alt = 1
            End If
            _intLinhasLidas += 1
        End While

        _strLog &= "Linhas incluídas na tabela SQL t_taegis_collectors: " & _intLinhasSQL_Inc & vbCrLf &
                   "Linhas alteradas na tabela SQL t_taegis_collectors: " & _intLinhasSQL_Alt & vbCrLf &
                   "Total de linhas CSV lidas: " & _intLinhasLidas

    End Sub

    Private Function ExisteCollectorsQL(strId As String) As Boolean
        'Verifica se o investigation existe na tabela SQL t_taegis_collectors
        '10/03/23
        '====================================================================
        Dim oCon As New SqlConnection(ConnectionString)
        Dim oCmd As New SqlCommand
        Dim intQtd As Integer = 0

        oCon.Open()

        With oCmd
            .Connection = oCon
            .CommandType = CommandType.Text
            .CommandText = "select count(*) from t_taegis_collectors " &
            "where id = '" & strId & "'"
            .CommandTimeout = 3600
            intQtd = .ExecuteScalar
        End With

        oCon.Close()

        Return (intQtd > 0)

    End Function

    Private Sub GravaSQL_Inclui(fields As String(), ByVal strTenantID As String, ByVal strClientName As String)
        'Inclui linhas na tabela SQL t_taegis_collectors
        '10/03/23-19/04/23
        '===============================================
        Dim oCon As New SqlConnection(ConnectionString)
        Dim oCmd As New SqlCommand

        oCon.Open()

        Try

            With oCmd
                .Connection = oCon
                .CommandType = CommandType.Text
                .CommandText = "insert into t_taegis_collectors(" &
                    "collector_num, " &
                    "cluster_type, " &
                    "created_at, " &
                    "deployments, " &
                    "description, " &
                    "health, " &
                    "id, " &
                    "name, " &
                    "dhcp, " &
                    "dns, " &
                    "hostname, " &
                    "ntp, " &
                    "proxy, " &
                    "role, " &
                    "status, " &
                    "type, " &
                    "updated_at, " &
                    "col_tenant_id," &
                    "col_client)" &
                    "values(" &
                    fields(0) & ", '" &
                    fields(1) & "', '" &
                    fields(2) & "', '" &
                    fields(3) & "', '" &
                    fields(4) & "', '" &
                    fields(5) & "', '" &
                    fields(6) & "', '" &
                    fields(7) & "', '" &
                    fields(8) & "', '" &
                    fields(9).Replace("'", "´") & "', '" &
                    fields(10) & "', '" &
                    fields(11) & "', '" &
                    fields(12) & "', '" &
                    fields(13) & "', '" &
                    fields(14).Replace("'", "´") & "', '" &
                    fields(15) & "', '" &
                    fields(16) & "', '" &
                    strTenantID & "', '" &
                    strClientName & "')"
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
        'Altera campo "updatedAt" na tabela SQL t_taegis_collectors
        '21/06/23
        '==========================================================
        Dim oCon As New SqlConnection(ConnectionString)
        Dim oCmd As New SqlCommand

        oCon.Open()

        Try

            With oCmd
                .Connection = oCon
                .CommandType = CommandType.Text
                .CommandText = "update t_taegis_collectors " &
                        "set cluster_type = '" & fields(1) & "', " &
                        "deployments = '" & fields(3).Replace("'", "") & "', " &
                        "description = '" & fields(4) & "', " &
                        "health = '" & fields(5) & "', " &
                        "name = '" & fields(7) & "', " &
                        "dhcp = '" & fields(8) & "', " &
                        "dns = '" & fields(9).Replace("'", "") & "', " &
                        "hostname = '" & fields(10) & "', " &
                        "ntp = '" & fields(11) & "', " &
                        "proxy = '" & fields(12) & "', " &
                        "role = '" & fields(13) & "', " &
                        "status = '" & fields(14).Replace("'", "") & "', " &
                        "type = '" & fields(15) & "', " &
                        "updated_at =  '" & fields(16) & "', " &
                        "alt_data = getdate(), " &
                        "alt_user = suser_name() " &
                        "where id = '" & fields(6) & "'"
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

    Private Sub GravaLogAlert(ByVal strTenantId As String, ByVal strClientId As String, ByVal strClientSecret As String, ByVal strClientName As String)
        'Grava o log de geração dos arquivos de alertas na tabela t_taegis_log
        '17/03/23-19/04/23
        '27/01/25
        '=====================================================================
        Dim oCon As New SqlConnection(ConnectionString)
        Dim oCmd As New SqlCommand

        oCon.Open()

        With oCmd
            .Connection = oCon
            .CommandType = CommandType.Text
            .CommandText = "insert into t_taegis_log(tenant_id, client_name, data_log, qtde_inc, tipo_log, texto_log)" &
            " values('" & strTenantId & "', '" & strClientName & "', getdate(), " & _intLinhasSQL_Inc & ", 'Collectors', '" & _strLog.Replace("'", "´") & "')"
            .CommandTimeout = 3600
            .ExecuteNonQuery()
        End With

        oCon.Close()

    End Sub

End Module
