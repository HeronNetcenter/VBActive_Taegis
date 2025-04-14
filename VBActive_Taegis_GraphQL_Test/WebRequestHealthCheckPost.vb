Imports System
Imports System.IO
Imports System.Net.Http
Imports System.Text

Module WebRequestHealthCheckPost
    'Teste VB.net com GraphQL
    '28/03/23
    '========================
    Public Sub Main()

        Dim request As HttpClient = New HttpClient
        Dim url As String = "https://urlname.com"
        Dim body As String = "{""query"":""query healthcheckExampleQuery(healthCheck{ok}}"",""variables"":{}}"
        Dim content = New StringContent(body, Encoding.UTF8, "application/json")
        Dim result = request.PostAsync(url, content)

        Console.WriteLine(result.Result)
        Console.ReadKey()

    End Sub
End Module
