Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Public Class ListFieldNameAndValue
    Private _Field As New List(Of String)()
    Private _Value As New List(Of Object)()

    Private _Key As New List(Of Object)()

    Public Property Count() As Integer
        Get
            Return Me._Value.Count
        End Get
        Set
            Count = Value
        End Set
    End Property

    Public Sub New()
        _Field = New List(Of String)()
        _Value = New List(Of Object)()
        _Key = New List(Of Object)()
    End Sub

    Public Sub AddItem(_FieldName As String, _Value As Object)
        Me._Field.Add(_FieldName)
        Me._Value.Add(_Value)
    End Sub

    Public Sub RemoveItem(_FieldName As String, _Value As Object)
        Me._Field.Remove(_FieldName)
        Me._Value.Remove(_Value)
    End Sub

    Public Sub ClearItem()
        Me._Field.Clear()
        Me._Value.Clear()
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="_ObjectName"></param>
    ''' <param name="_Value"></param>
    Public Sub AddObject(_ObjectName As Object, _Value As Object)
        Me._Key.Add(_ObjectName)
        Me._Value.Add(_Value)
    End Sub

    Public Function RemoveItembyFieldNameAndValue(_FieldName As String, _Value As Object) As Boolean
        For i As Integer = 0 To Me._Field.Count - 1
            If Me._Field(i) = _FieldName AndAlso Me._Value(i) = _Value Then
                Me._Field.RemoveAt(i)
                Me._Value.RemoveAt(i)

                Return True
            End If
        Next
        Return False
    End Function

    Public Function RemoveItembyId(_id As Integer) As Boolean
        Try
            If Me._Field.Count > _id AndAlso Me._Value.Count > _id Then
                Me._Field.RemoveAt(_id)
                Me._Value.RemoveAt(_id)

                Return True
            Else
                Return False
            End If
        Catch
            Return False
        End Try
    End Function

    Public Function getFieldbyId(_id As Integer) As String
        Return Me._Field(_id)
    End Function

    Public Function getValuebyId(_id As Integer) As Object
        If Me._Value(_id) Is Nothing Then
            Return String.Empty
        Else
            Return Me._Value(_id)
        End If
    End Function

    ''' <summary>
    ''' adding get object by object filter
    ''' </summary>
    ''' <param name="_id"></param>
    ''' <returns></returns>
    Public Function getKeybyId(_id As Integer) As Object
        Return Me._Key(_id)
    End Function

    Public Function getIndexKeybyObject(key As Object) As Object
        For i As Integer = 0 To Me._Key.Count - 1
            If getKeybyId(i) = key Then
                Return Me._Value(i)
            End If
        Next
        Return Nothing
    End Function

    Public Function getLengthFieldbyId(_id As Integer) As Integer
        Return Me._Field(_id).Length
    End Function

    Public Function getTypeValuebyId(_id As Integer) As Type
        Try
            Return Me._Value(_id).[GetType]()
        Catch
            Return Type.[GetType]("System.String")
        End Try
    End Function
End Class

