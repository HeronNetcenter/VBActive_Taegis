Imports System.Data.SqlClient

Public Class Form1
    'Contagem dos chamados por cliente/período - VS2022
    '01/02/24-07/03/24
    '==================================================

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Carga do form
        '01/02/24-07/03/24
        '=============
        ToolStripStatusLabel1.Text = CopyRight(2024)
        Me.Text = VersaoAuto()
        cboPeriodo.SelectedIndex = cboPeriodo.Items.Count - 1
        CargaComboClientes()

    End Sub

    Public Sub CargaComboClientes()
        'Carga do combo de clientes de acordo com o período selecionado
        '01/02/24-08/02/24
        '==============================================================
        Dim strSQL As String = "USE DWNetcenter
                                SELECT distinct i.organizacao, c.cnpj, c.desligado
                                FROM [DWNetcenter].[dbo].[t_sd_solicit_incid_it2m] i
                                inner join t_abrev_clientes c on i.organizacao = c.nome
                                where data_abertura >= DATEADD(month, 1, DATEADD(year, datediff(year, 0, getdate()) - 1, 0))
                                order by organizacao"
        Dim oCon As New SqlConnection(ConnectionString)
        Dim oCmd As New SqlCommand
        Dim oDrd As SqlDataReader = Nothing

        oCon.Open()

        With oCmd
            .Connection = oCon
            .CommandType = CommandType.Text
            .CommandText = strSQL
            .CommandTimeout = 3600
            oDrd = .ExecuteReader
        End With

        With oDrd

            While .Read
                cboCliente.Items.Add(.Item("organizacao").ToString &
                                           " [" & mascaraCNPJ_CPF(.Item("cnpj").ToString) & "]" &
                                           If(.Item("desligado"), " *** DESLIGADO ***", ""))
            End While

            .Close()
        End With

        oCon.Close()

    End Sub

    Private Sub cboCliente_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboCliente.SelectedIndexChanged
        'Seleção de cliente no combobox
        '02/02/24
        '==============================
        Dim strSel As New String(cboCliente.SelectedItem.ToString)
        Dim strCliente As New String(strSel.Substring(0, strSel.IndexOf("[")))

        Dim strSQL As String = "USE DWNetcenter
                                SELECT [status], count(*) qtde
                                FROM [DWNetcenter].[dbo].[t_sd_solicit_incid_it2m]
                                where organizacao = '" & strCliente & "'" &
                                "and data_abertura >= DATEADD(month, 1, DATEADD(year, datediff(year, 0, getdate()) - 1, 0))
                                group by status"
        Dim oCon As New SqlConnection(Proj.ConnectionString)
        Dim oCmd As New SqlCommand
        Dim oDrd As SqlDataReader = Nothing

        oCon.Open()

        With oCmd
            .Connection = oCon
            .CommandType = CommandType.Text
            .CommandText = strSQL
            .CommandTimeout = 3600
            oDrd = .ExecuteReader
        End With

        lblTotalEmAberto.Text = " - status ""em Aberto"": "
        lblTotalEmAndamento.Text = " - status ""em Andamento"": "
        lblTotalPausado.Text = " - status ""Pausado"": "
        lblTotalTerminado.Text = " - status ""Fechada"": "
        lblTotalChamados.Text = "Total de Chamados: "

        With oDrd
            Dim intTotal As Integer = 0

            While .Read
                Dim strStatus As New String(.Item("status").ToString)

                If strStatus = "em Aberto" Then
                    lblTotalEmAberto.Text = " - status """ & strStatus & """: " & .Item("qtde")
                    intTotal += .Item("qtde")
                ElseIf strStatus = "em Andamento" Then
                    lblTotalEmAndamento.Text = " - status """ & strStatus & """: " & .Item("qtde")
                    intTotal += .Item("qtde")
                ElseIf strStatus = "Pausado" Then
                    lblTotalPausado.Text = " - status """ & strStatus & """: " & .Item("qtde")
                    intTotal += .Item("qtde")
                ElseIf strStatus = "Fechada" Then
                    lblTotalTerminado.Text = " - status """ & strStatus & """: " & .Item("qtde")
                    intTotal += .Item("qtde")
                End If

            End While

            lblTotalChamados.Text = "Total de Chamados: " & intTotal
            .Close()
        End With

        oCon.Close()

    End Sub

    Private Sub btnFecha_Click(sender As Object, e As EventArgs) Handles btnFecha.Click
        'Botão fecha
        '02/02/24
        '===========
        Me.Close()

    End Sub

    Private Sub FechaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FechaToolStripMenuItem.Click
        'Menu Arquivo / Fecha
        '02/02/24
        '=====================
        btnFecha.PerformClick()

    End Sub

End Class
