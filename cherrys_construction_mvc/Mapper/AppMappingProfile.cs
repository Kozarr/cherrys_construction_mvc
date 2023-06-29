using AutoMapper;
using cherrys_construction_mvc.Models;
using cherrys_construction_mvc.ViewModels.Requests;
using cherrys_construction_mvc.ViewModels.Responce;

namespace cherrys_construction_mvc.Mapper
{
    public class AppMappingProfile : Profile
    {

        public AppMappingProfile()
        {
            #region Project

            _ = CreateMap<Project, ProjectResponce>()
                .ForPath(a => a.ProjectServiceType.Title, t => t.MapFrom(t => t.ServiceType.Title))
                .ForPath(a => a.ProjectServiceType.Id, t => t.MapFrom(t => t.ServiceType.Id))
                .ForPath(a => a.ProjectServiceType.IconImage, t => t.MapFrom(t => t.ServiceType.IconImage))
                .ForPath(a => a.ProjectServiceType.Description, t => t.MapFrom(t => t.ServiceType.Description))
                .ForPath(a => a.ProjectServiceType.PageDescription, t => t.MapFrom(t => t.ServiceType.PageDescription))
                .ForPath(a => a.ProjectServiceType.PageTitle, t => t.MapFrom(t => t.ServiceType.PageTitle))
                .ForPath(a => a.ProjectTestimony.Id, t => t.MapFrom(t => t.Testimony.Id))
                .ForPath(a => a.ProjectTestimony.Name, t => t.MapFrom(t => t.Testimony.Name))
                .ForPath(a => a.ProjectTestimony.Description, t => t.MapFrom(t => t.Testimony.Description))
                .ForPath(a => a.ProjectTestimony.Stars, t => t.MapFrom(t => t.Testimony.Stars))

                .ForPath(a => a.ProjectTestimony.ImageLink, t => t.MapFrom(t => t.Testimony.ImageLink))
                .ForPath(a => a.ProjectTestimony.Position, t => t.MapFrom(t => t.Testimony.Position))
                .ForPath(s => s.ProjectTags, t => t.MapFrom(a => a.ProjectTags))
                ;
            //_ = CreateMap<ProjectResponce, Project>();
            //.ForPath(a => a.ProjectTestimony.ProjectId, t => t.MapFrom(t => t.Testimony.ProjectId))
            _ = CreateMap<Project, ProjectRequest>();
            _ = CreateMap<ProjectRequest, Project>();


            #endregion
            #region Testimony

            _ = CreateMap<Testimony, TestimonyResponce>().
                ForPath(a => a.Project.Title, t => t.MapFrom(t => t.Project.Title));


            _ = CreateMap<Testimony, TestimonyRequest>();
            _ = CreateMap<TestimonyRequest, Testimony>();


            #endregion
            #region Image

            _ = CreateMap<ImageModel, ImageRequest>();
            _ = CreateMap<ImageRequest, ImageModel>();
            _ = CreateMap<ImageModel, ImageResponce>().ForMember(a => a.PathImage, o => o.MapFrom(s => s.PathImage));



            #endregion
            #region Member

            _ = CreateMap<Member, MemberResponce>();
            _ = CreateMap<MemberResponce, Member>();

            _ = CreateMap<Member, MemberRequest>();
            _ = CreateMap<MemberRequest, Member>();


            #endregion
            #region CompanyCertificate

            _ = CreateMap<CompanyCertificate, CompanyCertificateResponce>();
            _ = CreateMap<CompanyCertificateResponce, CompanyCertificate>();


            _ = CreateMap<CompanyCertificate, CompanyCertificateRequest>();
            _ = CreateMap<CompanyCertificateRequest, CompanyCertificate>();

            #endregion
            #region CompanyValue

            _ = CreateMap<CompanyValue, CompanyValueResponce>();
            _ = CreateMap<CompanyValueResponce, CompanyValue>();


            _ = CreateMap<CompanyValue, CompanyValueRequest>();
            _ = CreateMap<CompanyValueRequest, CompanyValue>();

            #endregion
            #region ServiceType

            _ = CreateMap<ServiceType, ServiceTypeResponce>();
            _ = CreateMap<ServiceTypeResponce, ServiceType>();


            _ = CreateMap<ServiceType, ServiceTypeRequest>();
            _ = CreateMap<ServiceTypeRequest, ServiceType>();

            #endregion
            #region Service

            _ = CreateMap<Service, ServiceResponce>();
            _ = CreateMap<ServiceResponce, Service>();


            _ = CreateMap<Service, ServiceRequest>();
            _ = CreateMap<ServiceRequest, Service>();

            #endregion
            #region CompanyStory

            _ = CreateMap<CompanyStory, CompanyStoryRequest>();
            _ = CreateMap<CompanyStoryRequest, CompanyStory>();


            _ = CreateMap<CompanyStory, CompanyStoryResponce>();
            _ = CreateMap<CompanyStoryResponce, CompanyStory>();

            #endregion
            #region HeroSliderImage

            _ = CreateMap<HeroSliderImage, HeroSliderImageRequest>();
            _ = CreateMap<HeroSliderImageRequest, HeroSliderImage>();


            _ = CreateMap<HeroSliderImage, HeroSliderImageResponce>();
            _ = CreateMap<HeroSliderImageResponce, HeroSlider>();

            #endregion
            #region HeroSlider

            _ = CreateMap<HeroSlider, HeroSliderResponce>();
            _ = CreateMap<HeroSliderResponce, HeroSlider>();

            _ = CreateMap<HeroSlider, HeroSliderRequest>();
            _ = CreateMap<HeroSliderRequest, HeroSlider>();
            #endregion
            #region CompanyQuality

            _ = CreateMap<CompanyQuality, CompanyQualityResponce>();
            _ = CreateMap<CompanyQualityResponce, CompanyQuality>();

            _ = CreateMap<CompanyQuality, CompanyQualityRequest>();
            _ = CreateMap<CompanyQualityRequest, CompanyQuality>();
            #endregion
            #region Tag

            _ = CreateMap<Tag, TagRequest>();
            _ = CreateMap<TagRequest, Tag>();

            _ = CreateMap<Tag, TagResponce>();
            _ = CreateMap<TagResponce, Tag>();
            #endregion
            #region ProjectTag

            _ = CreateMap<ProjectTag, ProjectTagRequest>();
            _ = CreateMap<ProjectTagRequest, ProjectTag>();

            _ = CreateMap<ProjectTag, ProjectTagResponce>();
            _ = CreateMap<ProjectTagResponce, ProjectTag>();

            #endregion
            #region CompanyInfo

            _ = CreateMap<CompanyInfo, CompanyInfoRequest>();
            _ = CreateMap<CompanyInfoRequest, CompanyInfo>();

            _ = CreateMap<CompanyInfo, CompanyInfoResponce>();
            _ = CreateMap<CompanyInfoResponce, CompanyInfo>();

            #endregion
            #region CallSettings

            _ = CreateMap<CallToActionSetting, CallToActionSettingRequest>();
            _ = CreateMap<CallToActionSettingRequest, CallToActionSetting>();

            _ = CreateMap<CallToActionSetting, CallToActionSettingResponce>();
            _ = CreateMap<CallToActionSettingResponce, CallToActionSetting>();

            #endregion
            #region CallMessage

            _ = CreateMap<CallToActionMessage, CallToActionMessageRequest>();
            _ = CreateMap<CallToActionMessageRequest, CallToActionMessage>();

            _ = CreateMap<CallToActionMessage, CallToActionMessageResponce>();
            _ = CreateMap<CallToActionMessageResponce, CallToActionMessage>();

            #endregion
            #region CompQualitySetting

            _ = CreateMap<CompanyQualitySetting, CompanyQualitySettingRequest>();
            _ = CreateMap<CompanyQualitySettingRequest, CompanyQualitySetting>();

            _ = CreateMap<CompanyQualitySetting, CompanyQualitiySettingResponce>();
            _ = CreateMap<CompanyQualitiySettingResponce, CompanyQualitySetting>();

            #endregion
            #region CompCertificateSetting

            _ = CreateMap<CompanyCertificateSetting, CompanyCertificateSettingRequest>();
            _ = CreateMap<CompanyCertificateSettingRequest, CompanyCertificateSetting>();

            _ = CreateMap<CompanyCertificateSetting, CompanyCertificateSettingResponce>();
            _ = CreateMap<CompanyCertificateSettingResponce, CompanyCertificateSetting>();

            #endregion
            #region LegalDoc

            _ = CreateMap<LegalDocument, LegalDocumentRequest>();
            _ = CreateMap<LegalDocumentRequest, LegalDocument>();

            _ = CreateMap<LegalDocument, LegalDocumentResponce>();
            _ = CreateMap<LegalDocumentResponce, LegalDocument>();

            #endregion

            #region BlogPost

            _ = CreateMap<BlogPost, BlogPostRequest>();
            _ = CreateMap<BlogPostRequest, BlogPost>();

            _ = CreateMap<BlogPost, BlogPostResponce>();
            _ = CreateMap<BlogPostResponce, BlogPost>();


            #endregion

            #region BlogCategory

            _ = CreateMap<BlogCategory, BlogCategoryRequest>();
            _ = CreateMap<BlogCategoryRequest, BlogCategory>();

            _ = CreateMap<BlogCategory, BlogCategoryResponce>();
            _ = CreateMap<BlogCategoryResponce, BlogCategory>();

            #endregion
        }
    }
}
