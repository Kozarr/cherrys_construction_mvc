using AutoMapper;
using cherrys_construction_mvc.EfRepository.Interfaces;
using cherrys_construction_mvc.Interfaces;
using cherrys_construction_mvc.Models;
using cherrys_construction_mvc.ViewModels.Requests;
using cherrys_construction_mvc.ViewModels.Responce;

namespace cherrys_construction_mvc.Services
{
    public class CallToActionSettingService : ICallToActionSetting
    {
        private readonly ILogger<CallToActionSettingService> _logger;
        private readonly IEfRepository<CallToActionSetting> _callSettingRepository;
        private readonly IMapper _mapper;
        public CallToActionSettingService(IEfRepository<CallToActionSetting> callSettingRepository, 
            IMapper mapper,
            ILogger<CallToActionSettingService> logger)
        {
            _callSettingRepository = callSettingRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task CreateCallToActionSettingAsync(CallToActionSettingRequest request)
        {
            var setting = _mapper.Map<CallToActionSetting>(request);
            await _callSettingRepository.AddAsync(setting);
            await _callSettingRepository.SaveChangesAsync();
        }

        public async Task DeleteCallToActionSettingAsync(int id)
        {
            var setting = await _callSettingRepository.GetByIdAsync(id);
            if(setting != null)
            {
                await _callSettingRepository.DeleteAsync(setting);
                await _callSettingRepository.SaveChangesAsync();
            }
            else
            {
                _logger.LogWarning("Could not find existing call to action settings in - CallToActionSetting Service");
            }           
        }

        public async Task<CallToActionSettingResponce> GetCallToActionSettingByIdAsync(int id)
        {
            var setting = await _callSettingRepository.GetByIdAsync(id);
            return _mapper.Map<CallToActionSettingResponce>(setting);
        }

        public async Task<IEnumerable<CallToActionSettingResponce>> GetCallToActionSettingsAsync()
        {
            var settings = await _callSettingRepository.ListAsync();
            return _mapper.Map<IEnumerable<CallToActionSettingResponce>>(settings);
        }

        public async Task UpdateCallToActionSettingAsync(int id, CallToActionSettingRequest request)
        {
            var setting = await _callSettingRepository.GetByIdAsync(id);
            if (setting != null)
            {
                _mapper.Map(request, setting);
                await _callSettingRepository.UpdateAsync(setting);
                await _callSettingRepository.SaveChangesAsync();
            }
            else
            {
                _logger.LogWarning("Could not find existing call to action settings in - CallToActionSetting Service");
            }
        }
    }
}
