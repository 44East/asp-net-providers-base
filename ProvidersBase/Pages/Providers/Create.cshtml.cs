using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProvidersBase.Model.DataAccessLayer;
using ProvidersBase.Model.Models;
using ProvidersBase.Model.ViewModels;

namespace ProvidersBase.Pages.Providers
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
            return Page();
        }

        [BindProperty]
        public ProviderCompanyVM ProviderCompanyVM { get; set; } = default!;


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Providers == null || ProviderCompanyVM == null)
            {
                return Page();
            }
            //binding data from the ViewModel to the General model and insert it into the DB
            var entry = _context.Add(new ProviderCompany());
            entry.CurrentValues.SetValues(ProviderCompanyVM);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
