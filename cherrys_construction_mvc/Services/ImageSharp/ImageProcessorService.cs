using cherrys_construction_mvc.Services.ImageSharp.Interface;
using cherrys_construction_mvc.Utility;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Metadata;

namespace cherrys_construction_mvc.Services.ImageSharp
{
    public class ImageProcessorService : IImageProcessorService
    {
        public void Resize(IFormFile formFile)
        {
        }

        public async Task<string> UploadImage(IFormFile formFile, IWebHostEnvironment webHostEnvironment, int size)
        {
            if (formFile == null)
            {
                ArgumentNullException argumentNullException = new ArgumentNullException(nameof(formFile));
                throw argumentNullException;
            }
            else
            {
                //set variables
                int quality = 26;
                string wwwRootPath = webHostEnvironment.WebRootPath;
                string newFileName = Guid.NewGuid().ToString();
                var uploadPath = Path.Combine(wwwRootPath, @"images");
                var extension = Path.GetExtension(formFile.FileName);

                //identify image from file path
                //ImageInfo imageInfo = Image.IdentifyAsync(inputStream);

                //load image
                using var image = Image.Load(formFile.OpenReadStream());

                //load metadata
                ImageMetadata metadata = image.Metadata;

                //get aspect ration
                int imageWidth, imageHeight;
                float aspectW = image.Width;
                float aspectH = image.Height;
                float aspectRatio = aspectW / aspectH;
                if (aspectRatio > 1.9)
                {
                    size = StaticDetails.UltrawideImage;
                    quality = 22;
                }
                // Wide Image
                if (aspectW > aspectH)
                {
                    imageWidth = size;
                    imageHeight = Convert.ToInt32(aspectH * size / (double)aspectW);
                }
                // Tall Image
                else
                {
                    imageHeight = size;
                    imageWidth = Convert.ToInt32(aspectW * size / (double)aspectH);
                }

                //resize
                image.Mutate(x => x.Resize(imageWidth, imageHeight));

                //compress
                var encoder = new JpegEncoder()
                {
                    Quality = quality
                };

                //save
                _ = new FileStream(Path.Combine(uploadPath, newFileName + extension), FileMode.Create);         
                await image.SaveAsync(Path.Combine(uploadPath, newFileName + extension), encoder);

                //return
                var newPathUrl = @"\images\" + newFileName + extension;
                return newPathUrl;
            }

        }
    }
}
