namespace TaskAndProjet.API.Models
{
    public class UserProjectsModels
    {
        public int UserId { get; set; }
        public UserModels Users { get; set; }

        public int ProjectId { get; set; }
        public ProjectsModels Projects { get; set; }    
    }
}
