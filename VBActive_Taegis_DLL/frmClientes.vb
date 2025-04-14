Imports System.Data.SqlClient
Imports System.Windows.Forms

Public Class frmClientes
    Private _blnInclui As Boolean = False
    Private _blnAltera As Boolean = False
    Private _ultSelecao As New String("")

    Private Sub frmClientes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Carga do form
        '29/03/23
        '==============
        Me.Text = ActiveTaegisDLL.VersaoAuto()
        ToolStripStatusLabel1.Text = ActiveTaegisDLL.CopyRight
        CargaClientes()

    End Sub

    Private Sub CargaClientes()
        'Carga da lista de clientes em lvwClientes
        '29/03/23
        '24/02/25
        '=========================================
        Dim oCon As New SqlConnection(ActiveTaegisDLL.ConnectionString)
        Dim oCmd As New SqlCommand
        Dim oDrd As SqlDataReader

        Try
            oCon.Open()

            With oCmd
                .Connection = oCon
                .CommandType = CommandType.Text
                .CommandText = "SELECT ISNULL(client_name, '-') client_name, 
                                ISNULL(client_abrev, '-') client_abrev, 
                                ISNULL(tenant_id, '-') tenant_id, 
                                ISNULL(client_id, '-') client_id, 
                                ISNULL(client_secret, '-') client_secret 
                                from dbo.t_taegis_tab_clients
                                ORDER BY client_name"
                oDrd = .ExecuteReader
            End With

            lvwClientes.Items.Clear()
            lvwClientes.BeginUpdate()

            With oDrd

                While .Read
                    Dim oItem As New ListViewItem(.Item("client_name").ToString)
                    oItem.SubItems.Add(.Item("client_abrev"))
                    oItem.SubItems.Add(.Item("tenant_id"))
                    oItem.SubItems.Add(.Item("client_id"))
                    oItem.SubItems.Add(.Item("client_secret"))
                    oItem.Tag = .Item("tenant_id")
                    lvwClientes.Items.AddRange(New ListViewItem() {oItem})
                End While

                .Close()
            End With

            lvwClientes.EndUpdate()

            If lvwClientes.Items.Count > 0 And _blnAltera = False And _blnInclui = False Then
                lvwClientes.Items(0).Selected = True
                lvwClientes.Focus()
            End If
            ToolStripStatusLabel2.Text = "Clientes: " & lvwClientes.Items.Count

        Catch ex As SqlException
            MessageBox.Show(ex.ToString, "Erro SQL", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Catch ex1 As Exception
            MessageBox.Show(ex1.ToString, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            oCon.Close()

        End Try

    End Sub

    Private Sub IncluiCliente()
        'Inclusão de cliente
        '29/03/23
        '24/02/25
        '=========================================
        Dim oCon As New SqlConnection(ActiveTaegisDLL.ConnectionString)
        Dim oCmd As New SqlCommand

        Try
            oCon.Open()

            With oCmd
                .Connection = oCon
                .CommandType = CommandType.Text
                .CommandText = "INSERT INTO dbo.t_taegis_tab_clients (client_name, client_abrev, tenant_id, client_id, client_secret) VALUES('" &
                                txtNomeCliente.Text & "', '" & txtAbrevCliente.Text & "', '" & txtTenantId.Text & "', '" & txtClientId.Text & "', '" & txtClientSecret.Text & "')"
                .ExecuteNonQuery()
            End With

        Catch ex As SqlException
            MessageBox.Show(ex.ToString, "Erro SQL", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Catch ex1 As Exception
            MessageBox.Show(ex1.ToString, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            oCon.Close()

        End Try

    End Sub

    Private Sub AlteraCliente()
        'Alteração de cliente
        '30/03/23
        '24/02/25
        '====================
        Dim oCon As New SqlConnection(ActiveTaegisDLL.ConnectionString)
        Dim oCmd As New SqlCommand

        Try
            oCon.Open()

            With oCmd
                .Connection = oCon
                .CommandType = CommandType.Text
                .CommandText = "UPDATE dbo.t_taegis_tab_clients " &
                               "SET client_name = '" & txtNomeCliente.Text & "', " &
                               "client_abrev = '" & txtAbrevCliente.Text & "', " &
                               "tenant_id = '" & txtTenantId.Text & "', " &
                               "client_id = '" & txtClientId.Text & "', " &
                               "client_secret = '" & txtClientSecret.Text & "' " &
                               "WHERE tenant_id = '" & txtTenantId.Text & "'"
                .ExecuteNonQuery()
            End With

        Catch ex As SqlException
            MessageBox.Show(ex.ToString, "Erro SQL", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Catch ex1 As Exception
            MessageBox.Show(ex1.ToString, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            oCon.Close()

        End Try

    End Sub
    Private Sub ExcluiCliente()
        'Exclusão do cliente selecionado
        '30/03/23
        '===============================
        Dim oCon As New SqlConnection(ActiveTaegisDLL.ConnectionString)
        Dim oCmd As New SqlCommand

        Try
            oCon.Open()

            With oCmd
                .Connection = oCon
                .CommandType = CommandType.Text
                .CommandText = "DELETE FROM dbo.t_taegis_tab_clients WHERE tenant_id = '" & txtTenantId.Text & "'"
                .ExecuteNonQuery()
            End With

        Catch ex As SqlException
            MessageBox.Show(ex.ToString, "Erro SQL", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Catch ex1 As Exception
            MessageBox.Show(ex1.ToString, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            oCon.Close()

        End Try

    End Sub

    Private Sub SelecionaItem(ByVal strTenant_Id As String)
        'Seleciona um item em lvwClientes pesquisando tenant_id
        '30/03/23
        '======================================================
        For Each oItem As ListViewItem In lvwClientes.Items
            If oItem.Tag = strTenant_Id Then
                lvwClientes.Items(oItem.Index).Selected = True
                lvwClientes.Focus()
                Exit For
            End If
        Next

    End Sub

    Private Sub btnInclui_Click(sender As Object, e As EventArgs) Handles btnInclui.Click
        'Botão inclui
        '29/03/23
        '============
        For Each oCtr As Control In Me.Controls
            If TypeOf oCtr Is TextBox Then
                oCtr.Enabled = True
                oCtr.Text = ""
            End If
        Next

        btnInclui.Enabled = False
        btnAltera.Enabled = False
        btnCancela.Enabled = True
        btnExclui.Enabled = False
        txtNomeCliente.Select()
        _blnInclui = True

    End Sub

    Private Sub txtNomeCliente_TextChanged(sender As Object, e As EventArgs) Handles txtNomeCliente.TextChanged
        'Alteração de campo
        '29/03/23
        '==================
        PreparParaGravar()

    End Sub

    Private Sub txtAbrevCliente_TextChanged(sender As Object, e As EventArgs) Handles txtAbrevCliente.TextChanged
        'Alteração de campo
        '24/02/25
        '==================
        PreparParaGravar()

    End Sub

    Private Sub txtTenantId_TextChanged(sender As Object, e As EventArgs) Handles txtTenantId.TextChanged
        'Alteração de campo
        '29/03/23
        '==================
        PreparParaGravar()

    End Sub

    Private Sub txtClientId_TextChanged(sender As Object, e As EventArgs) Handles txtClientId.TextChanged
        'Alteração de campo
        '29/03/23
        '==================
        PreparParaGravar()

    End Sub

    Private Sub txtClientSecret_TextChanged(sender As Object, e As EventArgs) Handles txtClientSecret.TextChanged
        'Alteração de campo
        '29/03/23
        '==================
        PreparParaGravar()

    End Sub

    Private Sub PreparParaGravar()
        'Prepara o botão grava
        '29/03/23
        '24/02/25
        '=====================
        btnGrava.Enabled = (txtNomeCliente.Text.Trim.Length > 0 And txtAbrevCliente.Text.Trim.Length > 0 And txtTenantId.Text.Trim.Length > 0 And txtClientId.Text.Trim.Length > 0 And txtClientSecret.Text.Trim.Length > 0)

    End Sub

    Private Sub btnCancela_Click(sender As Object, e As EventArgs) Handles btnCancela.Click
        'Botão Cancela
        '29/03/23
        '=============

        For Each oCtr As Control In Me.Controls
            If TypeOf oCtr Is TextBox Then
                oCtr.Enabled = False
            End If
        Next

        btnInclui.Enabled = True
        btnAltera.Enabled = True
        btnCancela.Enabled = False
        btnExclui.Enabled = True
        SelecionaItem(_ultSelecao)
        _blnInclui = False
        _blnAltera = False

    End Sub

    Private Sub btnGrava_Click(sender As Object, e As EventArgs) Handles btnGrava.Click
        'Botão Grava
        '29/03/23
        '===========
        If _blnInclui Then
            IncluiCliente()
        ElseIf _blnAltera Then
            AlteraCliente()
        End If

        For Each oCtr As Control In Me.Controls
            If TypeOf oCtr Is TextBox Then
                oCtr.Enabled = False
            End If
        Next

        btnInclui.Enabled = True
        btnCancela.Enabled = False
        btnGrava.Enabled = False
        CargaClientes()
        SelecionaItem(txtTenantId.Text)
        _blnAltera = False
        _blnInclui = False

    End Sub

    Private Sub lvwClientes_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvwClientes.SelectedIndexChanged
        'Linha selecionada
        '29/03/23
        '24/02/25
        '=================
        If lvwClientes.SelectedItems.Count > 0 Then

            With lvwClientes.SelectedItems(0)
                txtNomeCliente.Text = .SubItems(0).Text
                txtAbrevCliente.Text = .SubItems(1).Text
                txtTenantId.Text = .SubItems(2).Text
                txtClientId.Text = .SubItems(3).Text
                txtClientSecret.Text = .SubItems(4).Text
            End With

        End If

        btnGrava.Enabled = False
        btnAltera.Enabled = True
        btnCancela.Enabled = False
        btnExclui.Enabled = True
        _ultSelecao = txtTenantId.Text

    End Sub

    Private Sub btnFecha_Click(sender As Object, e As EventArgs) Handles btnFecha.Click
        'Botão Fecha
        '29/03/23
        '===========
        Me.Close()

    End Sub

    Private Sub btnAltera_Click(sender As Object, e As EventArgs) Handles btnAltera.Click
        'Botão altera
        '30/03/23
        '============
        For Each oCtr As Control In Me.Controls
            If TypeOf oCtr Is TextBox Then
                oCtr.Enabled = True
            End If
        Next

        btnInclui.Enabled = False
        btnAltera.Enabled = False
        btnCancela.Enabled = True
        btnExclui.Enabled = False
        txtNomeCliente.Select()
        _blnAltera = True

    End Sub

    Private Sub btnExclui_Click(sender As Object, e As EventArgs) Handles btnExclui.Click
        'Botão exclui
        '30/03/23
        '============
        Dim strMsg As New String("Confirma a exclusão do cliente " & txtNomeCliente.Text & "?")
        Dim strTit As New String("Exclusão de Cliente")

        If MessageBox.Show(strMsg, strTit, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            If MessageBox.Show("Tem certeza?", strTit, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                ExcluiCliente()

                For Each oCtr As Control In Me.Controls
                    If TypeOf oCtr Is TextBox Then
                        oCtr.Enabled = False
                    End If
                Next

                CargaClientes()
            Else
                SelecionaItem(txtTenantId.Text)
            End If
        Else
            SelecionaItem(txtTenantId.Text)
        End If

    End Sub

End Class