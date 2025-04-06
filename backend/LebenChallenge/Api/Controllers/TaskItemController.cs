using LebenChallenge.Application.DTO;
using LebenChallenge.Application.Interfaces;
using LebenChallenge.Application.UseCases;
using LebenChallenge.Application.UseCases.UpdateInformationUseCase;
using LebenChallenge.Application.UseCases.UpdatePriorityUseCase;
using LebenChallenge.Domain;
using Microsoft.AspNetCore.Mvc;

namespace LebenChallenge.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskItemController : ControllerBase
{
    private readonly ICreateTaskUseCase _createTaskUseCase;
    private readonly IGetAllTasksUseCase _getAllTasksUseCase;
    private readonly ICompleteTaskUseCase _completeTaskUseCase;
    private readonly IDeleteTaskUseCase _deleteTaskUseCase;
    private readonly IGetTaskByIdUseCase _getTaskByIdUseCase;
    private readonly IUpdateInformationUseCase _updateInformationUseCase;
    private readonly IUpdatePriorityUseCase _updatePriorityUseCase;

    public TaskItemController(
        ICreateTaskUseCase createTaskUseCase,
        ICompleteTaskUseCase completeTaskUseCase,
        IGetAllTasksUseCase getAllTasksUseCase,
        IDeleteTaskUseCase deleteTaskUseCase,
        IGetTaskByIdUseCase getTaskByIdUseCase,
        IUpdateInformationUseCase updateInformationUseCase,
        IUpdatePriorityUseCase updatePriorityUseCase

    )
    {
        _createTaskUseCase = createTaskUseCase;
        _completeTaskUseCase = completeTaskUseCase;
        _getAllTasksUseCase = getAllTasksUseCase;
        _deleteTaskUseCase = deleteTaskUseCase;
        _getTaskByIdUseCase = getTaskByIdUseCase;
        _updateInformationUseCase = updateInformationUseCase;
        _updatePriorityUseCase = updatePriorityUseCase;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var tasks = await _getAllTasksUseCase.ExecuteAsync();
        return Ok(tasks);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        TaskItem task = await _getTaskByIdUseCase.ExecuteAsync(id);
        return task != null ? Ok(task) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTaskDTO dto)
    {
        TaskItem newTaskItem = await _createTaskUseCase.ExecuteAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = newTaskItem.Id }, newTaskItem);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _deleteTaskUseCase.ExecuteAsync(new DeleteTaskDTO { Id = id });
        return NoContent();
    }

    [HttpPut("{id}/complete")]
    public async Task<IActionResult> Complete(int id)
    {
        CompleteTaskDTO dto = new CompleteTaskDTO { Id = id };
        TaskItem task = await _completeTaskUseCase.ExecuteAsync(dto);
        return task != null ? Ok(task) : NotFound();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTaskData(int id, [FromBody] ChangeDataDTO _changeDataDTO)
    {
        UpdateTaskDTO _updateTaskDTO = new UpdateTaskDTO
        {
            Id = id,
            changeDataDTO = _changeDataDTO
        };
        TaskItem task = await _updateInformationUseCase.ExecuteAsync(_updateTaskDTO);
        return task != null ? Ok(task) : NotFound();

    }

    [HttpPut("{id}/priority")]
    public async Task<IActionResult> UpdatePriority(int id, int priority)
    {
        AssignPriorityDTO assignPriorityDTO = new AssignPriorityDTO
        {
            Id = id,
            Priority = priority
        };
        TaskItem task = await _updatePriorityUseCase.ExecuteAsync(assignPriorityDTO);
        return task != null ? Ok(task) : NotFound();
    }
}
