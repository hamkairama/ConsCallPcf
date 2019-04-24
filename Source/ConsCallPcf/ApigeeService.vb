Imports System.Configuration
Imports System.IO
Imports System.Net
Imports System.Text
Imports System.Security
Imports System.Xml
Imports ConsCallPcf.PCFServiceModel
Imports ConsCallPcf.Helpers

Public Class ApigeeService

    Public Shared Function PCF_CallAIS(ByVal token As String, ByVal ILAPIParam As String, ByVal PolNum As String, ByRef result As String)
        Dim errs As New BusinessErrors
        Dim pcfSrv As New PCFService()

        Try
            errs = pcfSrv.CallAisResult(token, ILAPIParam, PolNum, result)
        Catch ex As WebException
            Dim ftpResponse = CType(ex.Response, HttpWebResponse)
            Dim concateEx = String.Format(" exception : [message : {0}, inner exception : {1}]", ex.Message.ToString, ex.StackTrace.ToString)
            result = "CALL PCF SERVICE FAIL, " & ftpResponse.StatusCode & ftpResponse.StatusDescription & concateEx
        End Try

        Return errs
    End Function

End Class
