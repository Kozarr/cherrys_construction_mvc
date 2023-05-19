using Ardalis.Specification;
using cherrys_construction_mvc.Models;

namespace cherrys_construction_mvc.Specification.ProjectSpec
{
    public class GetProjectTagDataByProjectId : Specification<ProjectTag>, ISingleResultSpecification<ProjectTag>
    {

        public GetProjectTagDataByProjectId(int projectId)
        {
            Query.Where(s => s.ProjectId == projectId).AsSplitQuery();
        }

        public GetProjectTagDataByProjectId(int projectId,int tagId)
        {
            Query.Where(s => s.ProjectId == projectId && s.TagId == tagId).AsSplitQuery();
        }

    }
}
