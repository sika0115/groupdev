Private Sub CommandButton1_Click()
Dim 接続 As Object
Dim ユーザID As String
Dim パスワード As String
Dim サーバ名 As String
Dim データベース名 As String
Dim 監視時間 As Long
Dim 権限モード As Long
Dim レコードセット As Object
Dim カーソルエンジン As Long
Dim カーソルタイプ As Long
Dim ロック情報 As Long
Dim オプション As Long
Dim ソース情報 As String
Dim 項目数 As Long
Dim レコード数 As Long
Dim ソース情報 As String
Dim Coll　As Long
Dim phone_number As String
 
 '//接続
Set 接続 = New ADODB.Connection
ユーザID = "sk16100"
パスワード = "sk16100"
サーバ名 = "jocalc1"
データベース名 = "r01_j4_exp"
監視時間 = 10
接続.ConnectionString = "driver={PostgreSQL Unicode(x64)}" & _
                        ";uid=" & ユーザID & _
                        ";pwd=" & パスワード & _
                        ";server=" & サーバ名 & _
                        ";database=" & データベース名
                        
接続.ConnectionTimeout = 監視時間
権限モード = adModeRead '//書込= adModeWrite，更新= adModeReadWrite
接続.Mode = 権限モード
接続.Open               '//  同期接続のみとする(非同期接続は不使用)

'//-----DebugCode-------
'//MsgBox ("open")
'//接続.Close
'//MsgBox ("close")

'//レコードセット
Set  レコードセット = New ADODB.Recordset 
カーソルエンジン = adUseClient  '// クライアント側=adUseClient，サーバ側=adUseServer 
レコードセット.CursorLocation = カーソルエンジン
カーソルタイプ = adOpenDynamic  '// adOpenKeyset，adOpenStatic，adOpenForwardOnly 
ロック情報 = adLockOptimistic   '// adLockReadOnly，adLockPessimistic 
オプション= adCmdText           '// adCmdUnknown，adCmdTableDirect
ソース情報 = "select * from company_19"
レコードセット.Open ソース情報, 接続, カーソルタイプ, ロック情報, オプション

For Coll = 1 To 117 Step 1
    レコードセット.AddNew
    レコードセット.Fields("code").Value = Sheet1.Cells(Coll, "A").Value   '//企業コード : code
    レコードセット.Fields("name").Value = Sheet1.Cells(Coll, "B").Value   '//企業名 : name
    レコードセット.Fields("address").Value = Sheet1.Cells(Coll, "C").Value'//所在地 : address
    '//レコードセット.Fields("phone").Value = Sheet1.Cells(Coll, "D").Value  '//電話番号 : phone
    レコードセット.Fields("labours").Value = Sheet1.Cells(Coll, "E").Value'//従業員数 : labours
    
    '//電話番号の処理
    If IsNull(Sheet1.Cells(Coll, "D").Value) Then
        phone_number = ""
    Else
        phone_number = Sheet1.Cells(Coll, "D").Value
    End If

    レコードセット.Fields("phone").Value = phone_number'//電話番号 : phone
    レコードセット.Update
    レコードセット.MoveNext
Next
レコードセット.Close
接続.Close
MsgBox("格納完了")
Unload UserForm1
End Sub
