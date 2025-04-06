namespace LebenChallenge.Application.DTO
{
    public struct UpdateTaskDTO
    {
        public int Id { get; set; }
        
        public ChangeDataDTO changeDataDTO { get; set; }
    }
}
