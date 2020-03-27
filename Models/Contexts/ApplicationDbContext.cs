using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Models.Models.Accounting;

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
            builder.Entity<ClientUser>().HasQueryFilter(x => x.State != Enums.State.Removed);
            builder.Entity<Movement>().HasQueryFilter(x => x.State != Enums.State.Removed && x.State != Enums.State.Payment);
        }

        #region HiAccounting
        public DbSet<ClientUser> ClientUsers { get; set; }
        public DbSet<Movement> Movements { get; set; }
        public DbSet<Enterprise> Enterprises { get; set; }
        #endregion

        #region HIInventaty
        public DbSet<Article> Articles { get; set; }
        #endregion

        #region Users
        public DbSet<User> ApplicationUsers { get; set; }
        #endregion
    }
}
