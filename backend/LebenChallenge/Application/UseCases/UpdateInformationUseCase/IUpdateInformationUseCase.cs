using LebenChallenge.Application.DTO;
using LebenChallenge.Domain;

namespace LebenChallenge.Application.UseCases.UpdateInformationUseCase
{
    public interface IUpdateInformationUseCase
    {
        Task<TaskItem> ExecuteAsync(UpdateTaskDTO editarTareaDTO);
    }
}
