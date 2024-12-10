using TaskAndProjet.API.Models;
using TaskAndProjet.API.ViewModels;

public interface IProjectInterface
{
    List<ProjectsModels> GetAllProjects(); // تغییر نوع برگشتی به List<ProjectsModels>
    ProjectsModels GetProjectById(int projectId);
    ResponseModel AddProject(ProjectsModels project, List<TaskModel> tasks);
    ResponseModel UpdateProject(int projectId, ProjectsModels project);
    ResponseModel DeleteProject(int projectId);
    ResponseModel AddUserToProject(int projectId, int userId);
}
