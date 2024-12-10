using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using TaskAndProjet.API.Interfaces;
using TaskAndProjet.API.Models;

namespace TaskAndProjet.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class TasksController : ControllerBase
    {
        ITaskInterface _TaskService;
        public TasksController(ITaskInterface taskService)
        {
            _TaskService = taskService;
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult GetTasks()
        {
            try
            {
                var tasks = _TaskService.GetTasks();
                if (tasks == null) return NotFound();
                return Ok(tasks);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("[action]/id")]
        public IActionResult GetTaskById(int id)
        {
            try
            {
                var Task = _TaskService.GetTaskById(id);
                if (Task == null) return NotFound();
                return Ok(Task);
            }catch(Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult SaveTask(TaskModel NewTask)
        {
            try
            {
                var model = _TaskService.SaveTask(NewTask);
                return Ok(model);
            }catch
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("[action]")]
        public  IActionResult DeleteTask(int id)
        {
            try
            {
                var model = _TaskService.DeleteTask(id);
                return Ok(model);
            }catch 
            {
                return BadRequest();
            }
        }

        [HttpPatch]
        [Route("UpdateTask/{taskId}")]
        public IActionResult UpdateTask(int taskId, [FromBody] JsonPatchDocument<TaskModel> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            var task = _TaskService.GetTaskById(taskId);
            if (task == null)
            {
                return NotFound();
            }

            patchDoc.ApplyTo(task, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = _TaskService.UpdateTask(task);
            return Ok(response);
        }
 
    }

}
