#!/bin/python

from datetime import datetime
import os

buildtype = input("TYPE=")
version = input("MAJOR=")
subversion = input("SUB=")
build = int((datetime.utcnow() - datetime(2020, 1, 1)).total_seconds())

print("NAME=" + buildtype + str(version) + "." + str(subversion) + "." + str(build))

input("any key to proceed")
pth = "./Builds/" + buildtype + str(version) + "/" + str(subversion) + "/" + str(build)

os.makedirs(pth, exist_ok=True)
os.mkdir(pth + "/linx64")
os.mkdir(pth + "/winx64")
os.mkdir(pth + "/macx64")
os.mkdir(pth + "/webugl")

