using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models.Models;

namespace Models.Contexts
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<User> ApplicationUsers { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Client> SubClients { get; set; }
        public DbSet<ClientUser> ClientUsers { get; set; }
        public DbSet<Movement> Movements { get; set; }
    }
}
