namespace Attempt1.DTOs
{
    public class CreateStudentDTO
    {
        public int Id { get; set; }
        public string StudentId { get; set; } = null!;
        public required string FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public string Course { get; set; } = string.Empty;
        public int Age { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    }
}
