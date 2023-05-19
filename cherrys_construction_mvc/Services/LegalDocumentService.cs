using AutoMapper;
using cherrys_construction_mvc.EfRepository.Interfaces;
using cherrys_construction_mvc.Interfaces;
using cherrys_construction_mvc.Models;
using cherrys_construction_mvc.ViewModels.Requests;
using cherrys_construction_mvc.ViewModels.Responce;

namespace cherrys_construction_mvc.Services
{
    public class LegalDocumentService: ILegalDocumentService
    {
        private readonly ILogger<LegalDocumentService> _logger;
        private readonly IEfRepository<LegalDocument> _legalDocumentRepository;
        private readonly IMapper _mapper;

        public LegalDocumentService(IEfRepository<LegalDocument> legalDocument,
            IMapper mapper,
            ILogger<LegalDocumentService> logger)
        {
            _legalDocumentRepository = legalDocument;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task CreateLegalDocumentAsync(LegalDocumentRequest request)
        {
            var doc = _mapper.Map<LegalDocument>(request);
            await _legalDocumentRepository.AddAsync(doc);
            await _legalDocumentRepository.SaveChangesAsync();
        }

        public async Task DeleteLegalDocumentAsync(int id)
        {
            var doc = await _legalDocumentRepository.GetByIdAsync(id);
            if(doc == null)
            {
                _logger.LogWarning("Could not find existing legal document in - LegalDocument Service");
            }
            else
            {
                await _legalDocumentRepository.DeleteAsync(doc);
                await _legalDocumentRepository.SaveChangesAsync();
            }
        }

        public async Task<LegalDocumentResponce> GetLegalDocumentByIdAsync(int id)
        {
            var doc = await _legalDocumentRepository.GetByIdAsync(id);
            return _mapper.Map<LegalDocumentResponce>(doc);
        }

        public async Task<IEnumerable<LegalDocumentResponce>> GetLegalDocumentsAsync()
        {
            var docs = await _legalDocumentRepository.ListAsync();
            return _mapper.Map<IEnumerable<LegalDocumentResponce>>(docs);
        }

        public async Task UpdateLegalDocumentAsync(int id, LegalDocumentRequest request)
        {
            var doc = await _legalDocumentRepository.GetByIdAsync(id);
            if(doc == null)
            {
                _logger.LogWarning("Could not find existing legal document in - LegalDocument Service");
            }
            else
            {
                _mapper.Map(request, doc);
                await _legalDocumentRepository.UpdateAsync(doc);
                await _legalDocumentRepository.SaveChangesAsync();
            }       
        }
    }
}
