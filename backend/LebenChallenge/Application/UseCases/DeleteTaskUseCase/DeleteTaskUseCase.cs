using LebenChallenge.Application.DTO;
using LebenChallenge.Application.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;

namespace LebenChallenge.Application.UseCases;

public class DeleteTaskUseCase : IDeleteTaskUseCase
{
    private readonly ITaskRepository _taskRepository;

    public DeleteTaskUseCase(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task ExecuteAsync(DeleteTaskDTO taskToDelete)
    {
        await _taskRepository.DeleteAsync(taskToDelete.Id);
    }
}
