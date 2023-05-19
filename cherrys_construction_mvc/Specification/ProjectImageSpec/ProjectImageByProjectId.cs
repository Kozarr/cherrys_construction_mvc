using Ardalis.Specification;
using cherrys_construction_mvc.Models;

namespace cherrys_construction_mvc.Specification.ProjectImageSpec
{
    public class ProjectImageByProjectId: Specification<ImageModel>, ISingleResultSpecification<ImageModel>
    {

        public ProjectImageByProjectId(int projectId)
        {
            Query.Where(a=>a.ProjectId == projectId);
        }
    }
}
