using CLONETRELLOBACK.models;
using Microsoft.EntityFrameworkCore;

namespace CLONETRELLOBACK.Data
{
    public class TaskContext : DbContext
    {
        public TaskContext(DbContextOptions<TaskContext> options)
            : base(options) { }

        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<Projects> Projects { get; set; }
        public DbSet<Lists> Lists { get; set; }
        public DbSet<Comments> Comments { get; set; }
    }
}
