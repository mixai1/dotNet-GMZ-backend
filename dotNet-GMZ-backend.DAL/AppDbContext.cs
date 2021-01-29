using System;
using dotNet_GMZ_backend.Models.IdentityModels;
using dotNet_GMZ_backend.Models.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace dotNet_GMZ_backend.DAL
{
    public class AppDbContext : IdentityDbContext<UserApp,RoleApp,String>
    {
        public DbSet<NewsRecord> NewsRecords { get; set; }
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
