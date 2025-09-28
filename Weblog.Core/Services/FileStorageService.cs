using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weblog.Core.Domain.RepositoryContracts;

namespace Weblog.Core.Services
{
    public class FileStorageService : IFileStorageRepository
    {
        public async Task<string> SaveFileAsync(IFormFile file, string folder)
        {
            if (file == null || file.Length == 0)
                return string.Empty;
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Uploads/PostImages");
            
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var fileLocation = "/" + folder + "/" + fileName;
            return fileLocation;
        }

        public Task DeleteFileAsync(string filePath, string folder)
        {
            if (string.IsNullOrEmpty(filePath)) return Task.CompletedTask;

            var fullPath = Path.Combine("wwwroot/Uploads/PostImages", folder, Path.GetFileName(filePath));
            if (File.Exists(fullPath))
                File.Delete(fullPath);

            return Task.CompletedTask;
        }
    }
}
