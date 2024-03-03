#!/bin/python

import version_system as v
import os
import subprocess

game = "bajtix/ta-ps"
ver = v.input_version()
pth = v.version_path(ver)

def push_build(platform):
    global pth, game, ver

    channel = f"{game}:{ver[0]}-{platform}"
    artifact = f"{pth}/{platform}.zip"

    if not os.path.exists(artifact):
        print(f"Artifact '{artifact}' does not exist!")
        return
    if input(f"Are you sure to push the build '{artifact} to {channel}'?").upper() != "Y":
        return
    
    subprocess.Popen(["butler","push",artifact,channel,"--userversion", v.construct_version(ver)]).wait()


if input(f"Are you sure to push builds for '{v.construct_version(ver)}'?").upper() != "Y":
    quit()


push_build("webugl")
push_build("winx64")
push_build("linx64")
push_build("macx64")
