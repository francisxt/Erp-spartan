using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Models.Contexts;
using Models.Enums;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commons.Others
{
    public static class DataSeederService
    {
        /// <summary>
        /// Generate data required of the application 
        /// </summary>
        /// <param name="app">builder of app</param>
        public static void SeedRolesAndUsers(IApplicationBuilder app)
        {
            using var appScoped = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            ApplicationDbContext _dbContext = appScoped.ServiceProvider.GetService<ApplicationDbContext>();
            UserManager<User> _userManager = appScoped.ServiceProvider.GetService<UserManager<User>>();
            if (!_dbContext.Roles.Any())
            {
                var roleList = new List<IdentityRole> {
                            new IdentityRole { Name = nameof(RolsAuthorization.Admin) , NormalizedName = nameof(RolsAuthorization.Admin) },
                            new IdentityRole { Name = nameof(RolsAuthorization.Client) , NormalizedName = nameof(RolsAuthorization.Client) },
                            new IdentityRole { Name = nameof(RolsAuthorization.ClientsUser) , NormalizedName = nameof(RolsAuthorization.ClientsUser) },
                    };
                _dbContext.Roles.AddRange(roleList);
                _dbContext.SaveChanges();
            }

            if (!_dbContext.ApplicationUsers.Any())
            {
                var user = new User
                {
                    CreateAt = DateTime.Now,
                    UserName = "admin@admin.com",
                    NormalizedUserName = nameof(RolsAuthorization.Admin),
                    EmailConfirmed = true,
                    Email = "admin@admin.com",
                    LockoutEnabled = false,
                    PhoneNumber = "000-000-0000",
                    Name = nameof(RolsAuthorization.Admin),
                    LastName = "Replace me",
                    NormalizedEmail = "admin@admin.com",
                };

                var result = _userManager.CreateAsync(user, "admin1234");
                _dbContext.ApplicationUsers.Add(user);
                if (_dbContext.SaveChanges() > 0)
                {
                    var role = _dbContext.Roles.FirstOrDefault(x => x.Name.Equals(nameof(RolsAuthorization.Admin)));
                    _dbContext.UserRoles.Add(new IdentityUserRole<string> { RoleId = role.Id, UserId = user.Id });
                    _dbContext.SaveChanges();
                }
            }
        }
    }
}
