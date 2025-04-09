using System.ComponentModel.DataAnnotations;

namespace rubber_tree_test_backend.Models;

public class InvoiceHeader
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