using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Concepts.Examples.PgStoredFunctionsEfCore.Models;

[Table("processeddata")]
public class ProcessedData
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [Column("name")]
    [MaxLength(50)]
    public string Name { get; set; }

    [Required]
    [Column("value")]
    public int Value { get; set; }
}