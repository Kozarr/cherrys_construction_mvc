
using AutoMapper;
using cherrys_construction_mvc.EfRepository.Interfaces;
using cherrys_construction_mvc.Interfaces;
using cherrys_construction_mvc.Models;
using cherrys_construction_mvc.Utility;
using cherrys_construction_mvc.ViewModels.Requests;
using cherrys_construction_mvc.ViewModels.Responce;
using Microsoft.AspNetCore.Hosting;
using System.Drawing;

namespace cherrys_construction_mvc.Services
{
    public class MemberService : IMemberService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IEfRepository<Member> _memberRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<MemberService> _logger;
        public MemberService(IEfRepository<Member> memberRepository, 
            IMapper mapper, 
            IWebHostEnvironment webHostEnvironment,
            ILogger<MemberService> logger)
        {
            _memberRepository = memberRepository;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }
        public async Task CreateMemberAsync(MemberRequest request)
        {
            if (request.Image != null)
            {           
                request.ImageLink = await Helper.Helper.UploadImage(request.Image, _webHostEnvironment, StaticDetails.SquareImage);
            }

            var member = _mapper.Map<Member>(request);

            await _memberRepository.AddAsync(member);
            await _memberRepository.SaveChangesAsync();
        }

        public async Task DeleteMemberAsync(int memberId)
        {
            var member = await _memberRepository.GetByIdAsync(memberId);
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            if(member == null)
            {
                _logger.LogWarning("Could not find member to delete from existing ID");
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(member.ImageLink))
                {
                    var oldImagePath = Path.Combine(wwwRootPath, member.ImageLink.TrimStart('\\'));
                    if (oldImagePath != null)
                    {
                        if (File.Exists(oldImagePath))
                        {
                            File.Delete(oldImagePath);
                        }
                    }
                }               
                await _memberRepository.DeleteAsync(member);
                await _memberRepository.SaveChangesAsync();
            }   
        }

        public async Task<MemberResponce> GetMemberByIdAsync(int memberId)
        {
            var member = await _memberRepository.GetByIdAsync(memberId);
            return _mapper.Map<MemberResponce>(member);
        }

        public async Task<IEnumerable<MemberResponce>> GetMemberssAsync()
        {
            var members = await _memberRepository.ListAsync();
            return _mapper.Map<IEnumerable<MemberResponce>>(members);
        }

        public async Task UpdateMemberAsync(int memberId, MemberRequest request)
        {
            var member = await _memberRepository.GetByIdAsync(memberId);
            if(member != null)
            {
                if (request.Image != null)
                {
                    if (!string.IsNullOrWhiteSpace(member.ImageLink))
                    {
                        string wwwRootPath = _webHostEnvironment.WebRootPath;
                        var oldImagePath = Path.Combine(wwwRootPath, member.ImageLink.TrimStart('\\'));
                        if (File.Exists(oldImagePath))
                        {
                            File.Delete(oldImagePath);
                        }
                    }                  
                    request.ImageLink = await Helper.Helper.UploadImage(request.Image, _webHostEnvironment, StaticDetails.SquareImage);
                }
                else
                {
                    request.ImageLink = member.ImageLink;
                }
                _mapper.Map(request, member);
                await _memberRepository.UpdateAsync(member);
                await _memberRepository.SaveChangesAsync();
            }
            else
            {
                _logger.LogWarning("Could not find existing member in Member Service");
            }
            
        }
    }
}
