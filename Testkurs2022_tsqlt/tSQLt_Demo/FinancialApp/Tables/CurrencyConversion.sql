CREATE TABLE [FinancialApp].[CurrencyConversion] (
    [id]             INT             NOT NULL,
    [sourceCurrency] CHAR (3)        NOT NULL,
    [destCurrency]   CHAR (3)        NOT NULL,
    [conversionRate] DECIMAL (10, 4) NOT NULL,
    [effectiveDate]  DATETIME        NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    FOREIGN KEY ([destCurrency]) REFERENCES [FinancialApp].[Currency] ([currencyCode]),
    FOREIGN KEY ([sourceCurrency]) REFERENCES [FinancialApp].[Currency] ([currencyCode])
);

