using System.Collections.Generic;
using TaskAndProjet.API.Models;
using TaskAndProjet.API.ViewModels;

namespace TaskAndProjet.API.Interfaces
{
    public interface IUserInterface
    {
        List<UserModels> GetAllUsers();
        UserModels GetUserById(int userId);
        ResponseModel AddUser(UserModels user);
        ResponseModel UpdateUser(int userId, UserModels user);
        ResponseModel DeleteUser(int userId);
        ResponseModel AddProjectToUser(int userId, int projectId); // افزودن متد جدید
    }
}
