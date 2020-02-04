<?php
  $date = "";
  $uid = "";
  $keyword = "";
  $report = "";
  $attachment = "";

  // フォームから受け取ってた値を変数に保存
  if($_SERVER['REQUEST_METHOD'] === 'POST'){
    $date = $_POST['date'];
    $uid = $_POST['uid'];
    $keyword = $_POST['keyword'];
    $report = $_POST['report'];
    if($_FILES['attachment']['name'] != ""){
      $attachment = "attachment/" . basename($_FILES['attachment']['name']);
    } else {
      $attachment = "";
    }
  }

  // DBにフォームから受け取った値を格納
  $server = "jocalc1";
  $database = "r01_j4_g7";
  $port_number = 5432;
  $user_id = "mk16186";
  $user_password = "mk16186";
  $connect = new PDO("pgsql:host = $server; dbname = $database; port = $port_number; user = $user_id; password = $user_password");
  $sql_text = "INSERT INTO daily ( date, userid, keyword, report, attachment) VALUES ('$date', '$uid', '$keyword', '$report', '$attachment')";
  $res = $connect -> query($sql_text);
?>

<!DOCTYPE HTML>
<html>
<head>
<mera charset = "utf-8">
</head>
<body>
<style>
    th, td{
        border : solid 1px;
        padding : 0.5em;
    }
    table{
	    border-collapse : collapse;
    }
</style>
<h2>登録完了</h2><hr>
<h3>登録された内容</h3><br>
<?php
    print "<table>\n";
    print "<tr>\n";
    print "<th>日付</th>\n";
    print "<th>ユーザID</th>\n";
    print "<th>担当業務</th>\n";
    print "<th>日報の内容</th>\n";
    print "</tr>\n";
    print "<tr>\n";
    print "<td>" . htmlspecialchars($date, ENT_QUOTES, 'UTF-8'). "</td>\n";
    print "<td>" . htmlspecialchars($uid, ENT_QUOTES, 'UTF-8'). "</td>\n";
    print "<td>" . htmlspecialchars($keyword, ENT_QUOTES, 'UTF-8'). "</td>\n";
    print "<td>" . htmlspecialchars($report, ENT_QUOTES, 'UTF-8'). "</td>\n";
    print "</tr>\n";
    print "</table>\n";
    print "<br>\n";

    print "<p><b>添付ファイル</b></p>\n";
    print "<embed src=$attachment width=800 height=500>";
    print "<br><br>\n";
?>

<input type="button" onclick="location.href='./index.php'"value="ホームへ戻る">

</body>
</html>
