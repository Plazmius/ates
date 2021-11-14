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
    public class DetailsModel : PageModel
    {
        private readonly Auth.Persistence.AuthContext _context;

        public DetailsModel(Auth.Persistence.AuthContext context)
        {
            _context = context;
        }

        public Popug Popug { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Popug = await _context.Popugs.FirstOrDefaultAsync(m => m.Id == id);

            if (Popug == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
