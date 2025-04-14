Imports System.Data.SqlClient
Imports System.Reflection.Assembly
Imports System.Windows.Forms

Public Class ActiveTaegisDLL
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

    ''' <summary>
    ''' Retorna a versão automática da aplicação
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function VersaoAuto() As String
        '20/08/20-04/11/20
        '========================================
        Dim version_auto As New String("")

        version_auto &= GetExecutingAssembly().GetName().Version.Major.ToString & "."
        version_auto &= GetExecutingAssembly().GetName().Version.Minor.ToString & "."
        version_auto &= GetExecutingAssembly().GetName().Version.Build.ToString & " Build "
        version_auto &= GetExecutingAssembly().GetName().Version.Revision.ToString

        Return Application.ProductName & " V-" & version_auto

    End Function
    Public Shared Function CopyRight() As String
        'Retorna o copyright
        '09/02/23
        '===================
        Return "Copyright © Netcenter 2023" & IIf(Year(Today) > 2023, "-" & Year(Now.Date), "")

    End Function

    Public Shared Function LeConfig() As String
        'Le os dados da tabela SQL t_active_config
        '09/02/23-27/02/23-15/03/23-30/08/23-20/09/23-18/10/23
        '23/12/24
        '=====================================================
        Dim oCon As New SqlConnection(ConnectionString)
        Dim oCmd As New SqlCommand
        Dim oDrd As SqlDataReader

        Dim strPastaPython As New String("")

        Dim strPastaProjAlerts As New String("")
        Dim strPastaProjAssets As New String("")
        Dim strPastaProjInvestigations As New String("")
        Dim strPastaProjCollectors As New String("")
        Dim strPastaProjEvents As New String("")
        Dim strPastaProjEventsDiario As New String("")
        Dim strPastaProjDatasources As New String("")

        Dim strPastaProjAlertsAuto As New String("")
        Dim strPastaProjAssetsAuto As New String("")
        Dim strPastaProjInvestigationsAuto As New String("")
        Dim strPastaProjCollectorsAuto As New String("")
        Dim strPastaProjEventsAuto As New String("")
        Dim strPastaProjEventsDiarioAuto As New String("")
        Dim strPastaProjDatasourcesAuto As New String("")

        Dim strPastas As New String("")

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
                strPastaPython = .Item("pasta_python").ToString.Trim  '0

                strPastaProjAlerts = .Item("pasta_proj_alerts").ToString.Trim '1
                strPastaProjAssets = .Item("pasta_proj_assets").ToString.Trim '2
                strPastaProjInvestigations = .Item("pasta_proj_investigations").ToString.Trim '3
                strPastaProjCollectors = .Item("pasta_proj_collectors").ToString.Trim '4
                strPastaProjEvents = .Item("pasta_proj_events").ToString.Trim '5
                strPastaProjEventsDiario = .Item("pasta_proj_events_diario").ToString.Trim '13
                strPastaProjDatasources = .Item("pasta_proj_datasources").ToString.Trim   '11

                strPastaProjAlertsAuto = .Item("pasta_proj_alerts_auto").ToString.Trim    '6
                strPastaProjAssetsAuto = .Item("pasta_proj_assets_auto").ToString.Trim    '7
                strPastaProjInvestigationsAuto = .Item("pasta_proj_investigations_auto").ToString.Trim    '8
                strPastaProjCollectorsAuto = .Item("pasta_proj_collectors_auto").ToString.Trim    '9
                strPastaProjEventsAuto = .Item("pasta_proj_events_auto").ToString.Trim    '10
                strPastaProjEventsDiarioAuto = .Item("pasta_proj_events_diario_auto").ToString.Trim    '14
                strPastaProjDatasourcesAuto = .Item("pasta_proj_datasources_auto").ToString.Trim  '12
            End If
            .Close()
        End With

        oCon.Close()

        '                   0                       1                           2                           3                               4
        strPastas = strPastaPython & ";" & strPastaProjAlerts & ";" & strPastaProjAssets & ";" & strPastaProjInvestigations & ";" & strPastaProjCollectors & ";"
        '                   5                               6                           7                                   8
        strPastas &= strPastaProjEvents & ";" & strPastaProjAlertsAuto & ";" & strPastaProjAssetsAuto & ";" & strPastaProjInvestigationsAuto & ";"
        '                   9                                   10                              11                              12
        strPastas &= strPastaProjCollectorsAuto & ";" & strPastaProjEventsAuto & ";" & strPastaProjDatasources & ";" & strPastaProjDatasourcesAuto & ";"
        '                   13                                  14
        strPastas &= strPastaProjEventsDiario & ";" & strPastaProjEventsDiarioAuto

        Return strPastas

        '               0                     1                          2                          3                                  4                              5                          6
        'Return strPastaPython & ";" & strPastaProjAlerts & ";" & strPastaProjAssets & ";" & strPastaProjInvestigations & ";" & strPastaProjCollectors & ";" & strPastaProjEvents & ";" & strPastaProjAlertsAuto &
        '                        ";" & strPastaProjAssetsAuto & ";" & strPastaProjInvestigationsAuto & ";" & strPastaProjCollectorsAuto & ";" & strPastaProjEventsAuto & ";" & strPastaProjDatasources & ";" & strPastaProjDatasourcesAuto
        '                                     7                                   8                                      9                                  10                             11                              12

    End Function

    Public Shared Function LeConfigPasta(ByVal strCampo As String) As String
        'Le uma pasta da tabela SQL t_active_config
        '19/10/23
        '24/12/24
        '==========================================
        Dim oCon As New SqlConnection(ConnectionString)
        Dim oCmd As New SqlCommand
        Dim strPasta As New String("")

        oCon.Open()

        With oCmd
            .Connection = oCon
            .CommandType = CommandType.StoredProcedure
            .Parameters.Add(New SqlParameter("@campo", strCampo))
            .CommandText = "p_le_active_config_pasta"
            .CommandTimeout = 3600
            strPasta = .ExecuteScalar
        End With

        oCon.Close()

        Return strPasta

    End Function

End Class
