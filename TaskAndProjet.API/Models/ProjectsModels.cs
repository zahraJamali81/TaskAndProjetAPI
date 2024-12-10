using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskAndProjet.API.Models
{
    public class ProjectsModels
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        public ICollection<TaskModel> Tasks { get; set; } = new List<TaskModel>();
        public ICollection<UserProjectsModels> UserProjects { get; set; }   
      

    }
}

