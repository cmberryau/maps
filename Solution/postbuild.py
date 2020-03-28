import sys
import os
import shutil

# perform a file copy from one folder to another
def copyintofolder(src_dir, dst_dir, file_prefix, extension):
    filename = file_prefix + extension

    if os.path.exists(src_dir):
        # create the destination dir if doesnt exist
        if not os.path.exists(dst_dir):
            os.makedirs(dst_dir)


        src_path = os.path.join(src_dir, filename)
        # copy the file into the destination dir
        if os.path.isfile(src_path):
            dst_path = os.path.join(dst_dir, filename)
            shutil.copyfile(src_path, dst_path)
        else:
            print('postbuild.py: Could not find file to copy: ' + src_path)
    else:
        print('postbuild.py: Could not find directory to copy into: ' + src_dir)
        exit(-1)

# perfom a delete from a folder
def deletefromfolder(directory, prefix, extension):
    if not os.path.exists:
        print('postbuild.py: Could not find directory to delete from' + directory)
        return

    fullpath = src_path = os.path.join(directory, prefix + extension)

    if not os.path.isfile(fullpath):
        print('postbuild.py: Could not find file to delete ' + fullpath)
        return

    os.remove(fullpath)


# print usage information
def printusage():
    print('postbuild.py: usage: ' + sys.argv[0] + ' -s source_path -n assembly_name -t target_path [-o (obfuscated?)]')

# on invalid number of arguments, print usage
if len(sys.argv) < 6:
    printusage()
    exit(-1)

source_path = None
assembly_name = None
target_path = None
obfuscated = False

i = 1
# run through arguments and fill info
for arg in sys.argv[i:]:
    if arg == '-s':
        source_path = sys.argv[i+1]
    elif arg == '-n':
        assembly_name = sys.argv[i+1]
    elif arg == '-t':
        target_path = sys.argv[i+1]
    elif arg == '-o':
        obfuscated = True
    i += 1

dest_dir = os.path.join(os.path.dirname(os.path.realpath(__file__)), target_path)

# obfuscated builds don't have a pdb and xml
if not obfuscated:
    copyintofolder(source_path, dest_dir, assembly_name, ".xml")
    copyintofolder(source_path, dest_dir, assembly_name, ".pdb")
#obfuscated builds should remove existing xmls and pdbs
else:
    deletefromfolder(dest_dir, assembly_name, ".pdb")
    deletefromfolder(dest_dir, assembly_name, ".pdb.meta")
    deletefromfolder(dest_dir, assembly_name, ".mdb")
    deletefromfolder(dest_dir, assembly_name, ".mdb.meta")
    deletefromfolder(dest_dir, assembly_name, ".dll.mdb")
    deletefromfolder(dest_dir, assembly_name, ".dll.mdb.meta")

copyintofolder(source_path, dest_dir, assembly_name, ".dll")