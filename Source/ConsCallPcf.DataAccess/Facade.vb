Imports ConsCallPcf.DataAccess
Imports System
Imports System.Data
Imports Microsoft.VisualBasic.CompilerServices
Imports Microsoft.Win32
Imports System.Collections
Imports System.Data.SqlClient
Imports System.Diagnostics
Imports System.Runtime.CompilerServices
Imports Oracle.ManagedDataAccess.Client
Imports System.Text
Imports System.Web
Imports System.Configuration
Imports ConsCallPcf.Helpers

Public Class Facade
    Public Function GetData(user_id As String) As DataTable
        Dim odal As New DalOracle("EntitiesCas")
        Dim odr As OracleDataReader = Nothing
        Dim carr As New ArrayList
        Dim singleData As New DataTable

        carr.Add(New cArrayList("created_by", user_id))
        odal.fn_GetOracleDataReader("procedure", carr, odr)
        singleData.Load(odr)

        odr.Close()
        carr.Clear()

        Return singleData
    End Function

    Public Function GetDataList() As DataTable
        Dim odal As New DalOracle("EntitiesCas")
        Dim odr As OracleDataReader = Nothing
        Dim carr As New ArrayList
        Dim listData As New DataTable

        Dim Query = ""
        Dim fileQueryLoc As String = WebConfigKey.GetFileQueryLoc
        Query = My.Computer.FileSystem.ReadAllText(fileQueryLoc)

        odal.fn_GetOracleDataReader(Nothing, Query, carr, odr)
        If odr IsNot Nothing Then
            listData.Load(odr)
            odr.Close()
        End If
        carr.Clear()

        Return listData
    End Function

    Public Function UpdateBpm(polNum As String) As String
        Dim odal As New DalOracle("EntitiesBpm")
        Dim odr As OracleDataReader = Nothing
        Dim carr As New ArrayList
        Dim listData As New DataTable
        Dim result As String = ""
        Dim Query = ""
        Dim fileQueryBpmLoc As String = WebConfigKey.GetFileQueryBpmLoc
        Query = My.Computer.FileSystem.ReadAllText(fileQueryBpmLoc)
        Query = Query.Replace("PARAMETER_VALUE", polNum)
        result = odal.fn_AddRecordSP(Query)

        Return result
    End Function

End Class
