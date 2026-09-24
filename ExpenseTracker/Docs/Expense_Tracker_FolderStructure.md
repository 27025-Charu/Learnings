# Expense Tracker - Folder Structure

## Purpose

This document describes the proposed folder structure for the Expense Tracker application and the responsibility of each component.

## Project Structure

ExpenseTracker/
│
├── Interfaces/
│   ├── IIncomeRepository.cs
│   └── IExpenseRepository.cs
│
├── Models/
│   ├── Income.cs
│   ├── Expense.cs
│   ├── IncomeCategory.cs
│   ├── ExpenseCategory.cs
│   └── MenuOption.cs
│
├── Repositories/
│   └── FinanceRepository.cs
│
├── Services/
│   ├── FinanceService.cs
│   └── IdGenerator.cs
│
├── Views/
│   └── FinanceView.cs
│
├── Program.cs
└── ExpenseTracker.csproj

## Folder Responsibilities

### Interfaces
Contains repository contracts for handling income and expense operations.

### Models
Contains domain entities, categories, and application menu definitions.

### Repositories
Responsible for data access and storage-related operations.

### Services
Contains business logic, validations, and utility services such as ID generation.

### Views
Handles user interaction and presentation logic.

### Program.cs
Application entry point that initializes and coordinates application execution.

### ExpenseTracker.csproj
Project configuration file containing dependencies and build settings.
``