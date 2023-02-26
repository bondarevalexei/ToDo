using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class Account
{
    [Column("AccountId")]
    public Guid Id { get; set; }
}
