CREATE TABLE Invoice (
    Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    PayerName VARCHAR(100) NOT NULL,
    InvoiceNumber VARCHAR(100) NOT NULL,
    IssueDate DATETIME NOT NULL,
    BillingDate DATETIME NULL,
    PaymentDate DATETIME NULL,
    InvoiceAmount DECIMAL(10,2) NOT NULL,
    InvoiceDocument VARCHAR(100) NOT NULL,
    BankSlipDocument VARCHAR(100) NOT NULL,
    InvoiceStatus INT NOT NULL
);