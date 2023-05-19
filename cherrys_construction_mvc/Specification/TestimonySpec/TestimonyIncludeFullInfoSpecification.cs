using Ardalis.Specification;
using cherrys_construction_mvc.Models;

namespace cherrys_construction_mvc.Specification.TestimonySpec
{


    public class TestimonyIncludeFullInfoSpecification : Specification<Testimony>, ISingleResultSpecification<Testimony>
    {
        public TestimonyIncludeFullInfoSpecification()
        {

            Query.Include(c => c.Project).AsSplitQuery();
        }

        public TestimonyIncludeFullInfoSpecification(int id)
        {
            Query.Where(s => id == s.Id).Include(c => c.Project).AsSplitQuery();
        }


    }
    
}
