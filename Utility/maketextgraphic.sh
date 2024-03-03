#!/bin/sh

convert -background none -fill white -font "./Assets/Library 3 am.otf" -pointsize 128 label:"$1" "./Recordings/tmp.png"

convert "./Recordings/tmp.png" -gravity center -background none -extent 2200x160 "./Recordings/$1.png"
