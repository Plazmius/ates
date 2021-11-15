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
using IdentityModel;
using SchemaRegistry;
using SchemaRegistry.Producers;
using User;

namespace Auth.Pages
{
    public class CreateModel : PageModel
    {
        private readonly UserManager<Popug> userManager;
        private IEventProducer _producer;

        public CreateModel(UserManager<Popug> userManager, IEventProducer producer)
        {
            this.userManager = userManager;
            _producer = producer;
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
            await _producer.AddEventAsync(Topics.UsersStreaming,
                $"{user.Email}-${DateTime.UtcNow.ToEpochTime()}",
                new UserCreated()
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Role = Popug.Role
                });
            return RedirectToPage("./Index");
        }
    }
}
