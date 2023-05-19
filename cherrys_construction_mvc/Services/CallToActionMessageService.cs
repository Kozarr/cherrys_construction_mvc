using AutoMapper;
using cherrys_construction_mvc.EfRepository.Interfaces;
using cherrys_construction_mvc.Interfaces;
using cherrys_construction_mvc.Models;
using cherrys_construction_mvc.ViewModels.Requests;
using cherrys_construction_mvc.ViewModels.Responce;

namespace cherrys_construction_mvc.Services
{
    public class CallToActionMessageService : ICallToActionMessage
    {
        private readonly IEfRepository<CallToActionMessage> _callMessageRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CallToActionMessageService> _logger;
        public CallToActionMessageService(IEfRepository<CallToActionMessage> callMessageRepository,
            IMapper mapper,
            ILogger<CallToActionMessageService> logger)
        {
            _callMessageRepository = callMessageRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task CreateCallToActionMessageAsync(CallToActionMessageRequest request)
        {
            var message = _mapper.Map<CallToActionMessage>(request);
            await _callMessageRepository.AddAsync(message);
            await _callMessageRepository.SaveChangesAsync();
        }

        public async Task DeleteCallToActionMessageAsync(int id)
        {
            var message = await _callMessageRepository.GetByIdAsync(id);
            if (message == null)
            {
                _logger.LogWarning("Could not find existing call to action message in - CallToActionMessage Service");
            }
            else
            {
                await _callMessageRepository.DeleteAsync(message);
                await _callMessageRepository.SaveChangesAsync();
            }
        }

        public async Task<CallToActionMessageResponce> GetCallToActionMessageByIdAsync(int id)
        {
            var message = await _callMessageRepository.GetByIdAsync(id);
            return _mapper.Map<CallToActionMessageResponce>(message);
        }

        public async Task<IEnumerable<CallToActionMessageResponce>> GetCallToActionMessagesAsync()
        {
            var messages = await _callMessageRepository.ListAsync();
            return _mapper.Map<IEnumerable<CallToActionMessageResponce>>(messages);
        }

        public async Task UpdateCallToActionMessageAsync(int id, CallToActionMessageRequest request)
        {
            var message = await _callMessageRepository.GetByIdAsync(id);

            if (message != null)
            {
                _mapper.Map(message, request);
                await _callMessageRepository.UpdateAsync(message);
                await _callMessageRepository.SaveChangesAsync();
            }
            else
            {
                _logger.LogWarning("Could not find existing call to action message in - CallToActionMessage Service");
            }
        }
    }
}
