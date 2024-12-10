using TaskAndProjet.API.ViewModels;
using TaskAndProjet.API.Models;
using System.Collections.Generic;

namespace TaskAndProjet.API.Interfaces
{
    public interface ITaskInterface
    {
        List<TaskModel> GetTasks();
        TaskModel GetTaskById(int taskId);
        ResponseModel SaveTask(TaskModel task);
        ResponseModel DeleteTask(int taskId);
        ResponseModel UpdateTask(TaskModel task);
        
    }
}
