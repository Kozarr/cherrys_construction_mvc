using Ardalis.Specification;
using cherrys_construction_mvc.Models;

namespace cherrys_construction_mvc.Specification.ProjectSpec
{
    public class ProjectsWithoutTestimony : Specification<Project>, ISingleResultSpecification<Project>
    {
        public ProjectsWithoutTestimony()
        {
            Query.Where(a => a.Testimony == null).AsSplitQuery();
        }
    }
}
