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
    public class UsersController : ControllerBase
    {
        private readonly IUserInterface _userService;

        public UsersController(IUserInterface userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _userService.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public IActionResult AddUser([FromBody] UserModels user)
        {
            var response = _userService.AddUser(user);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] UserModels user)
        {
            var response = _userService.UpdateUser(id, user);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var response = _userService.DeleteUser(id);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            return NoContent();
        }

        [HttpPost("{userId}/projects/{projectId}")]
        public IActionResult AddProjectToUser(int userId, int projectId)
        {
            var response = _userService.AddProjectToUser(userId, projectId);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            return NoContent();
        }
    }
}
