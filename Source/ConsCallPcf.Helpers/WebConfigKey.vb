Imports System.Configuration

Public NotInheritable Class WebConfigKey
    Private Sub New()
    End Sub
    Public Shared ReadOnly Property GetPcfToken() As String
        Get
            Return ConfigurationSettings.AppSettings("PcfToken").ToString()
        End Get
    End Property

    Public Shared ReadOnly Property GetEmailFrom() As String
        Get
            Return ConfigurationSettings.AppSettings("EmailFrom").ToString()
        End Get
    End Property

    Public Shared ReadOnly Property GetEmailTo() As String
        Get
            Return ConfigurationSettings.AppSettings("EmailTo").ToString()
        End Get
    End Property

    Public Shared ReadOnly Property GetEmailCc() As String
        Get
            Return ConfigurationSettings.AppSettings("EmailCc").ToString()
        End Get
    End Property

    Public Shared ReadOnly Property GetEmailSubject() As String
        Get
            Return ConfigurationSettings.AppSettings("EmailSubject").ToString()
        End Get
    End Property

    Public Shared ReadOnly Property GetFileQueryLoc() As String
        Get
            Return ConfigurationSettings.AppSettings("FileQueryLoc").ToString()
        End Get
    End Property

    Public Shared ReadOnly Property GetFileQueryBpmLoc() As String
        Get
            Return ConfigurationSettings.AppSettings("FileQueryBpmLoc").ToString()
        End Get
    End Property

    Public Shared ReadOnly Property GetMsgDataNotFound() As String
        Get
            Return ConfigurationSettings.AppSettings("DataNotFound").ToString()
        End Get
    End Property

End Class