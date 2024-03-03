#!/bin/python

import version_system as v
import os
from zipfile import ZipFile, ZIP_DEFLATED


def get_all_file_paths(directory): 
    file_paths = [] 
    for root, directories, files in os.walk(directory): 
        for filename in files: 
            filepath = os.path.join(root, filename) 
            file_paths.append(filepath) 
    return file_paths 

def zip_if_exists(version):
    global basepath

    pth = basepath + "/" + version
    print(f"Zip '{version}' ({pth})")
    if not os.path.exists(pth):
        print(f"No directory for {version}")
        return
    if len(os.listdir(pth)) == 0:
        print(f"Empty directory for {version}")
        return

    file_paths = get_all_file_paths(pth) 
  
    # writing files to a zipfile 
    with ZipFile(pth + ".zip",'w') as zip: 
        # writing each file one by one 
        for file in file_paths: 
            arcname = file.replace(pth + "/", "")
            print("Zipping: " + arcname)
            zip.write(file, arcname, ZIP_DEFLATED)
  
    print('All files zipped successfully!')       



build = v.input_version()
basepath = v.version_path(build)

if input(f"Are you sure to make create zip archives of '{basepath}'?").upper() != "Y":
    quit()

zip_if_exists("webugl")
zip_if_exists("winx64")
zip_if_exists("linx64")
zip_if_exists("macx64")