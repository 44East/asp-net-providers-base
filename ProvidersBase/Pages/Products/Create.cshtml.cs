using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProvidersBase.Model.DataAccessLayer;
using ProvidersBase.Model.Models;
using ProvidersBase.Model.ViewModels;

namespace ProvidersBase.Pages.Products
{
    public class CreateModel : PageModel
    {
        private readonly ProvidersContext _context;

        public CreateModel(ProvidersContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["ProviderId"] = new SelectList(_context.Providers, "Id", "CompanyTitle");
            return Page();
        }

        [BindProperty]
        public ProviderProductVM ProviderProductVM { get; set; } = default!;


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Products == null || ProviderProductVM == null)
            {
                ViewData["ProviderId"] = new SelectList(_context.Providers, "Id", "CompanyTitle");
                return Page();
            }

            //binding data from the ViewModel to the General model and insert it into the DB
            var entry = _context.Add(new ProviderProduct());
            entry.CurrentValues.SetValues(ProviderProductVM);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
