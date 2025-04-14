Imports System.Data.SqlClient
Imports System.Windows.Forms

Public Class frmConfig
    'Sistema Active - VS2022
    'Módulo: Configuração do sistema Active
    'Heron Jr
    '09/02/23-14/03/23-17/08/23-15/09/23-20/09/23
    '21/03/24-23/12/24
    '======================================
    Private _strPastaPython As New String("")

    Private _strPastaProjAlerts As New String("")
    Private _strPastaProjAssets As New String("")
    Private _strPastaProjInvestigations As New String("")
    Private _strPastaProjCollectors As New String("")
    Private _strPastaProjEvents As New String("")
    Private _strPastaProjEventsDiario As New String("")
    Private _strPastaProjDataSources As New String("")

    Private _strPastaProjAlertsAuto As New String("")
    Private _strPastaProjAssetsAuto As New String("")
    Private _strPastaProjInvestigationsAuto As New String("")
    Private _strPastaProjCollectorsAuto As New String("")
    Private _strPastaProjEventsAuto As New String("")
    Private _strPastaProjEventsDiarioAuto As New String("")
    Private _strPastaProjDataSourcesAuto As New String("")

    Private fd As New FolderBrowserDialog
    Private _strSaida As New String("")

    Private Sub frmConfig_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Carga do form
        '09/02/23-27/02/23-30/08/23-15/09/23-20/09/23
        '===========================================================================
        Dim strPastas As New String(ActiveTaegisDLL.LeConfig())

        Me.Text = ActiveTaegisDLL.VersaoAuto
        ToolStripStatusLabel1.Text = ActiveTaegisDLL.CopyRight

        _strPastaPython = strPastas.Split(";")(0)

        _strPastaProjAlerts = strPastas.Split(";")(1)
        _strPastaProjAssets = strPastas.Split(";")(2)
        _strPastaProjInvestigations = strPastas.Split(";")(3)
        _strPastaProjCollectors = strPastas.Split(";")(4)
        _strPastaProjEvents = strPastas.Split(";")(5)
        _strPastaProjEventsDiario = strPastas.Split(";")(13)
        _strPastaProjDataSources = strPastas.Split(";")(11)

        _strPastaProjAlertsAuto = strPastas.Split(";")(6)
        _strPastaProjAssetsAuto = strPastas.Split(";")(7)
        _strPastaProjInvestigationsAuto = strPastas.Split(";")(8)
        _strPastaProjCollectorsAuto = strPastas.Split(";")(9)
        _strPastaProjEventsAuto = strPastas.Split(";")(10)
        _strPastaProjEventsDiarioAuto = strPastas.Split(";")(14)
        _strPastaProjDataSourcesAuto = strPastas.Split(";")(12)

        txtPastaPython.Text = _strPastaPython

        txtPastaProjAlerts.Text = _strPastaProjAlerts
        txtPastaProjAssets.Text = _strPastaProjAssets
        txtPastaProjInvestigations.Text = _strPastaProjInvestigations
        txtPastaProjCollectors.Text = _strPastaProjCollectors
        txtPastaProjEvents.Text = _strPastaProjEvents
        txtPastaProjEventsDiario.Text = _strPastaProjEventsDiario
        txtPastaProjDataSources.Text = _strPastaProjDataSources

        txtPastaProjAlertsAuto.Text = _strPastaProjAlertsAuto
        txtPastaProjAssetsAuto.Text = _strPastaProjAssetsAuto
        txtPastaProjInvestigationsAuto.Text = _strPastaProjInvestigationsAuto
        txtPastaProjCollectorsAuto.Text = _strPastaProjCollectorsAuto
        txtPastaProjEventsAuto.Text = _strPastaProjEventsAuto
        txtPastaProjEventsDiarioAuto.Text = _strPastaProjEventsDiarioAuto
        txtPastaProjDataSourcesAuto.Text = _strPastaProjDataSourcesAuto

        For Each cControl As Control In Me.Controls
            If TypeOf (cControl) Is TextBox Then
                cControl.Enabled = False
            End If
        Next

        For Each cControl As Control In Me.Controls
            If TypeOf (cControl) Is Button Then
                cControl.Enabled = False
            End If
        Next

        btnEdita.Enabled = True
        btnFecha.Enabled = True

    End Sub

    Private Sub btnEdita_Click(sender As Object, e As EventArgs) Handles btnEdita.Click
        'Botão edita
        '09/02/23-27/02/23
        '===========
        For Each cControl As Control In Me.Controls
            If TypeOf (cControl) Is TextBox Then
                cControl.Enabled = True
            End If
        Next

        For Each cControl As Control In Me.Controls
            If TypeOf (cControl) Is Button Then
                cControl.Enabled = True
            End If
        Next

        btnEdita.Enabled = False

    End Sub

    Private Sub txtPastaPython_TextChanged(sender As Object, e As EventArgs) Handles txtPastaPython.TextChanged
        'Alteração de texto
        '09/02/23
        '==================
        btnCancela.Enabled = True
        btnGrava.Enabled = True

    End Sub

    Private Sub btnCancela_Click(sender As Object, e As EventArgs) Handles btnCancela.Click
        'Botão cancela
        '09/02/23-27/02/23
        '=============
        For Each cControl As Control In Me.Controls
            If TypeOf (cControl) Is TextBox Then
                cControl.Enabled = False
            End If
        Next

        For Each cControl As Control In Me.Controls
            If TypeOf (cControl) Is Button Then
                cControl.Enabled = False
            End If
        Next

        btnEdita.Enabled = True
        btnFecha.Enabled = True

    End Sub

    Private Sub btnGrava_Click(sender As Object, e As EventArgs) Handles btnGrava.Click
        'Botão grava
        '09/02/23-27/02/23
        '=============
        GravaConfig()

        For Each cControl As Control In Me.Controls
            If TypeOf (cControl) Is TextBox Then
                cControl.Enabled = False
            End If
        Next

        For Each cControl As Control In Me.Controls
            If TypeOf (cControl) Is Button Then
                cControl.Enabled = False
            End If
        Next

        btnEdita.Enabled = True
        btnFecha.Enabled = True

    End Sub

    Private Sub GravaConfig()
        'Grava alterações na tabela SQL t_active_config
        '09/02/23-27/02/23-14/03/23-17/08/23-15/09/23-20/09/23
        '23/12/24
        '=====================================================
        Dim oCon As New SqlConnection(ActiveTaegisDLL.ConnectionString)
        Dim oCmd As New SqlCommand

        oCon.Open()
        'MessageBox.Show("_strPastaProjEventsDiario: " & _strPastaProjEventsDiario)

        With oCmd
            .Connection = oCon
            .CommandType = CommandType.StoredProcedure
            .CommandText = "p_grava_active_config_auto"
            .Parameters.Add(New SqlParameter("@pasta_python", _strPastaPython))
            .Parameters.Add(New SqlParameter("@pasta_proj_alerts", _strPastaProjAlerts))
            .Parameters.Add(New SqlParameter("@pasta_proj_assets", _strPastaProjAssets))
            .Parameters.Add(New SqlParameter("@pasta_proj_investigations", _strPastaProjInvestigations))
            .Parameters.Add(New SqlParameter("@pasta_proj_collectors", _strPastaProjCollectors))
            .Parameters.Add(New SqlParameter("@pasta_proj_events", _strPastaProjEvents))
            .Parameters.Add(New SqlParameter("@pasta_proj_events_diario", _strPastaProjEventsDiario))
            .Parameters.Add(New SqlParameter("@pasta_proj_alerts_Auto", _strPastaProjAlertsAuto))
            .Parameters.Add(New SqlParameter("@pasta_proj_assets_Auto", _strPastaProjAssetsAuto))
            .Parameters.Add(New SqlParameter("@pasta_proj_investigations_Auto", _strPastaProjInvestigationsAuto))
            .Parameters.Add(New SqlParameter("@pasta_proj_collectors_Auto", _strPastaProjCollectorsAuto))
            .Parameters.Add(New SqlParameter("@pasta_proj_events_Auto", _strPastaProjEventsAuto))
            .Parameters.Add(New SqlParameter("@pasta_proj_events_diario_Auto", _strPastaProjEventsDiarioAuto))
            .Parameters.Add(New SqlParameter("@pasta_proj_datasources", _strPastaProjDataSources))
            .Parameters.Add(New SqlParameter("@pasta_proj_datasources_Auto", _strPastaProjDataSourcesAuto))
            .CommandTimeout = 3600
            .ExecuteNonQuery()
        End With

        oCon.Close()

    End Sub

    Private Sub btnFecha_Click(sender As Object, e As EventArgs) Handles btnFecha.Click
        'Botão fecha
        '09/02/23
        '===========
        Me.Close()

    End Sub

    Private Sub btnPastaPython_Click(sender As Object, e As EventArgs) Handles btnPastaPython.Click
        'Botão de busca do caminho da pasta_python
        '09/02/23
        '=========================================
        If _strPastaPython <> "" Then
            fd.SelectedPath = StrReverse(Split(StrReverse(_strPastaPython), "\", 2)(1)) & "\"
            SendKeys.Send("{TAB}{TAB}{RIGHT}")  'Workaround para tornar o controle de busca com EnsureVisible na caixa de pastas
        End If
        If fd.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtPastaPython.Text = fd.SelectedPath & "\python.exe"
            _strPastaPython = fd.SelectedPath & "\python.exe"
        End If

    End Sub

    Private Sub btnPastaProjAlerts_Click(sender As Object, e As EventArgs) Handles btnPastaProjAlerts.Click
        'Botão de busca da pasta do projeto Alerts (Debug/Release)
        '09/02/23-27/02/23-15/03/23-17/08/23-15/09/23
        '=========================================================
        If _strPastaProjAlerts <> "" Then
            fd.SelectedPath = StrReverse(Split(StrReverse(_strPastaProjAlerts), "\", 2)(1)) & "\"
            SendKeys.Send("{TAB}{TAB}{RIGHT}")  'Workaround para tornar o controle de busca com EnsureVisible na caixa de pastas
        End If
        If fd.ShowDialog() = Windows.Forms.DialogResult.OK And
            System.IO.File.Exists(fd.SelectedPath & "\DWConnections.config") Then
            txtPastaProjAlerts.Text = fd.SelectedPath
            _strPastaProjAlerts = fd.SelectedPath
        End If

    End Sub

    Private Sub btnPastaProjAssets_Click(sender As Object, e As EventArgs) Handles btnPastaProjAssets.Click
        'Botão de busca da pasta do projeto assets (Debug/Release)
        '27/02/23-15/03/23-17/08/23-15/09/23
        '=========================================================
        If _strPastaProjAssets <> "" Then
            fd.SelectedPath = StrReverse(Split(StrReverse(_strPastaProjAssets), "\", 2)(1)) & "\"
            SendKeys.Send("{TAB}{TAB}{RIGHT}")  'Workaround para tornar o controle de busca com EnsureVisible na caixa de pastas
        End If
        If fd.ShowDialog() = Windows.Forms.DialogResult.OK And
            System.IO.File.Exists(fd.SelectedPath & "\DWConnections.config") Then
            txtPastaProjAssets.Text = fd.SelectedPath
            _strPastaProjAssets = fd.SelectedPath
        End If

    End Sub

    Private Sub btnPastaProjInvestigations_Click(sender As Object, e As EventArgs) Handles btnPastaProjInvestigations.Click
        'Botão de busca da pasta do projeto investigations (Debug/Release)
        '27/02/23-15/03/23-17/08/23-15/09/23
        '================================================================
        If _strPastaProjInvestigations <> "" Then
            fd.SelectedPath = StrReverse(Split(StrReverse(_strPastaProjInvestigations), "\", 2)(1)) & "\"
            SendKeys.Send("{TAB}{TAB}{RIGHT}")  'Workaround para tornar o controle de busca com EnsureVisible na caixa de pastas
        End If
        If fd.ShowDialog() = Windows.Forms.DialogResult.OK And
            System.IO.File.Exists(fd.SelectedPath & "\DWConnections.config") Then
            txtPastaProjInvestigations.Text = fd.SelectedPath
            _strPastaProjInvestigations = fd.SelectedPath
        End If

    End Sub

    Private Sub btnPastaProjCollectors_Click(sender As Object, e As EventArgs) Handles btnPastaProjCollectors.Click
        'Botão de busca da pasta do projeto collectors (Debug/Release)
        '27/02/23-15/03/23-17/08/23-15/09/23
        '=============================================================
        If _strPastaProjCollectors <> "" Then
            fd.SelectedPath = StrReverse(Split(StrReverse(_strPastaProjCollectors), "\", 2)(1)) & "\"
            SendKeys.Send("{TAB}{TAB}{RIGHT}")  'Workaround para tornar o controle de busca com EnsureVisible na caixa de pastas
        End If
        If fd.ShowDialog() = Windows.Forms.DialogResult.OK And
            System.IO.File.Exists(fd.SelectedPath & "\DWConnections.config") Then
            txtPastaProjCollectors.Text = fd.SelectedPath
            _strPastaProjCollectors = fd.SelectedPath
        End If

    End Sub

    Private Sub btnPastaProjAlertsAuto_Click(sender As Object, e As EventArgs) Handles btnPastaProjAlertsAuto.Click
        'Botão de busca da pasta do projeto AlertsAuto (Debug/Release)
        '14/03/23-15/03/23-17/08/23-15/09/23
        '=============================================================
        If _strPastaProjAlertsAuto <> "" Then
            fd.SelectedPath = StrReverse(Split(StrReverse(_strPastaProjAlertsAuto), "\", 2)(1)) & "\"
            SendKeys.Send("{TAB}{TAB}{RIGHT}")  'Workaround para tornar o controle de busca com EnsureVisible na caixa de pastas
        End If
        If fd.ShowDialog() = Windows.Forms.DialogResult.OK And
            System.IO.File.Exists(fd.SelectedPath & "\DWConnections.config") Then
            txtPastaProjAlertsAuto.Text = fd.SelectedPath
            _strPastaProjAlertsAuto = fd.SelectedPath
        End If

    End Sub

    Private Sub btnPastaProjAssetsAuto_Click(sender As Object, e As EventArgs) Handles btnPastaProjAssetsAuto.Click
        'Botão de busca da pasta do projeto assets (Debug/Release)
        '14/02/23-16/03/23-17/08/23-15/09/23
        '=========================================================
        If _strPastaProjAssetsAuto <> "" Then
            fd.SelectedPath = StrReverse(Split(StrReverse(_strPastaProjAssetsAuto), "\", 2)(1)) & "\"
            SendKeys.Send("{TAB}{TAB}{RIGHT}")  'Workaround para tornar o controle de busca com EnsureVisible na caixa de pastas
        End If
        If fd.ShowDialog() = Windows.Forms.DialogResult.OK And
            System.IO.File.Exists(fd.SelectedPath & "\DWConnections.config") Then
            txtPastaProjAssetsAuto.Text = fd.SelectedPath
            _strPastaProjAssetsAuto = fd.SelectedPath
        End If

    End Sub

    Private Sub btnPastaProjInvestigationsAuto_Click(sender As Object, e As EventArgs) Handles btnPastaProjInvestigationsAuto.Click
        'Botão de busca da pasta do projeto investigations (Debug/Release)
        '14/02/23-17/08/23-15/09/23
        '================================================================
        If _strPastaProjInvestigationsAuto <> "" Then
            fd.SelectedPath = StrReverse(Split(StrReverse(_strPastaProjInvestigationsAuto), "\", 2)(1)) & "\"
            SendKeys.Send("{TAB}{TAB}{RIGHT}")  'Workaround para tornar o controle de busca com EnsureVisible na caixa de pastas
        End If
        If fd.ShowDialog() = Windows.Forms.DialogResult.OK And
            System.IO.File.Exists(fd.SelectedPath & "\DWConnections.config") Then
            txtPastaProjInvestigationsAuto.Text = fd.SelectedPath
            _strPastaProjInvestigationsAuto = fd.SelectedPath
        End If

    End Sub

    Private Sub btnPastaProjCollectorsAuto_Click(sender As Object, e As EventArgs) Handles btnPastaProjCollectorsAuto.Click
        'Botão de busca da pasta do projeto collectors (Debug/Release)
        '14/02/23-17/08/23-15/09/23
        '=============================================================
        If _strPastaProjCollectorsAuto <> "" Then
            fd.SelectedPath = StrReverse(Split(StrReverse(_strPastaProjCollectorsAuto), "\", 2)(1)) & "\"
            SendKeys.Send("{TAB}{TAB}{RIGHT}")  'Workaround para tornar o controle de busca com EnsureVisible na caixa de pastas
        End If
        If fd.ShowDialog() = Windows.Forms.DialogResult.OK And
            System.IO.File.Exists(fd.SelectedPath & "\DWConnections.config") Then
            txtPastaProjCollectorsAuto.Text = fd.SelectedPath
            _strPastaProjCollectorsAuto = fd.SelectedPath
        End If

    End Sub

    Private Sub btnPastaProjEvents_Click(sender As Object, e As EventArgs) Handles btnPastaProjEvents.Click
        'Botão de busca da pasta do projeto events (Debug/Release)
        '17/08/23-15/09/23
        '=============================================================
        If _strPastaProjEvents <> "" Then
            fd.SelectedPath = StrReverse(Split(StrReverse(_strPastaProjEvents), "\", 2)(1)) & "\"
            SendKeys.Send("{TAB}{TAB}{RIGHT}")  'Workaround para tornar o controle de busca com EnsureVisible na caixa de pastas
        End If
        If fd.ShowDialog() = Windows.Forms.DialogResult.OK And
            System.IO.File.Exists(fd.SelectedPath & "\DWConnections.config") Then
            txtPastaProjEvents.Text = fd.SelectedPath
            _strPastaProjEvents = fd.SelectedPath
        End If

    End Sub

    Private Sub btnPastaProjEventsDiario_Click(sender As Object, e As EventArgs) Handles btnPastaProjEventsDiario.Click
        'Botão de busca da pasta do projeto events diario (Debug/Release)
        '23/12/24
        '================================================================
        'MessageBox.Show("_strPastaProjEventsDiario: " & _strPastaProjEventsDiario.Length)
        If _strPastaProjEventsDiario <> "" Then
            fd.SelectedPath = StrReverse(Split(StrReverse(_strPastaProjEventsDiario), "\", 2)(1)) & "\"
            SendKeys.Send("{TAB}{TAB}{RIGHT}")  'Workaround para tornar o controle de busca com EnsureVisible na caixa de pastas
        End If
        If fd.ShowDialog() = Windows.Forms.DialogResult.OK And
            System.IO.File.Exists(fd.SelectedPath & "\DWConnections.config") Then
            txtPastaProjEventsDiario.Text = fd.SelectedPath
            _strPastaProjEventsDiario = fd.SelectedPath
        End If

    End Sub
    Private Sub btnPastaProjEventsAuto_Click(sender As Object, e As EventArgs) Handles btnPastaProjEventsAuto.Click
        'Botão de busca da pasta do projeto events (Debug/Release)
        '17/08/23-15/09/23
        '=============================================================
        If _strPastaProjEventsAuto <> "" Then
            fd.SelectedPath = StrReverse(Split(StrReverse(_strPastaProjEventsAuto), "\", 2)(1)) & "\"
            SendKeys.Send("{TAB}{TAB}{RIGHT}")  'Workaround para tornar o controle de busca com EnsureVisible na caixa de pastas
        End If
        If fd.ShowDialog() = Windows.Forms.DialogResult.OK And
            System.IO.File.Exists(fd.SelectedPath & "\DWConnections.config") Then
            txtPastaProjEventsAuto.Text = fd.SelectedPath
            _strPastaProjEventsAuto = fd.SelectedPath
        End If

    End Sub

    Private Sub btnPastaProjEventsDiarioAuto_Click(sender As Object, e As EventArgs) Handles btnPastaProjEventsDiarioAuto.Click
        'Botão de busca da pasta do projeto events diario (Debug/Release)
        '23/12/24
        '================================================================
        If _strPastaProjEventsDiarioAuto <> "" Then
            fd.SelectedPath = StrReverse(Split(StrReverse(_strPastaProjEventsDiarioAuto), "\", 2)(1)) & "\"
            SendKeys.Send("{TAB}{TAB}{RIGHT}")  'Workaround para tornar o controle de busca com EnsureVisible na caixa de pastas
        End If
        If fd.ShowDialog() = Windows.Forms.DialogResult.OK And
            System.IO.File.Exists(fd.SelectedPath & "\DWConnections.config") Then
            txtPastaProjEventsDiarioAuto.Text = fd.SelectedPath
            _strPastaProjEventsDiarioAuto = fd.SelectedPath
        End If

    End Sub

    Private Sub btnPastaProjDataSources_Click(sender As Object, e As EventArgs) Handles btnPastaProjDataSources.Click
        'Botão de busca da pasta do projeto events (Debug/Release)
        '20/09/23-17/10/13
        '=============================================================
        If _strPastaProjDataSources <> "" Then
            fd.SelectedPath = StrReverse(Split(StrReverse(_strPastaProjDataSources), "\", 2)(1)) & "\"
            SendKeys.Send("{TAB}{TAB}{RIGHT}")  'Workaround para tornar o controle de busca com EnsureVisible na caixa de pastas
        End If
        If fd.ShowDialog() = Windows.Forms.DialogResult.OK And
            System.IO.File.Exists(fd.SelectedPath & "\DWConnections.config") Then
            txtPastaProjDataSources.Text = fd.SelectedPath
            _strPastaProjDataSources = fd.SelectedPath
        End If

    End Sub

    Private Sub btnPastaProjDataSourcesAuto_Click(sender As Object, e As EventArgs) Handles btnPastaProjDataSourcesAuto.Click
        'Botão de busca da pasta do projeto events (Debug/Release)
        '17/08/23-15/09/23-20/09/23-17/10/23
        '=============================================================
        If _strPastaProjDataSourcesAuto <> "" Then
            fd.SelectedPath = StrReverse(Split(StrReverse(_strPastaProjDataSourcesAuto), "\", 2)(1)) & "\"
            SendKeys.Send("{TAB}{TAB}{RIGHT}")  'Workaround para tornar o controle de busca com EnsureVisible na caixa de pastas
        End If
        If fd.ShowDialog() = Windows.Forms.DialogResult.OK And
            System.IO.File.Exists(fd.SelectedPath & "\DWConnections.config") Then
            txtPastaProjDataSourcesAuto.Text = fd.SelectedPath
            _strPastaProjDataSourcesAuto = fd.SelectedPath
        End If

    End Sub

End Class