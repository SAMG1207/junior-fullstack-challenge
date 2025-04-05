using LebenChallenge.Application.DTO;
using LebenChallenge.Application.Interfaces;
using LebenChallenge.Domain;

namespace LebenChallenge.Application.UseCases.UpdatePriorityUseCase
{
    public class UpdatePriorityUseCase : IUpdatePriorityUseCase
    {
        private readonly ITaskRepository _taskRepository;
        public UpdatePriorityUseCase(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }
        public async Task<TaskItem> ExecuteAsync(AsignarPrioridadDTO asignarPrioridadDTO)
        {
           return await _taskRepository.AsignarPrioridad(asignarPrioridadDTO);
        }
    }
}
