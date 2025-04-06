using LebenChallenge.Application.DTO;
using LebenChallenge.Application.Interfaces;
using LebenChallenge.Domain;
using LebenChallenge.Infrastructure.Persistence;
using LebenChallenge.Utils;
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
        TaskItem taskItem = new TaskItem(task.Name, task.Description, task.DueDate, task.Priority);
        _context.Tasks.Add(taskItem);

        await _context.SaveChangesAsync();

        return taskItem;
    }

    public async Task<TaskItem> AssignPriority(AssignPriorityDTO assignPriorityDTO)
    {
        // Verifica que exista esta task
        var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == assignPriorityDTO.Id) ?? throw new Exception("Task not found");
        // Si existe, asigna la prioridad
        task.UpdatePriority(assignPriorityDTO.Priority);
        // Guarda los cambios
        await _context.SaveChangesAsync();
        // Devuelve la tarea actualizada
        return task;
    }

    public async Task DeleteAsync(int id)
    {
            // Verifica que exista esta task
            var task = await _context.Tasks.FindAsync(id) ?? throw new Exception("Task not found");
            // Si existe, la elimina
            _context.Tasks.Remove(task);
            // Guarda los cambios
            await _context.SaveChangesAsync();
    }
 
    public async Task<TaskItem> EditTask(UpdateTaskDTO updateTaskDTO)
    {
        // Verifica que exista esta task
        var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == updateTaskDTO.Id) ?? throw new Exception("Task not found");

        // Si existe a través de los métodos de la clase TaskItem y del DTO, actualiza los datos
        task.UpdateName(updateTaskDTO.changeDataDTO.Name);
        task.UpdateDescription(updateTaskDTO.changeDataDTO.Description);
        task.UpdateDueDate(updateTaskDTO.changeDataDTO.DueDate);

        // Guarda los cambios
        await _context.SaveChangesAsync();
        // Devuelve la tarea actualizada
        return task;
    }

    public async Task<IEnumerable<TaskItem>> GetAllAsync()
    {
        // Devuelve todas las tasks
       var Task = await _context.Tasks.ToListAsync();
       return Task;
    }

    public async Task<TaskItem> GetByIdAsync(int id)
    {
        // Verifica que exista esta task
        var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id) ?? throw new Exception("Task not found");
        // Si existe, la devuelve
        return task;

    }

    public async Task<TaskItem> UpdateAsync(TaskItem task)
    {
        // Verifica que exista esta task
        task.MarkAsCompleted();
        // Si existe, la actualiza
        _context.Tasks.Update(task);
        // Guarda los cambios
        await _context.SaveChangesAsync();
        // Devuelve la task actualizada
        return task;

    }
}
