Imports System.Data.SqlClient

Public Class ActiveCommonModule
    'Classe de módulos comuns para o Sistema Active
    '12/09/23
    '==============================================
    Public Shared ReadOnly Property ConnectionString() As String
        '12/11/20: Arquivo de configuração comum DWConnections.config para todo o grupo de apps DWNetcenter
        '          ATENÇÃO: O ARQUIVO DWCONNECTIONS.CONFIG DEVERÁ RESIDIR NA MESMA PASTA QUE OS .EXE E OS RESPECTIVOS .CONFIGs
        '                   PARA TESTAR A APP O DWCONNECTIONS.CONFIG TERÁ QUE EXISTIR NA PASTA RELEASE OU DEBUG, OU NUMA OUTRA QUALQUER.
        '===============================================================================================================================
        Get
            Return System.Configuration.ConfigurationManager.ConnectionStrings("DWNetcenterConnectionString").ConnectionString
        End Get

    End Property

    Public Shared Function LeConfig()
        'Le os dados da tabela SQL t_active_config
        '09/02/23-27/02/23-15/03/23-30/08/23
        '=========================================
        Dim oCon As New SqlConnection(ConnectionString)
        Dim oCmd As New SqlCommand
        Dim oDrd As SqlDataReader

        Dim strPastaPython As New String("")

        Dim strPastaProjAlerts As New String("")
        Dim strPastaProjAssets As New String("")
        Dim strPastaProjInvestigations As New String("")
        Dim strPastaProjCollectors As New String("")
        Dim strPastaProjEvents As New String("")

        Dim strPastaProjAlertsAuto As New String("")
        Dim strPastaProjAssetsAuto As New String("")
        Dim strPastaProjInvestigationsAuto As New String("")
        Dim strPastaProjCollectorsAuto As New String("")
        Dim strPastaProjEventsAuto As New String("")

        oCon.Open()

        With oCmd
            .Connection = oCon
            .CommandType = CommandType.StoredProcedure
            .CommandText = "p_le_active_config"
            .CommandTimeout = 3600
            oDrd = .ExecuteReader
        End With

        With oDrd
            If .Read Then
                strPastaPython = .Item("pasta_python")

                strPastaProjAlerts = .Item("pasta_proj_alerts")
                strPastaProjAssets = .Item("pasta_proj_assets")
                strPastaProjInvestigations = .Item("pasta_proj_investigations")
                strPastaProjCollectors = .Item("pasta_proj_collectors")
                strPastaProjEvents = .Item("pasta_proj_events")

                strPastaProjAlertsAuto = .Item("pasta_proj_alerts_auto")
                strPastaProjAssetsAuto = .Item("pasta_proj_assets_auto")
                strPastaProjInvestigationsAuto = .Item("pasta_proj_investigations_auto")
                strPastaProjCollectorsAuto = .Item("pasta_proj_collectors_auto")
                strPastaProjEventsAuto = .Item("pasta_proj_events_auto")
            End If
            .Close()
        End With

        oCon.Close()

        '               0                     1                          2                          3                                  4                              5
        Return strPastaPython & ";" & strPastaProjAlerts & ";" & strPastaProjAssets & ";" & strPastaProjInvestigations & ";" & strPastaProjCollectors & ";" & strPastaProjEvents &
                                ";" & strPastaProjAlertsAuto & ";" & strPastaProjAssetsAuto & ";" & strPastaProjInvestigationsAuto & ";" & strPastaProjCollectorsAuto & ";" & strPastaProjEventsAuto
        '                                     6                         7                                   8                                      9                                  10

    End Function

End Class
