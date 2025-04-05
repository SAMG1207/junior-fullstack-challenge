namespace LebenChallenge.Application.DTO;

public struct CreateTaskDTO
{

    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }

    public int Priority { get; set; } 
}
