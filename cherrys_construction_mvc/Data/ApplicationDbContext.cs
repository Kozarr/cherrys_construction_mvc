using cherrys_construction_mvc.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace cherrys_construction_mvc.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        // Database Tables(Sets)
        public DbSet<CompanyInfo> CompanyInfos { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Testimony> Testimonies { get; set; }
        public DbSet<ImageModel> Images { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<CompanyCertificate> CompanyCertificates { get; set; }
        public DbSet<CompanyValue> CompanyValues { get; set; }
        public DbSet<ServiceType> ServiceTypes { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<CompanyStory> CompanyStories { get; set; }
        public DbSet<HeroSlider> HeroSliders { get; set; }
        public DbSet<HeroSliderImage> HeroSliderImages { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<CompanyQuality> companyQualities { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<CallToActionSetting> callToActionSettings { get; set; }
        public DbSet<CallToActionMessage> CallToActionMessages { get; set; }
        public DbSet<CompanyCertificateSetting> CompanyCertificateSettings { get; set; }
        public DbSet<CompanyQualitySetting> CompanyQualitySettings { get; set; }
        public DbSet<LegalDocument> LegalDocuments { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<BlogCategory> BlogCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProjectTag>()
                .HasKey(bc => new { bc.ProjectId, bc.TagId });
            modelBuilder.Entity<ProjectTag>()
                .HasOne(bc => bc.Project)
                .WithMany(b => b.ProjectTags)
                .HasForeignKey(bc => bc.ProjectId);
            modelBuilder.Entity<ProjectTag>()
                .HasOne(bc => bc.Tag)
                .WithMany(c => c.ProjectTags)
                .HasForeignKey(bc => bc.TagId);
        }
    }
}