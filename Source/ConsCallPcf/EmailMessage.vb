Imports System.Text
Imports ConsCallPcf.EmailServiceModel
Imports ConsCallPcf.Helpers

Public Class EmailMessage
    Public Shared Function SendEmail(from As String, [to] As ListFieldNameAndValue, cc As ListFieldNameAndValue, subject As String, body As String, ByRef message As String) As ResultStatus
        Dim result As New ResultStatus
        result.Status = False
        Try
            result = ActionSendEmail(from, [to], cc, subject, body, message)
            If result.IsSuccess Then
                Dim emailTo As String = String.Empty
                Dim emailCc As String = String.Empty
                If [to] IsNot Nothing Then
                    For i As Integer = 0 To [to].Count - 1
                        If Not emailTo.Contains([to].getValuebyId(i).ToString()) Then
                            emailTo += String.Format("{0}, ", [to].getValuebyId(i).ToString())
                        End If
                    Next
                    If emailTo.Length > 0 Then
                        emailTo = String.Format("{0}", emailTo.Remove(emailTo.Trim().Length - 1))
                    End If
                End If
                If cc IsNot Nothing Then
                    For i As Integer = 0 To cc.Count - 1
                        If Not emailCc.Contains(cc.getValuebyId(i).ToString()) Then
                            emailCc += String.Format("{0}, ", cc.getValuebyId(i).ToString())
                        End If
                    Next
                    If emailCc.Length > 0 Then
                        emailCc = String.Format("{0}", emailCc.Remove(emailCc.Trim().Length - 1))
                    End If
                End If

            End If
            Return result
        Catch exp As Exception
            Throw exp
        End Try
    End Function

    Public Shared Function ActionSendEmail(from As String, [to] As ListFieldNameAndValue, cc As ListFieldNameAndValue, subject As String, body As String, ByRef message As String) As ResultStatus
        Dim result As New ResultStatus
        result.Status = False

        Dim recipient As String() = Nothing
        Dim ccRecipient As String() = Nothing
        Dim emailTo As String = String.Empty
        Dim emailCc As String = String.Empty

        Dim emailContract As New EmailContract()
        Dim emailResult As New EmailResultContract()
        Dim emailService As New MessageService()
        Dim errorMessage As New BusinessErrors()

        Try
            If [to] IsNot Nothing Then
                recipient = New String([to].Count - 1) {}
                For i As Integer = 0 To [to].Count - 1
                    If Not emailTo.Contains([to].getValuebyId(i).ToString()) Then
                        emailTo += String.Format("{0}, ", [to].getValuebyId(i).ToString())
                    End If
                    recipient(i) = [to].getValuebyId(i).ToString()
                Next
                If emailTo.Length > 0 Then
                    emailTo = String.Format("To : {0}", emailTo.Remove(emailTo.Trim().Length - 1))
                End If
            End If
            If cc IsNot Nothing Then
                ccRecipient = New String(cc.Count - 1) {}
                For i As Integer = 0 To cc.Count - 1
                    If Not emailCc.Contains(cc.getValuebyId(i).ToString()) Then
                        emailCc += String.Format("{0}, ", cc.getValuebyId(i).ToString())
                    End If
                    ccRecipient(i) = cc.getValuebyId(i).ToString()
                Next
                If emailCc.Length > 0 Then
                    emailCc = String.Format("CC : {0}", emailCc.Remove(emailCc.Trim().Length - 1))
                End If
            End If

            emailContract.Sender = from
            If [to] IsNot Nothing Then
                emailContract.Recipient = recipient
            End If
            If cc IsNot Nothing Then
                emailContract.CcRecipient = ccRecipient
            Else
                emailContract.CcRecipient = New String(-1) {}
            End If
            emailContract.Subject = subject
            emailContract.Message = body
            emailContract.WorkArea = "994"
            'work area HRD
            emailContract.AttachmentFile = New String(-1) {}

            errorMessage = emailService.SendEmail(emailContract, emailResult)

            If emailResult.Status IsNot Nothing Then
                message = String.Format("| Success Send Email | {0} | {1}", emailTo, emailCc)
                result.SetSuccessStatus(message)
            Else
                message = String.Format("| Failed Send Email | {0} | {1} | Error : {2}", emailTo, emailCc, errorMessage.ErrorList(0).ErrorMessages(0).Value.ToString())
                result.SetErrorStatus(message)
            End If
        Catch exp As Exception
            message = String.Format("| Failed Send Email | {0} | {1} | Error : {2}", emailTo, emailCc, exp.Message)
            result.SetErrorStatus(message)
        End Try
        Return result
    End Function

    Public Shared Function MappingEmailTemplate(sb As String) As String
        Dim result As String = ""
        If sb <> "" Then
            result = String.Format(TemplateEmail, MapTemplateEmailRow(sb))
        Else
            result = WebConfigKey.GetMsgDataNotFound()
        End If

        Return result
    End Function

    Private Const TemplateEmail As String = "<table style='border-color:#000000' cellpadding='0' cellspacing='0' border='1' width='100%'>
                                    <tr>
                                        <td><b>Time</b></td>
                                        <td><b>Pol_Num</b></td>
                                        <td><b>Response_AIS</b></td>
                                        <td><b>Response_BPM</b></td>                                  
                                    </tr>
                                    {0}
                                </table>"


    Public Shared Function MapTemplateEmailRow(sb As String) As String
        Dim result As New StringBuilder
        Dim split1 = sb.Split("||")
        For i = 0 To split1.Count - 1
            If split1(i) <> "" Then
                result.Append("<tr>")
                Dim dt = split1(i).ToString()
                Dim split2 = dt.Split(";")
                For j = 0 To split2.Count - 1
                    If split2(j) <> "" Then
                        result.Append("<td>" & split2(j) & "</td>")
                    End If
                Next

                result.Append("</tr>")
            End If
        Next

        Return result.ToString
    End Function

End Class
