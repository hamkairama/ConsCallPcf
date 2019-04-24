Public Class Common
    Public Shared Function SplitNameEmail(obj As String) As ListFieldNameAndValue
        Dim result As New ListFieldNameAndValue
        If obj IsNot Nothing Or obj <> "" Then
            Dim list = obj.Split(";")
            If list.Count >= 1 Then
                For i = 0 To list.Count - 1
                    If list(i) <> "" Then
                        result.AddItem("Email", list(i))
                    End If
                Next
            End If
        Else
            Return Nothing
        End If

        Return result
    End Function
End Class
