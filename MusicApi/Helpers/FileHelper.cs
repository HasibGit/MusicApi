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
    }
}
