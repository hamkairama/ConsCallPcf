Imports System.IO
Imports System.Security.Cryptography
Imports System.Text

Public Class Cryptoghrap
    Private Shared rgbIV As Byte() = Encoding.ASCII.GetBytes("oiwehioplsiajsdv")
    Private Shared key As Byte() = Encoding.ASCII.GetBytes("kajsdoaioalptljsadrq2jflkasd23jd")

    Public Shared Function GenerateKey() As String
        ' Create an instance of Symetric Algorithm. Key and IV is generated automatically.
        Dim desCrypto As DESCryptoServiceProvider = DirectCast(DESCryptoServiceProvider.Create(), DESCryptoServiceProvider)

        ' Use the Automatically generated key for Encryption. 
        Return ASCIIEncoding.ASCII.GetString(desCrypto.Key)
    End Function

    Public Shared Function EncryptString(ClearText As String) As String
        Dim clearTextBytes As Byte() = Encoding.UTF8.GetBytes(ClearText)
        Dim rijn As System.Security.Cryptography.SymmetricAlgorithm = SymmetricAlgorithm.Create()
        Dim ms As New MemoryStream()
        Dim cs As New CryptoStream(ms, rijn.CreateEncryptor(key, rgbIV), CryptoStreamMode.Write)

        cs.Write(clearTextBytes, 0, clearTextBytes.Length)
        cs.Close()
        Return Convert.ToBase64String(ms.ToArray())
    End Function

    Public Shared Function DecryptString(EncryptedText As String) As String
        Dim encryptedTextBytes As Byte() = Convert.FromBase64String(EncryptedText)
        Dim rijn As System.Security.Cryptography.SymmetricAlgorithm = SymmetricAlgorithm.Create()
        Dim ms As New MemoryStream()
        Dim cs As New CryptoStream(ms, rijn.CreateDecryptor(key, rgbIV), CryptoStreamMode.Write)
        Try


            cs.Write(encryptedTextBytes, 0, encryptedTextBytes.Length)
            cs.Close()
        Catch ex As Exception

        End Try

        Return Encoding.UTF8.GetString(ms.ToArray())
    End Function

    Public Shared Function EncryptStringURL(ClearText As String) As String
        Dim clearTextBytes As Byte() = Encoding.UTF8.GetBytes(ClearText)
        Dim rijn As System.Security.Cryptography.SymmetricAlgorithm = SymmetricAlgorithm.Create()
        Dim ms As New MemoryStream()
        Dim cs As New CryptoStream(ms, rijn.CreateEncryptor(key, rgbIV), CryptoStreamMode.Write)

        cs.Write(clearTextBytes, 0, clearTextBytes.Length)
        cs.Close()
        Dim StrTextToAsc As String = TextToAsc(Convert.ToBase64String(ms.ToArray()))
        Return StrTextToAsc
    End Function

    Public Shared Function DecryptStringURL(EncryptedText As String) As String
        Dim StrTextToChr As String = TextToChr(EncryptedText)
        Dim encryptedTextBytes As Byte() = Convert.FromBase64String(StrTextToChr)
        Dim rijn As System.Security.Cryptography.SymmetricAlgorithm = SymmetricAlgorithm.Create()
        Dim ms As New MemoryStream()
        Dim cs As New CryptoStream(ms, rijn.CreateDecryptor(key, rgbIV), CryptoStreamMode.Write)

        cs.Write(encryptedTextBytes, 0, encryptedTextBytes.Length)
        cs.Close()
        Return Encoding.UTF8.GetString(ms.ToArray())
    End Function

    Public Shared Function TextToAsc(ByVal Data As String) As String
        Dim StrTextToAsc As String = ""
        For i As Integer = 1 To Data.Length
            StrTextToAsc += "_" & Asc(Microsoft.VisualBasic.Mid(Data, i, 1))
        Next
        Return StrTextToAsc
    End Function
    Public Shared Function TextToChr(ByVal Data As String) As String
        Dim StrTextToAsc As String = ""
        For i As Integer = 1 To Split(Data, "_").Count - 1
            StrTextToAsc += Chr(Split(Data, "_")(i))
        Next
        Return StrTextToAsc
    End Function
End Class
