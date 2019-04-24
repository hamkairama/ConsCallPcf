#Region ".Net Base "
Imports System.Data
Imports System.Text
#End Region

<Serializable>
Public Class ResultStatus
#Region "Private Variables"
    Private _status As Integer = -1

    Public Property Status() As Integer
        Get
            Return _status
        End Get
        Set
            _status = Value
        End Set
    End Property

    Private _messageText As String

    Public Property MessageText() As String
        Get
            Return _messageText
        End Get
        Set
            _messageText = Value
        End Set
    End Property

#End Region

#Region "Constructors/Destructors/Finalisers"
#End Region

#Region "Public Methods and Properties"
    Public ReadOnly Property IsSuccess() As Boolean
        Get
            Return Status = 0
        End Get
    End Property


    Public Sub SetSuccessStatus(message As String)
        Status = 0
        _messageText = message
    End Sub

    Public Sub SetSuccessStatus()
        Status = 0
    End Sub

    Public Sub SetErrorStatus(message As String)
        Status = -1
        _messageText = message
    End Sub
#End Region
End Class
