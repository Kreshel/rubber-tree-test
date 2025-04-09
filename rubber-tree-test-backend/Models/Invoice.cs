using System.ComponentModel.DataAnnotations;

namespace rubber_tree_test_backend.Models;

public class Invoice
{
    [Key]
    [Required]
    public int Id { get; set; }
    [Required]
    public string CustomerName { get; set; } = null!;
    [Required]
    public string CustomerAddress { get; set; } = null!;
    public List<InvoiceLine>? Items { get; set; }
}

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
    [Required]
    public decimal TotalPrice => UnitPrice * Quantity;
}
