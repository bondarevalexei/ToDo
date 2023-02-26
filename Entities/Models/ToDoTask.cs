using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class ToDoTask
{
    [Column("ToDoTaskId")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Title is a required field.")]
    [MaxLength(100, ErrorMessage = "Max length for the title is 100 characters.")]
    public string? Title { get; set; }

    [Required(ErrorMessage = "Description is a required field.")]
    [MaxLength(100, ErrorMessage = "Max length for the description is 100 characters.")]
    public string? Description { get; set; }

    [ForeignKey(nameof(Account))]
    public Guid AccountId { get; set; }
    public Account? Account { get; set; }
}
