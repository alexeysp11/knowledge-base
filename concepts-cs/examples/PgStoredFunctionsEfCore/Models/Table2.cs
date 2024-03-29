using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Concepts.Examples.PgStoredFunctionsEfCore.Models;

[Table("table2")]
public class Table2
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [Column("value")]
    public int Value { get; set; }
}