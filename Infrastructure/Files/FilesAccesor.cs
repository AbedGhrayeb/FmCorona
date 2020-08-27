using Application.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace Infrastructure.Photos
{
    public class FilesAccesor : IFilesAccessor

    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FilesAccesor(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public string ChangeFile(IFormFile file,string imgUrl,string type)
        {

            if (!string.IsNullOrEmpty(imgUrl))
            {
                string imageToBeDeleted = Path.Combine(_webHostEnvironment.WebRootPath, $"files\\{type}\\", imgUrl);
                if ((File.Exists(imageToBeDeleted)))
                {
                    File.Delete(imageToBeDeleted);
                }
            }

            return UploadFile(file,type);
        }

        public string UploadFile(IFormFile file,string type)
        {
            string uniqueFileName = null;

            if (file != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, $"files\\{type}");
                 uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }       
    }
}
