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
    public class DeleteModel : PageModel
    {
        private readonly Auth.Persistence.AuthContext _context;

        public DeleteModel(Auth.Persistence.AuthContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Popug = await _context.Popugs.FindAsync(id);

            if (Popug != null)
            {
                _context.Popugs.Remove(Popug);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
