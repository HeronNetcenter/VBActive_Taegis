Imports System.Data.SqlClient
Imports VBActive_Taegis_DLL.ActiveTaegisDLL

''' <summary>
''' Sistema Active - VS2022
''' Extrai o Id da investigação do campo [resumo] da tabela t_sd_solicit_incid_it2m
''' Autor: Heron Domingues Jr
''' Data:  14/11/23
''' Alteração Data: 19/03/24-21/03/24
''' Obs: Nas propriedades do projeto desmarcar a opção "enable application framework" e mudar a caixa "Startup Object" para a Sub Main deste módulo
''' </summary>
''' <remarks></remarks>
Module Modulo_Active_Taegis_InvMutation
    '14/11/23
    'Módulo de extração de Id
    '========================
    Private _strLog As New String("")
    Private _strPastaProj As New String("")
    Private _strPastaPython As New String("")
    Private _intLinhasSQL_Mut As Integer = 0
    Private _aTenantId As New List(Of String)
    Private _aClientId As New List(Of String)
    Private _aClientSecret As New List(Of String)
    Private _aClientName As New List(Of String)

    Public Sub Main()
        '14/11/23
        'Chamada principal do Módulo de extração de Id
        '=============================================
        _strPastaPython = LeConfigPasta("pasta_python")
        _strPastaProj = LeConfigPasta("pasta_proj_investigations_auto")

        CargaSQLClientes()

        For i As Integer = 0 To _aTenantId.Count - 1
            Le_Tabela_IT2M(_aTenantId(i), _aClientId(i), _aClientSecret(i), _aClientName(i))
        Next

    End Sub

    Private Sub CargaSQLClientes()
        'Carga dos clientes do banco SQL Active
        '04/04/23
        '======================================
        Dim oCon As New SqlConnection(ConnectionString)
        Dim oCmd As New SqlCommand
        Dim oDrd As SqlDataReader

        oCon.Open()

        With oCmd
            .Connection = oCon
            .CommandType = CommandType.Text
            .CommandText = "SELECT tenant_id, client_id, client_secret, client_name from t_taegis_tab_clients"
            .CommandTimeout = 3600
            oDrd = .ExecuteReader
        End With

        With oDrd

            While .Read
                _aTenantId.Add(.Item("tenant_id"))
                _aClientId.Add(.Item("client_id"))
                _aClientSecret.Add(.Item("client_secret"))
                _aClientName.Add(.Item("client_name"))
            End While

            .Close()
        End With

        oCon.Close()

    End Sub

    Private Sub GravaLog(ByVal strTenantId As String, ByVal strClientId As String, ByVal strClientSecret As String, ByVal strClientName As String)
        '16/11/23
        'Grava o log de geração dos arquivos de DataSources na tabela t_taegis_log
        '=========================================================================
        Dim oCon As New SqlConnection(ConnectionString)
        Dim oCmd As New SqlCommand

        oCon.Open()

        With oCmd
            .Connection = oCon
            .CommandType = CommandType.Text
            .CommandText = "insert into t_taegis_log(tenant_id, client_name, data_log, qtde_inc, tipo_log, texto_log)" &
            " values('" & strTenantId & "', '" & strClientName & "', getdate(), " & _intLinhasSQL_Mut & ", 'Investigation Mutation', '" & _strLog & "')"
            .CommandTimeout = 3600
            .ExecuteNonQuery()
        End With

        oCon.Close()

    End Sub

    Private Sub Le_Tabela_IT2M(ByVal strTenantId As String, ByVal strClientId As String, ByVal strClientSecret As String, ByVal strClientName As String)
        'Leitura da tabela t_sd_solicit_incid_it2m e pesquisa de Id de investigações ainda não atualizados
        'O Id da investigação é encontrado no campo [resumo] da tabela t_sd_solicit_incid_it2m quando:
        '   - O campo [resumo] começa com "INVnnnnn -" e termina com "(<id da investigação>)"
        '   - O <id da investigação>" deverá ser procurado em Taegis.Investigations e, quando encontrado,
        '     atualizar via Python o campo "Service Desk Id" com o valor do Numero do Chamado <numero> no IT2M.
        '14/11/23
        '04/04/25
        '=================================================================================================
        Dim oCon As New SqlConnection(ConnectionString)
        Dim oCmd As New SqlCommand
        Dim oDrd As SqlDataReader

        oCon.Open()

        With oCmd
            .Connection = oCon
            .CommandType = CommandType.Text
            .CommandText = "SELECT REPLACE(RIGHT(resumo, CHARINDEX('(', REVERSE(resumo)) - 1), ')','') AS Inv_Id, 
                                   SUBSTRING(numero, 2, 10) as numero_chamado, SUBSTRING(resumo, 1, 8) AS inv_resumo 
                               FROM [DWNetcenter].[dbo].[t_sd_solicit_incid_it2m] 
                               WHERE CHARINDEX('INV', resumo) = 1 
		                            AND ISNUMERIC(SUBSTRING(resumo, 4, 5)) = 1 
		                            AND CHARINDEX('-', resumo) = 10 
		                            -- AND RIGHT(resumo, 1) = ')' 
		                            AND RIGHT(resumo, 15) <> '(Single Source)' 
									AND CHARINDEX('(', resumo) > 0
                                    AND organizacao LIKE '" & Mid(strClientName, 1, 5) & "%'"
            .CommandTimeout = 3600
            oDrd = .ExecuteReader
        End With

        With oDrd

            While .Read
                If Not MutationJaEfetuada(strClientName, .Item("inv_resumo")) Then
                    AtualizaInvestigation(strTenantId, strClientId, strClientSecret, strClientName, .Item("Inv_Id"), .Item("numero_chamado"), .Item("inv_resumo"))
                    _intLinhasSQL_Mut += 1
                End If
            End While

            .Close()
        End With

        oCon.Close()

    End Sub

    Private Sub AtualizaInvestigation(ByVal strTenantId As String, ByVal strClientId As String, ByVal strClientSecret As String, ByVal strClientName As String,
                                      ByVal strId As String, ByVal strNumeroChamado As String, ByVal strResumo As String)
        '14/11/23
        'Atualiza (Mutation) via Python o campo Service Desk Id na investigação de código strId
        '======================================================================================
        Dim oProcess As Process = New Process()
        Dim strEnvironment = "delta"
        Dim oStartInfo As New ProcessStartInfo(_strPastaPython, "investigations_mut_pars.py " & strEnvironment & " " &
                                               strTenantId & " " & strClientId & " " & strClientSecret & " " &
                                               strId & " " & strNumeroChamado)

        oStartInfo.UseShellExecute = False
        oStartInfo.CreateNoWindow = True
        oStartInfo.RedirectStandardOutput = True
        oProcess.StartInfo = oStartInfo
        oProcess.Start()
        oProcess.Dispose()
        oProcess = Nothing

        _strLog = "Mutation gravado com sucesso - Cliente: " & strClientName & " Investigação: " & strResumo & " Chamado: " & strNumeroChamado & vbCrLf
        GravaLog(strTenantId, strClientId, strClientSecret, strClientName)

    End Sub

    Private Function MutationJaEfetuada(ByVal strClientName As String, ByVal strInvResumo As String) As Boolean
        'Verifica se a investigação já foi registrada
        '17/11/23
        '============================================
        Dim oCon As New SqlConnection(ConnectionString)
        Dim oCmd As New SqlCommand
        Dim intQtde As Integer = 0

        oCon.Open()

        With oCmd
            .Connection = oCon
            .CommandType = CommandType.Text
            .CommandText = "SELECT COUNT(*) as Qtde
                              FROM [Active].[dbo].[t_taegis_log]
                              where client_name='" & strClientName & "'
	                            and tipo_log='Investigation Mutation'
	                            and CHARINDEX('Chamado:', texto_log, 1) > 0
	                            and CHARINDEX('" & strInvResumo & "', texto_log, 1) > 0"
            .CommandTimeout = 3600
            intQtde = .ExecuteScalar
        End With

        oCon.Close()

        Return (intQtde > 0)

    End Function

End Module
