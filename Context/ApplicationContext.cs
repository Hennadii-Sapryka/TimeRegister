using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeRegisterApp.Models;

namespace TimeRegisterApp.Context
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<SpentTime> SpentTimes { get; set; }
        public DbSet<Checkpoint> Checkpoints { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            
        }
    }
}
