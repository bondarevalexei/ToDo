namespace Shared.DTO
{
    public record AccountForCreationDto
    {
        public IEnumerable<ToDoTaskForCreationDto> ToDoTasks { get; init; }
    }
}
