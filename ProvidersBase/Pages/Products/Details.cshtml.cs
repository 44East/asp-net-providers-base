using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProvidersBase.Model.DataAccessLayer;
using ProvidersBase.Model.Models;

namespace ProvidersBase.Pages.Products
{
    public class DetailsModel : PageModel
    {
        private readonly ProvidersContext _context;

        public DetailsModel(ProvidersContext context)
        {
            _context = context;
        }

        public ProviderProduct ProviderProduct { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }
            //Binding data from the conected tables
            var providerproduct = await _context.Products
                .Include(p => p.Provider)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (providerproduct == null)
            {
                return NotFound();
            }
            else
            {
                ProviderProduct = providerproduct;
            }
            return Page();
        }
    }
}
