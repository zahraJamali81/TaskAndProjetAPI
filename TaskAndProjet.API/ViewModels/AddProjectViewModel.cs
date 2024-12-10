using TaskAndProjet.API.Models;

namespace TaskAndProjet.API.ViewModels
{
    public class AddProjectViewModel
    {
        public ProjectsModels Project { get; set; }
        public List<TaskModel> Tasks { get; set; }
    }
}
