using cherrys_construction_mvc.Services.ImageSharp.Interface;
using cherrys_construction_mvc.Utility;
using Microsoft.AspNetCore.Hosting;
using SixLabors.ImageSharp.Formats.Jpeg;
using System.Security.Cryptography;

namespace cherrys_construction_mvc.Services.ImageSharp
{
    public class ImageProcessorService : IImageProcessorService
    {
        public void DeleteImage(string webRootPath, string oldImagePath)
        {
            var oldPath = Path.Combine(webRootPath, oldImagePath.TrimStart('\\'));
            if (File.Exists(oldPath))
            {
                File.Delete(oldPath);
            }
        }

        public async Task<string> ProcessImageAsync(IFormFile formFile, IWebHostEnvironment webEnv, int size, bool autoRotate = false)
        {
            if (formFile == null)
            {
                ArgumentNullException argumentNullException = new(nameof(formFile));
                throw argumentNullException;
            }
            else
            {
                //load image
                using Image image = Image.Load(formFile.OpenReadStream());

                //orientation
                if (autoRotate)
                {
                    image.Mutate(x => x.AutoOrient());
                }

                //default compression quality
                int quality = 30;

                //get aspect ration
                int imageWidth, imageHeight;
                float aspectW = image.Width;
                float aspectH = image.Height;
                float aspectRatio = aspectW / aspectH;

                // path variables
                string wwwRootPath = webEnv.WebRootPath;
                string newFileName = Guid.NewGuid().ToString();
                var uploadPath = Path.Combine(wwwRootPath, @"images");
                var extension = Path.GetExtension(formFile.FileName);

                // ultra-wide image
                if (aspectRatio > 1.9)
                {
                    quality = 25;
                    size = StaticDetails.UltrawideImage;
                }

                // wide image
                if (aspectW > aspectH)
                {
                    imageWidth = size;
                    imageHeight = Convert.ToInt32(aspectH * size / (double)aspectW);
                }

                // tall image
                else
                {
                    imageHeight = size;
                    imageWidth = Convert.ToInt32(aspectW * size / (double)aspectH);
                }

                //resize
                image.Mutate(x => x.Resize(imageWidth, imageHeight));             

                //create encoder
                var encoder = new JpegEncoder()
                {
                    Quality = quality
                };

                //compress and save
                await image.SaveAsync(Path.Combine(uploadPath, newFileName + extension), encoder);

                //return image url
                return @"\images\" + newFileName + extension;
            }
        }
    }
}
