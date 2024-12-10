using Microsoft.EntityFrameworkCore;

namespace TaskAndProjet.API.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> db) : base(db) { }

        public DbSet<TaskModel> Tasks { get; set; }
        public DbSet<ProjectsModels> Projects { get; set; }
        public DbSet<UserModels> Users { get; set; }
        public DbSet<UserProjectsModels> UserProjects { get; set; }





        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //یک به  چند Task,User
            modelBuilder.Entity<TaskModel>()
                .HasOne(t => t.Users)
                .WithMany(u => u.Tasks)
                .HasForeignKey(t => t.UserId);

            //یک به چند Task , Project
            modelBuilder.Entity<TaskModel>()
               .HasOne(t => t.Projects)
               .WithMany(u => u.Tasks)
               .HasForeignKey(t => t.UserId);

            //چند به چند User,Project
            modelBuilder.Entity<UserProjectsModels>()
         .HasKey(up => new { up.UserId, up.ProjectId });

            modelBuilder.Entity<UserProjectsModels>()
                .HasOne(up => up.Users)
                .WithMany(u => u.UserProjects)
                .HasForeignKey(up => up.UserId);

            modelBuilder.Entity<UserProjectsModels>()
                .HasOne(up => up.Projects)
                .WithMany(p => p.UserProjects)
                .HasForeignKey(up => up.ProjectId);

        }





    }
}
