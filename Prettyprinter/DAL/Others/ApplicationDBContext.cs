﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Prettyprinter.Models;

namespace Prettyprinter.DAL
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Folder> Folder { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Metadata> Metadata { get; set; }
        public DbSet<AccessControl> AccessControl { get; set; }
    }
}
