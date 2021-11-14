using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Tasks.Persistence
{
    public class TasksContext: DbContext
    {
        public DbSet<PopugTask> Tasks { get; set; }
        public TasksContext(DbContextOptions options) : base(options)
        {
            
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder
                .UseSnakeCaseNamingConvention();
        
    }

    // PopugTask because Task is reserved name for TPL class
    public class PopugTask
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public Popug Assignee { get; set; }
    }

    public class Popug
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
    }
}