# Invoice Management System - Coding Exercise Instructions
## Overview
You are working on an invoice management system that allows CRUD operations on invoices and their line items. The initial structure and some methods have been implemented, but you need to complete three methods related to invoice line management.
Your Task

## Note
You must have the SDK for .NET 9 installed
If the API is running, you should be able to access the API Docs via
https://localhost:7153/swagger/index.html

## Complete the implementation of the following three methods in the InvoiceQuery class:
### 1. CreateLineAsync(int invoiceId, InvoiceLineMutation mutation)
This method should:
- Create a new invoice line item for the specified invoice
- Assign the next available line number sequentially
- Add the new line to the invoice's Items collection
- Save the updated data
- Return the newly created line number
### 2. UpdateLineAsync(int invoiceId, int lineNumber, InvoiceLineMutation mutation)
This method should:
- Find the specified line item in the invoice
- Update its properties with the values from the mutation object
- Save the changes to the data store
### 3. DeleteLineAsync(int invoiceId, int lineNumber)
This method should:
- Remove the specified line item from the invoice
- Re-sequence the remaining line numbers to ensure they remain sequential (no gaps)
- Save the updated data to the data store
### Important Notes
- Follow the existing code patterns shown in the other methods
- Remember to save changes using the jsonDataService.SaveDataAsync method
- Maintain proper null checking and error handling
- Ensure async/await patterns are used correctly
- The Items property in the Invoice class is a list of InvoiceLine objects
- Make sure to respect the simulated async work with the delay mechanism
### Evaluation Criteria
- Correctness of implementation
- Code quality and consistency with existing patterns
- Proper error handling
- Understanding of asynchronous programming
- Attention to requirements (especially re-sequencing in delete operation)
Good luck!
