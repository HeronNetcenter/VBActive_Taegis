Imports System.Data.SqlClient
Imports System.IO
Imports Microsoft.VisualBasic.FileIO
Imports VBActive_Taegis_DLL
Imports VBActive_Taegis_DLL.ActiveTaegisDLL

Public Class Form1
    'Formulário para Data Sources - VS2022
    '19/09/23
    '07/03/24-21/04/24
    '============================
    Private fd As New FolderBrowserDialog
    Private _strPastaProj As New String("")
    Private _strSaida As New String("")
    Private _strPastaPython As New String("")
    Private _intLinhasSQL_Inc As Integer = 0
    Private _intLinhasSQL_Alt As Integer = 0
    Private _intLinhasLidas As Integer = 0
    Private _datExtracao1 As Date
    Private _datExtracao2 As Date
    Private _aTenantId As New List(Of String)
    Private _aClientId As New List(Of String)
    Private _aClientSecret As New List(Of String)
    Private _intClientIndex As Integer = 0
    Private _lastDayLastMonth As String = New DateTime(Now.Year, Now.Month, 1).AddDays(-1).ToString("yyyyMMdd")

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Carga do form
        '16/08/23-13/09/23-17/10/23-19/10/23-05/06/24
        '=======================================================================
        Me.Text = Proj.VersaoAuto1()
        ToolStripStatusLabel1.Text = CopyRight()

        CargaComboClientes()
        _intClientIndex = cboClientes.Items.IndexOf("NETCENTER")
        cboClientes.SelectedIndex = _intClientIndex

        _strPastaPython = LeConfigPasta("pasta_python")
        _strPastaProj = LeConfigPasta("pasta_proj_datasources").Replace("\Debug", "\Release")
        txtPastaPython.Text = _strPastaPython
        txtPastaProj.Text = _strPastaProj
        _datExtracao1 = DateAdd(DateInterval.Day, -1, dtpDataExtracao.Value.Date)
        _datExtracao2 = dtpDataExtracao.Value.Date

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

    Private Sub TabelaDeClientesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TabelaDeClientesToolStripMenuItem.Click
        'Tabela de Clientes
        '14/09/23
        '==================
        Dim f As New frmClientes()
        f.ShowDialog()

    End Sub

    Private Sub ConfiguraToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ConfiguraToolStripMenuItem.Click
        Dim f As New frmConfig()
        f.ShowDialog()
        btnAtualizaPastas.Visible = True
        btnContaDataSources.Enabled = False

    End Sub

    Private Sub btnContaDataSources_Click(sender As Object, e As EventArgs) Handles btnContaDataSources.Click
        'Botão Conta Data Sources - Grava arquivo data_sources_count_aaaammdd.csv
        '19/09/23-18/10/23
        '========================================================================
        Cursor = Cursors.WaitCursor
        Dim oProcess As Process = New Process()
        Dim oStartInfo As New ProcessStartInfo(_strPastaPython, "data_sources_count_oauth2_pars.py " &
                                               cboClientes.Text & " delta " & _aTenantId(_intClientIndex) & " " & _aClientId(_intClientIndex) & " " & _aClientSecret(_intClientIndex))
        'Exemplo: python data_sources_query.py NETCENTER delta <tenant_id> <client_id> <client secret>

        Dim strSaida As New String("data_sources_count_" & _aTenantId(_intClientIndex) & "_" & MesPassado() & ".csv")
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
        txtMensagens.Text &= "1) Arquivo CSV " & strSaida & " gravado com sucesso - " & QuantidadeDataSources(strSaida) & vbCrLf
        btnSQL.Enabled = System.IO.File.Exists(strSaida)
        oProcess.Dispose()
        oProcess = Nothing

        'Salta para o final de txtMensagens
        SaltaParaFimDaMensagem()

    End Sub

    Private Function QuantidadeDataSources(ByVal strArquivo As String) As String
        'Retorna a quantidade de DataSources no arquivo csv
        '17/08/23-28/08/23-20/09/23
        '=================================================
        Dim strTenant As New String("")
        Dim dblHealthy As Double = 0
        Dim dblWarning As Double = 0
        Dim dblNodata As Double = 0

        Dim tfp As New TextFieldParser(strArquivo)
        tfp.Delimiters = New String() {";"}
        tfp.TextFieldType = FieldType.Delimited

        While tfp.EndOfData = False
            Dim fields = tfp.ReadFields()
            strTenant = _aTenantId(_intClientIndex)
            dblHealthy = fields(2)
            dblWarning = fields(3)
            dblNodata = fields(4)
        End While

        Return "Tenant: " & strTenant & vbCrLf & vbTab & "DataSources: " & vbCrLf & vbTab & " - Healthy=" & dblHealthy & vbCrLf & vbTab & " - Warnings=" & dblWarning & vbCrLf & vbTab & " - Nodata=" & dblNodata

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

    Private Sub btnSQL_Click(sender As Object, e As EventArgs) Handles btnSQL.Click
        'Botão SQL - Grava tabela SQL t_taegis_data_sources a partir do arquivo data_sources02_aaaammdd.csv
        '16/08/23-28/08/23-31/07/23
        '======================================================================================
        Cursor = Cursors.WaitCursor
        Dim strSaida As New String("\data_sources_count_" & _aTenantId(_intClientIndex) & "_" & MesPassado() & ".csv")

        Dim tfp As New TextFieldParser(_strPastaProj & strSaida)
        tfp.Delimiters = New String() {";"}
        tfp.TextFieldType = FieldType.Delimited

        'tfp.ReadLine() ' skip header

        While tfp.EndOfData = False
            Dim fields = tfp.ReadFields()

            If Not ExisteDataSourcesQL(_aTenantId(_intClientIndex)) Then
                GravaSQL_Inclui(fields)
                _intLinhasSQL_Inc += 1
            Else
                GravaSQL_Altera(fields)
                _intLinhasSQL_Alt += 1
            End If
            _intLinhasLidas += 1
        End While

        Cursor = Cursors.Default
        txtMensagens.Text &= "Linhas alteradas na tabela SQL t_taegis_data_sources: " & _intLinhasSQL_Alt & vbCrLf &
                        "Linhas incluídas na tabela SQL t_taegis_data_sources: " & _intLinhasSQL_Inc & vbCrLf &
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

    Private Function ExisteDataSourcesQL(strTenantId As String) As Boolean
        'Verifica se o eventa existe na tabela SQL t_taegis_data_sources
        '16/08/23-28/08/23-20/09/23-13/10/23
        '===============================================================
        Dim oCon As New SqlConnection(ConnectionString)
        Dim oCmd As New SqlCommand
        Dim intQtd As Integer = 0

        oCon.Open()

        With oCmd
            .Connection = oCon
            .CommandType = CommandType.Text
            .CommandText = "select count(*) from t_taegis_data_sources " &
                           "where event_tenant_id = '" & strTenantId & "' " &
                           "And event_date = '" & _lastDayLastMonth & "'"
            .CommandTimeout = 3600
            intQtd = .ExecuteScalar
        End With

        oCon.Close()

        Return (intQtd > 0)

    End Function

    Private Sub GravaSQL_Inclui(fields As String())
        'Inclui linhas na tabela SQL t_taegis_data_sources
        '16/08/23-28/08/23-20/09/23
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
                    _aTenantId(_intClientIndex) & "', '" &
                    cboClientes.Text & "', " &
                    fields(2) & ", " &
                    fields(3) & ", " &
                    fields(4) & ")"
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
        'Altera linhas na tabela SQL t_taegis_data_sources
        '31/08/23-11/09/23-20/09/23
        '===========================================
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
                    "where event_tenant_id = '" & _aTenantId(_intClientIndex) & "'"
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
        '==============
        Dim strSaida1 As New String("data_sources01_" & _aTenantId(_intClientIndex) & "_" & DateTime.Now.AddDays(-1).ToString("yyyyMMdd") & ".json")
        Dim strSaida2 As New String("data_sources02_" & _aTenantId(_intClientIndex) & "_" & DateTime.Now.AddDays(-1).ToString("yyyyMMdd") & ".csv")

        btnContaDataSources.Enabled = Len(Trim(txtPastaProj.Text)) > 0 And Len(Trim(txtPastaPython.Text)) > 0
        btnSQL.Enabled = System.IO.File.Exists(strSaida2)

    End Sub

    Private Sub txtPastaProj_TextChanged(sender As Object, e As EventArgs) Handles txtPastaProj.TextChanged
        'Texto alterado
        '16/08/23
        '==============
        Dim strSaida1 As New String("data_sources01_" & _aTenantId(_intClientIndex) & "_" & DateTime.Now.AddDays(-1).ToString("yyyyMMdd") & ".json")
        Dim strSaida2 As New String("data_sources02_" & _aTenantId(_intClientIndex) & "_" & DateTime.Now.AddDays(-1).ToString("yyyyMMdd") & ".csv")

        btnContaDataSources.Enabled = Len(Trim(txtPastaProj.Text)) > 0 And Len(Trim(txtPastaPython.Text)) > 0
        btnSQL.Enabled = System.IO.File.Exists(strSaida2)

    End Sub

    Private Sub btnAtualizaPastas_Click(sender As Object, e As EventArgs) Handles btnAtualizaPastas.Click
        RecargaPastas()
        btnAtualizaPastas.Visible = False
        btnContaDataSources.Enabled = True

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

    Private Sub cboClientes_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboClientes.SelectedIndexChanged
        'Alteração do combo
        '31/03/23
        '==================
        _intClientIndex = cboClientes.SelectedIndex

    End Sub

    Private Sub btnGeraCSVDataSources_Click(sender As Object, e As EventArgs) Handles btnGeraCSVDataSources.Click
        'Botão gera CSV de data sources
        '20/02/25
        '==============================
        GeraCSVDataSources()

    End Sub

    Private Sub GeraCSVDataSources()
        'Gera CSV de data sources
        '20/02/25
        '========================
        Dim filePath As String = "C:\DWNetcenter\ACTIVE\DATA_SOURCES\" & cboClientes.SelectedItem & ".CSV"
        Dim statusCounts As Dictionary(Of String, Integer) = CountStatusTypes(filePath)
        Dim intHealthy As Integer = 0
        Dim intWarning As Integer = 0
        Dim intNodata As Integer = 0

        ' Display results
        For Each status In statusCounts
            txtMensagens.Text &= ($"{status.Key}: {status.Value}") & vbCrLf
            If status.Key = "HEALTHY" Then
                intHealthy = status.Value
            ElseIf status.Key = "WARNING" Then
                intWarning = status.Value
            ElseIf status.Key = "NODATA" Then
                intNodata = status.Value
            End If
        Next

        GravaSQLDataSources(intHealthy, intWarning, intNodata)

    End Sub

    Function CountStatusTypes(csvFilePath As String) As Dictionary(Of String, Integer)
        'Contagem dos data sources do cliente
        '20/02/25
        '====================================
        Dim statusTypes As String() = {"HEALTHY", "WARNING", "NO_DATA"}
        Dim counts As New Dictionary(Of String, Integer)

        ' Initialize dictionary with zero counts
        For Each status In statusTypes
            counts(status) = 0
        Next

        Try
            Using reader As New StreamReader(csvFilePath)
                Dim header As String = reader.ReadLine() ' Read and ignore header line

                While Not reader.EndOfStream
                    Dim line As String = reader.ReadLine()
                    Dim fields As String() = line.Split(","c)

                    ' Assuming "Status" is the second column (index 1)
                    If fields.Length > 1 Then
                        Dim status As String = fields(1).Trim().ToUpper()
                        If counts.ContainsKey(status) Then
                            counts(status) += 1
                        End If
                    End If
                End While
            End Using
        Catch ex As Exception
            txtMensagens.Text &= ($"Error reading file: {ex.Message}") & vbCrLf
        End Try

        Return counts

    End Function

    Private Sub GravaSQLDataSources(ByVal intHealth As Integer, ByVal intWarning As Integer, ByVal intNodata As Integer)

        If Not ExisteDataSourcesQL(_aTenantId(_intClientIndex)) Then
            GravaSQL_Inclui_CSV(intHealth, intWarning, intNodata)
            _intLinhasSQL_Inc += 1
        Else
            GravaSQL_Altera_CSV(intHealth, intWarning, intNodata)
            _intLinhasSQL_Alt += 1
        End If

    End Sub

    Private Sub GravaSQL_Inclui_CSV(ByVal intHealth As Integer, ByVal intWarning As Integer, ByVal intNodata As Integer)
        'Inclui linhas na tabela SQL t_taegis_data_sources
        '20/02/25
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
                    "values('" & _lastDayLastMonth & "', '" &
                    _aTenantId(_intClientIndex) & "', '" &
                    cboClientes.Text & "', " &
                    intHealth & ", " &
                    intWarning & ", " &
                    intNodata & ")"
                .CommandTimeout = 3600
                .ExecuteNonQuery()
            End With

            oCon.Close()

        Catch ex As SqlException
            txtMensagens.Text &= vbCrLf & "Erro SQL: " & ex.ToString
            txtMensagens.Text &= vbCrLf & "==================================================================================="

        Catch ex1 As Exception
            txtMensagens.Text &= vbCrLf & "Inclui SQL => Erro linha " & _lastDayLastMonth & " - " & _aTenantId(_intClientIndex) & ": " & ex1.ToString
            txtMensagens.Text &= vbCrLf & "==================================================================================="

        End Try

    End Sub

    Private Sub GravaSQL_Altera_CSV(ByVal intHealth As Integer, ByVal intWarning As Integer, ByVal intNodata As Integer)
        'Altera linhas na tabela SQL t_taegis_data_sources
        '20/02/25
        '===========================================
        Dim oCon As New SqlConnection(ConnectionString)
        Dim oCmd As New SqlCommand

        oCon.Open()

        Try

            With oCmd
                .Connection = oCon
                .CommandType = CommandType.Text
                .CommandText = "update t_taegis_data_sources SET " &
                    "event_qty_healthy = " & intHealth & ", " &
                    "event_qty_warning = " & intWarning & ", " &
                    "event_qty_nodata = " & intNodata & ", " &
                    "alt_data = getdate(), " &
                    "alt_user = suser_name() " &
                    "where event_tenant_id = '" & _aTenantId(_intClientIndex) & "' " &
                    "And event_date = '" & _lastDayLastMonth & "'"
                .CommandTimeout = 3600
                .ExecuteNonQuery()
            End With

            oCon.Close()

        Catch ex As SqlException
            txtMensagens.Text &= vbCrLf & "Erro SQL: " & ex.ToString
            txtMensagens.Text &= vbCrLf & "==================================================================================="

        Catch ex1 As Exception
            txtMensagens.Text &= vbCrLf & "Inclui SQL => Erro linha " & _lastDayLastMonth & " - " & _aTenantId(_intClientIndex) & ": " & ex1.ToString
            txtMensagens.Text &= vbCrLf & "==================================================================================="

        End Try

    End Sub

End Class
