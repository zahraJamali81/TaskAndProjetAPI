using Microsoft.EntityFrameworkCore;
using TaskAndProjet.API.Interfaces;
using TaskAndProjet.API.Models;
using TaskAndProjet.API.ViewModels;

public class ProjectsService : IProjectInterface
{
    private readonly Context _context;

    public ProjectsService(Context context)
    {
        _context = context;
    }

    public List<ProjectsModels> GetAllProjects()
    {
        return _context.Projects.Include(p => p.UserProjects).ThenInclude(up => up.Users).ToList();
    }

    public ProjectsModels GetProjectById(int projectId)
    {
        return _context.Projects
                       .Include(p => p.UserProjects)
                       .ThenInclude(up => up.Users)
                       .Select(p => new ProjectsModels
                       {
                           Id = p.Id,
                           Name = p.Name,
                           Description = p.Description,
                           UserProjects = p.UserProjects.Select(up => new UserProjectsModels
                           {
                               UserId = up.UserId,
                               Users = new UserModels
                               (
                                   up.Users.Id, // مقدار id ارسال می‌شود
                                   up.Users.UserName,
                                   up.Users.FirstName,
                                   up.Users.LastName)
                           }).ToList()
                       }).FirstOrDefault(p => p.Id == projectId);
    }

    public ResponseModel AddProject(ProjectsModels project, List<TaskModel> tasks)
    {
        _context.Projects.Add(project);
        _context.SaveChanges();

        foreach (var item in tasks)
        {
            _context.Tasks.Add(new TaskModel { ProjectId = project.Id, Title = item.Title, Description = item.Description });
        }
        _context.SaveChanges();

        return new ResponseModel { Message = "Project and tasks added successfully" };
    }

    public ResponseModel UpdateProject(int projectId, ProjectsModels project)
    {
        var existingProject = _context.Projects.Find(projectId);
        if (existingProject != null)
        {
            existingProject.Name = project.Name;
            existingProject.Description = project.Description;
            // به‌روزرسانی سایر ویژگی‌ها در صورت نیاز

            _context.SaveChanges();
            return new ResponseModel { Message = "Project updated successfully" };
        }
        return new ResponseModel { Message = "Project not found" };
    }

    public ResponseModel DeleteProject(int projectId)
    {
        var project = _context.Projects.Find(projectId);
        if (project != null)
        {
            _context.Projects.Remove(project);
            _context.SaveChanges();
            return new ResponseModel { Message = "Project deleted successfully" };
        }
        return new ResponseModel { Message = "Project not found" };
    }

    public ResponseModel AddUserToProject(int projectId, int userId)
    {
        var userProject = new UserProjectsModels
        {
            ProjectId = projectId,
            UserId = userId
        };

        _context.UserProjects.Add(userProject);
        _context.SaveChanges();

        return new ResponseModel { Message = "User added to project successfully" };
    }
}
