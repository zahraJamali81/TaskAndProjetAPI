using TaskAndProjet.API.Interfaces;
using TaskAndProjet.API.Models;
using TaskAndProjet.API.ViewModels;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TaskAndProjet.API.Services
{
    public class TaskService : ITaskInterface
    {
        private readonly Context _context;

        public TaskService(Context context)
        {
            _context = context;
        }

        public List<TaskModel> GetTasks()
        {
            List<TaskModel> tasks;
            try
            {
                tasks = _context.Set<TaskModel>()
                    .ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
            return tasks;
        }

        public TaskModel GetTaskById(int taskId)
        {
            TaskModel task;
            try
            {
                task = _context.Find<TaskModel>(taskId);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return task;
        }

        public ResponseModel SaveTask(TaskModel task)
        {
            ResponseModel model = new ResponseModel();

            try
            {
                TaskModel _NewTask = GetTaskById(task.Id);
                if (_NewTask != null)
                {
                    _NewTask.Title = task.Title;
                    _NewTask.Description = task.Description;
                    _context.Update<TaskModel>(_NewTask);
                    model.Message = "New Task update Successfully";
                    model.Success = true;
                }
                else
                {
                    _context.Add<TaskModel>(task);
                    model.Message = "New Task Inserted Successfully";
                    model.Success = true;
                }
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                model.Message = "Error: " + ex.Message;
                model.Success = false;
            }

            return model;
        }



        public ResponseModel DeleteTask(int taskId)
        {
            ResponseModel model = new ResponseModel();
            try
            {
                TaskModel _TaskDel = GetTaskById(taskId);
                if (_TaskDel != null)
                {
                    _context.Remove<TaskModel>(_TaskDel);
                    _context.SaveChanges();
                    model.Success = true;
                    model.Message = "Task deleted successfully";
                }
                else
                {
                    model.Success = false;
                    model.Message = "Task Not Found";
                }
            }catch (Exception ex)
            {
                model.Success = false;
                model.Message = "Error :" + ex.Message;
            }

            return model;
        }
        public ResponseModel UpdateTask(TaskModel task)
        {
            var model = new ResponseModel();
            try
            {
                var existingTask = GetTaskById(task.Id);
                if (existingTask != null)
                {
                    existingTask.Title = task.Title ?? existingTask.Title;
                    existingTask.Description = task.Description ?? existingTask.Description;
                    _context.Update(existingTask);
                    _context.SaveChanges();
                    model.Message = "Task updated successfully";
                }
                else
                {
                    model.Message = "Task not found";
                }
            }
            catch (Exception ex)
            {
                model.Message = "Error: " + ex.Message;
            }
            return model;
        }


    }
}
