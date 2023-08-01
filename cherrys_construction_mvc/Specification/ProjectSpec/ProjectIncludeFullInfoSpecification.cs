using Ardalis.Specification;
using cherrys_construction_mvc.Models;

namespace cherrys_construction_mvc.Specification.ProjectSpec
{
    public class ProjectIncludeFullInfoSpecification: Specification<Project>, ISingleResultSpecification<Project>
    {
        public ProjectIncludeFullInfoSpecification()
        {
            Query.Include(t => t.Testimony).Include(p => p.Images).Include(a => a.ProjectTags).ThenInclude(s => s.Tag).AsSplitQuery();
            //Query.Include(c=>c.ServiceType).Include(t=>t.Testimony).Include(p=>p.Images).Include(a=>a.ProjectTags).ThenInclude(s=>s.Tag).AsSplitQuery();
        }

        public ProjectIncludeFullInfoSpecification(int id)
        {
            //Query.Where(s => id == s.Id).Include(c => c.ServiceType).Include(t => t.Testimony).Include(a => a.Images).Include(a => a.ProjectTags).ThenInclude(a => a.Tag).AsSplitQuery();
            Query.Where(s => id == s.Id).Include(t=>t.Testimony).Include(a=>a.Images).Include(a => a.ProjectTags).ThenInclude(a=>a.Tag).AsSplitQuery();
        }

    }
}
