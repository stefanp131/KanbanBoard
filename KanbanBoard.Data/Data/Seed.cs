using System.Collections.Generic;
using System.Threading.Tasks;
using KanbanBoard.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace KanbanBoard.Data.Data;

public class Seed
{
    public static async Task SeedUsers(UserManager<AppUser> userManager,
        RoleManager<AppRole> roleManager)
    {
        if (await roleManager.Roles.AnyAsync()) return;

        var roles = new List<AppRole>
        {
            new AppRole { Name = "Member" },
            new AppRole { Name = "Admin" },
        };

        foreach (var role in roles)
        {
            await roleManager.CreateAsync(role);
        }

        if (await userManager.Users.AnyAsync()) return;

        var admin = new AppUser
        {
            UserName = "admin"
        };

        await userManager.CreateAsync(admin, "Qwerty1!");
        await userManager.AddToRolesAsync(admin, new[] { "Admin" });
    }
}