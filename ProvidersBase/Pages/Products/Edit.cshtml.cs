using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProvidersBase.Model.DataAccessLayer;
using ProvidersBase.Model.Models;

namespace ProvidersBase.Pages.Products
{
    public class EditModel : PageModel
    {
        private readonly ProvidersContext _context;

        public EditModel(ProvidersContext context)
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

            var providerproduct =  await _context.Products.FirstOrDefaultAsync(m => m.Id == id);
            if (providerproduct == null)
            {
                return NotFound();
            }
            ProviderProduct = providerproduct;
            ViewData["ProviderId"] = new SelectList(_context.Providers, "Id", "CompanyTitle");
            return Page();
        }

        
        public async Task<IActionResult> OnPostAsync(ProviderProduct providerProduct)
        {
            if (!ModelState.IsValid)
            {
                ViewData["ProviderId"] = new SelectList(_context.Providers, "Id", "CompanyTitle");
                return Page();
            }
            
            _context.Attach(ProviderProduct).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProviderProductExists(ProviderProduct.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToPage("./Index");

        }
        private bool ProviderProductExists(int id)
        {
            return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }
}
