using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProvidersBase.Model.DataAccessLayer;
using ProvidersBase.Model.Models;

namespace ProvidersBase.Pages.Users
{
    public class DeleteModel : PageModel
    {
        private readonly ProvidersContext _context;

        public DeleteModel(ProvidersContext context)
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
            else 
            {
                ProviderUser = provideruser;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }
            var provideruser = await _context.Users.FindAsync(id);

            if (provideruser != null)
            {
                ProviderUser = provideruser;
                _context.Users.Remove(ProviderUser);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
