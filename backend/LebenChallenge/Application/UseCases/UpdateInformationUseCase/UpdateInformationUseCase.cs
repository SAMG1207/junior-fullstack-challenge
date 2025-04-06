using LebenChallenge.Application.DTO;
using LebenChallenge.Application.Interfaces;
using LebenChallenge.Domain;

namespace LebenChallenge.Application.UseCases.UpdateInformationUseCase
{
    public class UpdateInformationUseCase : IUpdateInformationUseCase
    {
        private readonly ITaskRepository _taskRepository;

        public UpdateInformationUseCase(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }
        public async Task<TaskItem> ExecuteAsync(UpdateTaskDTO updateTaskDTO)
        {
           await _taskRepository.EditTask(updateTaskDTO); // Modificamos la tarea
           return await _taskRepository.GetByIdAsync(updateTaskDTO.Id); // devolvemos la tarea modificada
        }
    }
}
