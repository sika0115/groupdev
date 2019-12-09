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
Dim Row　As Long '//行
'//Dim phone_number As String
 
'//(1)
Private Sub CommandButton1_Click()

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

'//レコードセットオープン
Set  レコードセット = New ADODB.Recordset '//SQL発行後データ格納

'//カーソルエンジンの設定
カーソルエンジン = adUseClient  '// クライアント側=adUseClient，サーバ側=adUseServer 
レコードセット.CursorLocation = カーソルエンジン
カーソルタイプ = adOpenDynamic  '// adOpenKeyset，adOpenStatic，adOpenForwardOnly 
ロック情報 = adLockOptimistic   '// adLockReadOnly，adLockPessimistic 
オプション= adCmdText           '// adCmdUnknown，adCmdTableDirect
ソース情報 = "select * from company_19"

レコードセット.Open ソース情報, 接続, カーソルタイプ, ロック情報, オプション

'//レコード追加、更新
For Row = 2 To 119 Step 1
    レコードセット.AddNew
    レコードセット.Fields("code").Value = Worksheets("データ").Cells(Row, 1).Value   '//企業コード : code
    レコードセット.Fields("name").Value = Worksheets("データ").Cells(Row, 2).Value   '//企業名 : name
    レコードセット.Fields("address").Value = Worksheets("データ").Cells(Row, 3).Value'//所在地 : address
    レコードセット.Fields("phone").Value = Worksheets("データ").Cells(Row, 4).Value  '//電話番号 : phone
    レコードセット.Fields("labours").Value = Worksheets("データ").Cells(Row, 5).Value'//従業員数 : labours
    
    '//電話番号の処理
    '//If IsNull(Sheet1.Cells(Row, "D").Value) Then
       '// phone_number = ""
    '//Else
       '// phone_number = Sheet1.Cells(Row, "D").Value
    '//End If
    '//レコードセット.Fields("phone").Value = phone_number'//電話番号 : phone

    レコードセット.Update
Next

レコードセット.Close
接続.Close
MsgBox("company_19に格納完了")

End Sub

'//(2)
Private Sub CommandButton2_Click()

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

'//レコードセットオープン
Set  レコードセット = New ADODB.Recordset '//SQL発行後データ格納

'//カーソルエンジンの設定
カーソルエンジン = adUseClient  '// クライアント側=adUseClient，サーバ側=adUseServer 
レコードセット.CursorLocation = カーソルエンジン
カーソルタイプ = adOpenDynamic  '// adOpenKeyset，adOpenStatic，adOpenForwardOnly 
ロック情報 = adLockOptimistic   '// adLockReadOnly，adLockPessimistic 
オプション= adCmdText           '// adCmdUnknown，adCmdTableDirect
ソース情報 = "select * from company_19"

'//レコード削除
For Row = 2 To 119 Step 1
    レコードセット.Open ソース情報, 接続, カーソルタイプ, ロック情報, オプション
    レコードセット.Delete
    レコードセット.Close
Next

接続.Close
MsgBox("company_19のレコードを全消去")

End Sub