using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using PsuHistory.Common.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace PsuHistory.Business.Service.Helpers
{
    public class FileHelper
    {
        public IHostingEnvironment hostingEnvironment;

        public FileHelper(IHostingEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;
        }

        public virtual async Task<List<FileModel>> SaveFileRange(ICollection<IFormFile> saveFiles)
        {
            List<FileModel> fileModels = new List<FileModel>();

            foreach(var file in saveFiles)
            {
                fileModels.Add(await SaveFile(file));
            }

            return fileModels;
        }

        public virtual async Task<FileModel> SaveFile(IFormFile saveFile)
        {
            FileModel fileModel = null;

            if (saveFile != null)
            {
                fileModel = new FileModel();

                fileModel.FilePath = "/Files/";
                fileModel.FileName = Guid.NewGuid().ToString();
                fileModel.FileType = saveFile.ContentType.Split('/')[1];

                var fullPath = fileModel.FilePath + fileModel.FileName + "." + fileModel.FileType;

                using (var fileStream = new FileStream(hostingEnvironment.WebRootPath + fullPath, FileMode.Create))
                {
                    await saveFile.CopyToAsync(fileStream);
                }
            }

            return fileModel;
        }

        public virtual void DeleteFile(string fullPath)
        {
            File.Delete(fullPath);
        }
    }
}
