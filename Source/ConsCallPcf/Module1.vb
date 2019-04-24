Imports System.Text
Imports ConsCallPcf.DataAccess
Imports ConsCallPcf.Helpers
Imports ConsCallPcf.PCFServiceModel

Module Module1

    Sub Main()
        Dim dtDataTable As New DataTable
        Dim facade = New Facade
        Dim pcfSrv As New PCFService()
        Dim errs As New BusinessErrors
        Dim rs As New ResultStatus
        Dim resultCallAI = ""
        Dim MessageForEmail As String = ""

        Dim emailFrom = Nothing
        Dim emailCc = Nothing
        Dim emailSubject = Nothing
        Dim emailTo = Nothing
        Dim retEmail = Nothing

        Dim emailToList As New ListFieldNameAndValue
        Dim emailCcList As New ListFieldNameAndValue

        Dim resultUpdateBpm As String = ""

        Console.WriteLine("start . . .")
        resultUpdateBpm = facade.UpdateBpm("O321620230513641")
        Try
            'prepare email --> get value from app.config
            emailFrom = WebConfigKey.GetEmailFrom
            emailCc = WebConfigKey.GetEmailCc
            emailSubject = WebConfigKey.GetEmailSubject
            emailTo = WebConfigKey.GetEmailTo

            emailToList = Common.SplitNameEmail(emailTo)
            emailCcList = Common.SplitNameEmail(emailCc)

            dtDataTable = facade.GetDataList()
            If dtDataTable IsNot Nothing Then
                Dim iLAPIParam = ""
                Dim polNum = ""

                For Each row As DataRow In dtDataTable.Rows
                    iLAPIParam = row("CLOB_RESULT")
                    polNum = row("POL_NUM")
                    errs = ApigeeService.PCF_CallAIS(WebConfigKey.GetPcfToken, iLAPIParam, polNum, resultCallAI)

                    If resultCallAI.Contains("Clean case") Then
                        resultUpdateBpm = facade.UpdateBpm(polNum)
                    End If

                    MessageForEmail &= vbTab & vbTab & Format(Now, "yyyy-MM-dd HH:mm:ss.fff") & " ; " & vbTab & vbTab & polNum & vbTab & vbTab & " ; " & resultCallAI & vbTab & " ; " & vbTab & vbTab & resultUpdateBpm & vbTab & vbTab & "||"
                Next
            End If

            rs = EmailMessage.SendEmail(emailFrom, emailToList, emailCcList, emailSubject, EmailMessage.MappingEmailTemplate(MessageForEmail), retEmail)
        Catch ex As Exception
            Dim msgEx = String.Format(" exception : [message : {0}, inner exception : {1}]", ex.Message.ToString, ex.StackTrace.ToString)
            rs = EmailMessage.SendEmail(emailFrom, emailToList, emailCcList, emailSubject, msgEx, retEmail)
        End Try

        'Console.WriteLine(MessageForEmail.ToString() + rs.MessageText.ToString)
        'Console.ReadKey()
    End Sub
End Module
