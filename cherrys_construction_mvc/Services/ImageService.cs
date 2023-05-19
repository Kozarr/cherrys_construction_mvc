using AutoMapper;
using cherrys_construction_mvc.EfRepository.Interfaces;
using cherrys_construction_mvc.Interfaces;
using cherrys_construction_mvc.Models;
using cherrys_construction_mvc.Specification.ProjectImageSpec;
using cherrys_construction_mvc.ViewModels.Requests;
using cherrys_construction_mvc.ViewModels.Responce;

namespace cherrys_construction_mvc.Services
{
    public class ImageService : IImageService
    {
        private readonly IEfRepository<ImageModel> _imageRepository;
        private readonly IMapper _mapper;
        public ImageService(IEfRepository<ImageModel> imageRepository, IMapper mapper)
        {
            _imageRepository = imageRepository;
            _mapper = mapper;   
        }
        public async Task CreateImageAsync(ImageRequest request)
        {
            var image = _mapper.Map<ImageModel>(request);

            await _imageRepository.AddAsync(image);
            await _imageRepository.SaveChangesAsync();
        }

        public async Task DeleteImageAsync(int imageId)
        {
            var image = await _imageRepository.GetByIdAsync(imageId);
            if (image != null)
            {
                await _imageRepository.DeleteAsync(image);
                await _imageRepository.SaveChangesAsync();
            }
        }

        public async Task<ImageResponce> GetByIdImage(int imageId)
        {
            var image = await _imageRepository.GetByIdAsync(imageId);
            return _mapper.Map<ImageResponce>(image);
        }

        public async Task<IEnumerable<ImageResponce>> GetByProjectIdImage(int projectId)
        {
            var spec = new ProjectImageByProjectId(projectId);
            var images = await _imageRepository.ListAsync(spec);
            return _mapper.Map<IEnumerable<ImageResponce>>(images);

        }
    }
}
