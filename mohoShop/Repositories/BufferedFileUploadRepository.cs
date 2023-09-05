using mohoShop.Interfaces;

namespace mohoShop.Repositories
{
    public class BufferedFileUploadRepository : IBufferedFileUploadService
    {
        private readonly string[] AllowedImageExtensions = { ".jpg", ".jpeg", ".png", ".gif" };

        public string UploadFile(IFormFile file)
        {
            try
            {
                if (file.Length > 0)
                {
                    // Validate the content type to ensure it's an image
                    if (!IsImageContentType(file.ContentType))
                    {
                        return null; // Invalid content type
                    }

                    // Generate a unique file name
                    // var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

                    // Specify the folder path
                    var path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "Assets", "Images"));

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    // Save the file with the unique name
                    using (var fileStream = new FileStream(Path.Combine(path, file.FileName), FileMode.Create))
                    {
                        file.CopyToAsync(fileStream);
                    }

                    // Return the path to the saved file
                    return Path.Combine("Assets", "Images", file.FileName);
                }
                else
                {
                    // File is empty
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("File Copy Failed", ex);
            }
        }

        private bool IsImageContentType(string contentType)
        {
            // You can add more image content types as needed
            return contentType.StartsWith("image/", StringComparison.OrdinalIgnoreCase);
        }
    }
}
