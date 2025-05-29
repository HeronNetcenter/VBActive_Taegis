Imports System.Data.SqlClient
Imports Microsoft.VisualBasic.FileIO
Imports VBActive_Taegis_DLL.ActiveTaegisDLL

Public Class Form1
    'Sistema Active - VS2022
    'Módulo: Contagem dos eventos Taegis
    'Obs: There is so much info about how to execute pything script, but none of them saying one simple thing:
    '     that the script must be with the same directory as your project.
    'Heron Jr
    '16/08/23-29/08/23
    '19/03/24-21/03/24-23/12/24
    '==========================================================================================================
    Private fd As New FolderBrowserDialog
    Private _strPastaProj As New String("")
    Private _strSaida As New String("")
    Private _strPastaPython As New String("")
    Private _intLinhasSQL_Inc As Integer = 0
    Private _intLinhasSQL_Alt As Integer = 0
    Private _intLinhasLidas As Integer = 0
    Private _datExtracao As Date
    'Private _datExtracao1 As Date
    'Private _datExtracao2 As Date
    Private _aTenantId As New List(Of String)
    Private _aClientId As New List(Of String)
    Private _aClientSecret As New List(Of String)
    Private _aEnvironment As New List(Of String)
    Private _intClientIndex As Integer = 0
    Private _blnDiario As Boolean = False
    Public Value As String

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Carga do form
        '16/08/23-13/09/23-17/10/23-20/10/23
        '05/06/24-20/12/24-23/12/24
        '=======================================================================
        Me.Text = Proj.VersaoAuto1()
        ToolStripStatusLabel1.Text = CopyRight()

        '=================================================================================
        'ConfigArquivos() 'TESTES EM 23/12/24
        '=================================================================================

        CargaComboClientes()
        '_intClientIndex = cboClientes.Items.IndexOf("NETCENTER")
        _intClientIndex = 0
        cboClientes.SelectedIndex = _intClientIndex

        _strPastaPython = LeConfigPasta("pasta_python")
        _strPastaProj = LeConfigPasta("pasta_proj_events")  '.Replace("\Release", "\Debug") 'Troca de pasta para testes no modo Debug
        txtPastaPython.Text = _strPastaPython
        txtPastaProj.Text = _strPastaProj
        '_datExtracao1 = DateAdd(DateInterval.Day, -1, dtpDataExtracao.Value.Date)
        '_datExtracao2 = dtpDataExtracao.Value.Date
        _datExtracao = dtpDataExtracao.Value.Date

        ExcluiArquivosAnteriores()

    End Sub

    Private Sub CargaComboClientes()
        'Carga do combo de clientes cboClientes
        '16/08/23-29/08/23
        '23/12/24
        '======================================
        Dim oCon As New SqlConnection(ConnectionString)
        Dim oCmd As New SqlCommand
        Dim oDrd As SqlDataReader = Nothing

        oCon.Open()

        With oCmd
            .Connection = oCon
            .CommandType = CommandType.Text
            .CommandText = "USE ACTIVE
                            SELECT ac.client_name client_name, ac.client_abrev client_abrev, ac.tenant_id tenant_id, ac.client_id client_id, ac.client_secret client_secret 
                                FROM t_taegis_tab_clients ac
                            INNER JOIN DWNetcenter.dbo.t_abrev_clientes dc ON ac.client_abrev = dc.cliente"
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

    Private Sub btnEvents_Click(sender As Object, e As EventArgs) Handles btnEvents.Click
        'Botão Conta Eventos Diários - Grava arquivo "events_count_tenentId_aaaammdd_aaaammdd.csv"
        'São eventos das 24hs do dia anterior 00:00:00 a 23:59:59
        '16/08/23-29/08/23
        '19/12/24-23/12/24
        '=========================================================================================
        Cursor = Cursors.WaitCursor
        Static intLinha As Integer = 1
        Dim oProcess As Process = New Process()
        'Dim oStartInfo As New ProcessStartInfo(_strPastaPython,
        '                                       If(_datExtracao2 = Now.Date,
        '                                           "events_count_oauth2_dia_pars.py ",
        '                                           "events_count_par01.py " & _datExtracao1.Year & "-" & _datExtracao1.Month.ToString("00") &
        '                                           "-" & _datExtracao1.Day.ToString("00") &
        '                                           " " & _datExtracao2.Year & "-" & _datExtracao2.Month.ToString("00") & "-" &
        '                                           _datExtracao2.Day.ToString("00") & " ") &
        '                                       cboClientes.Text & " delta " & _aTenantId(_intClientIndex) & " " & _aClientId(_intClientIndex) & " " &
        '                                       _aClientSecret(_intClientIndex))
        'Exemplo: python events_query.py 142779 delta

        Dim oStartInfo As New ProcessStartInfo(_strPastaPython,
                                                   "events_count_oauth2_dia_pars.py " & _datExtracao.Year & "-" & _datExtracao.Month.ToString("00") &
                                                   "-" & _datExtracao.Day.ToString("00") & " " &
                                               cboClientes.Text & " delta " & _aTenantId(_intClientIndex) & " " & _aClientId(_intClientIndex) & " " &
                                               _aClientSecret(_intClientIndex))
        'Exemplo: python events_query.py 142779 delta

        Dim strSaida As New String("events_count_" & _aTenantId(_intClientIndex) & "_" & Ontem() & ".csv")
        Dim strLinhas() As String

        oStartInfo.UseShellExecute = False
        oStartInfo.CreateNoWindow = True
        oStartInfo.RedirectStandardOutput = True
        oProcess.StartInfo = oStartInfo
        oProcess.Start()
        Application.DoEvents()

        Dim sOutput As String

        Using oStreamReader As System.IO.StreamReader = oProcess.StandardOutput
            sOutput = oStreamReader.ReadToEnd
            strLinhas = sOutput.Split(New String() {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries)
        End Using

        'txtMensagens.Text = sOutput
        Cursor = Cursors.Default
        txtMensagens.Text &= intLinha & ") Arquivo CSV " & strSaida & " gravado com sucesso - " & QuantidadeEventos(strSaida) & vbCrLf : intLinha += 1
        btnSQLDiario.Enabled = System.IO.File.Exists(strSaida)
        oProcess.Dispose()
        oProcess = Nothing

        'Salta para o final de txtMensagens
        SaltaParaFimDaMensagem()

    End Sub

    Private Sub btnEventsMesAnt_Click(sender As Object, e As EventArgs) Handles btnEventsMesAnt.Click
        'Botão Conta Eventos Diários - Grava arquivo "events_count_tenentId_aaaammdd_aaaammdd.csv"
        'São eventos das mês anterior de aaaamm01 a aaaamm31
        '23/12/24
        '=========================================================================================
        Cursor = Cursors.WaitCursor
        Static intLinha As Integer = 1
        Dim oProcess As Process = New Process()
        'Dim oStartInfo As New ProcessStartInfo(_strPastaPython,
        '                                       If(_datExtracao2 = Now.Date,
        '                                           "events_count_oauth2_dia_pars.py ",
        '                                           "events_count_par01.py " & _datExtracao1.Year & "-" & _datExtracao1.Month.ToString("00") &
        '                                           "-" & _datExtracao1.Day.ToString("00") &
        '                                           " " & _datExtracao2.Year & "-" & _datExtracao2.Month.ToString("00") & "-" &
        '                                           _datExtracao2.Day.ToString("00") & " ") &
        '                                       cboClientes.Text & " delta " & _aTenantId(_intClientIndex) & " " & _aClientId(_intClientIndex) & " " &
        '                                       _aClientSecret(_intClientIndex))
        'Exemplo: python events_query.py 142779 delta

        Dim oStartInfo As New ProcessStartInfo(_strPastaPython,
                                                   "events_count_oauth2_pars.py " &
                                               cboClientes.Text.Replace(" ", "_") & " delta " & _aTenantId(_intClientIndex) & " " & _aClientId(_intClientIndex) & " " &
                                               _aClientSecret(_intClientIndex))
        'Exemplo: python events_query.py 142779 delta

        Dim strSaida As New String("events_count_" & _aTenantId(_intClientIndex) & "_" & MesPassado() & ".csv")
        Dim strLinhas() As String

        oStartInfo.UseShellExecute = False
        oStartInfo.CreateNoWindow = True
        oStartInfo.RedirectStandardOutput = True
        oProcess.StartInfo = oStartInfo
        oProcess.Start()
        Application.DoEvents()

        Dim sOutput As String

        Using oStreamReader As System.IO.StreamReader = oProcess.StandardOutput
            sOutput = oStreamReader.ReadToEnd
            strLinhas = sOutput.Split(New String() {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries)
        End Using

        'txtMensagens.Text = sOutput
        Cursor = Cursors.Default
        txtMensagens.Text &= intLinha & ") Arquivo CSV " & strSaida & " gravado com sucesso - " & QuantidadeEventos(strSaida) & vbCrLf : intLinha += 1
        btnSQL.Enabled = System.IO.File.Exists(strSaida)
        oProcess.Dispose()
        oProcess = Nothing

        'Salta para o final de txtMensagens
        SaltaParaFimDaMensagem()

    End Sub

    Private Function QuantidadeEventos(ByVal strArquivo As String) As String
        'Retorna a quantidade de eventos no arquivo csv
        '17/08/23-28/08/23
        '==============================================
        Dim strTenant As New String("")
        Dim dblEventos As Double = 0

        Dim tfp As New TextFieldParser(strArquivo)
        tfp.Delimiters = New String() {";"}
        tfp.TextFieldType = FieldType.Delimited

        While tfp.EndOfData = False
            Dim fields = tfp.ReadFields()
            strTenant = _aTenantId(_intClientIndex)
            dblEventos = fields(2)
        End While

        Return "Tenant: " & strTenant & ", Eventos: " & dblEventos

    End Function

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

    Private Function Ontem() As String
        'Retorna o dia de ontem
        '19/12/24-20/12/24
        '======================
        'Dim strFirstDay As New String(DateTime.Now.AddDays(-1).ToString("yyyyMMdd"))
        Dim strFirstDay As New String(_datExtracao.AddDays(-1).ToString("yyyyMMdd"))

        Return strFirstDay & "_" & strFirstDay

    End Function

    Private Sub btnSQL_Click(sender As Object, e As EventArgs) Handles btnSQL.Click
        'Botão SQL - Grava tabela SQL t_taegis_events a partir do arquivo events02_aaaammdd.csv
        '16/08/23-28/08/23-31/07/23
        '19/12/24
        '======================================================================================
        Cursor = Cursors.WaitCursor
        Dim strSaida As New String("\events_count_" & _aTenantId(_intClientIndex) & "_" & MesPassado() & ".csv")

        Dim tfp As New TextFieldParser(_strPastaProj & strSaida)
        tfp.Delimiters = New String() {";"}
        tfp.TextFieldType = FieldType.Delimited

        'tfp.ReadLine() ' skip header

        While tfp.EndOfData = False
            Dim fields = tfp.ReadFields()

            If Not ExisteEventoSQL(_aTenantId(_intClientIndex), fields(0), fields(1)) Then
                GravaSQL_Inclui(fields)
                _intLinhasSQL_Inc += 1
            Else
                GravaSQL_Altera(fields)
                _intLinhasSQL_Alt += 1
            End If
            _intLinhasLidas += 1
        End While

        Cursor = Cursors.Default
        txtMensagens.Text &= "Linhas alteradas na tabela SQL t_taegis_events: " & _intLinhasSQL_Alt & vbCrLf &
                        "Linhas incluídas na tabela SQL t_taegis_events: " & _intLinhasSQL_Inc & vbCrLf &
                        "Total de linhas CSV lidas: " & _intLinhasLidas
        txtMensagens.Text &= vbCrLf & "===================================================================================" & vbCrLf
        _intLinhasSQL_Alt = 0
        _intLinhasSQL_Inc = 0
        _intLinhasLidas = 0

        'Salta para o final de txtMensagens
        SaltaParaFimDaMensagem()

    End Sub

    Private Sub btnSQLDiario_Click(sender As Object, e As EventArgs) Handles btnSQLDiario.Click
        'Botão SQL Diário - Grava tabela SQL t_taegis_events_diario a partir do arquivo events_count_tenantId_aaaammdd_aaaammdd.csv
        '23/12/24
        '==========================================================================================================================
        Cursor = Cursors.WaitCursor
        Dim strSaida As New String("\events_count_" & _aTenantId(_intClientIndex) & "_" & Ontem() & ".csv")

        Dim tfp As New TextFieldParser(_strPastaProj & strSaida)
        tfp.Delimiters = New String() {";"}
        tfp.TextFieldType = FieldType.Delimited

        'tfp.ReadLine() ' skip header

        While tfp.EndOfData = False
            Dim fields = tfp.ReadFields()

            _blnDiario = True
            If Not ExisteEventoSQL(_aTenantId(_intClientIndex), fields(0), fields(1), True) Then
                GravaSQL_Inclui(fields)
                _intLinhasSQL_Inc += 1
            Else
                GravaSQL_Altera(fields)
                _intLinhasSQL_Alt += 1
            End If
            _intLinhasLidas += 1
            _blnDiario = False
        End While

        Cursor = Cursors.Default
        txtMensagens.Text &= "Linhas alteradas na tabela SQL t_taegis_events_diario: " & _intLinhasSQL_Alt & vbCrLf &
                        "Linhas incluídas na tabela SQL t_taegis_events_diario: " & _intLinhasSQL_Inc & vbCrLf &
                        "Total de linhas CSV lidas: " & _intLinhasLidas
        txtMensagens.Text &= vbCrLf & "===================================================================================" & vbCrLf
        _intLinhasSQL_Alt = 0
        _intLinhasSQL_Inc = 0
        _intLinhasLidas = 0

        'Salta para o final de txtMensagens
        SaltaParaFimDaMensagem()

    End Sub

    Private Sub SaltaParaFimDaMensagem()
        'Salta para o fim do texto de mensagens txtMensagens
        '16/08/23
        '===================================================
        txtMensagens.SelectionStart = txtMensagens.Text.Length
        txtMensagens.ScrollToCaret()

    End Sub

    Private Function ExisteEventoSQL(strTenantId As String, strDateStart As String, strDateEnd As String, Optional ByVal blnDiario As Boolean = False) As Boolean
        'Verifica se o evento existe na tabela SQL t_taegis_events ou t_taegis_events_diario
        '16/08/23-28/08/23
        '10/01/25
        '===================================================================================
        Dim oCon As New SqlConnection(ConnectionString)
        Dim oCmd As New SqlCommand
        Dim intQtd As Integer = 0

        oCon.Open()

        With oCmd
            .Connection = oCon
            .CommandType = CommandType.Text
            .CommandText = "select count(*) from t_taegis_events" & If(blnDiario, "_diario", "") & " " &
                           "where event_date_start = '" & strDateStart & "' AND event_date_end = '" & strDateEnd &
                           "' AND event_tenant_id = '" & strTenantId & "'"
            .CommandTimeout = 3600
            intQtd = .ExecuteScalar
        End With

        oCon.Close()

        Return (intQtd > 0)

    End Function

    Private Sub GravaSQL_Inclui(fields As String())
        'Inclui linhas na tabela SQL t_taegis_events
        '16/08/23-28/08/23
        '23/12/24
        '===========================================
        Dim oCon As New SqlConnection(ConnectionString)
        Dim oCmd As New SqlCommand

        oCon.Open()

        Try

            With oCmd
                .Connection = oCon
                .CommandType = CommandType.Text
                .CommandText = "insert into t_taegis_events" & If(_blnDiario, "_diario", "") & "(" &
                    "event_date_start, " &
                    "event_date_end, " &
                    "event_tenant_id," &
                    "event_client, " &
                    "event_qty) " &
                    "values('" & fields(0) & "', '" &
                    fields(1) & "', '" &
                    _aTenantId(_intClientIndex) & "', '" &
                    cboClientes.Text & "', " &
                    fields(2) & ")"
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
        'Altera linhas na tabela SQL t_taegis_events
        '31/08/23-11/09/23-20/10/23
        '===========================================
        Dim oCon As New SqlConnection(ConnectionString)
        Dim oCmd As New SqlCommand

        oCon.Open()

        Try

            With oCmd
                .Connection = oCon
                .CommandType = CommandType.Text
                .CommandText = "update t_taegis_events" & If(_blnDiario, "_diario ", " ") &
                    "set event_qty = " & fields(2) & ", " &
                    "alt_data = getdate(), " &
                    "alt_user = suser_name() " &
                    "where event_tenant_id = '" & _aTenantId(_intClientIndex) & "' and event_date_start = '" & fields(0) & "'"
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

    Private Sub txtPastaPython_TextChanged(sender As Object, e As EventArgs) Handles txtPastaPython.TextChanged
        'Texto alterado
        '16/08/23
        '23/12/24
        '==============
        Dim strSaida1 As New String("events01_" & _aTenantId(_intClientIndex) & "_" & DateTime.Now.AddDays(-1).ToString("yyyyMMdd") & ".json")
        Dim strSaida2 As New String("events02_" & _aTenantId(_intClientIndex) & "_" & DateTime.Now.AddDays(-1).ToString("yyyyMMdd") & ".csv")

        btnEvents.Enabled = Len(Trim(txtPastaProj.Text)) > 0 And Len(Trim(txtPastaPython.Text)) > 0
        btnEventsMesAnt.Enabled = btnEvents.Enabled
        btnSQL.Enabled = System.IO.File.Exists(strSaida2)
        btnSQLDiario.Enabled = btnSQL.Enabled

    End Sub

    Private Sub txtPastaProj_TextChanged(sender As Object, e As EventArgs) Handles txtPastaProj.TextChanged
        'Texto alterado
        '16/08/23
        '23/12/24
        '==============
        Dim strSaida1 As New String("events01_" & _aTenantId(_intClientIndex) & "_" & DateTime.Now.AddDays(-1).ToString("yyyyMMdd") & ".json")
        Dim strSaida2 As New String("events02_" & _aTenantId(_intClientIndex) & "_" & DateTime.Now.AddDays(-1).ToString("yyyyMMdd") & ".csv")

        btnEvents.Enabled = Len(Trim(txtPastaProj.Text)) > 0 And Len(Trim(txtPastaPython.Text)) > 0
        btnEventsMesAnt.Enabled = btnEvents.Enabled
        btnSQL.Enabled = System.IO.File.Exists(strSaida2)
        btnSQLDiario.Enabled = btnSQL.Enabled

    End Sub

    Private Sub ConfiguraçãoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ConfiguraçãoToolStripMenuItem.Click
        'Menu Arquivo/Configuração F2
        '16/08/23
        '23/12/24
        '============================
        ConfigArquivos()

    End Sub

    Private Sub ConfigArquivos()
        'Arquivo/Configuração F2
        '23/12/24
        '============================
        Dim f As New VBActive_Taegis_DLL.frmConfig
        f.ShowDialog()
        btnAtualizaPastas.Visible = True
        btnEvents.Enabled = False
        btnEventsMesAnt.Enabled = btnEvents.Enabled

    End Sub
    Private Sub btnAtualizaPastas_Click(sender As Object, e As EventArgs) Handles btnAtualizaPastas.Click
        'Botão atualiza pastas
        '16/08/23
        '23/12/24
        '=====================
        RecargaPastas()
        btnAtualizaPastas.Visible = False
        btnEvents.Enabled = True
        btnEventsMesAnt.Enabled = btnEvents.Enabled

    End Sub

    Private Sub RecargaPastas()
        'Atualiza as pastas Python e Projeto
        '16/08/23-13/09/23
        '===================================
        Dim strPastas As New String(LeConfig())

        txtPastaPython.Text = strPastas.Split(";")(0)
        txtPastaProj.Text = strPastas.Split(";")(1)

    End Sub

    Private Sub btnFecha_Click(sender As Object, e As EventArgs) Handles btnFecha.Click
        'Botão Fecha
        '16/08/23
        '===========
        Me.Close()

    End Sub

    Private Sub FechaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FechaToolStripMenuItem.Click
        'Menu: Arquivo/Fecha
        '16/08/23
        '===================
        btnFecha.PerformClick()

    End Sub

    Private Sub dtpDataExtracao_ValueChanged(sender As Object, e As EventArgs) Handles dtpDataExtracao.ValueChanged
        'Alteração de valor
        '16/08/23
        '18/12/24-20/12/24-23/12/24
        '10/01/25
        '==========================
        Dim intEventos As Integer = DataJaExtraida()

        btnEvents.Enabled = True
        btnEventsPeriodo.Enabled = True

        With dtpDataExtracao
            If .Value.Date > Now.Date Then
                MessageBox.Show("Datas futuras não são permitidas", "Erro de Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Else
                _datExtracao = dtpDataExtracao.Value.Date
                '_datExtracao2 = DateAdd(DateInterval.Day, 1, dtpDataExtracao.Value.Date)
                btnSQL.Enabled = False
                btnSQLDiario.Enabled = btnSQL.Enabled
                'If intEventos > 0 Then
                '    Dim strMsg As New String("Data selecionada " & .Value.ToString("dd/MM/yyyy") & " já existe no banco de dados para este cliente com " & intEventos & " events. Continua?")
                '    Dim strTit As New String("Seleção de Data para Events")
                '    If MessageBox.Show(strMsg, strTit, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                '        btnEvents.Enabled = False
                '    End If
                'End If
            End If
        End With

        btnEventsMesAnt.Enabled = btnEvents.Enabled

    End Sub

    Private Function DataJaExtraida() As Integer
        'Retorna True se a data selecionada já consta na tabela t_taegis_events no banco de dados
        '16/08/23
        '========================================================================================
        Dim oCon As New SqlConnection(ConnectionString)
        Dim oCmd As New SqlCommand
        Dim intQtd As Integer = 0
        Dim strDataSel As New String(dtpDataExtracao.Value.ToString("yyyyMMdd"))

        oCon.Open()

        With oCmd
            .Connection = oCon
            .CommandType = CommandType.Text
            .CommandText = "select count(*) from t_taegis_events " &
                           "where event_date_start = '" & strDataSel & "' and event_tenant_id = '" & _aTenantId(_intClientIndex) & "'"
            .CommandTimeout = 3600
            intQtd = .ExecuteScalar
        End With

        oCon.Close()

        Return intQtd

    End Function

    Private Sub ExcluiArquivosAnteriores()
        '16/08/23
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

    Private Sub TabelaDeClientesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TabelaDeClientesToolStripMenuItem.Click
        'Menu Arquivo: Tabela de Clientes
        '16/08/23
        '================================
        Dim f As New VBActive_Taegis_DLL.frmClientes
        f.ShowDialog()

    End Sub

    Private Sub cboClientes_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboClientes.SelectedIndexChanged
        'Alteração do combo
        '31/03/23
        '==================
        _intClientIndex = cboClientes.SelectedIndex

    End Sub

    Private Sub btnEventsPeriodo_Click(sender As Object, e As EventArgs) Handles btnEventsPeriodo.Click
        'Botão para retornar os eventos diários de um período
        '10/01/25-12/05/25
        '====================================================
        Dim selectedDate As DateTime = dtpDataExtracao.Value
        Dim strMsg As String = ""
        Dim strTit As String = "Seleção de Data para Events"

        ' Calculate the first day of the current month
        Dim firstDayOfMonth As DateTime = New DateTime(selectedDate.Year, selectedDate.Month, 1)
        Dim currentDate = firstDayOfMonth

        ' Calculate the last day of the current month
        Dim lastDayOfMonth As DateTime = firstDayOfMonth.AddMonths(1).AddDays(-1)

        strMsg = "Confirma o período de eventos diários de " & firstDayOfMonth.ToString("dd/MM/yyyy") & " a " &
                 lastDayOfMonth.ToString("dd/MM/yyyy") & vbCrLf
        If MessageBox.Show(strMsg, strTit, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            txtMensagens.AppendText("Início da geração de eventos diários do período " & firstDayOfMonth & " a " & lastDayOfMonth & vbCrLf)
            txtMensagens.AppendText("===================================================================================" & vbCrLf)
            Cursor = Cursors.WaitCursor
            Application.DoEvents()

            ' Loop through each day of the month
            While currentDate <= lastDayOfMonth
                txtMensagens.AppendText("Geração de eventos diários do dia " & currentDate.ToString("dd/MM/yyyy") & vbCrLf)
                txtMensagens.SelectionStart = txtMensagens.Text.Length
                txtMensagens.ScrollToCaret()
                _datExtracao = currentDate
                btnEvents.PerformClick()
                Application.DoEvents()
                btnSQLDiario.PerformClick()
                Application.DoEvents()
                currentDate = currentDate.AddDays(1)
            End While

            txtMensagens.AppendText("Fim da geração de eventos diários do período " & firstDayOfMonth & " a " & lastDayOfMonth & vbCrLf)
            txtMensagens.SelectionStart = txtMensagens.Text.Length
            txtMensagens.ScrollToCaret()
            Cursor = Cursors.Default
        End If

    End Sub

End Class
