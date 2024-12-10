using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskAndProjet.API.Models
{
    public class UserModels
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";

        [MaxLength(50)]
        public string UserName { get; set; }
        public string Password { get; set; }

        public ICollection<TaskModel> Tasks { get; set; } = new List<TaskModel>();
        public ICollection<UserProjectsModels> UserProjects { get; set; }


        public UserModels(int id, string userName, string firstName, string lastName)
        {
            Id = id;
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
            Tasks = Tasks ?? new List<TaskModel>();
            UserProjects = UserProjects ?? new List<UserProjectsModels>();
        }


    }
}



