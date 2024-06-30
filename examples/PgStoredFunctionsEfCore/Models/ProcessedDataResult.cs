using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Concepts.Examples.PgStoredFunctionsEfCore.Models;

[Keyless]
public class ProcessedDataResult
{
    public long Result { get; set; }
}