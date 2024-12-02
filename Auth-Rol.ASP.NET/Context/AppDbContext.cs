﻿using Auth_Rol.ASP.NET.Users.Model;
using Microsoft.EntityFrameworkCore;

namespace Auth_Rol.ASP.NET.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<UsersModel> UserModel { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UsersModel>(tb =>
            {
                tb.HasKey(row => row.Id);
                tb.Property(row => row.Id).UseIdentityColumn().ValueGeneratedOnAdd();

                tb.Property(row =>row.Username).HasMaxLength(50).IsUnicode();
                tb.Property(row =>row.Email).HasMaxLength(50).IsUnicode();
                tb.Property(row =>row.Password).HasMaxLength(50);

            });

            modelBuilder.Entity<UsersModel>().ToTable("Users");
        }
    }
}
