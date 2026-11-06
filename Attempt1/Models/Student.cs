namespace Attempt1.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string StudentId { get; set; } = null!;
        public required string FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public string Course { get; set; } = string.Empty;
        public int Age { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
