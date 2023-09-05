namespace mohoShop.Interfaces
{
    public interface IBufferedFileUploadService
    {
        string UploadFile(IFormFile file);
    }
}
