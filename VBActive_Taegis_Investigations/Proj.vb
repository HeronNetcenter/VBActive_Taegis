Imports System.Reflection.Assembly
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.IO

Public Class Proj
    '17/10/233
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

    Public Shared Function GetJsonInvestigationCount(filePath As String) As Integer
        'Retorna o número de items de um arquivo JSON
        '20/03/25
        '============================================
        ' Construct the file path
        'Dim fileName As String = $"investigations01_{tenantId}_{dateStr}.json"
        'Dim filePath As String = Path.Combine("C:\Path\To\Your\Directory", fileName) ' Replace with your actual directory

        Try
            ' Check if file exists
            If Not File.Exists(filePath) Then
                Console.WriteLine($"JSON file not found: {filePath}")
                Return -1
            End If

            ' Read and parse the JSON file
            Dim jsonText As String = File.ReadAllText(filePath)
            Dim investigations As List(Of Dictionary(Of String, Object)) = JsonConvert.DeserializeObject(Of List(Of Dictionary(Of String, Object)))(jsonText)

            ' Return the count
            If investigations Is Nothing Then
                Return 0 ' Handle empty or malformed JSON
            Else
                Return investigations.Count
            End If

        Catch ex As Exception
            Console.WriteLine($"Error reading JSON file: {ex.Message}")
            Return -1
        End Try
    End Function

End Class
