using System.ComponentModel.DataAnnotations;

namespace Shared.DTO
{
    public abstract record ToDoTaskForManipulationDto
    {
        [Required(ErrorMessage = "Title is a required field.")]
        [MaxLength(100, ErrorMessage = "Max length for the title is 100 characters.")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Description is a required field.")]
        [MaxLength(100, ErrorMessage = "Max length for the description is 100 characters.")]
        public string? Description { get; set; }
    }
}