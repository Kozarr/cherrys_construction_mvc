namespace cherrys_construction_mvc.Services.ImageSharp.Interface
{
    public interface IImageProcessorService
    {
        public void Resize(IFormFile formFile);
        public Task<string> UploadImage(IFormFile formFile, IWebHostEnvironment webHostEnvironment, int size);
    }
}
