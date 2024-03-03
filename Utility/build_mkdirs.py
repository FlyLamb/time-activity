#!/bin/python

import version_system as v
import os


build = v.input_version()
pth = v.version_path(build)

if input(f"Are you sure to make create the directories at '{pth}'?").upper() != "Y":
    quit()

os.makedirs(pth, exist_ok=True)
os.mkdir(pth + "/linx64")
os.mkdir(pth + "/winx64")
os.mkdir(pth + "/macx64")
os.mkdir(pth + "/webugl")

