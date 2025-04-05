using LebenChallenge.Application.DTO;
using LebenChallenge.Domain;

namespace LebenChallenge.Application.UseCases.UpdatePriorityUseCase
{
    public interface IUpdatePriorityUseCase
    {
        Task<TaskItem> ExecuteAsync(AsignarPrioridadDTO asignarPrioridadDTO);
    }
}
