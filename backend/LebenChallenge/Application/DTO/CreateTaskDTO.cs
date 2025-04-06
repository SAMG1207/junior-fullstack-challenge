using System.ComponentModel;

namespace LebenChallenge.Application.DTO;

public struct CreateTaskDTO
{

    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }

    [DefaultValue(1)]
    public int Priority { get; set; } 
}
