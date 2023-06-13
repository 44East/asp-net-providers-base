using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProvidersBase.Model.DataAccessLayer;
using ProvidersBase.Model.Models;
using ProvidersBase.Model.SortingStates;

namespace ProvidersBase.Pages.Providers
{
    public class IndexModel : PageModel
    {
        private readonly ProvidersContext _context;

        public IndexModel(ProvidersContext context)
        {
            _context = context;
        }

        public IList<ProviderCompany> ProviderCompanies { get;set; } = default!;
        /// <summary>
        /// The property for storing a find request 
        /// </summary>
        public string CurrentFilter { get; set; }

        public async Task OnGetAsync(string searchByINN, ProviderSortStates sortOrder = ProviderSortStates.TitleAsc)
        {
            if (_context.Providers != null)
            {
                ProviderCompanies = await _context.Providers.ToListAsync();
            }
            // Set up ViewData for the sort order.These values will be used in the Razor view to create links to sort the data.
            ViewData["TitleSort"] = sortOrder == ProviderSortStates.TitleAsc ? ProviderSortStates.TitleDesc : ProviderSortStates.TitleAsc;
            ViewData["INNSort"] = sortOrder == ProviderSortStates.INNAsc ? ProviderSortStates.INNDesc : ProviderSortStates.TitleAsc;

            CurrentFilter = searchByINN;
            //Get the raw data 
            IQueryable<ProviderCompany> providersIQ = from p in _context.Providers select p;

            if (!string.IsNullOrEmpty( searchByINN))
            {
                providersIQ = providersIQ.Where(p => p.INN.Contains(searchByINN));
            }

            // Sort the providers based on the sortOrder parameter by ProviderSortStates.
            // The switch statement selects the appropriate LINQ method based on the sortOrder value.
            providersIQ = sortOrder switch
            {
                ProviderSortStates.TitleDesc => providersIQ.OrderByDescending(p => p.CompanyTitle),
                ProviderSortStates.INNAsc => providersIQ.OrderBy(p => p.INN),
                ProviderSortStates.INNDesc => providersIQ?.OrderByDescending(p => p.INN),
                _ => providersIQ.OrderBy(p => p.CompanyTitle)
            };

            //Get the sorted data for view
            ProviderCompanies = await providersIQ.AsNoTracking().ToListAsync();
        }
    }
}
