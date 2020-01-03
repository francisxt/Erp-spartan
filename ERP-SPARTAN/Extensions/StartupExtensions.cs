using BusinesLogic.Interfaces;
using BusinesLogic.Services;
using BusinesLogic.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Models.Contexts;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_SPARTAN.Extensions
{
    public static class StartupExtensions
    {
        public static void ConfigureDbContexts(this IServiceCollection services , IConfiguration configuration)
         => services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        public static void AddNewIdentityConfiguration(this IServiceCollection services)
            => services.AddDefaultIdentity<User>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.User.RequireUniqueEmail = true;
                options.Password.RequireNonAlphanumeric = false;
            }).AddRoles<IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
        public static void ImplementServices(this IServiceCollection services)
        {
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        } 
    }
}
