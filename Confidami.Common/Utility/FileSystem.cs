using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confidami.Common.Utility
{
    public class FileSystem
    {
        public static string GetTempUploadFolder()
        {
            string defaultFolfer = Config.UploadsTempFolder;
            return Path.IsPathRooted(defaultFolfer) ? defaultFolfer : Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, defaultFolfer));
        }
        public static string GetFullUploadFolder()
        {
            string defaultFolfer = Config.UploadsFolder;
            return Path.IsPathRooted(defaultFolfer) ? defaultFolfer : Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, defaultFolfer));
        }

        public static void MoveFilesFolder(string sourceFolder, string destinationFolder, bool createIfNotExistsDest = false)
        {
           sourceFolder = Path.IsPathRooted(sourceFolder)? sourceFolder: Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, sourceFolder));
           destinationFolder = Path.IsPathRooted(destinationFolder)? destinationFolder: Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, destinationFolder));

            if(!Directory.Exists(sourceFolder))
                throw new Exception("Source folder " + sourceFolder + " not found");

            if(!Directory.Exists(destinationFolder))
                if(!createIfNotExistsDest)
                    throw new Exception("Destination folder " + destinationFolder + " not found");
                else
                {
                    Directory.CreateDirectory(destinationFolder);
                }

            var files = Directory.GetFiles(sourceFolder);
            foreach (var name in files)
            {
                name.CannotBeNull("name");
                var dest = Path.Combine(destinationFolder, Path.GetFileName(name) );
                File.Move(name,dest);
            }

            Directory.Delete(sourceFolder);
          }


        public static void RemoveFile(string path)
        {
            path.CannotBeNull("path");
            if(!File.Exists(path))
                throw new Exception("Impossbile trovare file: " + path);

            File.Delete(path);
        }
    }
}
