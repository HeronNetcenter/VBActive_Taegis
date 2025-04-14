Imports System.Data.SqlClient
Imports VBActive_Taegis_DLL.ActiveTaegisDLL

Public Class frmInvestigationsList
    Private Sub frmInvestigationsList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Carga do form
        '10/10/23-17/10/23
        '=============
        Me.Text = "Investigations List - Cliente: " & Form1.cboClientes.Text & " - " & Proj.VersaoAuto1()
        ToolStripStatusLabel1.Text = CopyRight()
        CargaInvestigationsList()

    End Sub

    Private Sub CargaInvestigationsList()
        'Carga de lvwInvestigationsList
        '10/10/23-17/10/23
        '==============================
        Dim oCon As New SqlConnection(ConnectionString)
        Dim oCmd As New SqlCommand
        Dim oDrd As SqlDataReader
        Dim strMessage As New String("")

        oCon.Open()

        Try

            With oCmd
                .Connection = oCon
                .CommandType = CommandType.Text
                .CommandText = "SELECT id, created_at, description, priority, status, service_desk_id, shortId, alt_data FROM t_taegis_investigations " &
                                       "WHERE inv_client = '" & Form1.cboClientes.Text & "'"

                .CommandTimeout = 3600
                oDrd = .ExecuteReader
            End With

            With oDrd

                While .Read
                    Dim lstItem = New ListViewItem
                    lstItem = lvwInvestigationsList.Items.Add(.Item("id"))
                    lstItem.SubItems.Add(.Item("shortId"))
                    lstItem.SubItems.Add(.Item("created_at"))
                    lstItem.SubItems.Add(.Item("description"))
                    lstItem.SubItems.Add(.Item("priority"))
                    lstItem.SubItems.Add(.Item("status"))
                    lstItem.SubItems.Add(.Item("service_desk_id"))
                    lstItem.SubItems.Add(.Item("alt_data"))
                End While

                .Close()
            End With

            oCon.Close()

        Catch ex As SqlException
            MessageBox.Show(ex.ToString, "Erro SQL: ", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Catch ex1 As Exception
            MessageBox.Show("Inclui SQL => " & ex1.ToString, "Erro:", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

    End Sub

    Private Sub lvwInvestigationsList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvwInvestigationsList.SelectedIndexChanged
        'Seleciona item em lvwInvestigationsList
        '10/10/23
        '=======================================
        If lvwInvestigationsList.SelectedItems.Count > 0 Then
            btnTransfereId.Enabled = True
        End If

    End Sub

    Private Sub btnFecha_Click(sender As Object, e As EventArgs) Handles btnFecha.Click
        'Botão Fecha
        '10/10/23
        '===========
        Me.Close()

    End Sub

    Private Sub btnTransfereId_Click(sender As Object, e As EventArgs) Handles btnTransfereId.Click
        'Transfere o id da investigação para o respectivo campo no form principal
        '10/10/23
        '========================================================================
        Form1.txtInvestigationId.Text = lvwInvestigationsList.SelectedItems(0).SubItems(0).Text
        Me.Close()

    End Sub

End Class