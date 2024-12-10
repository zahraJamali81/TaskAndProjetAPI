using System.Collections.Generic;
using System.Linq;
using TaskAndProjet.API.Models;
using TaskAndProjet.API.Interfaces;
using Microsoft.EntityFrameworkCore;
using TaskAndProjet.API.ViewModels;

namespace TaskAndProjet.API.Services
{
    public class UserService : IUserInterface
    {
        private readonly Context _context;

        public UserService(Context context)
        {
            _context = context;
        }

        public List<UserModels> GetAllUsers()
        {
            return _context.Users.Include(u => u.UserProjects).ThenInclude(up => up.Projects).ToList();
        }

        public UserModels GetUserById(int userId)
        {
            return _context.Users
                           .Include(u => u.UserProjects)
                           .ThenInclude(up => up.Projects)
                           .FirstOrDefault(u => u.Id == userId);
        }

        public ResponseModel AddUser(UserModels user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return new ResponseModel { Success = true, Message = "User added successfully" };
        }

        public ResponseModel UpdateUser(int userId, UserModels user)
        {
            var existingUser = _context.Users.Find(userId);
            if (existingUser != null)
            {
                existingUser.FirstName = user.FirstName;
                existingUser.LastName = user.LastName;
                existingUser.UserName = user.UserName;
                existingUser.Password = user.Password;

                _context.SaveChanges();
                return new ResponseModel { Success = true, Message = "User updated successfully" };
            }
            return new ResponseModel { Success = false, Message = "User not found" };
        }

        public ResponseModel DeleteUser(int userId)
        {
            var user = _context.Users.Find(userId);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
                return new ResponseModel { Success = true, Message = "User deleted successfully" };
            }
            return new ResponseModel { Success = false, Message = "User not found" };
        }

        public ResponseModel AddProjectToUser(int userId, int projectId)
        {
            var user = _context.Users.Include(u => u.UserProjects).FirstOrDefault(u => u.Id == userId);
            var project = _context.Projects.Find(projectId);

            if (user == null || project == null)
            {
                return new ResponseModel { Success = false, Message = "User or Project not found" };
            }

            user.UserProjects.Add(new UserProjectsModels { UserId = userId, ProjectId = projectId });
            _context.SaveChanges();

            return new ResponseModel { Success = true, Message = "Project added to user successfully" };
        }
    }
}
