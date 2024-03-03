# def get_version():
#     auto_build_id = int((datetime.utcnow() - datetime(2020, 1, 1)).total_seconds())

#     provided = input("Name=")
#     buildtype = ""
#     version = ""
#     subversion = ""
#     build = ""

#     if provided.count('.') >= 1:
#         version_string = provided.split('.')
#     else:
#         version_string = []

#     if len(version_string) == 4:
#         return (version_string[0], version_string[1], version_string[2], version_string[3])
#     elif len(version_string) == 3:
#         return (version_string[0], version_string[1], version_string[2], auto_build_id)
#     elif len(version_string) == 2:

        

# if __main__:
#     v = get_version()
#     print(v)

import re

def extract_version_elements(input_string):
    if input_string is None or input_string.strip() == "":
        return None, None, None, None 

    patterns = [
        re.compile(r'(?P<buildtype>[a-zA-Z]+)?(?P<mainversion>\d+)?\.(?P<subversion>\d+)?\.(?P<build>\d+)?'),
        re.compile(r'(?P<buildtype>[a-zA-Z]+)?(?P<mainversion>\d+)?\.(?P<subversion>\d+)?'),
        re.compile(r'(?P<buildtype>[a-zA-Z]+)?(?P<mainversion>\d+)?'),
        re.compile(r'(?P<buildtype>[a-zA-Z]+)?')
    ]

    for pattern in patterns:
        match = pattern.match(input_string)

        if match:
            buildtype = match.group('buildtype') if match.group('buildtype') is not None else ""
            mainversion = None
            subversion = None
            build = None

            if 'mainversion' in match.groupdict().keys():
                value = match.group('mainversion')
                mainversion = value if value is not None and value.isdigit() else None

            if 'subversion' in match.groupdict().keys():
                value = match.group('subversion')
                subversion = value if  value is not None and value.isdigit() else None

            if 'build' in match.groupdict().keys():
                value = match.group('build')
                build = value if value is not None and value.isdigit() else None

            return buildtype, mainversion, subversion, build

    return None, None, None, None

def construct_version_elements(buildtype, mainversion, subversion, build):
    buildtype = buildtype if buildtype is not None else ""
    if mainversion is None and subversion is None and build is None:
        return buildtype
    if subversion is None and build is None:
        return f"{buildtype}{mainversion}"
    if build is None:
        return f"{buildtype}{mainversion}.{subversion}"

    return f"{buildtype}{mainversion}.{subversion}.{build}"

def construct_version(v):
    return construct_version_elements(v[0], v[1], v[2], v[3])

def input_version():
    from datetime import datetime

    version = None, None, None, None
    auto_build = int((datetime.utcnow() - datetime(2020, 1, 1)).total_seconds())
    while None in version:
        if version[0] is None:
            reply = input(f"( ) BuildType=")
            if reply == "^":
                reply = last_version()
            version = extract_version_elements(reply)
            continue;
        if version[1] is None:
            reply = input(f"({construct_version(version)}) Version=")
            version = extract_version_elements(construct_version(version) + reply)
            continue;
        if version[2] is None:
            reply = input(f"({construct_version(version)}) Subversion=")
            version = extract_version_elements(construct_version(version) + "." + reply)
            continue;
        if version[3] is None:
            reply = input(f"({construct_version(version)}) Build[{auto_build}]=").strip() or str(auto_build)
            version = extract_version_elements(construct_version(version) + "." + reply)
            continue;

    print(f"({construct_version(version)}) Selected!")
    last_version(construct_version(version))
    return version


def last_version(t = None):
    if t is None:
        from os import path
        if not path.exists("last.txt"):
            return None
        with open("last.txt","r") as f:
            return f.read()
    else:
        with open("last.txt","w") as f:
            f.write(t)

def version_path(v):
    return f"../Builds/{v[0]}{v[1]}/{v[2]}/{v[3]}"
    
if __name__ == "__main__":
    input_version()