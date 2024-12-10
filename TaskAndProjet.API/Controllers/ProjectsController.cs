using Microsoft.AspNetCore.Mvc;
using TaskAndProjet.API.Interfaces;
using TaskAndProjet.API.Models;
using TaskAndProjet.API.ViewModels;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace TaskAndProjet.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectInterface _projectService;

        public ProjectsController(IProjectInterface projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        public IActionResult GetAllProjects()
        {
            var projects = _projectService.GetAllProjects();
            return Ok(projects);
        }

        [HttpGet("{id}")]
        public IActionResult GetProjectById(int id)
        {
            var project = _projectService.GetProjectById(id);
            if (project == null)
            {
                return NotFound();
            }
            return Ok(project);
        }

        [HttpPost]
        public IActionResult AddProject([FromBody] AddProjectViewModel model)
        {
            var response = _projectService.AddProject(model.Project, model.Tasks);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            return CreatedAtAction(nameof(GetProjectById), new { id = model.Project.Id }, model.Project);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProject(int id, [FromBody] ProjectsModels project)
        {
            var response = _projectService.UpdateProject(id, project);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProject(int id)
        {
            var response = _projectService.DeleteProject(id);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            return NoContent();
        }

        [HttpPost("{projectId}/users/{userId}")]
        public IActionResult AddUserToProject(int projectId, int userId)
        {
            var response = _projectService.AddUserToProject(projectId, userId);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            return NoContent();
        }
    }
}
