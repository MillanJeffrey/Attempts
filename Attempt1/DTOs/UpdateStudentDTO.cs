using System.ComponentModel.DataAnnotations;

namespace Attempt1.DTOs
{
    public class UpdateStudentDTO
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public required string FirstName { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public required string LastName { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public required string Course { get; set; }
    }
}
