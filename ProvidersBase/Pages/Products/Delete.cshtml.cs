using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProvidersBase.Model.DataAccessLayer;
using ProvidersBase.Model.Models;

namespace ProvidersBase.Pages.Products
{
    public class DeleteModel : PageModel
    {
        private readonly ProvidersContext _context;

        public DeleteModel(ProvidersContext context)
        {
            _context = context;
        }

        [BindProperty]
      public ProviderProduct ProviderProduct { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var providerproduct = await _context.Products.FirstOrDefaultAsync(m => m.Id == id);

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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }
            var providerproduct = await _context.Products.FindAsync(id);

            if (providerproduct != null)
            {
                ProviderProduct = providerproduct;
                _context.Products.Remove(ProviderProduct);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
