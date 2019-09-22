using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace UrlShorten.EntityFrameworkCore
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
            
        }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UrlMap>().Property(e => e.Identity).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
        }


        public virtual DbSet<UrlMap> UrlMaps { get; set; }
        public virtual DbSet<HitLog> HitLogs { get; set; }
    }
}
