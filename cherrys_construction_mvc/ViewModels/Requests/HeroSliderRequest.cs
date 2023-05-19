using cherrys_construction_mvc.ViewModels.Responce;

namespace cherrys_construction_mvc.ViewModels.Requests
{
    public class HeroSliderRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ButtonText { get; set; }
        public List<HeroSliderImageResponce> ActiveImages { get; set; }
        public List<IFormFile> ListImages { get; set; }
        public List<int> SelectedDeletePhoto { get; set; }


    }
}
