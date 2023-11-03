using Microsoft.AspNetCore.Mvc;
using MimeKit;

namespace MusicApi.Helpers
{
    public static class FileHelper
    {
        public static async Task<string> UploadFile(IFormFile file)
        {
            string fileName = "";
            string fileId = "";
            try
            {
                var extension = "." + file.FileName.Split(".")[file.FileName.Split(".").Length - 1];
                fileName = Guid.NewGuid().ToString() + extension;

                fileId = fileName.Substring(0, fileName.Length - extension.Length);

                var rootDirectory = Directory.GetCurrentDirectory();
                var filePath = Path.Combine(rootDirectory, "Upload\\Files");

                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                    Console.WriteLine("Directory created successfully");
                }

                var exactPath = Path.Combine(filePath, fileName);

                using (var stream = new FileStream(exactPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);

                    return fileId;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return fileId;
            }
        }

        public static async Task<byte[]> GetImageByImageIdAsync(string fileId)
        {
            var rootDirectory = Directory.GetCurrentDirectory();
            string[] possibleExtensions = new string[] { "png", "jpg", "jpeg" };

            foreach (var extension in possibleExtensions)
            {
                var filePath = Path.Combine(rootDirectory, "Upload\\Files", $"{fileId}.{extension}");

                if (File.Exists(filePath))
                {
                    return await System.IO.File.ReadAllBytesAsync(filePath);
                }
            }

            // Return null or an empty byte array if file not found
            return null;
        }

        public static async Task<byte[]> GetAudioByFileIdAsync(string fileId)
        {
            var rootDirectory = Directory.GetCurrentDirectory();
            string[] possibleExtensions = new string[] { "mp3" };

            foreach (var extension in possibleExtensions)
            {
                var filePath = Path.Combine(rootDirectory, "Upload\\Files", $"{fileId}.{extension}");

                if (File.Exists(filePath))
                {
                    return await System.IO.File.ReadAllBytesAsync(filePath);
                }
            }

            // Return null or an empty byte array if file not found
            return null;
        }
    }
}
