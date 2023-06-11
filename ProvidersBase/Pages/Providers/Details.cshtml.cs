using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProvidersBase.Model.DataAccessLayer;
using ProvidersBase.Model.Models;

namespace ProvidersBase.Pages.Providers
{
    public class DetailsModel : PageModel
    {
        private readonly ProvidersBase.Model.DataAccessLayer.ProvidersContext _context;

        public DetailsModel(ProvidersBase.Model.DataAccessLayer.ProvidersContext context)
        {
            _context = context;
        }

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
            else 
            {
                ProviderCompany = providercompany;
            }
            return Page();
        }
    }
}
