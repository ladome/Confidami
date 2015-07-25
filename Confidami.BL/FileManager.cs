using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Confidami.Common;
using Confidami.Common.Utility;
using Confidami.Data;
using Confidami.Model;

namespace Confidami.BL
{
    public class FileManager
    {
        private readonly FileRepository _fileRepository;

        public FileManager()
            : this(new FileRepository())
        {
        }

        private FileManager(FileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }


        public int UploadFileInTempFolder(Stream file, string fileName, string contentType, int contentLenght,string parentFolder = null)
        {
            parentFolder.CannotBeNull("userId");
            fileName.CannotBeNull("filename");
            file.CannotBeNull("file");

            UploadFileInFolder(file, fileName, contentType, contentLenght, true, parentFolder);
            var newId = _fileRepository.InsertTempUpload(parentFolder, fileName,contentType,contentLenght);
            return newId;
        }

        public void MoveTempInFinalFolder(string source,string destination)
        {
            source.CannotBeNull("source");
            source.CannotBeNull("destination");

            var pathSource = Path.Combine(FileSystem.GetTempUploadFolder(), source);
            var pathDestination = Path.Combine(FileSystem.GetFullUploadFolder(), destination);
            FileSystem.MoveFilesFolder(pathSource,pathDestination,true);
            _fileRepository.DeleteInTempFile(source);
        }

        public void DeleteTempFile(string sourceFolder,string fileName)
        {
            sourceFolder.CannotBeNull("sourceFolder");
            fileName.CannotBeNull("fileName");

            var path = Path.Combine(FileSystem.GetTempUploadFolder(), Path.Combine(sourceFolder,fileName));
            FileSystem.RemoveFile(path);
        }

        public void DeleteTempAttachment(int id)
        {
            var res = _fileRepository.GetTempAttachmentById(id);
            if (res != null)
            {
                DeleteTempFile(res.UserId, res.FileName);
                _fileRepository.DeleteTempAttachment(id);
            }
        }

        public void DeleteTempAttachment(TempAttachMent tempAttachment)
        {
            tempAttachment.CannotBeNull("tempAttachment");
            tempAttachment.UserId.CannotBeNull("sourceFolder");
            tempAttachment.FileName.CannotBeNull("fileName");

            DeleteTempFile(tempAttachment.UserId, tempAttachment.FileName);
            _fileRepository.DeleteTempAttachment(tempAttachment.Id);
        }

        public void UploadFileInFolder(Stream file, string fileName, string contentType, int contentLenght,bool isTmpFolder = false,string parentFolder =null)
        {
            file.CannotBeNull("file");
            fileName.CannotBeNull("fileName");

            string defaultFolfer = isTmpFolder ? Config.UploadsTempFolder : Config.UploadsFolder;
            var path = Path.IsPathRooted(defaultFolfer)? defaultFolfer: Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, defaultFolfer));

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var dir = new DirectoryInfo(path);

            if (!string.IsNullOrEmpty(parentFolder))
            {
                path= dir.CreateSubdirectory(parentFolder).FullName;
            }

            var buffer = new byte[contentLenght];
            file.Read(buffer,0,contentLenght);
            var fileNameFull = Path.Combine(path, fileName);
            using (var fileStream = new FileStream(fileNameFull, FileMode.Create))
            {
                fileStream.Write(buffer,0,contentLenght);
            }

            var versions = Config.UploadThumbSettings;

            FileSystem.ResizeImage(fileNameFull, path, versions);
        }

        public List<TempAttachMent> GetTempAttachMentsByUserId(string userId)
        {
            return _fileRepository.GetTempAttachmentsByUserId(userId).ToList();
        }

        public TempAttachMent GetTempAttachMentById(int id)
        {
            return _fileRepository.GetTempAttachmentById(id);
        }
    }
}