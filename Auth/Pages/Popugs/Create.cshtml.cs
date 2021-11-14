using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Auth.Persistence;
using Auth.Persistence.Models;
using Microsoft.AspNetCore.Identity;
using Auth.Pages.Popugs;

namespace Auth.Pages
{
    public class CreateModel : PageModel
    {
        private readonly UserManager<Popug> userManager;

        public CreateModel(UserManager<Popug> userManager)
        {
            this.userManager = userManager;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public CreatePopugModel Popug { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!Roles.AllRoles.Contains(Popug.Role))
            {
                ModelState.AddModelError("Role", "InvalidRole");
                return Page();
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = new Popug
            {
                UserName = Popug.UserName,
                Email = Popug.Email,
            };
            await userManager.CreateAsync(user, Popug.Password);
            await userManager.AddToRoleAsync(user, Popug.Role);

            return RedirectToPage("./Index");
        }
    }
}
