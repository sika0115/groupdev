<!DOCTYPE html>
<html>
<head>
<meta charset="UTF-8">
<title>検索・表示画面</title>
</head>

<body>
<style>
    th, td{
	    border: solid 1px;
    }
    th, td:nth-of-type(5){
	    text-align: right;
    }
    table{
	    border-collapse: collapse;
    }
</style>

    <h2>企業一覧表</h2><br>
    <form method="post" action="index.php">
        企業名部分一致検索
        <input type="text" name="search_text">
        <input type="submit" value="検索">
        ソート
        <select name="sort">
		    <option value="ASC">昇順</option>
			<option value="DESC">降順</option>
        </select>
        項目
        <select name="item">
			<option value="code">企業コード</option>
			<option value="name">企業名</option>
			<option value="address">所在地</option>
			<option value="phone">電話番号</option>
			<option value="labors">従業員数</option>
        </select>
    </form>
    <br>

    <?php
		if(!empty($_POST['search_text'])){
			$search_text = $_POST['search_text'];
			$sort = $_POST['sort'];
			$item = $_POST['item'];
            $result = DBsearch($search_text, $item, $sort);
            
            if(!is_null($result)){
	            require_once("Company.php");
	            $company = [];
         	    foreach($result as $row){
            	    $company[] = new Company($row['code'], $row['name'], $row['address'], $row['phone'], $row['labors']);
         	    }
                print "企業数：" . htmlspecialchars(count($company), ENT_QUOTES, 'UTF-8') . "件\n<br>\n";
                
			    if(count($company) != 0){
	   	            print "<table>\n";
   	      	        print "<tr>\n";
	      	        print "<th>企業コード</th><th>企業名</th><th>所在地</th><th>電話番号</th><th>従業員数</th>\n";
   	      	        print "<tr>\n";
	      	        foreach($company as $row){
      	   	            print "<tr>\n";
   	   	                print "<td>" . htmlspecialchars($row->getCode(), ENT_QUOTES, 'UTF-8') . "</td>\n";
	   	                print "<td>" . htmlspecialchars($row->getName(), ENT_QUOTES, 'UTF-8') . "</td>\n";
	            	    print "<td>" . htmlspecialchars($row->getAddress(), ENT_QUOTES, 'UTF-8') . "</td>\n";
            		    print "<td>" . htmlspecialchars($row->getPhone(), ENT_QUOTES, 'UTF-8') . "</td>\n";
         		        print "<td>" . htmlspecialchars(number_format($row->getLabors()), ENT_QUOTES, 'UTF-8') . "</td>\n";
      		            print "</tr>\n";
   		            }
		            print "</table>\n";
			    }
		    }
	    }
		else{
			print("検索したい企業名を入力してください");
		}
   ?>
</body>
</html>

<?php
   function DBsearch($searchWord, $item, $sort){
      try{
   	      $server = "jocalc1";
      	   $DB = "r01_j4_exp";
         	$portNumber = "5432";
	         $userID = "sk16100";
   	      $userPass = "sk16100";
      	   $dbh = new PDO("pgsql:host=$server; dbname=$DB; port=$portNumber; user=$userID; password=$userPass");
         	$sql = "SELECT * FROM company_19 WHERE name LIKE '%" . $searchWord . "%' ORDER BY " . $item . " " . $sort;
				$stmt = $dbh->query($sql);
      	   $result = $stmt->fetchAll(PDO::FETCH_ASSOC);
         	$dbh = null;
	      }
   	   catch(Exception $e){
   	   	$result = null;
      	   print($e);
	      }
      return $result;
   }
?>