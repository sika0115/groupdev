<!DOCTYPE html>
<html>
<head>
<meta charset="UTF-8">
<title>s-2</title>
</head>

<body bgcolor=#606060>
<h2><font face="ＭＳ ゴシック" color=#2b2b2b>企業一覧表</font></h2>
<hr>
<form name="form" method="post" action="s-2.php">
<h3>検索</h3>
<br>
<?php 
    $campany_val = "";

?>

<font face="ＭＳ ゴシック">企業名部分一致検索</font>
    <input type="text" name="CompanyName" value="<?php print "$campany_val";?>" size="20" maxlength="80" style="font-family: ＭＳ ゴシック; font-size: 11pt; color: black; background-color: white; ime-mode: auto"><br>
<br>
<font face="ＭＳ ゴシック">
    <input type="button" name = "search" value="検索"> 
<!--onClick="Input_Search(frm.srv, frm.uid, frm.pas, frm.dbm)"> -->
</form>
<hr>

<!--#PostgreSQLデータベースアクセス-->
<?php
    //DBシステムの接続情報設定
    $server = "jocalc1";
    $database = "r01_j4_exp";
    $port_number = 5432;
    $user_id = "sk16100";
    $password = "sk16100";
    
    //DBシステムとの接続
    $connect = new PDO("pgsql:host = $server;
                        dbname = $database;
                        port = $port_number;
                        user = $user_id;
                        password = $user_password");
    
    //SQL文の定義
    $sql_text = "select company, turnover from テーブル where trunover > 100000 order by trunover desc";
    //DBの検索
    $result = $connect -> query($sql_text);

    //HTML表の作成
    print "<table boder=1 cellspacing=1 cellpadding= 1>¥n";
    print "<tr>";
    print "<th>企業コード</th>";
	print "<th>企業名</th>";
	print "<th>所在地</th>";
	print "<th>電話番号</th>";
	print "<th>従業員数</th>";
	print "</tr>";



?>
</body>
</font>
</html>
