﻿using Microsoft.AspNetCore.Http;

namespace Application.Interfaces
{
    public interface IFilesAccessor
    {
        public string UploadFile(IFormFile file);
        public string ChangeFile(IFormFile file,string fileUrl);
    }
}
