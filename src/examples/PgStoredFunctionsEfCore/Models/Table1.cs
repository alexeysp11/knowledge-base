using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KnowledgeBase.Examples.PgStoredFunctionsEfCore.Models;

[Table("table1")]
public class Table1
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [Column("name")]
    [MaxLength(50)]
    public string Name { get; set; }
}