Imports System.Data.SqlClient
Imports VBActive_Taegis_DLL.ActiveTaegisDLL

Public Class frmMutations
    'Form para lista de Mutations
    '22/11/23
    '============================
    Public _intCliente As Integer = 0

    Private Sub frmMutations_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Carga do form
        '22/11/23
        '=============
        Dim f As New Form1

        'Me.Text = "Mutations List - Cliente: " & cboClientes.Text & " - " & Proj.VersaoAuto1()
        ToolStripStatusLabel1.Text = CopyRight()
        cboDias.SelectedIndex = 2
        cboClientes.SelectedIndex = _intCliente
        ListaMutations()

    End Sub

    Private Sub ListaMutations()
        'Lista de Mutations nas investigações
        '22/11/23
        '====================================
        Dim oCon As New SqlConnection(ConnectionString)
        Dim oCmd As New SqlCommand
        Dim oDrd As SqlDataReader
        Dim strMessage As New String("")

        oCon.Open()

        Try

            With oCmd
                .Connection = oCon
                .CommandType = CommandType.Text
                .CommandText = "SELECT [id_log]
                                      ,[client_name]
                                      ,[data_log]
                                      ,[texto_log]
                                  FROM [Active].[dbo].[t_taegis_log]
                                  WHERE [tipo_log] = 'Investigation Mutation' " &
                                      If(cboClientes.Text <> "- TODOS", "AND [client_name] = '" & cboClientes.Text & "' ", " ") &
                                  "ORDER BY id_log DESC"
                .CommandTimeout = 3600
                oDrd = .ExecuteReader
            End With

            With oDrd
                Dim intDias As Integer = cboDias.Text
                Dim strDataIntervalo As New String(DateAdd(DateInterval.Day, intDias * (-1), Now.Date).ToString("yyyy-MM-dd"))
                lvwMutationsList.Items.Clear()

                While .Read
                    Dim strDescricao As New String(.Item("texto_log").ToString)
                    Dim strInvestigation As New String(strDescricao.Substring(strDescricao.IndexOf(": INV") + 2, 8))
                    Dim strChamado As New String(strDescricao.Substring(strDescricao.IndexOf("ado: ") + 4, 8))
                    Dim datLog As Date = .Item("data_log")
                    Dim lstItem = New ListViewItem

                    lstItem = lvwMutationsList.Items.Add(.Item("id_log"))
                    lstItem.SubItems.Add(.Item("client_name"))
                    lstItem.SubItems.Add(.Item("data_log"))
                    lstItem.SubItems.Add(strInvestigation)
                    lstItem.SubItems.Add(strChamado)
                    lstItem.ForeColor = If(strDataIntervalo <= datLog.ToString("yyyy-MM-dd"),
                                            Color.Red, Color.Blue)

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

    Private Sub cboClientes_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboClientes.SelectedIndexChanged
        'Alteração de item no combo
        '22/11/23
        '==========================
        Me.Text = "Mutations List - Cliente: " & cboClientes.Text & " - " & Proj.VersaoAuto1()
        ListaMutations()

    End Sub

    Private Sub btnFecha_Click(sender As Object, e As EventArgs) Handles btnFecha.Click
        'Botão fecha
        '22/11/23'
        '===========
        Me.Close()

    End Sub

    Private Sub cboDias_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboDias.SelectedIndexChanged
        'Alteração de item no combo
        '23/11/23
        '==========================
        Me.Text = "Mutations List - Cliente: " & cboClientes.Text & " - " & Proj.VersaoAuto1()
        ListaMutations()

    End Sub

End Class