using System.ComponentModel.DataAnnotations;

namespace rubber_tree_test_backend.Models;

public class InvoiceLine
{
    [Required]
    public int InvoiceId { get; set; }
    [Required]
    public int LineNumber { get; set; }
    [Required]
    public string ItemNumber { get; set; } = null!;
    public string? Description { get; set; }
    [Required]
    public decimal UnitPrice { get; set; }
    [Required]
    public int Quantity { get; set; }
}
