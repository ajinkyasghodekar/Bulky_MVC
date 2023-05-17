﻿using BulkyBook.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyBook.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        // Creating table for Category model 
        public DbSet<Category> Categories { get; set; }

        // Seed sample data to Categories Table 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1,Name = "Action", DisplayOrder = 1},
                new Category { Id = 2, Name = "SciFi", DisplayOrder = 2 },
                new Category { Id = 3, Name = "History", DisplayOrder = 3 },
                new Category { Id = 4, Name = "Motivational", DisplayOrder = 4 },
                new Category { Id = 5, Name = "Technology", DisplayOrder = 5 }
                );
        }
    }
}
