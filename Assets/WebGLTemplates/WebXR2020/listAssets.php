<?php

$content = scandir(__DIR__ . "/assets");

$counter = 0;

foreach ($content as $item) {
    if (!is_dir($item)) {
        if($counter == 0) {
            echo $item;
            $counter++;
        } else {
            echo " " . $item;
        }
    }
}
$counter = 0;
?>