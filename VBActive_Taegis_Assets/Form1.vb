Imports System.Data.SqlClient
Imports Microsoft.VisualBasic.FileIO
Imports VBActive_Taegis_DLL.ActiveTaegisDLL

Public Class Form1
    'Sistema Active - VS2022
    'Módulo: Acionamento dos assets Taegis
    'Obs: There is so much info about how to execute pything script, but none of them saying one simple thing:
    '     that the script must be with the same directory as your project.
    'Heron Jr
    '27/02/23-27/06/23
    '07/03/24-21/03/24
    '==========================================================================================================
    Private fd As New FolderBrowserDialog
    Private _strPastaProj As New String("")
    Private _strSaida As New String("")
    Private _strPastaPython As New String("")
    Private _intLinhasSQL_Inc As Integer = 0
    Private _intLinhasSQL_Alt As Integer = 0
    Private _intLinhasLidas As Integer = 0
    Private _aTenantId As New List(Of String)
    Private _aClientId As New List(Of String)
    Private _aClientSecret As New List(Of String)
    Private _intClientIndex As Integer = 0
    Public Value As String

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Carga do form
        '27/02/23-17/04/23-17/10/23-19/10/23
        '==============
        Me.Text = Proj.VersaoAuto1()
        ToolStripStatusLabel1.Text = CopyRight()

        CargaComboClientes()
        _intClientIndex = cboClientes.Items.IndexOf("NETCENTER")
        cboClientes.SelectedIndex = _intClientIndex

        _strPastaPython = LeConfigPasta("pasta_python")
        _strPastaProj = LeConfigPasta("pasta_proj_assets")
        txtPastaPython.Text = _strPastaPython
        txtPastaProj.Text = _strPastaProj

        ExcluiArquivosAnteriores()

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

    Private Sub ExcluiArquivosAnteriores()
        '15/03/23
        'Exclusão de arquivos de trabalho *.json e *.csv
        '===============================================
        Dim strJson As New String(_strPastaProj & "\*.json")
        Dim strCsv As New String(_strPastaProj & "\*.csv")

        For Each fileFound As String In IO.Directory.GetFiles(_strPastaProj).Where(Function(fi) IO.Path.GetFileName(fi) Like "*.json").ToArray
            IO.File.Delete(fileFound)
        Next

        For Each fileFound As String In IO.Directory.GetFiles(_strPastaProj).Where(Function(fi) IO.Path.GetFileName(fi) Like "*.csv").ToArray
            IO.File.Delete(fileFound)
        Next

    End Sub

    Private Sub btnAssets1_Click(sender As Object, e As EventArgs) Handles btnAssets1.Click
        'Botão assets1 - Grava arquivo assets01_aaaammdd.json
        '27/02/23-17/04/23-25/10/23
        '====================================================
        Cursor = Cursors.WaitCursor
        Dim oProcess As Process = New Process()
        Dim oStartInfo As New ProcessStartInfo(_strPastaPython, "assets01.py " &
                                               _aTenantId(_intClientIndex) & " " & _aClientId(_intClientIndex) & " " & _aClientSecret(_intClientIndex))
        Dim strSaida As New String("assets01_" & _aTenantId(_intClientIndex) & "_" & DateTime.Now.ToString("yyyyMMdd") & ".json")
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
        Cursor = Cursors.Default
        txtMensagens.Text &= "1) Arquivo JSON " & strSaida & " gravado com sucesso - Linhas: " & strLinhas.Length & vbCrLf
        btnParseJson.Enabled = System.IO.File.Exists(strSaida)
        btnReiniciar.Enabled = True

    End Sub

    Private Sub ConfiguraçãoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ConfiguraçãoToolStripMenuItem.Click
        'Menu Arquivo/Configuração F2
        '27/02/23
        '============================
        Dim f As New VBActive_Taegis_DLL.frmConfig
        f.ShowDialog()

    End Sub

    Private Sub btnParseJson_Click(sender As Object, e As EventArgs) Handles btnParseJson.Click
        'Botão Parse Json - Grava arquivo assets02_aaaammdd.json
        '27/02/23-17/04/23
        '=======================================================
        Cursor = Cursors.WaitCursor
        Dim oProcess As Process = New Process()
        Dim oStartInfo As New ProcessStartInfo(_strPastaPython, "parsejson.py " &
                                               _aTenantId(_intClientIndex))
        Dim strSaida As New String("assets02_" & _aTenantId(_intClientIndex) & "_" & DateTime.Now.ToString("yyyyMMdd") & ".json")
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
        Cursor = Cursors.Default
        txtMensagens.Text &= "1) Arquivo JSON " & strSaida & " gravado com sucesso - Linhas: " & strLinhas.Length & vbCrLf
        btnAssets2.Enabled = System.IO.File.Exists(strSaida)

    End Sub

    Private Sub txtPastaPython_TextChanged(sender As Object, e As EventArgs) Handles txtPastaPython.TextChanged
        'Texto alterado
        '27/02/23
        '==============
        Dim strSaida1 As New String("assets01_" & DateTime.Now.ToString("yyyyMMdd") & ".json")
        Dim strSaida2 As New String("assets02_" & DateTime.Now.ToString("yyyyMMdd") & ".json")
        Dim strSaida3 As New String("assets02_" & DateTime.Now.ToString("yyyyMMdd") & ".csv")

        btnAssets1.Enabled = Len(Trim(txtPastaProj.Text)) > 0 And Len(Trim(txtPastaPython.Text)) > 0
        btnParseJson.Enabled = System.IO.File.Exists(strSaida1)
        btnAssets2.Enabled = System.IO.File.Exists(strSaida2)
        btnSQL.Enabled = System.IO.File.Exists(strSaida3)

    End Sub

    Private Sub txtPastaProj_TextChanged(sender As Object, e As EventArgs) Handles txtPastaProj.TextChanged
        'Texto alterado
        '27/02/23
        '==============
        Dim strSaida1 As New String("assets01_" & DateTime.Now.ToString("yyyyMMdd") & ".json")
        Dim strSaida2 As New String("assets02_" & DateTime.Now.ToString("yyyyMMdd") & ".json")
        Dim strSaida3 As New String("assets02_" & DateTime.Now.ToString("yyyyMMdd") & ".csv")

        btnAssets1.Enabled = Len(Trim(txtPastaProj.Text)) > 0 And Len(Trim(txtPastaPython.Text)) > 0
        btnParseJson.Enabled = System.IO.File.Exists(strSaida1)
        btnAssets2.Enabled = System.IO.File.Exists(strSaida2)
        btnSQL.Enabled = System.IO.File.Exists(strSaida3)

    End Sub

    Private Sub btnAssets2_Click(sender As Object, e As EventArgs) Handles btnAssets2.Click
        'Botão Assets2 - Grava arquivo assets02_aaaammdd.csv
        '27/02/23-17/04/23
        '===================================================
        Cursor = Cursors.WaitCursor
        Dim oProcess As Process = New Process()
        Dim oStartInfo As New ProcessStartInfo(_strPastaPython, "assets02.py " &
                                               _aTenantId(_intClientIndex))
        Dim strSaida As New String("assets02_" & _aTenantId(_intClientIndex) & "_" & DateTime.Now.ToString("yyyyMMdd") & ".csv")
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
        Cursor = Cursors.Default
        txtMensagens.Text &= "2) Arquivo CSV " & strSaida & " gravado com sucesso - Linhas: " & strLinhas.Length & vbCrLf
        btnSQL.Enabled = System.IO.File.Exists(strSaida)

    End Sub

    Private Sub btnSQL_Click(sender As Object, e As EventArgs) Handles btnSQL.Click
        'Botão SQL - Grava tabela SQL t_taegis_assets a partir do arquivo assets02_aaaammdd.csv
        '27/02/23-28/02/23-17/04/23-27/06/23-06/07/23-25/10/23
        '======================================================================================
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
        '===================> Inclusão de campos em 27/06/23
        '10 - osRelease
        '11 - systemType
        '12 - status
        '13 - osDistributor
        Cursor = Cursors.WaitCursor
        Dim strData As New String(DateTime.Now.ToString("yyyyMMdd"))
        Dim strSaida As New String("\assets02_" & _aTenantId(_intClientIndex) & "_" & strData & ".csv")

        Dim tfp As New TextFieldParser(_strPastaProj & strSaida)
        tfp.Delimiters = New String() {";"}
        tfp.TextFieldType = FieldType.Delimited

        tfp.ReadLine() ' skip header

        While tfp.EndOfData = False
            Dim fields = tfp.ReadFields()

            If Not ExisteAssetSQL(fields(4)) Then
                GravaSQL_Inclui(fields)
                _intLinhasSQL_Inc += 1
            Else
                GravaSQL_Altera(fields)
                _intLinhasSQL_Alt += 1
            End If
            _intLinhasLidas += 1
        End While

        Cursor = Cursors.Default
        txtMensagens.Text &= "Linhas incluídas na tabela SQL t_taegis_assets: " & _intLinhasSQL_Inc & vbCrLf &
                             "Linhas alteradas na tabela SQL t_taegis_assets: " & _intLinhasSQL_Alt & vbCrLf &
                             "Total de linhas CSV lidas: " & _intLinhasLidas
        txtMensagens.Text &= vbCrLf & "==================================================================================="
        btnReiniciar.Enabled = True

    End Sub

    Private Function ExisteAssetSQL(strId As String) As Boolean
        'Verifica se o asset existe na tabela SQL t_taegis_assets
        '27/02/23
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

    Private Sub GravaSQL_Inclui(fields As String())
        'Inclui linhas na tabela SQL t_taegis_assets
        '27/02/23-17/04/23-27/06/23
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
                    _aTenantId(_intClientIndex) & "', '" &
                    cboClientes.Text & "', '" &
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
            txtMensagens.Text &= vbCrLf & "Erro SQL: " & ex.ToString
            txtMensagens.Text &= vbCrLf & "==================================================================================="

        Catch ex1 As Exception
            txtMensagens.Text &= vbCrLf & "Inclui SQL => Erro linha " & fields(0) & ": " & ex1.ToString
            txtMensagens.Text &= vbCrLf & "==================================================================================="

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
            txtMensagens.Text &= vbCrLf & "Erro SQL: " & ex.ToString
            txtMensagens.Text &= vbCrLf & "==================================================================================="

        Catch ex1 As Exception
            txtMensagens.Text &= vbCrLf & "Inclui SQL => Erro linha " & fields(0) & ": " & ex1.ToString
            txtMensagens.Text &= vbCrLf & "==================================================================================="

        End Try

    End Sub

    Private Sub btnFecha_Click(sender As Object, e As EventArgs) Handles btnFecha.Click
        'Botão fecha
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

    Private Sub cboClientes_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboClientes.SelectedIndexChanged
        'Alteração do combo
        '17/04/23
        '==================
        _intClientIndex = cboClientes.SelectedIndex

    End Sub

    Private Sub TabelaDeClientesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TabelaDeClientesToolStripMenuItem.Click
        'Tabela de clientes - F4
        '12/09/23
        '=======================
        Dim f As New VBActive_Taegis_DLL.frmClientes
        f.ShowDialog()

    End Sub

    Private Sub btnReiniciar_Click(sender As Object, e As EventArgs) Handles btnReiniciar.Click
        'Botão reiniciar
        '25/10/23
        '===============
        'ExcluiArquivosAnteriores()
        btnParseJson.Enabled = False
        btnAssets2.Enabled = False
        btnSQL.Enabled = False

    End Sub

End Class
