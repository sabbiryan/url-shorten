using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace UrlShorten.EntityFrameworkCore
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
            
        }


        public virtual DbSet<UrlMap> UrlMaps { get; set; }
        public virtual DbSet<HitLog> HitLogs { get; set; }
    }
}
