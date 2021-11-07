using System;
using System.Linq;
using System.Threading.Tasks;
using Auth.Persistence.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Auth.Persistence
{
    public class SeedDb
    {
        public static void EnsureSeedData(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var context = scope.ServiceProvider.GetService<AuthContext>();
            context.Database.Migrate();

            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
            SeedRole(roleManager, Roles.Admin);
            SeedRole(roleManager, Roles.Manager);
            SeedRole(roleManager, Roles.Worker);
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Popug>>();
            
            SeedUser(userManager, "alice", "alice@gmail.com", Roles.Admin);
            SeedUser(userManager, "bob", "bob@gmail.com", Roles.Manager);
        }

        private static void SeedUser(UserManager<Popug> userManager, string username, string email, string role)
        {
            Seed(() => userManager.FindByNameAsync(username), async () =>
            {
                var user = new Popug
                {
                    UserName = username,
                    Email = email
                };
                await userManager.CreateAsync(user, "Password123!");
                return await userManager.AddToRoleAsync(user, role);
            });
        }

        private static void SeedRole(RoleManager<Role> roleManager, string role)
        {
            Seed(() => roleManager.FindByNameAsync(role), () => roleManager.CreateAsync(new Role(role)));
        }
        private static void Seed<T>(Func<Task<T>> findInstruction, Func<Task<IdentityResult>> seedInstruction)
        {
            var findResult = findInstruction().Result;
            if (findResult == null)
            {
                return;
            }
            
            EvaluateIdentityInstruction<T>(seedInstruction);
        }

        private static void EvaluateIdentityInstruction<T>(Func<Task<IdentityResult>> seedInstruction)
        {
            var result = seedInstruction().Result;

            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }
        }
    }
}