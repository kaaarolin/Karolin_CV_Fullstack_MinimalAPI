using Karolin_CV_Fullstack.Models;
using Microsoft.EntityFrameworkCore;

namespace Karolin_CV_Fullstack.Data
{
    public class CV_DbContext : DbContext
    {
        public CV_DbContext(DbContextOptions<CV_DbContext> options) : base(options) { }
        public DbSet<Tech_skills> TechSkills { get; set; }
        public DbSet<Projects> Projects { get; set; }
    }
}
