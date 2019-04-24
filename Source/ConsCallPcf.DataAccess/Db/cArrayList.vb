
' Decompiled with JetBrains decompiler
' Type: Dal.cArrayList
' Assembly: Dal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
' MVID: 1B3934F6-49EC-4CE2-BC46-8A155E5D784C
' Assembly location: C:\Users\hamkair\Desktop\Sources\Bin\Dal.dll

Imports System.Data
Imports System.Runtime.CompilerServices


Public Class cArrayList
    Private mvar_data_type As DbType
    Private mvar_size As Integer
    Private mvar_sname As String
    Private mvar_value As Object

    Public Property p_dbtype() As DbType
        Get
            Return Me.mvar_data_type
        End Get
        Set
            Me.mvar_data_type = Value
        End Set
    End Property

    Public Property p_name() As String
        Get
            Return Me.mvar_sname
        End Get
        Set
            Me.mvar_sname = Value
        End Set
    End Property

    Public Property p_size() As Integer
        Get
            Return Me.mvar_size
        End Get
        Set
            Me.mvar_size = Value
        End Set
    End Property

    Public Property p_value() As Object
        Get
            Return Me.mvar_value
        End Get
        Set
            Me.mvar_value = RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(Value)))
        End Set
    End Property

    Public Sub New(sName As String, oValue As Object)
        Me.p_name = sName
        Me.p_value = RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(oValue)))
    End Sub

    Public Sub New(sName As String, oValue As Object, oDbType As DbType)
        Me.p_name = sName
        Me.p_value = RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(oValue)))
        Me.p_dbtype = oDbType
    End Sub

    Public Sub New(sName As String, oValue As Object, nSize As Integer)
        Me.p_name = sName
        Me.p_value = RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(oValue)))
        Me.p_size = nSize
    End Sub

    Public Sub New(sName As String, oValue As Object, oDBType As DbType, nSize As Integer)
        Me.p_name = sName
        Me.p_value = RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(oValue)))
        Me.p_size = nSize
        Me.p_dbtype = oDBType
    End Sub
End Class


