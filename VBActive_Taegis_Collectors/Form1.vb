Imports System.Data.SqlClient
Imports Microsoft.VisualBasic.FileIO
Imports VBActive_Taegis_DLL.ActiveTaegisDLL

Public Class Form1
    'Sistema Active - VS2022
    'Módulo: Acionamento dos collectors Taegis
    'Obs: There is so much info about how to execute python script, but none of them saying one simple thing:
    '     that the script must be with the same directory as your project.
    'Heron Jr
    '10/03/23-19/04/23
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
        '10/03/23-19/04/23-13/09/23-17/10/23-19/10/23
        '27/01/25
        '==============
        Me.Text = Proj.VersaoAuto1()
        ToolStripStatusLabel1.Text = CopyRight()

        CargaComboClientes()
        _intClientIndex = cboClientes.Items.IndexOf("NETCENTER")
        cboClientes.SelectedIndex = _intClientIndex

        _strPastaPython = LeConfigPasta("pasta_python")
        _strPastaProj = LeConfigPasta("pasta_proj_collectors") '.Replace("\Release", "\Debug") 'Troca de pasta para testes no modo Debug
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

    Private Sub ConfiguraçãoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ConfiguraçãoToolStripMenuItem.Click
        'Menu Arquivo/Configuração F2
        '10/03/23
        '============================
        Dim f As New VBActive_Taegis_DLL.frmConfig
        f.ShowDialog()

    End Sub

    Private Sub BtnCollectors1_Click(sender As Object, e As EventArgs) Handles BtnCollectors1.Click
        'Botão collectors1 - Grava arquivo collectors01_TenantId_aaaammdd.json
        '10/03/23-19/04/23
        '=====================================================================
        Cursor = Cursors.WaitCursor
        Dim oProcess As Process = New Process()
        Dim oStartInfo As New ProcessStartInfo(_strPastaPython, "collectors01.py " &
                                               _aTenantId(_intClientIndex) & " " & _aClientId(_intClientIndex) & " " & _aClientSecret(_intClientIndex))
        Dim strSaida As New String("collectors01_" & _aTenantId(_intClientIndex) & "_" & DateTime.Now.ToString("yyyyMMdd") & ".json")
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

    End Sub

    Private Sub btnParseJson_Click(sender As Object, e As EventArgs) Handles btnParseJson.Click
        'Botão Parse Json - Grava arquivo collectors02_TenantId_aaaammdd.json
        '10/03/23-19/04/23
        '====================================================================
        Cursor = Cursors.WaitCursor
        Dim oProcess As Process = New Process()
        Dim oStartInfo As New ProcessStartInfo(_strPastaPython, "parsejson.py " &
                                               _aTenantId(_intClientIndex))
        Dim strSaida As New String("collectors02_" & _aTenantId(_intClientIndex) & "_" & DateTime.Now.ToString("yyyyMMdd") & ".json")
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
        btnCollectors2.Enabled = System.IO.File.Exists(strSaida)

    End Sub

    Private Sub btnCollectors2_Click(sender As Object, e As EventArgs) Handles btnCollectors2.Click
        'Botão collectors2 - Grava arquivo collectors02_TenantId_aaaammdd.csv
        '10/03/23-19/04/22
        '====================================================================
        Cursor = Cursors.WaitCursor
        Dim oProcess As Process = New Process()
        Dim oStartInfo As New ProcessStartInfo(_strPastaPython, "collectors02.py " &
                                               _aTenantId(_intClientIndex))
        Dim strSaida As New String("collectors02_" & _aTenantId(_intClientIndex) & "_" & DateTime.Now.ToString("yyyyMMdd") & ".csv")
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
        'Botão SQL - Grava tabela SQL t_taegis_collectors a partir do arquivo collectors02_TenantId_aaaammdd.csv
        '10/03/23-19/04/23-21/06/23
        '=======================================================================================================
        Cursor = Cursors.WaitCursor
        Dim strData As New String(DateTime.Now.ToString("yyyyMMdd"))
        Dim strSaida As New String("\collectors02_" & _aTenantId(_intClientIndex) & "_" & strData & ".csv")

        Dim tfp As New TextFieldParser(_strPastaProj & strSaida)
        tfp.Delimiters = New String() {";"}
        tfp.TextFieldType = FieldType.Delimited

        tfp.ReadLine() ' skip header

        While tfp.EndOfData = False
            Dim fields = tfp.ReadFields()

            If Not ExistecollectorsQL(fields(6)) Then
                GravaSQL_Inclui(fields)
                _intLinhasSQL_Inc += 1
            Else
                GravaSQL_Altera(fields)
                _intLinhasSQL_Alt = 1
            End If
            _intLinhasLidas += 1
        End While

        Cursor = Cursors.Default
        txtMensagens.Text &= "Linhas incluídas na tabela SQL t_taegis_collectors: " & _intLinhasSQL_Inc & vbCrLf &
                             "Linhas alteradas na tabela SQL t_taegis_collectors: " & _intLinhasSQL_Alt & vbCrLf &
                             "Total de linhas CSV lidas: " & _intLinhasLidas
        txtMensagens.Text &= vbCrLf & "==================================================================================="

    End Sub

    Private Function ExistecollectorsQL(strId As String) As Boolean
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

    Private Sub GravaSQL_Inclui(fields As String())
        'Inclui linhas na tabela SQL t_taegis_collectors
        '10/03/23-19/04/23
        '===============================================
        Dim oCon As New SqlConnection(ConnectionString)
        Dim oCmd As New SqlCommand

        oCon.Open()

        'Try

        With oCmd
                .Connection = oCon
                .CommandType = CommandType.Text
            .CommandText = "insert into t_taegis_collectors(" &
                    "collector_num, " &     '0
                    "cluster_type, " &      '1
                    "created_at, " &        '2
                    "deployments, " &       '3
                    "description, " &       '4
                    "health, " &            '5
                    "id, " &                '6
                    "name, " &              '7
                    "dhcp, " &              '8
                    "dns, " &               '9
                    "hostname, " &          '10
                    "ntp, " &               '11
                    "proxy, " &             '12
                    "role, " &              '13
                    "status, " &            '14
                    "type, " &              '15
                    "updated_at, " &        '16
                    "col_tenant_id," &      '17
                    "col_client)" &         '18
                    "values(" &
                    fields(0) & ", '" &
                    fields(1) & "', '" &
                    fields(2) & "', '" &
                    fields(3).Replace("'", "´") & "', '" &
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
                    _aTenantId(_intClientIndex) & "', '" &
                    cboClientes.Text & "')"
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


    Private Sub GravaSQL_Altera(fields As String())
        'Altera campo "updatedAt" na tabela SQL t_taegis_collectors
        '21/06/23-06/12/23
        '==========================================================
        Dim oCon As New SqlConnection(ConnectionString)
        Dim oCmd As New SqlCommand

        oCon.Open()

        'Try

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
                'MessageBox.Show(.CommandText)
                'Clipboard.SetText(.CommandText)
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

    Private Sub FechaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FechaToolStripMenuItem.Click
        'Menu: Arquivo/Fecha
        '10/03/23
        '===================
        btnFecha.PerformClick()

    End Sub

    Private Sub txtPastaPython_TextChanged(sender As Object, e As EventArgs) Handles txtPastaPython.TextChanged
        'Texto alterado
        '10/03/23
        '==============
        Dim strSaida1 As New String("collectors01_" & DateTime.Now.ToString("yyyyMMdd") & ".json")
        Dim strSaida2 As New String("collectors02_" & DateTime.Now.ToString("yyyyMMdd") & ".json")
        Dim strSaida3 As New String("collectors02_" & DateTime.Now.ToString("yyyyMMdd") & ".csv")

        BtnCollectors1.Enabled = Len(Trim(txtPastaProj.Text)) > 0 And Len(Trim(txtPastaPython.Text)) > 0
        btnParseJson.Enabled = System.IO.File.Exists(strSaida1)
        btnCollectors2.Enabled = System.IO.File.Exists(strSaida2)
        btnSQL.Enabled = System.IO.File.Exists(strSaida3)

    End Sub

    Private Sub txtPastaProj_TextChanged(sender As Object, e As EventArgs) Handles txtPastaProj.TextChanged
        'Texto alterado
        '10/03/23
        '==============
        Dim strSaida1 As New String("collectors01_" & DateTime.Now.ToString("yyyyMMdd") & ".json")
        Dim strSaida2 As New String("collectors02_" & DateTime.Now.ToString("yyyyMMdd") & ".json")
        Dim strSaida3 As New String("collectors02_" & DateTime.Now.ToString("yyyyMMdd") & ".csv")

        BtnCollectors1.Enabled = Len(Trim(txtPastaProj.Text)) > 0 And Len(Trim(txtPastaPython.Text)) > 0
        btnParseJson.Enabled = System.IO.File.Exists(strSaida1)
        btnCollectors2.Enabled = System.IO.File.Exists(strSaida2)
        btnSQL.Enabled = System.IO.File.Exists(strSaida3)

    End Sub

    Private Sub btnFecha_Click(sender As Object, e As EventArgs) Handles btnFecha.Click
        'Menu: Arquivo/Fecha
        '10/03/23
        '===================
        Me.Close()

    End Sub

    Private Sub cboClientes_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboClientes.SelectedIndexChanged
        'Alteração do combo
        '17/04/23
        '==================
        _intClientIndex = cboClientes.SelectedIndex

    End Sub

    Private Sub TabelaDeClientesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TabelaDeClientesToolStripMenuItem.Click
        'Manutenção da tabela de clientes
        '13/09/23
        '================================
        Dim f As New VBActive_Taegis_DLL.frmClientes
        f.ShowDialog()

    End Sub

End Class
