Imports System.Data.SqlClient
Imports System.IO
Imports System.Text.RegularExpressions
Imports Microsoft.VisualBasic.FileIO
Imports VBActive_Taegis_DLL.ActiveTaegisDLL

Public Class Form1
    'Sistema Active - VS2022
    'Módulo: Acionamento dos investigations Taegis
    'Obs: There is so much info about how to execute pything script, but none of them saying one simple thing:
    '     that the script must be with the same directory as your project.
    'Heron Jr
    '28/02/23-17/04/23
    '19/03/24-21/03/24
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
        '28/02/23-02/03/23-17/04/23-13/09/23-17/10/23-20/10/23
        '09/03/25-20/03/25
        '=====================================================
        Me.Text = Proj.VersaoAuto1()
        ToolStripStatusLabel1.Text = CopyRight()

        CargaComboClientes()
        _intClientIndex = cboClientes.Items.IndexOf("NETCENTER")
        cboClientes.SelectedIndex = _intClientIndex
        btnInvestigations1.Enabled = False

        _strPastaPython = LeConfigPasta("pasta_python")
        _strPastaProj = LeConfigPasta("pasta_proj_investigations")  '.Replace("\Release", "\Debug") 'Troca de pasta para testes no modo Debug
        txtPastaPython.Text = _strPastaPython
        txtPastaProj.Text = _strPastaProj

        ExcluiArquivosAnteriores()

    End Sub

    Private Sub CargaComboClientes()
        'Carga do combo de clientes cboClientes
        '17/04/23
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
        '17/04/23
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
        '28/02/23
        '============================
        Dim f As New VBActive_Taegis_DLL.frmConfig
        f.ShowDialog()

    End Sub

    Private Sub btnInvestigations1_Click_1(sender As Object, e As EventArgs) Handles btnInvestigations1.Click
        'Botão investigations1 - Grava arquivo investigations01_aaaammdd.json
        '28/02/23-02/03/23-18/04/23-09/10/23
        '20/03/25
        '====================================================================
        Cursor = Cursors.WaitCursor
        btnMutation.Enabled = False
        Dim oProcess As Process = New Process()
        Dim oStartInfo As New ProcessStartInfo(_strPastaPython, "investigations01.py " &
                                               _aTenantId(_intClientIndex) & " " & _aClientId(_intClientIndex) & " " & _aClientSecret(_intClientIndex))
        Dim strSaida1 As New String("investigations01_" & _aTenantId(_intClientIndex) & "_" & DateTime.Now.ToString("yyyyMMdd") & ".json")
        Dim strSaida2 As New String("investigations02_" & _aTenantId(_intClientIndex) & "_" & DateTime.Now.ToString("yyyyMMdd") & ".csv")
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
        If System.IO.File.Exists(strSaida2) Then
            txtMensagens.Text &= "1) Arquivo JSON " & strSaida1 & " gravado com sucesso - Investigações: " & Proj.GetJsonInvestigationCount(strSaida1) & vbCrLf
            txtMensagens.Text &= "2) Arquivo CSV " & strSaida2 & " gravado com sucesso - Investigações: " & (File.ReadAllLines(strSaida2).Length - 1).ToString & vbCrLf
            btnSQL.Enabled = True
        Else
            txtMensagens.ForeColor = Color.Red
            txtMensagens.Text &= "ERRO NA GERAÇÃO DOS ARQUIVOS JSON/CSV *******"
            txtMensagens.ForeColor = Color.Blue
        End If
        'btnParseJson.Enabled = System.IO.File.Exists(strSaida) ==> DESATIVADO EM 20/03/25 - O PYTHON INVESTIGATIONS01 FAZ SOZINHO O JSON E O CSV
        '20/03/25 ==> VAI DIRETO PARA O SQL

    End Sub

    Private Sub btnParseJson_Click(sender As Object, e As EventArgs) Handles btnParseJson.Click
        'Botão Parse Json - Grava arquivo investigations02_aaaammdd.json
        '28/02/23-18/04/23
        '20/03/25 ==> ESTA SUB NÃO É MAIS USADA - O PYTHON INVESTIGATIONS01 FAZ SOZINHO O JSON E O CSV
        '===============================================================
        Cursor = Cursors.WaitCursor
        Dim oProcess As Process = New Process()
        Dim oStartInfo As New ProcessStartInfo(_strPastaPython, "parsejson.py " &
                                               _aTenantId(_intClientIndex))
        Dim strSaida As New String("investigations02_" & _aTenantId(_intClientIndex) & "_" & DateTime.Now.ToString("yyyyMMdd") & ".json")
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
        txtMensagens.Text &= "2) Arquivo JSON " & strSaida & " gravado com sucesso - Linhas: " & strLinhas.Length & vbCrLf
        'btnInvestigations2.Enabled = System.IO.File.Exists(strSaida) ==> DESATIVADO EM 20/03/25 - O PYTHON INVESTIGATIONS01 FAZ SOZINHO O JSON E O CSV

    End Sub

    Private Sub btnInvestigations2_Click_1(sender As Object, e As EventArgs) Handles btnInvestigations2.Click
        'Botão investigations2 - Grava arquivo investigations02_aaaammdd.csv
        '28/02/23-02/03/23-18/04/23
        '20/03/25 ==> ESTA SUB NÃO É MAIS USADA - O PYTHON INVESTIGATIONS01 FAZ SOZINHO O JSON E O CSV
        '===================================================================
        Cursor = Cursors.WaitCursor
        Dim oProcess As Process = New Process()
        Dim oStartInfo As New ProcessStartInfo(_strPastaPython, "investigations02.py " &
                                               _aTenantId(_intClientIndex))
        Dim strSaida As New String("investigations02_" & _aTenantId(_intClientIndex) & "_" & DateTime.Now.ToString("yyyyMMdd") & ".csv")
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

    Private Sub txtPastaPython_TextChanged(sender As Object, e As EventArgs) Handles txtPastaPython.TextChanged
        'Texto alterado
        '28/02/23-18/04/23
        '10/03/25-20/03/25
        '=================
        Dim strSaida1 As New String("investigations01_" & _aTenantId(_intClientIndex) & "_" & DateTime.Now.ToString("yyyyMMdd") & ".json")
        Dim strSaida2 As New String("investigations02_" & _aTenantId(_intClientIndex) & "_" & DateTime.Now.ToString("yyyyMMdd") & ".json")
        Dim strSaida3 As New String("investigations02_" & _aTenantId(_intClientIndex) & "_" & DateTime.Now.ToString("yyyyMMdd") & ".csv")

        'btnInvestigations1.Enabled = Len(Trim(txtPastaProj.Text)) > 0 And Len(Trim(txtPastaPython.Text)) > 0
        chkProcessoManual.Enabled = Len(Trim(txtPastaProj.Text)) > 0 And Len(Trim(txtPastaPython.Text)) > 0
        'btnParseJson.Enabled = System.IO.File.Exists(strSaida1) ==> DESATIVADO EM 20/03/25 - O PYTHON INVESTIGATIONS01 FAZ SOZINHO O JSON E O CSV
        'btnInvestigations2.Enabled = System.IO.File.Exists(strSaida2) ==> DESATIVADO EM 20/03/25 - O PYTHON INVESTIGATIONS01 FAZ SOZINHO O JSON E O CSV
        btnSQL.Enabled = System.IO.File.Exists(strSaida3)

    End Sub

    Private Sub txtPastaProj_TextChanged(sender As Object, e As EventArgs) Handles txtPastaProj.TextChanged
        'Texto alterado
        '28/02/23-18/04/23-09/10/23
        '10/03/25-20/03/25
        '==========================
        Dim strSaida1 As New String("investigations01_" & _aTenantId(_intClientIndex) & "_" & DateTime.Now.ToString("yyyyMMdd") & ".json")
        Dim strSaida2 As New String("investigations02_" & _aTenantId(_intClientIndex) & "_" & DateTime.Now.ToString("yyyyMMdd") & ".json")
        Dim strSaida3 As New String("investigations02_" & _aTenantId(_intClientIndex) & "_" & DateTime.Now.ToString("yyyyMMdd") & ".csv")

        'btnInvestigations1.Enabled = Len(Trim(txtPastaProj.Text)) > 0 And Len(Trim(txtPastaPython.Text)) > 0
        chkProcessoManual.Enabled = Len(Trim(txtPastaProj.Text)) > 0 And Len(Trim(txtPastaPython.Text)) > 0
        btnMutation.Enabled = btnInvestigations1.Enabled
        'btnParseJson.Enabled = System.IO.File.Exists(strSaida1) ==> DESATIVADO EM 20/03/25 - O PYTHON INVESTIGATIONS01 FAZ SOZINHO O JSON E O CSV
        'btnInvestigations2.Enabled = System.IO.File.Exists(strSaida2) ==> DESATIVADO EM 20/03/25 - O PYTHON INVESTIGATIONS01 FAZ SOZINHO O JSON E O CSV
        btnSQL.Enabled = System.IO.File.Exists(strSaida3)

    End Sub

    Private Sub btnSQL_Click(sender As Object, e As EventArgs) Handles btnSQL.Click
        'Botão SQL - Grava tabela SQL t_taegis_investigations a partir do arquivo investigations02_aaaammdd.csv
        '28/02/23-18/04/23
        '======================================================================================================
        '00 - investigation_num
        '01 - description
        '02 - id
        '03 - status
        Cursor = Cursors.WaitCursor
        Dim strData As New String(DateTime.Now.ToString("yyyyMMdd"))
        Dim strSaida As New String("\investigations02_" & _aTenantId(_intClientIndex) & "_" & strData & ".csv")

        Dim tfp As New TextFieldParser(_strPastaProj & strSaida)
        tfp.Delimiters = New String() {";"}
        tfp.TextFieldType = FieldType.Delimited

        tfp.ReadLine() ' skip header

        While tfp.EndOfData = False
            Dim fields = tfp.ReadFields()

            If Not ExisteInvestigationSQL(fields(1)) Then
                GravaSQL_Inclui(fields)
                _intLinhasSQL_Inc += 1
            Else
                GravaSQL_Altera(fields)
                _intLinhasSQL_Alt += 1
            End If
            _intLinhasLidas += 1
        End While

        Cursor = Cursors.Default
        txtMensagens.Text &= "Linhas incluídas na tabela SQL t_taegis_investigations: " & _intLinhasSQL_Inc & vbCrLf &
                             "Linhas alteradas na tabela SQL t_taegis_investigations: " & _intLinhasSQL_Alt & vbCrLf &
                             "Total de linhas CSV lidas: " & _intLinhasLidas
        txtMensagens.Text &= vbCrLf & "==================================================================================="

    End Sub

    Private Function ExisteInvestigationSQL(strId As String) As Boolean
        'Verifica se o investigation existe na tabela SQL t_taegis_investigations
        '28/02/23-02/03/23-09/10/23
        '========================================================================
        Dim oCon As New SqlConnection(ConnectionString)
        Dim oCmd As New SqlCommand
        Dim intQtd As Integer = 0

        oCon.Open()

        With oCmd
            .Connection = oCon
            .CommandType = CommandType.Text
            .CommandText = "select count(*) from t_taegis_investigations " &
            "where id = '" & strId & "' and inv_client = '" & cboClientes.Text & "'"
            .CommandTimeout = 3600
            intQtd = .ExecuteScalar
        End With

        oCon.Close()

        Return (intQtd > 0)

    End Function

    Private Sub GravaSQL_Inclui(fields As String())
        'Inclui linhas na tabela SQL t_taegis_investigations
        '28/02/23-02/03/23-18/04/23-06/10/23-10/10/23
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
                    _aTenantId(_intClientIndex) & "', '" &
                    cboClientes.Text & "')"
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
        'Altera linhas na tabela SQL t_taegis_investigations
        '28/09/23-06/10/23-10/10/23
        '20/03/25
        '===================================================
        Dim oCon As New SqlConnection(ConnectionString)
        Dim oCmd As New SqlCommand

        oCon.Open()

        'Try

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

        'Catch ex As SqlException
        '    txtMensagens.Text &= vbCrLf & "Erro SQL: " & ex.ToString
        '    txtMensagens.Text &= vbCrLf & "==================================================================================="

        'Catch ex1 As Exception
        '    txtMensagens.Text &= vbCrLf & "Inclui SQL => Erro linha " & fields(0) & ": " & ex1.ToString
        '    txtMensagens.Text &= vbCrLf & "==================================================================================="

        'End Try

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
        '10/03/25-20/03/25
        '==================
        _intClientIndex = cboClientes.SelectedIndex
        txtInvestigationId.Text = ""
        txtServiceDeskId.Text = ""
        btnInvestigations1.Enabled = _intClientIndex >= 0
        chkProcessoManual.Enabled = _intClientIndex >= 0

    End Sub

    Private Sub TabelaDeClientesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TabelaDeClientesToolStripMenuItem.Click
        'Manutenção tabela de clientes
        '13/09/23
        '=============================
        Dim f As New VBActive_Taegis_DLL.frmClientes
        f.ShowDialog()

    End Sub

    Private Sub btnMutation_Click(sender As Object, e As EventArgs) Handles btnMutation.Click
        'Botão Mutation - prepara alteração do campo service_desk_id em Investigations
        '09/10/23
        '=============================================================================
        btnInvestigations1.Enabled = False
        txtInvestigationId.Enabled = True
        txtServiceDeskId.Enabled = True
        btnInestigationsList.Enabled = True

    End Sub

    Private Sub txtInvestigationId_TextChanged(sender As Object, e As EventArgs) Handles txtInvestigationId.TextChanged
        'Campo preenchido
        '09/10/23
        '================
        If Not ExisteInvestigationSQL(txtInvestigationId.Text) Then
            MessageBox.Show("Id de Investigation não existe para a empresa")
        Else
            btnRunMutation.Enabled = (txtInvestigationId.Text.Trim.Length > 0) And (txtServiceDeskId.Text.Trim.Length > 0)
        End If

    End Sub

    Private Sub txtServiceDeskId_TextChanged(sender As Object, e As EventArgs) Handles txtServiceDeskId.TextChanged
        'Campo preenchido
        '09/10/23
        '================
        btnRunMutation.Enabled = (txtInvestigationId.Text.Trim.Length > 0) And (txtServiceDeskId.Text.Trim.Length > 0)

    End Sub

    Private Sub btnRunMutation_Click(sender As Object, e As EventArgs) Handles btnRunMutation.Click
        'Processa a alteração do campo service_desk_id em Investigations
        '09/10/23
        '===============================================================
        Cursor = Cursors.WaitCursor
        Dim oProcess As Process = New Process()
        'Dim oStartInfo As New ProcessStartInfo(_strPastaPython, "investigations_mut_pars.py " & cboClientes.Text & " " & "delta" & " " &
        '                                       _aTenantId(_intClientIndex) & " " & _aClientId(_intClientIndex) & " " & _aClientSecret(_intClientIndex) & " " &
        '                                       txtInvestigationId.Text & " " & txtServiceDeskId.Text)
        Dim oStartInfo As New ProcessStartInfo(_strPastaPython, "investigations_mut_pars.py " & "delta" & " " &
                                               _aTenantId(_intClientIndex) & " " & _aClientId(_intClientIndex) & " " & _aClientSecret(_intClientIndex) & " " &
                                               txtInvestigationId.Text & " " & txtServiceDeskId.Text)
        'Dim strSaida As New String("investigations01_" & _aTenantId(_intClientIndex) & "_" & DateTime.Now.ToString("yyyyMMdd") & ".json")
        Dim strLinhas() As String

        txtMensagens.Text = "investigations_mut_pars.py " & "delta" & " " &
                            _aTenantId(_intClientIndex) & " " & _aClientId(_intClientIndex) & " " & _aClientSecret(_intClientIndex) & " " &
                            txtInvestigationId.Text & " " & txtServiceDeskId.Text

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

        txtMensagens.Text &= vbCrLf & sOutput
        Cursor = Cursors.Default
        txtMensagens.Text &= vbCrLf & "Alteração efetuada com sucesso" & vbCrLf

    End Sub

    Private Sub btnInestigationsList_Click(sender As Object, e As EventArgs) Handles btnInestigationsList.Click
        'Botão Investigations List
        '10/10/23
        '=========================
        Dim f As New frmInvestigationsList

        f.ShowDialog()

    End Sub

    Private Sub ListaDeMutationsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ListaDeMutationsToolStripMenuItem.Click
        'Chama o form de lista de mutations
        '22/11/23
        '==================================
        Dim f As New frmMutations

        For Each item In cboClientes.Items
            f.cboClientes.Items.Add(item)
        Next

        f.cboClientes.Items.Add("- TODOS")
        f._intCliente = cboClientes.Items.Count
        f.ShowDialog()

    End Sub

    Private Sub chkProcessoManual_CheckedChanged(sender As Object, e As EventArgs) Handles chkProcessoManual.CheckedChanged
        'Liga/desliga o processo manual
        '09/03/25-10/03/25
        '==============================
        txtMensagens.Text &= "Mova o arquivo de investigações CSV (manual) para a pasta do Projeto." & vbCrLf
        btnInvestigations1.Enabled = (Not chkProcessoManual.Checked)
        btnCSVManual.Enabled = chkProcessoManual.Checked

    End Sub

    Private Sub btnCSVManual_Click(sender As Object, e As EventArgs) Handles btnCSVManual.Click
        'Renomeia os arquivos CSV de geração manual pelo Taegis XDR
        '09/03/25
        '==========================================================
        RenameInvestigationFiles()
        btnSQLManual.Enabled = True

    End Sub

    Private Sub btnSQLManual_Click(sender As Object, e As EventArgs) Handles btnSQLManual.Click
        'Botão SQL - Grava tabela SQL t_taegis_investigations a partir do arquivo investigations02_aaaammdd.csv
        '28/02/23-18/04/23
        '======================================================================================================
        '00 - Col 01 - ID
        '01 - Col 02 - ShortID
        '02 - Col 03 - Description
        '03 - Col 04 - 
        '04 - Col 05 - 
        '05 - Col 06 - Tenant ID
        '06 - Col 07 - Status
        '07 - Col 08 - Updated At
        '08 - Col 09 - Created At
        '09 - Col 10 - 
        '10 - Col 11 - 
        '11 - Col 12 - 
        '12 - Col 13 - Priority
        '13 - Col 14 - Type
        Cursor = Cursors.WaitCursor
        Dim strData As New String(DateTime.Now.ToString("yyyyMMdd"))
        Dim strSaida As New String("\investigations02_manual_" & _aTenantId(_intClientIndex) & "_" & strData & ".csv")

        Dim tfp As New TextFieldParser(_strPastaProj & strSaida)
        'ATENÇÃO: OS ARQUIVOS CSV GERADOS PELA TAEGIS SÃO DELIMITADOS POR VÍRGULAS
        tfp.Delimiters = New String() {","}
        tfp.TextFieldType = FieldType.Delimited

        tfp.ReadLine() ' skip header

        While tfp.EndOfData = False
            Dim fields = tfp.ReadFields()

            If Not ExisteInvestigationSQL(fields(0)) Then
                GravaSQLManual_Inclui(fields)
                _intLinhasSQL_Inc += 1
            Else
                GravaSQLManual_Altera(fields)
                _intLinhasSQL_Alt += 1
            End If
            _intLinhasLidas += 1
        End While

        Cursor = Cursors.Default
        txtMensagens.Text &= "Linhas incluídas na tabela SQL t_taegis_investigations: " & _intLinhasSQL_Inc & vbCrLf &
                             "Linhas alteradas na tabela SQL t_taegis_investigations: " & _intLinhasSQL_Alt & vbCrLf &
                             "Total de linhas CSV lidas: " & _intLinhasLidas
        txtMensagens.Text &= vbCrLf & "==================================================================================="

    End Sub

    Sub RenameInvestigationFiles()
        'Renomeia os arquivos CSV de geração manual pelo Taegis XDR
        '09/03/25
        '==========================================================
        Dim searchPattern As String = "investigations__*.csv"
        'Dim regexPattern As String = "^investigations__(.+?)_-_production_(\d{4})-(\d{2})-(\d{2})T\d{2}_\d{2}_\d{2}\.csv$"
        Dim regexPattern As String = "^investigations__([^_-]+(?:_[^_-]+)*)_-_production_(\d{4})-(\d{2})-(\d{2})T\d{2}_\d{2}_\d{2}\.csv$"

        Try
            For Each filePath In Directory.GetFiles(_strPastaProj, searchPattern)
                Dim fileName As String = Path.GetFileName(filePath)
                Dim match As Match = Regex.Match(fileName, regexPattern)

                If match.Success Then
                    Dim originalName As String = match.Groups(1).Value
                    Dim yyyyMMdd As String = match.Groups(2).Value & match.Groups(3).Value & match.Groups(4).Value
                    Dim newFileName As String = $"investigations02_manual_{_aTenantId(_intClientIndex)}_{yyyyMMdd}.csv"
                    Dim newFilePath As String = Path.Combine(_strPastaProj, newFileName)

                    File.Move(filePath, newFilePath)
                    txtMensagens.Text &= ($"Renamed: {fileName} -> {newFileName}") & vbCrLf
                End If
            Next
        Catch ex As Exception
            Console.WriteLine("Error: " & ex.Message)
        End Try

    End Sub

    Private Sub GravaSQLManual_Inclui(fields As String())
        'Inclui linhas na tabela SQL t_taegis_investigations
        '09/03/25
        '===================================================
        '00 - Col 01 - ID
        '01 - Col 02 - ShortID
        '02 - Col 03 - Description
        '03 - Col 04 - Author
        '04 - Col 05 - 
        '05 - Col 06 - Tenant ID
        '06 - Col 07 - Status
        '07 - Col 08 - Updated At
        '08 - Col 09 - Created At
        '09 - Col 10 - 
        '10 - Col 11 - 
        '11 - Col 12 - 
        '12 - Col 13 - Priority
        '13 - Col 14 - Type
        Dim oCon As New SqlConnection(ConnectionString)
        Dim oCmd As New SqlCommand

        oCon.Open()

        Try

            With oCmd
                .Connection = oCon
                .CommandType = CommandType.Text
                .CommandText = "insert into t_taegis_investigations(" &
                        "id, " &
                        "shortId, " &
                        "description, " &
                        "created_by, " &
                        "inv_tenant_id," &
                        "status, " &
                        "updated_at, " &
                        "created_at, " &
                        "priority, " &
                        "type, " &
                        "inv_client)" &
                        "values(" & "'" &
                        fields(0) & "', '" &
                        fields(1) & "', '" &
                        fields(2).Replace("'", "'") & "', '" &
                        fields(3) & "', '" &
                        _aTenantId(_intClientIndex) & "', '" &
                        fields(6) & "', '" &
                        fields(7) & "', '" &
                        fields(8) & "', '" &
                        fields(12) & "', '" &
                        fields(13) & "', '" &
                        cboClientes.Text & "')"
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

    Private Sub GravaSQLManual_Altera(fields As String())
        'Altera linhas na tabela SQL t_taegis_investigations
        '09/03/25
        '===================================================
        '00 - Col 01 - ID
        '01 - Col 02 - ShortID
        '02 - Col 03 - Description
        '03 - Col 04 - Author
        '04 - Col 05 - 
        '05 - Col 06 - Tenant ID
        '06 - Col 07 - Status
        '07 - Col 08 - Updated At
        '08 - Col 09 - Created At
        '09 - Col 10 - 
        '10 - Col 11 - 
        '11 - Col 12 - 
        '12 - Col 13 - Priority
        '13 - Col 14 - Type
        Dim oCon As New SqlConnection(ConnectionString)
        Dim oCmd As New SqlCommand

        oCon.Open()

        Try

            With oCmd
                .Connection = oCon
                .CommandType = CommandType.Text
                .CommandText = "update t_taegis_investigations set " &
                            "description = '" & fields(2).Replace("'", "'") & "', " &
                            "created_by = '" & fields(3) & "', " &
                            "priority = '" & fields(12) & "', " &
                            "status = '" & fields(6) & "', " &
                            "type = '" & fields(13) & "', " &
                            "shortId = '" & fields(1) & "', " &
                            "alt_data = GETDATE(), " &
                            "alt_user = SUSER_NAME() " &
                            "WHERE id = '" & fields(0) & "'"

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

    Private Sub btnSelecioneCliente_Click(sender As Object, e As EventArgs) Handles btnSelecioneCliente.Click
        'Botão selecione um cliente para ativar o combo de clientes
        '10/03/25
        '==========================================================
        cboClientes.Enabled = True

    End Sub

End Class
