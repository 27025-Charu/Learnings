# Expense Tracker Application - Requirements Document

## 1. Overview & Objectives

### Objective
To develop a simple, console-based Expense Tracker Application that enables users to track income and expenses. The application helps users monitor earnings, record expenditures, and view their overall financial status through summary reports.

### Success Metrics
- Users can add, update, delete, and view income records.
- Users can add, update, delete, and view expense records.
- Users can view total income, total expenses, and net balance.
- Users can categorize expenses and income entries.
- Appropriate success and error messages are displayed for all operations.
- Input validation prevents invalid data entry.

---

## 2. Problem Statement

Many individuals find it difficult to track their day-to-day expenses and understand how their income is being spent. Without a structured tracking mechanism, users may lose visibility into their spending habits and overall financial health.

The Expense Tracker Application provides a simple console-based solution that allows users to record income and expenses, categorize transactions, and view summarized financial information. This helps users make better financial decisions and maintain spending awareness.

---

## 3. Technical Scope

### In Scope (Version 1)

#### Interface
- Text-based, menu-driven console application.
- Interactive prompts for all user operations.
- Clear success and error messages.
- Menu refresh after major operations.

#### CRUD Operations

##### Create
- Add income records.
- Add expense records.
- Generate unique IDs automatically.

##### Read
- View all income records.
- View all expense records.
- View financial summary.

##### Update
- Update income records using ID.
- Update expense records using ID.

##### Delete
- Delete income records using ID.
- Delete expense records using ID.
- Confirmation before deletion.

#### Storage
- In-memory storage using lists and/or dictionaries.
- Data available only during application execution.
- No database integration.

#### Validation
- Mandatory field validation.
- Amount validation for numeric values.
- ID validation for update and delete operations.
- Prevention of empty input values.

### Out of Scope (Version 1)
- Database integration.
- User authentication and authorization.
- Multi-user support.
- Cloud synchronization.
- Advanced search and filtering.
- Data export functionality.

### Assumptions
- Single-user application.
- Runs locally on a user machine.
- Data persistence is not required.
- Expected data volume is small.

---

## 4. Design

As Version 1 is a console-based application, no graphical user interface or wireframes are required.

The application will provide:
- Menu-driven navigation.
- Console-based user input.
- Formatted output screens for records and summaries.

---

## 5. Open Questions & Risks

### Open Questions
- Should salary remain mandatory before expense entry?
- Should custom categories exist only during the current session?
- Should reporting support date-based filtering in future versions?

### Risks
- Data will be lost when the application is terminated due to in-memory storage.
- Sequential ID generation must avoid duplicate IDs after record deletion.
- No backup or recovery mechanism exists in Version 1.

---

## 6. Success Criteria

### Primary Metric
Users can successfully perform add, view, update, and delete operations for income and expense records while maintaining accurate financial summaries.

### Secondary Metric
All validation rules prevent invalid input and display meaningful error messages.

### Guardrail
Any successful create, update, or delete operation must immediately reflect in record views and summary calculations.

---

# Functional Requirements

## Income Management

- The application shall allow users to record Salary and Freelancing Earnings.
- Salary shall be a mandatory income source before expenses can be recorded.
- Income records shall contain:
  - ID
  - Amount
  - Date
  - Category/Source
- Multiple income entries shall be supported.

## Expense Management

- The application shall allow users to record expenses.
- Expense records shall contain:
  - ID
  - Expense Name
  - Amount
  - Date
  - Category
- The application shall provide default categories such as:
  - Food
  - Transport
  - Rent
  - EB Bill
- Users shall be able to create custom categories.
- Expenses shall be categorized as:
  - Default Expenses
  - Additional Expenses
- Default expenses may include recurring necessities such as Rent and EB Bill.
- Additional expenses may include Shopping, Entertainment, Travel, and other optional spending.
- Multiple expense entries shall be supported.

## Data Management

- The application shall support a single user only.
- Data shall be stored in memory using lists and/or dictionaries.
- No database or external storage mechanism shall be used.
- Every income and expense record shall contain a unique identifier.

### ID Format

- Income IDs: `INC001`, `INC002`, `INC003`, ...
- Expense IDs: `EXP001`, `EXP002`, `EXP003`, ...

## Reporting and Summary

- Users shall be able to view all income records.
- Users shall be able to view all expense records.
- The application shall display:
  - Total Income
  - Total Expenses
  - Net Balance

### Net Balance Formula

```text
Net Balance = Total Income - Total Expenses
```

## User Interaction

- The application shall accept user input through the console.
- Users shall be able to repeatedly add income and expense records during execution.
- The application shall validate all amount-related inputs.
- Success and error messages shall be displayed for every operation.

---

# Non-Functional Requirements

## Usability

- The application shall provide a simple and user-friendly console interface.
- Clear prompts and messages shall be displayed for all user actions.

## Performance

- Summary calculations shall be updated immediately after data modifications.
- The application shall respond promptly to user interactions.

## Data Handling

- Data shall be maintained only during the runtime of the application.
- All data shall be lost when the application exits.

## Maintainability

- The application shall follow a modular structure to support future enhancements and maintenance.

## Interface Refresh

- The console menu shall refresh after completion of major operations, including:
  - Adding income
  - Adding expenses
  - Updating records
  - Deleting records
  - Viewing reports and summaries