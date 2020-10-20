using EduBIN.Utility.Dependencies;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EduBIN.BLL.Services
{
    public class FileService
    {
        private readonly string FileDir = "Storage/";
        private readonly string Files = "Files";
        private readonly string Images = "Images";
        private const int ImageSize = 200 * 1024; //200KB
        
        public Dictionary<int, string> UploadImage(IFormFile ImageFile)
        {
            string error = null;
            string uniqueFileName;
            Dictionary<int, string> keyValues = new Dictionary<int, string>();

            if (ImageFile != null)
            {
                string uploadFolder = Path.Combine(FileDir, "Images");
                string extension = Path.GetExtension(ImageFile.FileName);
                uniqueFileName = Guid.NewGuid().ToString() + "_" + ImageFile.FileName;
                if (IsValidImageExtenstion(extension))
                {
                    var fileSize = ImageFile.Length;
                    if (fileSize < ImageSize)
                    {
                        Save(uploadFolder, uniqueFileName, ImageFile);
                        keyValues.Add(1, uniqueFileName);
                    }
                    else error = Messages.InvalidImageSize;
                }
                else error = Messages.InvaldImageExtrension;
            }
            keyValues.Add(2, error);
            return keyValues;
        }

        public Dictionary<int, string> UploadFile(IFormFile File)
        {
            string error = null;
            string uniqueFileName;
            Dictionary<int, string> keyValues = new Dictionary<int, string>();
            
            if (File != null)
            {
                string extension = Path.GetExtension(File.FileName);
                string uploadFolder = Path.Combine(FileDir, Files);
                uniqueFileName = Guid.NewGuid().ToString() + "_" + File.FileName;

                if (IsValidFileExtenstion(extension))
                {
                    Save(uploadFolder, uniqueFileName, File);
                    keyValues.Add(1, uniqueFileName);
                }
                else error = Messages.InvaldFileExtrension;
            }
            keyValues.Add(2, error);
            return keyValues;
        }

        public string Delete(string FilePath)
        {
            if (FilePath != null)
            {
                string uploadImageUrl = Path.Combine(FileDir, Images, FilePath);
                string uploadFileUrl = Path.Combine(FileDir, Files, FilePath);

                if (File.Exists(uploadFileUrl))
                {
                    return DeleteUrl(uploadFileUrl) ? null : Messages.Issue;
                }
                else if (File.Exists(uploadImageUrl))
                {
                    return DeleteUrl(uploadImageUrl) ? null : Messages.Issue;
                }
                else return Messages.NotFound;
            }
            return Messages.Empty;
        }
       
        private void Save(string uploadFolder, string uniqueFileName, IFormFile File)
        {
            string filePath = Path.Combine(uploadFolder, uniqueFileName);
            FileStream createFile = new FileStream(filePath, FileMode.Create);
            using (createFile)
            {
                File.CopyTo(createFile);
            }
        }
        
        private bool IsValidImageExtenstion(string extension)
        {
            return (extension.ToLower() == ".jpg" || extension.ToLower() == ".png" || extension.ToLower() == ".jpeg") ?
                true : false;
        }
        
        private bool IsValidFileExtenstion(string extension)
        {
            return (extension.ToLower() == ".docx" || extension.ToLower() == ".pdf" || extension.ToLower() == ".txt") ?
               true : false;
        }
        
        private bool DeleteUrl(string path)
        {
            File.Delete(path);
            return true;
        }

    }
}
