namespace Attempt1.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public required string Email { get; set; } = string.Empty;
        public required string PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}
