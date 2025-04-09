using System.ComponentModel.DataAnnotations;

namespace rubber_tree_test_backend.Mutations;

public class InvoiceMutation
{
    [Required]
    public string CustomerName { get; set; } = null!;
    [Required]
    public string CustomerAddress { get; set; } = null!;
}

public class InvoiceLineMutation
{
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

