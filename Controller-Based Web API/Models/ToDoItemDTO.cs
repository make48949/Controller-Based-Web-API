namespace Controller_Based_Web_API.Models
{
    public class ToDoItemDTO
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public bool IsComplete { get; set; }
    }
}
