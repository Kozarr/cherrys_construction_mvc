using cherrys_construction_mvc.Utility;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace cherrys_construction_mvc.Helper
{
    public static class Helper
    {
        public static async Task<string> UploadImageOld(IFormFile imageFile, IWebHostEnvironment webHostEnvironment)
        {
            string wwwRootPath = webHostEnvironment.WebRootPath;

            string newFileName = Guid.NewGuid().ToString();
            var uploadPath = Path.Combine(wwwRootPath, @"images");
            var extension = Path.GetExtension(imageFile.FileName);

            using (var fileStreams = new FileStream(Path.Combine(uploadPath, newFileName + extension), FileMode.Create))
            {
                imageFile.CopyTo(fileStreams);
            }
            string ImageUrl = @"\images\" + newFileName + extension;
            return ImageUrl;

        }

        public static void DeleteImage(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public static async Task<byte[]> IFormFileToBytes(IFormFile image)
        {
            using (var ms = new MemoryStream())
            {
                image.CopyTo(ms);
                var fileBytes = ms.ToArray();
                return fileBytes;
            }
        }

        public static async Task<IFormFile> BytesToIFormFile(byte[] imageBytes, string imageName, string fileName)
        {
            var stream = new MemoryStream(imageBytes);
            IFormFile returnFile = new FormFile(stream, 0, imageBytes.Length, imageName, fileName);
            return returnFile;
        }

        public static async Task<string> UploadImage(IFormFile FormFileImage, IWebHostEnvironment webHostEnvironment, int size)
        {
            string wwwRootPath = webHostEnvironment.WebRootPath;
            string newFileName = Guid.NewGuid().ToString();
            var uploadPath = Path.Combine(wwwRootPath, @"images");
            var extension = Path.GetExtension(FormFileImage.FileName);
            int quality = 90;
            var file = await IFormFileToBytes(FormFileImage);

            if (file == null)
            {
                ArgumentNullException argumentNullException = new ArgumentNullException(nameof(file));
                throw argumentNullException;
            }
            else
            {
                using (var ms = new MemoryStream(file))
                {
                    Bitmap bitmap = new(System.Drawing.Image.FromStream(ms));
                    using var image = bitmap;
                    float aspectW = image.Width;
                    float aspectH = image.Height;
                    float aspectRatio = aspectW / aspectH;
                    if (aspectRatio > 1.9)
                    {
                        size = StaticDetails.UltrawideImage;
                        quality = 80;
                    }
                    int width, height;
                    //Wide Image
                    if (image.Width > image.Height)
                    {
                        width = size;
                        height = Convert.ToInt32(image.Height * size / (double)image.Width);
                    }
                    //Tall Image
                    else
                    {
                        width = Convert.ToInt32(image.Width * size / (double)image.Height);
                        height = size;
                    }
                    var resized = new Bitmap(width, height);
                    using var graphics = Graphics.FromImage(resized);
                    graphics.CompositingQuality = CompositingQuality.HighSpeed;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.CompositingMode = CompositingMode.SourceCopy;
                    graphics.DrawImage(image, 0, 0, width, height);

                    using (var output = File.Open(Path.Combine(uploadPath, newFileName + extension), FileMode.Create))
                    {
                        var qualityParamId = Encoder.Quality;
                        var encoderParameters = new EncoderParameters(1);
                        encoderParameters.Param[0] = new EncoderParameter(qualityParamId, quality);
                        var codec = ImageCodecInfo.GetImageDecoders().FirstOrDefault(codec => codec.FormatID == ImageFormat.Jpeg.Guid);
                        // if this below has issues when hosted, then it needs to be switched to a FileStream CopyTo method
                        resized.Save(output, codec, encoderParameters);

                        string ImageUrl = @"\images\" + newFileName + extension;
                        return ImageUrl;
                    }

                }
            }
        }

    }
}