using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Models
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> context) : base(context)
        {
            
        }

        public DbSet<Signup> Signups { get; set; }
        public DbSet<Group> Groups { get; set; }//making signup instance
        public DbSet<StudentRegistration> StudentRegistrations { get; set; }
        //singups bhanne table banaune
        public DbSet<Course> Course { get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<AttendanceDetails> AttendanceDetails { get; set; }
    }
}
