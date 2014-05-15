using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace _3vikna.Models
{
    public class AppDataContext : IdentityDbContext 
    {
        public AppDataContext()
            : base("AppDataContext")
        {

        }

        public DbSet<Subtitles> Subtitles { get; set; }
        public DbSet<Requests> Requests { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Upvote> Upvote { get; set; }
    }
}