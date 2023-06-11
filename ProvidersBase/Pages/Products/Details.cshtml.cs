using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProvidersBase.Model.DataAccessLayer;
using ProvidersBase.Model.Models;

namespace ProvidersBase.Pages.Products
{
    public class DetailsModel : PageModel
    {
        private readonly ProvidersBase.Model.DataAccessLayer.ProvidersContext _context;

        public DetailsModel(ProvidersBase.Model.DataAccessLayer.ProvidersContext context)
        {
            _context = context;
        }

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
    }
}
