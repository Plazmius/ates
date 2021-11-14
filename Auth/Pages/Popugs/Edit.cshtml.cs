using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Auth.Persistence;
using Auth.Persistence.Models;

namespace Auth.Pages
{
    public class EditModel : PageModel
    {
        private readonly Auth.Persistence.AuthContext _context;

        public EditModel(Auth.Persistence.AuthContext context)
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var toUpdate = await _context.Popugs.FirstOrDefaultAsync(m => m.Id == Popug.Id);
            if (toUpdate == null)
            {
                return NotFound();
            }

            _context.Entry(toUpdate).CurrentValues.SetValues(Popug);
            await _context.SaveChangesAsync();
                
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PopugExists(Popug.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            
            return RedirectToPage("./Index");
        }

        private bool PopugExists(Guid id)
        {
            return _context.Popugs.Any(e => e.Id == id);
        }
    }
}
