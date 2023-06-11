using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProvidersBase.Model.DataAccessLayer;
using ProvidersBase.Model.Models;

namespace ProvidersBase.Pages.Users
{
    public class CreateModel : PageModel
    {
        private readonly ProvidersBase.Model.DataAccessLayer.ProvidersContext _context;

        public CreateModel(ProvidersBase.Model.DataAccessLayer.ProvidersContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["ProviderId"] = new SelectList(_context.Providers, "Id", "Address");
            return Page();
        }

        [BindProperty]
        public ProviderUser ProviderUser { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Users == null || ProviderUser == null)
            {
                return Page();
            }

            _context.Users.Add(ProviderUser);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
