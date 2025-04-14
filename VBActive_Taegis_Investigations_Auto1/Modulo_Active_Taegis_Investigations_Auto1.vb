Imports System.Data.SqlClient
Imports System.IO
Imports System.Text.RegularExpressions
Imports Microsoft.VisualBasic.FileIO
Imports VBActive_Taegis_DLL.ActiveTaegisDLL

''' <summary>
''' Sistema Active - VS2022
''' Processa o promeiro passo da extração de investigações da Taegis para a geração do arquivo alerts01_yyyymmdd.json
''' Autor: Heron Domingues Jr
''' Data:  17/03/23
''' Alteração Data: 18/04/23
'''                 19/03/24-21/03/24
'''                 25/03/25
''' Obs: Nas propriedades do projeto desmarcar a opção "enable application framework" e mudar a caixa "Startup Object" para a Sub Main deste módulo
''' </summary>
''' <remarks></remarks>
Module Modulo_Active_Taegis_Investigations_Auto1
    '17/03/23-18/04/23-21/11/23
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

    'Flag para gravação da linha do log apenas para os casos de inclusão de investigação - 21/11/23
    Private _blnGravaLog As Boolean = False

    'Parâmetros para enviar aos módulos Python - 17/04/23
    Private _aTenantId As New List(Of String)
    Private _aClientId As New List(Of String)
    Private _aClientSecret As New List(Of String)
    Private _aClientName As New List(Of String)

    Public Value As String

    Public Sub Main()
        'Chamada principal do Módulo de extração 1
        '17/03/23-18/04/23-20/10/23-21/11/23
        '19/03/25
        '=========================================
        _strPastaPython = LeConfigPasta("pasta_python")
        _strPastaProj = LeConfigPasta("pasta_proj_investigations_auto") '.Replace("Release", "Debug")   'PARA TESTES USAR O REPLACE DE TROCA DE PASTAS

        ExcluiArquivosAnteriores()
        CargaSQLClientes()

        For i As Integer = 0 To _aTenantId.Count - 1
            GeraInvestigations1_Json(_aTenantId(i), _aClientId(i), _aClientSecret(i), _aClientName(i))
            'ParseJson(_aTenantId(i))   'DESATIVADO EM 25/03/25
            'GeraInvestigations2_CSV(_aTenantId(i))   'DESATIVADO EM 25/03/25
            GeraSQL(_aTenantId(i), _aClientId(i), _aClientSecret(i), _aClientName(i))
            If _blnGravaLog Then
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
    Private Sub GeraInvestigations1_Json(ByVal strTenantId As String, ByVal strClientId As String, ByVal strClientSecret As String, ByVal strClientName As String)
        'Gera investigations1 - Grava arquivo investigations01_TenantId_aaaammdd.json
        '17/03/23-18/04/23
        '25/03/25
        '============================================================================
        Dim oProcess As Process = New Process()
        Dim oStartInfo As New ProcessStartInfo(_strPastaPython, "investigations01.py " &
                                               strTenantId & " " & strClientId & " " & strClientSecret)
        Dim strSaida1 As New String("investigations01_" & strTenantId & "_" & DateTime.Now.ToString("yyyyMMdd") & ".json")
        Dim strSaida2 As New String("investigations02_" & strTenantId & "_" & DateTime.Now.ToString("yyyyMMdd") & ".csv")
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

        _strLog &= "1) Arquivo JSON " & strSaida1 & " gravado com sucesso - Linhas: " & Proj.GetJsonInvestigationCount(strSaida1) & vbCrLf
        _strLog &= "1) Arquivo CSV " & strSaida2 & " gravado com sucesso - Linhas: " & (File.ReadAllLines(strSaida2).Length - 1) & vbCrLf

    End Sub

    Private Sub ParseJson(ByVal strTenantId As String)
        'Parse Json - Grava arquivo investigations02_TenantId_aaaammdd.json
        '17/03/23-18/04/23
        '25/03/25 ==> ESTA SUB NÃO É MAIS USADA - O PYTHON INVESTIGATIONS01 FAZ SOZINHO O JSON E O CSV
        '=============================================================================================
        Dim oProcess As Process = New Process()
        Dim oStartInfo As New ProcessStartInfo(_strPastaPython, "parsejson.py " &
                                               strTenantId)
        Dim strSaida As New String("investigations02_" & strTenantId & "_" & DateTime.Now.ToString("yyyyMMdd") & ".json")
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

    Private Sub GeraInvestigations2_CSV(ByVal strTenantId As String)
        'Gera investigations2 - Grava arquivo investigations02_TenantId_aaaammdd.csv
        '17/03/23-18/04/23
        '20/03/25 ==> ESTA SUB NÃO É MAIS USADA - O PYTHON INVESTIGATIONS01 FAZ SOZINHO O JSON E O CSV
        '=============================================================================================
        Dim oProcess As Process = New Process()
        Dim oStartInfo As New ProcessStartInfo(_strPastaPython, "investigations02.py " &
                                               strTenantId)
        Dim strSaida As New String("investigations02_" & strTenantId & "_" & DateTime.Now.ToString("yyyyMMdd") & ".csv")
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
        _strLog &= "2) Arquivo CSV " & strSaida & " gravado com sucesso - Linhas: " & strLinhas.Length & vbCrLf

    End Sub

    Private Sub GeraSQL(ByVal strTenantId As String, ByVal strClientId As String, ByVal strClientSecret As String, ByVal strClientName As String)
        'Gera SQL - Grava tabela SQL t_taegis_investigations a partir do arquivo investigations02_TenentId_aaaammdd.csv
        '17/03/23-18/04/23-12/10/23-21/11/23
        '==============================================================================================================
        '00 - investigation_num
        '01 - description
        '02 - id
        '03 - status
        Dim strData As New String(DateTime.Now.ToString("yyyyMMdd"))
        Dim strSaida As New String("\investigations02_" & strTenantId & "_" & strData & ".csv")
        Dim tfp As New TextFieldParser(_strPastaProj & strSaida)

        _blnGravaLog = False
        tfp.Delimiters = New String() {";"}
        tfp.TextFieldType = FieldType.Delimited

        tfp.ReadLine() ' skip header

        While tfp.EndOfData = False
            Dim fields = tfp.ReadFields()

            If Not ExisteInvestigationSQL(fields(1)) Then
                GravaSQL_Inclui(fields, strTenantId, strClientName)
                _intLinhasSQL_Inc += 1
                _blnGravaLog = True
            Else
                GravaSQL_Altera(fields)
                _intLinhasSQL_Alt += 1
            End If
            _intLinhasLidas += 1
        End While

        _strLog &= "Linhas incluídas na tabela SQL t_taegis_investigations: " & _intLinhasSQL_Inc & vbCrLf &
                        "Total de linhas CSV lidas: " & _intLinhasLidas

    End Sub

    Private Function ExisteInvestigationSQL(strId As String) As Boolean
        'Verifica se o investigation existe na tabela SQL t_taegis_investigations
        '17/03/23
        '========================================================================
        Dim oCon As New SqlConnection(ConnectionString)
        Dim oCmd As New SqlCommand
        Dim intQtd As Integer = 0

        oCon.Open()

        With oCmd
            .Connection = oCon
            .CommandType = CommandType.Text
            .CommandText = "select count(*) from t_taegis_investigations " &
            "where id = '" & strId & "'"
            .CommandTimeout = 3600
            intQtd = .ExecuteScalar
        End With

        oCon.Close()

        Return (intQtd > 0)

    End Function

    Private Sub GravaSQL_Inclui(fields As String(), ByVal strTenantID As String, ByVal strClientName As String)
        'Inclui linhas na tabela SQL t_taegis_investigations
        '17/03/23-18/04/23-12/10/23
        '25/03/25
        '===================================================
        Dim oCon As New SqlConnection(ConnectionString)
        Dim oCmd As New SqlCommand

        oCon.Open()

        Try

            With oCmd
                .Connection = oCon
                .CommandType = CommandType.Text
                .CommandText = "insert into t_taegis_investigations(" &
                    "invest_num, " &
                    "id, " &
                    "archived_at, " &
                    "created_at, " &
                    "created_by, " &
                    "description, " &
                    "priority, " &
                    "status, " &
                    "type, " &
                    "updated_at, " &
                    "service_desk_id, " &
                    "shortId, " &
                    "inv_tenant_id," &
                    "inv_client)" &
                    "values(" &
                    fields(0) & ", '" &
                    fields(1) & "', '" &
                    fields(2) & "', '" &
                    fields(3) & "', '" &
                    fields(4) & "', '" &
                    fields(5).Replace("'", "´") & "', '" &
                    fields(6) & "', '" &
                    fields(7) & "', '" &
                    fields(8) & "', '" &
                    fields(9) & "', '" &
                    fields(10) & "', '" &
                    fields(11) & "', '" &
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
        'Altera linhas na tabela SQL t_taegis_investigations
        '12/10/23
        '25/03/25
        '===================================================
        Dim oCon As New SqlConnection(ConnectionString)
        Dim oCmd As New SqlCommand

        oCon.Open()

        Try

            With oCmd
                .Connection = oCon
                .CommandType = CommandType.Text
                .CommandText = "update t_taegis_investigations set " &
                        "description = '" & fields(5).Replace("'", "´") & "', " &
                        "priority = '" & fields(6) & "', " &
                        "status = '" & fields(7) & "', " &
                        "type = '" & fields(8) & "', " &
                        "service_desk_id = '" & fields(10) & "', " &
                        "shortId = '" & fields(11) & "', " &
                        "alt_data = GETDATE(), " &
                        "alt_user = SUSER_NAME() " &
                        "WHERE id = '" & fields(1) & "'"

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
        '17/03/23-18/04/23
        'Grava o log de geração dos arquivos de alertas na tabela t_taegis_log
        '=====================================================================
        Dim oCon As New SqlConnection(ConnectionString)
        Dim oCmd As New SqlCommand

        oCon.Open()

        With oCmd
            .Connection = oCon
            .CommandType = CommandType.Text
            .CommandText = "insert into t_taegis_log(tenant_id, client_name, data_log, qtde_inc, tipo_log, texto_log)" &
            " values('" & strTenantId & "', '" & strClientName & "', getdate(), " & _intLinhasSQL_Inc & ", 'Investigations', '" & _strLog & "')"
            .CommandTimeout = 3600
            .ExecuteNonQuery()
        End With

        oCon.Close()

    End Sub

End Module
