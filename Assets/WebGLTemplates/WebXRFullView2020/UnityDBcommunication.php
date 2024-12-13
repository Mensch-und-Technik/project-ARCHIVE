<?php

$con = mysqli_connect('localhost', 'root', '', 'mediainventory');
//$con = mysqli_connect('mysql.hrz.tu-chemnitz.de', 'Project_ARCHIVE_DB_rw', 'thiwa2Oe', 'Project_ARCHIVE_DB');

	//check connection
	if(mysqli_connect_errno()){
		echo "1: Connection failed";	//error code #1 = connection failed
		exit();
	}

	if(!empty($_POST)){
		$arrayOfTruth = explode(" ", $_POST["stringOfTruth"]);
		$stringOfTruth = "'" . implode("','", $arrayOfTruth) . "'";
		if($_POST["task"] == "ReadRecords"){
			//entries that may still be within database but are no longer within the assets directory get deleted
			$deleteNonExists = "DELETE FROM objects WHERE datapath NOT IN ($stringOfTruth);";
			if (mysqli_query($con, $deleteNonExists)) {
				//echo "Record deleted successfully";
				$checkExistsQuery = "SELECT datapath, posX, posY, posZ, rotX, rotY, rotZ FROM objects WHERE datapath IN ($stringOfTruth);";
				$checkExists = mysqli_query($con, $checkExistsQuery) or die("2: Existence check query failed");	//error code #2 = name check query failed	
			} else {
				echo "Error deleting record: " . $con->error;
			}

			
			$numOfRecords = mysqli_num_rows($checkExists);
			if($numOfRecords > 0){
				foreach($checkExists as $record){
					$recordAsString = implode(",", $record);
					echo $recordAsString;
					$numOfRecords--;
					if($numOfRecords > 0){
						echo "\t";
					}
				}
			}

		}
		if($_POST["task"] == "UpdateRecords"){
			$xPositions = explode(" ", $_POST["xPositions"]);
			$yPositions = explode(" ", $_POST["yPositions"]);
			$zPositions = explode(" ", $_POST["zPositions"]);
			$xRotations = explode(" ", $_POST["xRotations"]);
			$yRotations = explode(" ", $_POST["yRotations"]);
			$zRotations = explode(" ", $_POST["zRotations"]);
			
			//entries that exist within the assets directory are inserted, those that existed get updated
			$insertMultiQuery = "";
			for ($i = 0; $i < sizeof($arrayOfTruth); $i++) {
				$insertMultiQuery .= "INSERT INTO objects (datapath, posX, posY, posZ, rotX, rotY, rotZ) VALUES ('" . $arrayOfTruth[$i] . "', $xPositions[$i], $yPositions[$i], $zPositions[$i], $xRotations[$i], $yRotations[$i], $zRotations[$i]) ON DUPLICATE KEY UPDATE posX=$xPositions[$i], posY=$yPositions[$i], posZ=$zPositions[$i], rotX=$xRotations[$i], rotY=$yRotations[$i], rotZ=$zRotations[$i];";
			}
			
			if (mysqli_multi_query($con, $insertMultiQuery)) {
				echo "New records created successfully";
			} else {
				echo "Error: " . $insertMultiQuery . "<br>" . mysqli_error($conn);
			}
		}
	}

	//$con->close();
	exit(0);
?>
