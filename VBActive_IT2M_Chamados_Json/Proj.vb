Imports System.Reflection.Assembly
Imports System.ComponentModel

Module Proj

    ''' <summary>
    ''' Retorna a versão automática da aplicação
    ''' </summary>
    ''' <returns></returns>
    Public Function VersaoAuto() As String
        '20/08/20
        '=================================
        Dim version_auto As New String("")

        version_auto &= GetExecutingAssembly().GetName().Version.Major.ToString & "."
        version_auto &= GetExecutingAssembly().GetName().Version.Minor.ToString & "."
        version_auto &= GetExecutingAssembly().GetName().Version.Build.ToString & " Build "
        version_auto &= GetExecutingAssembly().GetName().Version.Revision.ToString

        Return Application.ProductName & " V-" & version_auto

    End Function

    Public Function CopyRight(ByVal intAno As Integer) As String
        'Retorna o copyright
        '05/08/08
        '===================
        Return "Copyright © Netcenter " & intAno & IIf(Year(Today) > intAno, "-" & Year(Now.Date), "")

    End Function

    ''' <summary>
    ''' Conexão banco DWNetcenter
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ConnectionString() As String
        '02/06/16
        '19/07/16: 1.10-Arquivo de configuração comum DWConnections.config para todo o grupo de apps DWNetcenter
        '               ATENÇÃO: O ARQUIVO DWCONNECTIONS.CONFIG DEVERÁ RESIDIR NA MESMA PASTA QUE OS .EXE E OS RESPECTIVOS .CONFIGs
        '                        PARA TESTAR A APP O DWCONNECTIONS.CONFIG TERÁ QUE EXISTIR NA PASTA RELEASE OU DEBUG, OU NUMA OUTRA QUALQUER.
        '====================================================================================================================================
        Get
            Return System.Configuration.ConfigurationManager.ConnectionStrings("DWNetcenterConnectionString").ConnectionString
        End Get

    End Property

    ''' <summary>
    ''' Máscara para CNPJ e CPF
    ''' </summary>
    ''' <param name="doc"></param>
    ''' <returns></returns>
    Public Function mascaraCNPJ_CPF(doc As String) As String
        '02/02/24
        '========
        Dim mascara As New MaskedTextProvider("00\.000\.000/0000-00")

        If doc.Length = 11 Then
            mascara = New MaskedTextProvider("000\.000\.000-00")
        End If

        mascara.Set(doc)
        Return mascara.ToString()

    End Function

End Module
