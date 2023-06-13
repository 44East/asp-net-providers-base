using ProvidersBase.Model.Models;
namespace ProvidersBase.Model.ViewModels
{
    /// <summary>
    /// View model of <see cref="ProviderProduct"/> uses for creation a new object into DB, for safety it contains only primitive types
    /// </summary>
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
