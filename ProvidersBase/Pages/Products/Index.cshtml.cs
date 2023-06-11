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
    public class IndexModel : PageModel
    {
        private readonly ProvidersBase.Model.DataAccessLayer.ProvidersContext _context;

        public IndexModel(ProvidersBase.Model.DataAccessLayer.ProvidersContext context)
        {
            _context = context;
        }

        public IList<ProviderProduct> ProviderProduct { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Products != null)
            {
                ProviderProduct = await _context.Products
                .Include(p => p.Provider).ToListAsync();
            }
        }
    }
}
