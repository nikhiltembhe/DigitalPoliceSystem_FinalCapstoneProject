using DigitalPoliceSystem.Models.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace DigitalPoliceSystem.Data
{
    //Singleton
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedIdentityRolesAsync(RoleManager<IdentityRole> rolemanager)
        {
            foreach (MyIdentityRoleNames rolename in Enum.GetValues(typeof(MyIdentityRoleNames)))
            {
                if (!await rolemanager.RoleExistsAsync(rolename.ToString()))
                {
                    await rolemanager.CreateAsync(
                        new IdentityRole { Name = rolename.ToString() });
                }
            }
        }

        public static async Task SeedIdentityUserAsync(UserManager<IdentityUser> usermanager)
        {
            IdentityUser user;

            user = await usermanager.FindByNameAsync("admin@police.com");
            if (user == null)
            {
                user = new IdentityUser()
                {
                    UserName = "admin@police.com",
                    Email = "admin@police.com",
                    EmailConfirmed = true
                };
                await usermanager.CreateAsync(user, password: "Password@1234");

                await usermanager.AddToRolesAsync(user, new string[] {
                    MyIdentityRoleNames.PoliceAdmin.ToString(),
                    MyIdentityRoleNames.PoliceUser.ToString()
                });
            }

            user = await usermanager.FindByNameAsync("user@police.com");
            if (user == null)
            {
                user = new IdentityUser()
                {
                    UserName = "user@police.com",
                    Email = "user@police.com",
                    EmailConfirmed = true
                };
                await usermanager.CreateAsync(user, password: "Asdf@1234");
                //await usermanager.AddToRolesAsync(user, new string[] {
                //    MyIdentityRoleNames.AppUser.ToString()
                //});
                await usermanager.AddToRoleAsync(user, MyIdentityRoleNames.PoliceUser.ToString());
            }
        }
    }
}
