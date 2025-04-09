using Microsoft.AspNetCore.Mvc;
using rubber_tree_test_backend.Interfaces;
using rubber_tree_test_backend.Models;
using rubber_tree_test_backend.Mutations;
using rubber_tree_test_backend.Queries;

namespace rubber_tree_test_backend.Controllers;

[ApiController]
[Route("[controller]")]
public class InvoiceController(IJsonDataService jsonDataService) : ControllerBase
{
    private readonly InvoiceQuery _invoiceQuery = new(jsonDataService);

    [HttpGet("{invoiceId}")]
    public async Task<ActionResult<InvoiceHeader>> GetInvoiceById(int invoiceId)
    {
        InvoiceHeader? invoice = await _invoiceQuery.GetInvoiceByIdAsync(invoiceId);
        if (invoice is null)
        {
            return NotFound();
        }

        return Ok(invoice);
    }

    [HttpGet("List")]
    public async Task<ActionResult<List<InvoiceHeader>>> GetAllInvoices() => Ok(await _invoiceQuery.GetAllInvoicesAsync());

    [HttpPost]
    public async Task<ActionResult<int>> CreateInvoice([FromBody] InvoiceMutation mutation)
    {
        if (mutation is null)
        {
            return BadRequest("Invoice cannot be null");
        }

        int newInvoiceId = await _invoiceQuery.CreateAsync(mutation);

        return newInvoiceId;
    }

    [HttpPut("{invoiceId}")]
    public async Task<IActionResult> UpdateInvoice(int invoiceId, [FromBody] InvoiceMutation invoice)
    {
        if (invoice is null)
        {
            return BadRequest("Invoice cannot be null");
        }

        await _invoiceQuery.UpdateAsync(invoiceId, invoice);

        return NoContent();
    }

    [HttpDelete("{invoiceId}")]
    public async Task<IActionResult> DeleteInvoice(int invoiceId)
    {
        await _invoiceQuery.DeleteAsync(invoiceId);

        return NoContent();
    }

    [HttpGet("Line/{invoiceId}/{lineNumber}")]
    public async Task<ActionResult<InvoiceLine>> GetInvoiceLineByInvoiceIdAndLineNumber(int invoiceId, int lineNumber)
    {
        InvoiceLine? invoiceLine = await _invoiceQuery.GetInvoiceLineByInvoiceIdAndLineNumber(invoiceId, lineNumber);
        if (invoiceLine is null)
        {
            return NotFound();
        }
        return Ok(invoiceLine);
    }

    [HttpPost("Line/{invoiceId}")]
    public async Task<ActionResult<int?>> CreateInvoiceLine(int invoiceId, [FromBody] InvoiceLineMutation mutation)
    {
        if (mutation is null)
        {
            return BadRequest("Invoice cannot be null");
        }

        return await _invoiceQuery.CreateLineAsync(invoiceId, mutation);
    }

    [HttpPut("Line/{invoiceId}/{lineNumber}")]
    public async Task<IActionResult> UpdateInvoiceLine(int invoiceId, int lineNumber, [FromBody] InvoiceLineMutation mutation)
    {
        if (mutation is null)
        {
            return BadRequest("Invoice cannot be null");
        }

        await _invoiceQuery.UpdateLineAsync(invoiceId, lineNumber, mutation);

        return NoContent();
    }

    [HttpDelete("Line/{invoiceId}/{lineNumber}")]
    public async Task<IActionResult> DeleteInvoiceLine(int invoiceId, int lineNumber)
    {
        await _invoiceQuery.DeleteLineAsync(invoiceId, lineNumber);

        return NoContent();
    }
}
