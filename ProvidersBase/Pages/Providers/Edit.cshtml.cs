using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProvidersBase.Model.DataAccessLayer;
using ProvidersBase.Model.Models;

namespace ProvidersBase.Pages.Providers
{
    public class EditModel : PageModel
    {
        private readonly ProvidersContext _context;

        public EditModel(ProvidersContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ProviderCompany ProviderCompany { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Providers == null)
            {
                return NotFound();
            }

            var providercompany = await _context.Providers.FirstOrDefaultAsync(m => m.Id == id);
            if (providercompany == null)
            {
                return NotFound();
            }
            ProviderCompany = providercompany;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ProviderCompany).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProviderCompanyExists(ProviderCompany.Id))
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

        private bool ProviderCompanyExists(int id)
        {
            return (_context.Providers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
