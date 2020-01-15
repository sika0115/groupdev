<?php
  if($_SERVER['REQUEST_METHOD'] === 'POST'){
    $uid = $_POST['uid'];
    $keyword = $_POST['keyword'];
    $report = $_POST['report'];
  }
  echo "<pre>";
  //アップロードされたファイルの配列を表示してるだけ
  print_r($_FILES);
  echo "<pre>";

?>
<DOCTYPE HTML>
<html>
<head>
<meta charset="UTF-8">
<title>登録結果表示画面</title>
<body>
<h2>登録完了</h2>
<hr>

  // フォームから送信されたデータを表示してるだけ
  uid: <?php echo $uid; ?><br>
  keyword: <?php echo $keyword; ?><br>
  report: <?php echo $report; ?><br>

<style>
table {
    border-collapse: collapse;
}
td {
    border : solid 1px;
    padding : 0.5em;
}
</style>

<?php
//結果の表示


?>

<input type="button" onclick="location.href='./index.php'"value="ホームへ戻る">

</body>
</html>
