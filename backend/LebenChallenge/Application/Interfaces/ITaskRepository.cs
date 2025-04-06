using LebenChallenge.Application.DTO;
using LebenChallenge.Domain;

namespace LebenChallenge.Application.Interfaces;

public interface ITaskRepository
{
    Task<TaskItem> AddAsync(TaskItem task);
    Task<TaskItem> GetByIdAsync(int id);
    Task<IEnumerable<TaskItem>> GetAllAsync();
    Task<TaskItem> UpdateAsync(TaskItem task);
    Task DeleteAsync(int id);

    Task<TaskItem> EditTask(UpdateTaskDTO updateTaskDTO);

    Task<TaskItem> AssignPriority(AssignPriorityDTO assignPriorityDTO);
}
