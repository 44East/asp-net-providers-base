using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProvidersBase.Model.DataAccessLayer;
using ProvidersBase.Model.Models;

namespace ProvidersBase.Pages.Users
{
    public class DetailsModel : PageModel
    {
        private readonly ProvidersContext _context;

        public DetailsModel(ProvidersContext context)
        {
            _context = context;
        }

      public ProviderUser ProviderUser { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }
            //Binding data from the conected tables
            var provideruser = await _context.Users
                .Include(u => u.Provider)
                .FirstOrDefaultAsync(m => m.Id == id);
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
    }
}
