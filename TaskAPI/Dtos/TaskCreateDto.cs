namespace TaskApi.Dtos
{
    public class TaskCreateDto
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsDone { get; set; } = false;
    }
}
