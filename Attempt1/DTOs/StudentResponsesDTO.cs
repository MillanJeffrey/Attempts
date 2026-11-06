namespace Attempt1.DTOs
{
    public class StudentResponsesDTO
    {
        public int Id { get; set; }
        public required string FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public string Course { get; set; } = string.Empty;
    }
}
