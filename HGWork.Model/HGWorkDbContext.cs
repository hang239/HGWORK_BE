using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HGWork.Model
{
    public class HGWorkDbContext : DbContext
    {
        public HGWorkDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<DailyNote> Reports { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<DailyNote> DailyNotes { get; set; }
        public DbSet<ProjectUser> ProjectUsers { get; set; }
    }
}
