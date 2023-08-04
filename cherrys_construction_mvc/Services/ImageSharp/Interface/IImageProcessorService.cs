namespace cherrys_construction_mvc.Services.ImageSharp.Interface
{
    public interface IImageProcessorService
    {
        public Task<string> ProcessImageAsync(IFormFile formFile, IWebHostEnvironment webHostEnv, int size, bool autoRotate = false);
        public void DeleteImage(string webRootPath, string oldImagePath);
    }
}
