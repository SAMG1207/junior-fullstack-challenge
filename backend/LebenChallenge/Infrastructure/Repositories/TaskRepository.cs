using LebenChallenge.Application.DTO;
using LebenChallenge.Application.Interfaces;
using LebenChallenge.Domain;
using LebenChallenge.Infrastructure.Persistence;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace LebenChallenge.Infrastructure.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly InMemoryDbContext _context;

    public TaskRepository(InMemoryDbContext inMemoryDbContext)
    {
        _context = inMemoryDbContext;
    }

    public async Task<TaskItem> AddAsync(TaskItem task)
    {
        if (task.Priority < 1 || task.Priority > 5)
        {
            throw new Exception("Priority must be between 1 and 5");
        }
        TaskItem taskItem = new TaskItem(task.Name, task.Description, task.DueDate, task.Priority);
        _context.Tasks.Add(taskItem);

        await _context.SaveChangesAsync();

        return taskItem;
    }

    public async Task<TaskItem> AsignarPrioridad(AsignarPrioridadDTO asignarPrioridadDTO)
    {
        if (asignarPrioridadDTO.Prioridad < 1 || asignarPrioridadDTO.Prioridad > 5)
        {
            throw new Exception("Priority must be between 1 and 5");
        }
        var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == asignarPrioridadDTO.Id);
        if (task == null)
        {
            throw new Exception("Task not found");
        }
        task.UpdatePriority(asignarPrioridadDTO.Prioridad);
        await _context.SaveChangesAsync();
        return task;
    }

    public async Task DeleteAsync(int id)
    {
        try
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                throw new Exception("Task not found");
            }
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
        }
        catch(Exception)
        {
            throw new Exception("Error deleting task");
        }

    }

    public async Task<TaskItem> EditarTarea(EditarTareaDTO editarTareaDTO)
    {
        var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == editarTareaDTO.Id);
        if (task == null)
        {
            throw new Exception("Task not found");
        }
       
         task.UpdateName(editarTareaDTO.datosACambiarDTO.Nombre);
         task.UpdateDescription(editarTareaDTO.datosACambiarDTO.Descripcion);
         task.UpdateDueDate(editarTareaDTO.datosACambiarDTO.FechaLimite);

        await _context.SaveChangesAsync();
        return task;
    }

    public async Task<IEnumerable<TaskItem>> GetAllAsync()
    {
       var Task = await _context.Tasks.ToListAsync();
       return Task;
    }

    public async Task<TaskItem> GetByIdAsync(int id)
    {
        try
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id);
            if (task == null)
            {
                throw new Exception("Task not found");
            }
            return task;
        }
        catch (Exception)
        {
            throw new Exception("Error getting task");
        }
    }

    public async Task<TaskItem> UpdateAsync(TaskItem task)
    {
        try
        {
            task.MarkAsCompleted();
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
            return task;
        }
        catch (Exception)
        {
            throw new Exception("Error updating task");
        }
     
    }
}
