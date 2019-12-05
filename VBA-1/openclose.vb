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
Dim 条件 As String
 
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
MsgBox ("open")
接続.Close
MsgBox ("close")
End Sub
