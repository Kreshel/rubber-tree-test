using rubber_tree_test_backend.Interfaces;
using rubber_tree_test_backend.Models;
using rubber_tree_test_backend.Mutations;

namespace rubber_tree_test_backend.Queries;

public class InvoiceQuery(IJsonDataService jsonDataService)
{
    private readonly int _delay = 100; // Simulate async work

    public async Task<Invoice?> GetInvoiceByIdAsync(int id)
    {
        List<Invoice> invoices = await jsonDataService.GetDataAsync<Invoice>("invoices.json");

        // Simulate a database call
        await Task.Delay(_delay); // Simulate async work

        return invoices.FirstOrDefault(i => i.Id == id);
    }

    public async Task<List<Invoice>> GetAllInvoicesAsync()
    {
        List<Invoice> invoices = await jsonDataService.GetDataAsync<Invoice>("invoices.json");

        // Simulate a database call
        await Task.Delay(_delay); // Simulate async work

        return invoices;
    }

    public async Task<int> CreateAsync(InvoiceMutation mutation)
    {
        List<Invoice> invoices = await jsonDataService.GetDataAsync<Invoice>("invoices.json");

        // Simulate a database call
        await Task.Delay(_delay); // Simulate async work

        Invoice invoice = new()
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
        List<Invoice> invoices = await jsonDataService.GetDataAsync<Invoice>("invoices.json");

        // Simulate a database call
        await Task.Delay(_delay); // Simulate async work

        Invoice? existingInvoice = invoices.FirstOrDefault(i => i.Id == invoiceId);
        if (existingInvoice is not null)
        {
            existingInvoice.CustomerName = mutation.CustomerName;
            existingInvoice.CustomerAddress = mutation.CustomerAddress;

        }

        await jsonDataService.SaveDataAsync("invoices.json", invoices);
    }

    public async Task DeleteAsync(int invoiceId)
    {
        List<Invoice> invoices = await jsonDataService.GetDataAsync<Invoice>("invoices.json");

        // Simulate a database call
        await Task.Delay(_delay); // Simulate async work

        Invoice? invoiceToDelete = invoices.FirstOrDefault(i => i.Id == invoiceId);
        if (invoiceToDelete is not null)
        {
            invoices.Remove(invoiceToDelete);
        }

        await jsonDataService.SaveDataAsync("invoices.json", invoices);
    }

    public async Task<int?> CreateLineAsync(int invoiceId, InvoiceLineMutation mutation)
    {
        List<Invoice> invoices = await jsonDataService.GetDataAsync<Invoice>("invoices.json");

        // Simulate a database call
        await Task.Delay(_delay); // Simulate async work

        Invoice? invoice = invoices.FirstOrDefault(i => i.Id == invoiceId);

        if (invoice is not null)
        {
            // perform create action here, the line number should be the next available line number for the invoice
            // make sure to return the newly created line number if successful
        }

        return null;
    }

    public async Task UpdateLineAsync(int invoiceId, int lineNumber, InvoiceLineMutation mutation)
    {
        List<Invoice> invoices = await jsonDataService.GetDataAsync<Invoice>("invoices.json");

        // Simulate a database call
        await Task.Delay(_delay); // Simulate async work

        Invoice? invoice = invoices.FirstOrDefault(i => i.Id == invoiceId);

        if (invoice is not null)
        {
            // perform update action here
        }

    }

    public async Task DeleteLineAsync(int invoiceId, int lineNumber)
    {
        List<Invoice> invoices = await jsonDataService.GetDataAsync<Invoice>("invoices.json");

        // Simulate a database call
        await Task.Delay(_delay); // Simulate async work

        Invoice? invoice = invoices.FirstOrDefault(i => i.Id == invoiceId);

        if (invoice is not null)
        {
            // perform delete action here, remember to re-sequence the line numbers so they remain sequential 

            //await jsonDataService.SaveDataAsync("invoices.json", invoices);
        }
    }
}
