Imports System.Data.SqlClient
Imports System.Reflection.Assembly
Imports VBActive_Taegis_DLL.ActiveTaegisDLL

Public Class Proj
    '09/02/23
    '============
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

    Public Shared Function MaiorQtdeAlertasMes(ByVal strMes As String, ByVal strAno As String, ByVal strClientName As String)
        'Retorna a maior quantidade de alertas em um dia do mês em lvwAlertasMes
        '22/03/23-10/04/23
        '=======================================================================
        Dim oCon As New SqlConnection(ConnectionString)
        Dim oCmd As New SqlCommand
        Dim intMaxAlertas As Integer = 0

        Try
            oCon.Open()

            With oCmd
                .Connection = oCon
                .CommandType = CommandType.Text
                .CommandText = "SELECT Max(Alertas) " &
                               "from (SELECT count(*) Alertas, tta.alert_date " &
                               "from dbo.t_taegis_alerts tta " &
                               "WHERE SUBSTRING(tta.alert_date, 5, 2) = '" & strMes &
                               "' AND SUBSTRING(tta.alert_date, 1, 4) = '" & strAno &
                               "' AND alert_client = '" & strClientName & "' " &
                               "GROUP BY tta.alert_date) a"
                intMaxAlertas = .ExecuteScalar
            End With

        Catch ex As SqlException
            MessageBox.Show(ex.ToString, "Erro SQL", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Catch ex1 As Exception
            MessageBox.Show(ex1.ToString, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            oCon.Close()

        End Try

        Return intMaxAlertas

    End Function

    Public Shared Function MaiorQtdeAlertasMesGeral(ByVal strMes As String, ByVal strAno As String)
        'Retorna a maior quantidade de alertas em um dia do mês em lvwAlertasMes - todos os clientes
        '22/03/23-07/04/23
        '===========================================================================================
        Dim oCon As New SqlConnection(ConnectionString)
        Dim oCmd As New SqlCommand
        Dim intMaxAlertas As Integer = 0

        Try
            oCon.Open()

            With oCmd
                .Connection = oCon
                .CommandType = CommandType.Text
                .CommandText = "SELECT Max(Alertas) " &
                               "from (SELECT count(*) Alertas, tta.alert_client, tta.alert_date " &
                               "from dbo.t_taegis_alerts tta " &
                               "WHERE SUBSTRING(tta.alert_date, 5, 2) = '" & strMes & "' AND SUBSTRING(tta.alert_date, 1, 4) = '" & strAno & "' " &
                               "GROUP BY tta.alert_client, tta.alert_date) a"
                intMaxAlertas = .ExecuteScalar
            End With

        Catch ex As SqlException
            MessageBox.Show(ex.ToString, "Erro SQL", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Catch ex1 As Exception
            MessageBox.Show(ex1.ToString, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            oCon.Close()

        End Try

        Return intMaxAlertas

    End Function

End Class
