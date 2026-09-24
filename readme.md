classDiagram
    %% ===== Enums =====
    class TransactionType {
        <<enumeration>>
        Income
        Expense
    }
 
    class IncomeCategory {
        <<enumeration>>
        Salary
        FreeLancing
        Other
    }
 
    class ExpenseCategory {
        <<enumeration>>
        Food
        Travel
        Utilities
        Entertainment
        Other
    }
 
    %% ===== Interfaces =====
    class ITransaction {
        <<interface>>
        +string Id
        +decimal Amount
        +DateOnly Date
        +TransactionType Type
    }
 
    class IRepository {
        <<interface>>
        +Add(ITransaction transaction) void
        +Delete(string id, TransactionType type) bool
        +GetAll(TransactionType type) List~ITransaction~
        +GetById(string id, TransactionType type) ITransaction
        +Update(ITransaction transaction) void
        +GetByType(TransactionType type) List~ITransaction~
    }
 
    %% ===== Models =====
    class Income {
        +string Id
        +List~IncomeCategoryAmount~ Entries
        +TransactionType Type
        +DateOnly Date
        +decimal Amount
    }
 
    class Expense {
        +string Id
        +List~ExpenseCategoryAmount~ Entries
        +TransactionType Type
        +DateOnly Date
        +decimal Amount
    }
 
    class IncomeCategoryAmount {
        +IncomeCategory Category
        +decimal Amount
        +DateOnly Date
    }
 
    class ExpenseCategoryAmount {
        +ExpenseCategory Category
        +decimal Amount
        +DateOnly Date
    }
 
    %% ===== Repositories =====
    class FinanceRepository {
        -List~ITransaction~ _transactions
        +Add(ITransaction) void
        +Delete(string, TransactionType) bool
        +GetAll(TransactionType) List~ITransaction~
        +GetById(string, TransactionType) ITransaction
        +Update(ITransaction) void
        +GetByType(TransactionType) List~ITransaction~
        +Exists(string, TransactionType) bool
    }
 
    class CsvRepository {
        -string _filePath
        +Add(ITransaction) void
        +Delete(string, TransactionType) bool
        +GetAll(TransactionType) List~ITransaction~
        +GetById(string, TransactionType) ITransaction
        +Update(ITransaction) void
        +GetByType(TransactionType) List~ITransaction~
        -LoadAll() List~ITransaction~
        -SaveAll(List~ITransaction~) void
    }
 
    %% ===== Service =====
    class FinanceService {
        -IRepository _repo
        +Add(ITransaction, TransactionType) void
        +GetAll(TransactionType) List~ITransaction~
        +GetById(string, TransactionType) ITransaction
        +Exists(string, TransactionType) bool
        +Update(string, ITransaction) bool
        +Delete(string, TransactionType) bool
        +GetUserSummary(string) tuple
        +HasBothIncomeAndExpense(string) bool
        +GetByDateRange(string, TransactionType, DateOnly, DateOnly) tuple
        +GetMonthlyCategoryBreakdown(string, TransactionType) Dictionary
    }
 
    %% ===== Views =====
    class IncomeView {
        -FinanceService _service
        +ReadIncomeDetails(bool, TransactionType, string) bool
        +DisplayAllIncomes() void
        +DeleteIncome(string) bool
        +DisplayListOfIncomes(List~Income~) void
        +ReadId() string
    }
 
    class ExpenseView {
        -FinanceService _service
        +ReadExpenseDetails(bool, TransactionType, string) bool
        +DisplayAllExpenses() void
        +DeleteExpense(string) bool
        +DisplayListOfExpenses(List~Expense~) void
        +ReadId() string
    }
 
    class FinanceView {
        -FinanceService _service
        +DisplayMainMenu() void
        +DisplayIncomeMenu() void
        +DisplayExpenseMenu() void
        +DisplaySummary(decimal, decimal) void
        +DisplayUserSummary(decimal, decimal, decimal) void
        +DisplayTransaction(ITransaction) void
        +DisplaySummaryChart(decimal, decimal) void
        +DisplayMonthlyCategoryChart(Dictionary) void
        +ReadChoice() int
        +ReadId() string
        +ReadDateRange() tuple
    }
 
    class Helper {
        +TryReadDecimal(string, decimal) bool
        +DisplayError(string) void
        +DisplaySuccess(string) void
        +ShowRetryMessage(string) void
        +IsValidDecimal(decimal, string) bool
    }
 
    class Program {
        -IRepository Repository
        -FinanceService Service
        -IncomeView IncomeView
        -ExpenseView ExpenseView
        -FinanceView CommonView
        +Main() void
        -ShowSummary() void
        -ShowSummaryPerUser() void
    }
 
    %% ===== Relationships =====
    FinanceRepository ..|> IRepository : realizes
    CsvRepository ..|> IRepository : realizes
    Income ..|> ITransaction : realizes
    Expense ..|> ITransaction : realizes
 
    Income *-- IncomeCategoryAmount : composition
    Expense *-- ExpenseCategoryAmount : composition
    IncomeCategoryAmount --> IncomeCategory : uses
    ExpenseCategoryAmount --> ExpenseCategory : uses
    Income --> TransactionType : uses
    Expense --> TransactionType : uses
 
    FinanceRepository o-- ITransaction : holds list
    CsvRepository ..> ITransaction : depends on
 
    FinanceService --> IRepository : association
    FinanceService ..> ITransaction : depends on
 
    IncomeView --> FinanceService : association
    ExpenseView --> FinanceService : association
    FinanceView --> FinanceService : association
    IncomeView ..> Income : depends on
    ExpenseView ..> Expense : depends on
    IncomeView ..> Helper : depends on
    ExpenseView ..> Helper : depends on
    FinanceView ..> Helper : depends on
 
    Program ..> IRepository : creates
    Program --> FinanceService : association
    Program --> IncomeView : association
    Program --> ExpenseView : association
    Program --> FinanceView : association