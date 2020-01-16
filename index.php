<!DOCTYPE html>
<html>
<head>
<meta charset="UTF-8">
<title>企業検索</title>
</head>

<body>
<style>
    th, td{
        border : solid 1px;
        padding : 0.5em;
    }
    td.labors_right{
        text-align: right;
    }
    table{
	    border-collapse : collapse;
    }
</style>

<h2>企業一覧表</h2>
<!-- 入力フォーム -->
<form method="post" action="index.php">
    企業名部分一致検索
    <input type="text" name="search_text" size="20">
    <input type="submit" name = "button" value="検索">
    ソート順
    <select name="sort">
		<option value="asc">昇順</option>
		<option value="desc">降順</option>
    </select>
    
    項目
    <select name="sort_item">
		<option value="code">企業コード</option>
		<option value="name">企業名</option>
		<option value="address">所在地</option>
		<option value="phone">電話番号</option>
		<option value="labors">従業員数</option>
    </select>
</form>
<br>

<?php
   function database_search($search_word, $sort_item, $sort){
        try{
            //DBシステムの接続情報設定
   	        $server = "j4-php_db_1";
      	    $database = "r01_j4_exp";
            $port_number = "5432";
	        $user_id = "sk16100";
   	        $user_password = "sk16100";
            
            //DBシステムとの接続
            $connect = new 
            PDO("pgsql:host=$server; dbname=$database; port=$port_number; user=$user_id; password=$user_password");
            
            //SQL文の定義 
            $sql = "SELECT * FROM company_19 WHERE name LIKE '%" . $search_word . "%' ORDER BY " . $sort_item . " " . $sort;
            
            //DBの検索
            $state = $connect->query($sql);
      	    $result = $state->fetchAll(PDO::FETCH_ASSOC);
         	$connect = null;
	    }
   	    catch(Exception $e){
   	   	    $result = null;
	    }
        return $result;
    }

	if(!empty($_POST['search_text'])){ 
		$search_text = $_POST['search_text'];
		$sort = $_POST['sort'];
		$sort_item = $_POST['sort_item'];
        $result = database_search($search_text, $sort_item, $sort);
            
        if(!is_null($result)){
	        require_once("Company.php");
	        $company = [];
            foreach($result as $row){
                $company[] = new Company($row['code'], $row['name'], $row['address'], $row['phone'], $row['labors']);
            }
            print "検索件数 : " . htmlspecialchars(count($company), ENT_QUOTES, 'UTF-8') . "件\n<br>\n";
                
		    if(count($company) != 0){
	   	        print "<table>\n";
                print "<tr>\n";
                print "<th>企業コード</th>";
                print"<th>企業名</th>";
                print"<th>所在地</th>";
                print"<th>電話番号</th>";
                print"<th>従業員数</th>\n";
   	            print "<tr>\n";
  	            foreach($company as $row){
      	            print "<tr>\n";
   	   	            print "<td>" . htmlspecialchars($row->get_code(), ENT_QUOTES, 'UTF-8') . "</td>\n";
	                print "<td>" . htmlspecialchars($row->get_name(), ENT_QUOTES, 'UTF-8') . "</td>\n";
        	        print "<td>" . htmlspecialchars($row->get_address(), ENT_QUOTES, 'UTF-8') . "</td>\n";
                    print "<td>" . htmlspecialchars($row->get_phone(), ENT_QUOTES, 'UTF-8') . "</td>\n";
    	            print "<td class=labors_right>" . htmlspecialchars(number_format($row->get_labors()), ENT_QUOTES, 'UTF-8') . "</td>\n";
  		            print "</tr>\n";
   		        }
		        print "</table>\n";
	    	}
        }
	}
	else{
        //入力無しの際の表示
        print "<p>＜Error＞検索したい企業名が入力されていません</p>";
	}
?>
</body>
</html>
