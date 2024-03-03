#!/bin/python

import subprocess
import version_system as v

build = v.input_version()

strver = v.construct_version(build)
message = input("Message=").strip() or "No description"

if input(f"Are you sure to create a git tag '{strver}'?").upper() != "Y":
    quit()

subprocess.Popen(["git","tag","-a",strver,"-m",message])

if input(f"Do you want to push '{strver}'?").upper() != "Y":
    quit()

subprocess.Popen(["git","push","origin",strver])

print("All set!")