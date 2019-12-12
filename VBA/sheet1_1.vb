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
Dim Row As Long
Const COLL_CODE As Integer = 1
Const COLL_NAME As Integer = 2
Const COLL_ADDRESS As Integer = 3
Const COLL_PHONE As Integer = 4
Const COLL_LABORS As Integer = 5
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
Set レコードセット = New ADODB.Recordset  '//SQL発行後データ格納

'//カーソルエンジンの設定
カーソルエンジン = adUseClient  '// クライアント側=adUseClient，サーバ側=adUseServer
レコードセット.CursorLocation = カーソルエンジン
カーソルタイプ = adOpenDynamic  '// adOpenKeyset，adOpenStatic，adOpenForwardOnly
ロック情報 = adLockOptimistic   '// adLockReadOnly，adLockPessimistic
オプション = adCmdText          '// adCmdUnknown，adCmdTableDirect
ソース情報 = "select * from company_19"

レコードセット.Open ソース情報, 接続, カーソルタイプ, ロック情報, オプション

'//レコード追加、更新
For Row = 2 To 119 Step 1
    レコードセット.AddNew
    レコードセット.Fields("code").Value = Worksheets("データ").Cells(Row, COLL_CODE).Value   '//企業コード : code
    レコードセット.Fields("name").Value = Worksheets("データ").Cells(Row, COLL_NAME).Value   '//企業名 : name
    レコードセット.Fields("address").Value = Worksheets("データ").Cells(Row, COLL_ADDRESS).Value '//所在地 : address
    レコードセット.Fields("phone").Value = Worksheets("データ").Cells(Row, COLL_PHONE).Value  '//電話番号 : phone
    レコードセット.Fields("labors").Value = Worksheets("データ").Cells(Row, COLL_LABORS).Value '//従業員数 : labors
    
    レコードセット.Update
Next

レコードセット.Close
接続.Close
MsgBox ("company_19に格納完了")
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
Set レコードセット = New ADODB.Recordset  '//SQL発行後データ格納

'//カーソルエンジンの設定
カーソルエンジン = adUseClient  '// クライアント側=adUseClient，サーバ側=adUseServer
レコードセット.CursorLocation = カーソルエンジン
カーソルタイプ = adOpenDynamic  '// adOpenKeyset，adOpenStatic，adOpenForwardOnly
ロック情報 = adLockOptimistic   '// adLockReadOnly，adLockPessimistic
オプション = adCmdText          '// adCmdUnknown，adCmdTableDirect
ソース情報 = "select * from company_19"

'//レコード削除
For Row = 2 To 119 Step 1
    レコードセット.Open ソース情報, 接続, カーソルタイプ, ロック情報, オプション
    レコードセット.Delete
    レコードセット.Close
Next

接続.Close
MsgBox ("company_19のレコードを全消去")

End Sub
