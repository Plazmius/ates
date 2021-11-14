using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Auth.Persistence;
using Auth.Persistence.Models;

namespace Auth.Pages
{
    public class IndexModel : PageModel
    {
        private readonly Auth.Persistence.AuthContext _context;

        public IndexModel(Auth.Persistence.AuthContext context)
        {
            _context = context;
        }

        public IList<Popug> Popug { get;set; }

        public async Task OnGetAsync()
        {
            Popug = await _context.Popugs.ToListAsync();
        }
    }
}
