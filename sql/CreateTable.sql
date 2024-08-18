USE [TaxInvoicesDB]
GO

/****** Object:  Table [dbo].[Invoice]    Script Date: 8/17/2024 7:39:52 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Invoice](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PayerName] [varchar](100) NOT NULL,
	[InvoiceNumber] [varchar](100) NOT NULL,
	[IssueDate] [datetime] NOT NULL,
	[BillingDate] [datetime] NULL,
	[PaymentDate] [datetime] NULL,
	[InvoiceAmount] [decimal](10, 2) NOT NULL,
	[InvoiceDocument] [varchar](100) NOT NULL,
	[BankSlipDocument] [varchar](100) NOT NULL,
	[InvoiceStatus] [int] NOT NULL,
	[PaymentPromise] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO