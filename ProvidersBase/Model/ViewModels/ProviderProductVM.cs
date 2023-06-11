
namespace ProvidersBase.Model.ViewModels
{
    public class ProviderProductVM
    {
        public int Id { get; set; }
        public int ProviderId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int InPacking { get; set; }
    }
}
