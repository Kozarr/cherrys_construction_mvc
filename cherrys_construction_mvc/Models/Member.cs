using cherrys_construction_mvc.EfRepository.Interfaces;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace cherrys_construction_mvc.Models
{
    public class Member: IAggregateRoot
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        [ValidateNever]
        public string? Role { get; set; }
        public string? Description { get; set; }
        [ValidateNever]
        public string? ImageLink { get; set; }
        [ValidateNever]
        public string? InstagramLink { get; set; }
    }
}
