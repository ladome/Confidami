using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Confidami.Common;
using Confidami.Common.Utility;

namespace Confidami.BL
{
    public class Validation
    {
        private readonly FileManager _fileManager;

        public Validation(FileManager fileManager)
        {
            _fileManager = fileManager;
        }

        public Validation() : this(new FileManager())
        {
            
        }

        private FileManager FileManager
        {
            get
            {
                if(_fileManager == null)
                    return new FileManager();
                return _fileManager;
            }
        }

        public bool FileAlreadyExists(string folder, string filename)
        {
            folder.CannotBeNull("folder");
            filename.CannotBeNull("filename");

            var res = FileManager.GetTempAttachMentsByUserId(folder);
            return res.Any(x=> String.Equals(x.FileName, filename, StringComparison.CurrentCultureIgnoreCase));
        }

        public bool IsAdmittedExtension(string extension)
        {
            extension.CannotBeNullEmptyOrWithespace("extension");
            extension = extension.RemoveExtensionPoint();

            return Config.AcceptedExtensions.Any(x => x.Contains(extension));
        }

        public ErrorCodeStore ErrorCodes
        {
            get { return new ErrorCodeStore();}
        }




    }


    public class ValidationHelper
    {

    }
}
