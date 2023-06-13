using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProvidersBase.Model.DataAccessLayer;
using ProvidersBase.Model.Models;
using ProvidersBase.Model.SortingStates;

namespace ProvidersBase.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly ProvidersContext _context;

        public IndexModel(ProvidersContext context)
        {
            _context = context;
        }

        public IList<ProviderProduct> ProviderProducts { get;set; } = default!;
        /// <summary>
        /// The property for storing a find request 
        /// </summary>
        public string CurrentFilter { get; set; }

        public async Task OnGetAsync(string searchByINN, ProductSortStates sortOrder = ProductSortStates.TitleAsc)
        {
            if (_context.Products != null)
            {
                ProviderProducts = await _context.Products
                .Include(p => p.Provider).ToListAsync();
            }
            // Set up ViewData for the sort order.These values will be used in the Razor view to create links to sort the data.
            ViewData["TitleSort"] = sortOrder == ProductSortStates.TitleAsc ? ProductSortStates.TitleDesc : ProductSortStates.TitleAsc;
            ViewData["ProviderSort"] = sortOrder == ProductSortStates.ProviderAsc ? ProductSortStates.ProviderDesc : ProductSortStates.ProviderAsc;
            ViewData["PriceSort"] = sortOrder == ProductSortStates.PriceAsc ? ProductSortStates.PriceDesc : ProductSortStates.PriceAsc;

            CurrentFilter = searchByINN;
            //Get the raw data 
            IQueryable<ProviderProduct> productsIQ = _context.Products.Include(p => p.Provider);

            if (!string.IsNullOrEmpty(searchByINN))
            {
                productsIQ = productsIQ.Where(p => p.Provider.INN.Contains(searchByINN));
            }

            // Sort the products based on the sortOrder parameter by ProductSortStates.
            // The switch statement selects the appropriate LINQ method based on the sortOrder value.
            productsIQ = sortOrder switch
            {
                ProductSortStates.TitleDesc => productsIQ.OrderByDescending(p => p.Title),
                ProductSortStates.ProviderAsc => productsIQ.OrderBy(p => p.Provider.CompanyTitle),
                ProductSortStates.ProviderDesc => productsIQ.OrderByDescending(p => p.Provider.CompanyTitle),
                ProductSortStates.PriceAsc => productsIQ.OrderBy(p => p.Price),
                ProductSortStates.PriceDesc => productsIQ.OrderByDescending(p => p.Price),
                _ => productsIQ.OrderBy(p => p.Title)
            };

            //Get the sorted data for view
            ProviderProducts = await productsIQ.AsNoTracking().ToListAsync();
        }
    }
}
