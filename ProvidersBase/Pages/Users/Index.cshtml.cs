using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;using ProvidersBase.Model.DataAccessLayer;
using ProvidersBase.Model.Models;
using ProvidersBase.Model.SortingStates;

namespace ProvidersBase.Pages.Users
{
    public class IndexModel : PageModel
    {
        private readonly ProvidersContext _context;

        public IndexModel(ProvidersContext context)
        {
            _context = context;
        }

        public IList<ProviderUser> ProviderUsers { get;set; } = default!;
        /// <summary>
        /// The property for storing a find request 
        /// </summary>
        public string CurrentFilter { get; set; }

        public async Task OnGetAsync(string searchByINN, UserSortStates sortOrder = UserSortStates.NameAsc)
        {
            if (_context.Users != null)
            {
                ProviderUsers = await _context.Users
                .Include(p => p.Provider).ToListAsync();
            }
            // Set up ViewData for the sort order.These values will be used in the Razor view to create links to sort the data.
            ViewData["NameSort"] = sortOrder == UserSortStates.NameAsc ? UserSortStates.NameDesc : UserSortStates.NameAsc;
            ViewData["ProviderSort"] = sortOrder == UserSortStates.ProviderAsc ? UserSortStates.ProviderDesc : UserSortStates.ProviderAsc;
            ViewData["UsernameSort"] = sortOrder == UserSortStates.UsernameAsc ? UserSortStates.NameDesc : UserSortStates.UsernameAsc;

            CurrentFilter = searchByINN;
            //Get the raw data 
            IQueryable<ProviderUser> UsersIQ = _context.Users.Include(p => p.Provider);

            if (!string.IsNullOrEmpty(searchByINN))
            {
                UsersIQ = UsersIQ.Where(p => p.Provider.INN.Contains(searchByINN));
            }

            // Sort the Users based on the sortOrder parameter by UserSortStates.
            // The switch statement selects the appropriate LINQ method based on the sortOrder value.
            UsersIQ = sortOrder switch
            {
                UserSortStates.NameDesc => UsersIQ.OrderByDescending(p => p.Name),
                UserSortStates.ProviderAsc => UsersIQ.OrderBy(p => p.Provider.CompanyTitle),
                UserSortStates.ProviderDesc => UsersIQ.OrderByDescending(p => p.Provider.CompanyTitle),
                UserSortStates.UsernameAsc => UsersIQ.OrderBy(p => p.Username),
                UserSortStates.UsernameDesc => UsersIQ.OrderByDescending(p => p.Username),
                _ => UsersIQ.OrderBy(p => p.Name)
            };

            //Get the sorted data for view
            ProviderUsers = await UsersIQ.AsNoTracking().ToListAsync();
        }
    }
}
