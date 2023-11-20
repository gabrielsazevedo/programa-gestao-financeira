class ExpenseManager
{
    private List<Expense> expenses;

    public IReadOnlyList<Expense> Expenses => expenses.AsReadOnly();

    public ExpenseManager()
    {
        expenses = new List<Expense>();
    }

    public void AddExpense(Expense expense)
    {
        expenses.Add(expense);
    }

    public void EditExpense(Expense oldExpense, Expense newExpense)
    {
        if (expenses.Contains(oldExpense))
        {
            int index = expenses.IndexOf(oldExpense);
            expenses[index] = newExpense;
        }
    }

    public void RemoveExpense(Expense expense)
    {
        expenses.Remove(expense);
    }

    public decimal CalculateTotalExpenses()
    {
        decimal total = 0;
        foreach (var expense in expenses)
        {
            total += expense.Amount;
        }
        return total;
    }
}