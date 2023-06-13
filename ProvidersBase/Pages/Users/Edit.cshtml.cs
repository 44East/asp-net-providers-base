using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProvidersBase.Model.DataAccessLayer;
using ProvidersBase.Model.Models;

namespace ProvidersBase.Pages.Users
{
    public class EditModel : PageModel
    {
        private readonly ProvidersContext _context;

        public EditModel(ProvidersContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ProviderUser ProviderUser { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var provideruser = await _context.Users.FirstOrDefaultAsync(m => m.Id == id);
            if (provideruser == null)
            {
                return NotFound();
            }
            ProviderUser = provideruser;
            ViewData["ProviderId"] = new SelectList(_context.Providers, "Id", "CompanyTitle");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(ProviderUser providerUser)
        {
            if (!ModelState.IsValid)
            {
                ViewData["ProviderId"] = new SelectList(_context.Providers, "Id", "CompanyTitle");
                return Page();
            }

            _context.Attach(ProviderUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProviderUserExists(ProviderUser.Id))
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

        private bool ProviderUserExists(int id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
