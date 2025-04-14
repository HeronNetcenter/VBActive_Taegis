Imports System.Data.SqlClient
Imports System.Reflection.Assembly

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

End Class
