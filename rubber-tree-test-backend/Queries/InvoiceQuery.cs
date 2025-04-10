using rubber_tree_test_backend.Interfaces;
using rubber_tree_test_backend.Models;
using rubber_tree_test_backend.Mutations;

namespace rubber_tree_test_backend.Queries;

public class InvoiceQuery(IJsonDataService jsonDataService)
{
    private readonly int _delay = 100; // Simulate async work

    public async Task<InvoiceHeader?> GetInvoiceByIdAsync(int id)
    {
        List<InvoiceHeader> invoices = await jsonDataService.GetDataAsync<InvoiceHeader>("invoices.json");

        // Simulate a database call
        await Task.Delay(_delay); // Simulate async work

        return invoices.FirstOrDefault(i => i.Id == id);
    }

    public async Task<List<InvoiceHeader>> GetAllInvoicesAsync()
    {
        List<InvoiceHeader> invoices = await jsonDataService.GetDataAsync<InvoiceHeader>("invoices.json");

        // Simulate a database call
        await Task.Delay(_delay); // Simulate async work

        return invoices;
    }

    public async Task<int> CreateAsync(InvoiceMutation mutation)
    {
        List<InvoiceHeader> invoices = await jsonDataService.GetDataAsync<InvoiceHeader>("invoices.json");

        // Simulate a database call
        await Task.Delay(_delay); // Simulate async work

        InvoiceHeader invoice = new()
        {
            Id = invoices.Count > 0 ? invoices.Max(i => i.Id) + 1 : 1,
            CustomerName = mutation.CustomerName,
            CustomerAddress = mutation.CustomerAddress,
            Items = []
        };

        invoices.Add(invoice);

        await jsonDataService.SaveDataAsync("invoices.json", invoices);

        return invoice.Id;
    }

    public async Task UpdateAsync(int invoiceId, InvoiceMutation mutation)
    {
        List<InvoiceHeader> invoices = await jsonDataService.GetDataAsync<InvoiceHeader>("invoices.json");

        // Simulate a database call
        await Task.Delay(_delay); // Simulate async work

        InvoiceHeader? existingInvoice = invoices.FirstOrDefault(i => i.Id == invoiceId);
        if (existingInvoice is not null)
        {
            existingInvoice.CustomerName = mutation.CustomerName;
            existingInvoice.CustomerAddress = mutation.CustomerAddress;

        }

        await jsonDataService.SaveDataAsync("invoices.json", invoices);
    }

    public async Task DeleteAsync(int invoiceId)
    {
        List<InvoiceHeader> invoices = await jsonDataService.GetDataAsync<InvoiceHeader>("invoices.json");

        // Simulate a database call
        await Task.Delay(_delay); // Simulate async work

        InvoiceHeader? invoiceToDelete = invoices.FirstOrDefault(i => i.Id == invoiceId);
        if (invoiceToDelete is not null)
        {
            invoices.Remove(invoiceToDelete);
        }

        await jsonDataService.SaveDataAsync("invoices.json", invoices);
    }

    public async Task<InvoiceLine?> GetInvoiceLineByInvoiceIdAndLineNumber(int invoiceId, int lineNumber)
    {
        List<InvoiceHeader> invoices = await jsonDataService.GetDataAsync<InvoiceHeader>("invoices.json");

        // Simulate a database call
        await Task.Delay(_delay); // Simulate async work

        InvoiceHeader? invoice = invoices.FirstOrDefault(i => i.Id == invoiceId);

        if (invoice is not null)
        {
            return invoice.Items?.FirstOrDefault(i => i.LineNumber == lineNumber);
        }

        return null;
    }

    public async Task<int?> CreateLineAsync(int invoiceId, InvoiceLineMutation mutation)
    {
        List<InvoiceHeader> invoices = await jsonDataService.GetDataAsync<InvoiceHeader>("invoices.json");

        // Simulate a database call
        await Task.Delay(_delay); // Simulate async work

        InvoiceHeader? invoice = invoices.FirstOrDefault(i => i.Id == invoiceId);

        if (invoice is not null)
        {
            // Create a new invoice line item for the specified invoice
            InvoiceLine line = new()
            {
                InvoiceId = invoice.Id,
                // Assign the next available line number sequentially
                LineNumber = invoice.Items?.Count > 0 ? invoice.Items.Max(l => l.LineNumber) + 1 : 1,
                ItemNumber = mutation.ItemNumber,
                Description = mutation.Description,
                UnitPrice = mutation.UnitPrice,
                Quantity = mutation.Quantity
            };

            // Add the new line to the invoice's Items collection
            invoice.Items!.Add(line);

            // Save the updated data
            await jsonDataService.SaveDataAsync("invoices.json", invoices);

            // Return the newly created line number
            return line.LineNumber;
        }

        return null;
    }

    public async Task UpdateLineAsync(int invoiceId, int lineNumber, InvoiceLineMutation mutation)
    {
        List<InvoiceHeader> invoices = await jsonDataService.GetDataAsync<InvoiceHeader>("invoices.json");

        // Simulate a database call
        await Task.Delay(_delay); // Simulate async work

        InvoiceHeader? invoice = invoices.FirstOrDefault(i => i.Id == invoiceId);

        if (invoice is not null)
        {
            // Find the specified line item in the invoice
            InvoiceLine? existingLine = invoice.Items?.FirstOrDefault(l => l.LineNumber == lineNumber);

            if (existingLine is not null)
            {
                // Update its properties with the values from the mutation object
                existingLine.ItemNumber = mutation.ItemNumber;
                existingLine.Description = mutation.Description;
                existingLine.UnitPrice = mutation.UnitPrice;
                existingLine.Quantity = mutation.Quantity;

                // Save the changes to the data store
                await jsonDataService.SaveDataAsync("invoices.json", invoices);
            }
        }

    }

    public async Task DeleteLineAsync(int invoiceId, int lineNumber)
    {
        List<InvoiceHeader> invoices = await jsonDataService.GetDataAsync<InvoiceHeader>("invoices.json");

        // Simulate a database call
        await Task.Delay(_delay); // Simulate async work

        InvoiceHeader? invoice = invoices.FirstOrDefault(i => i.Id == invoiceId);

        if (invoice is not null)
        {
            // Remove the specified line item from the invoice
            InvoiceLine? lineToDelete = invoice.Items?.FirstOrDefault(l => l.LineNumber == lineNumber);

            if (lineToDelete is not null)
            {
                invoice.Items?.Remove(lineToDelete);

                // Re - sequence the remaining line numbers to ensure they remain sequential(no gaps)
                int sequence = 1;
                foreach (var line in invoice.Items!.OrderBy(l => l.LineNumber))
                {
                    line.LineNumber = sequence;
                    sequence++;
                }

                // Save the updated data to the data store
                await jsonDataService.SaveDataAsync("invoices.json", invoices);
            }
        }
    }
}
