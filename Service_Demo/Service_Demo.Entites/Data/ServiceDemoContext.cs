using Microsoft.EntityFrameworkCore;
using Service_Demo.Entites.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Demo.Entites.Data
{
    public class ServiceDemoContext : DbContext
    {
        public ServiceDemoContext(DbContextOptions<ServiceDemoContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; } 
        public DbSet<Skills> Skills { get; set; }
        public DbSet<UserSkill> UserSkills { get; set; }
    }
}
