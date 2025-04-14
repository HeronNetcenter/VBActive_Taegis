Imports System.Reflection.Assembly

Public Class Proj
    '17/10/23
    '========
    ''' <summary>
    ''' Retorna a versão automática da aplicação
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function VersaoAuto1() As String
        '17/10/23
        '========================================
        Dim version_auto As New String("")

        version_auto &= GetExecutingAssembly().GetName().Version.Major.ToString & "."
        version_auto &= GetExecutingAssembly().GetName().Version.Minor.ToString & "."
        version_auto &= GetExecutingAssembly().GetName().Version.Build.ToString & " Build "
        version_auto &= GetExecutingAssembly().GetName().Version.Revision.ToString

        Return Application.ProductName & " V-" & version_auto

    End Function

End Class
