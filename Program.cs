class Program
{
    static void Main()
    {
        ExpenseManager manager = new ExpenseManager();
        InitializeData(manager);

        while (true)
        {
            PrintMenu();
            string choice = GetUserChoice();

            ProcessUserChoice(choice, manager);
        }
    }

    static void PrintMenu()
    {
        Console.WriteLine("\n1. Exibir despesas");
        Console.WriteLine("2. Adicionar despesa");
        Console.WriteLine("3. Editar despesa");
        Console.WriteLine("4. Remover despesa");
        Console.WriteLine("5. Exibir despesas totais");
        Console.WriteLine("6. Sair \n");
    }

    static string GetUserChoice()
    {
        Console.Write("Escolha uma opção: ");
        return Console.ReadLine();
    }

    static void ProcessUserChoice(string choice, ExpenseManager manager)
    {
        switch (choice)
        {
            case "1":
                DisplayExpenses(manager);
                break;
            case "2":
                AddExpense(manager);
                break;
            case "3":
                EditExpense(manager);
                break;
            case "4":
                RemoveExpense(manager);
                break;
            case "5":
                DisplayTotalExpenses(manager);
                break;
            case "6":
                Environment.Exit(0);
                break;
            default:
                Console.WriteLine("Opção inválida. Tente novamente.");
                break;
        }
    }

    static void InitializeData(ExpenseManager manager)
    {
        Category groceries = new Category("Compras");
        Category rent = new Category("Aluguel");

        manager.AddExpense(new Expense("Compras no shopping", 150.0m, groceries));
        manager.AddExpense(new Expense("Aluguel", 1200.0m, rent));
        manager.AddExpense(new Expense("Jantar em restaurante", 80.0m, groceries));
    }

    static void DisplayExpenses(ExpenseManager manager)
    {
        Console.WriteLine("\n Exibindo todas as despesas:");
        foreach (var expense in manager.Expenses)
        {
            Console.WriteLine($"- {expense.Description}: ${expense.Amount}");
        }
    }

    static void AddExpense(ExpenseManager manager)
    {
        Console.Write("Informe a descrição da despesa: ");
        string description = Console.ReadLine();

        decimal amount = GetValidAmount();

        Console.Write("Informe a categoria da despesa: ");
        string category = Console.ReadLine();

        Expense newExpense = new Expense(description, amount, new Category(category));
        manager.AddExpense(newExpense);

        Console.WriteLine("Despesa adicionada com sucesso.");
    }

    static decimal GetValidAmount()
    {
        decimal amount;
        while (true)
        {
            Console.Write("Informe o valor da despesa: ");
            if (decimal.TryParse(Console.ReadLine(), out amount))
            {
                return amount;
            }
            else
            {
                Console.WriteLine("Valor inválido. Por favor, informe um valor válido.");
            }
        }
    }

    static void EditExpense(ExpenseManager manager)
    {
        Console.Write("Informe a descrição da despesa que deseja editar: ");
        string descriptionToEdit = Console.ReadLine();

        Expense expenseToEdit = FindExpenseByDescription(manager, descriptionToEdit);

        if (expenseToEdit != null)
        {
            EditExpenseDetails(manager, expenseToEdit);
            Console.WriteLine("Despesa editada com sucesso.");
        }
        else
        {
            Console.WriteLine("Despesa não encontrada.");
        }
    }

    static Expense FindExpenseByDescription(ExpenseManager manager, string description)
    {
        return manager.Expenses.FirstOrDefault(e => e.Description.Equals(description, StringComparison.OrdinalIgnoreCase));
    }

    static void EditExpenseDetails(ExpenseManager manager, Expense expense)
    {
        Console.WriteLine($"Editando despesa: {expense.Description}, {expense.Amount:C}, {expense.Category.Name}");

        Console.Write("Informe a nova descrição da despesa: ");
        string newDescription = Console.ReadLine();

        decimal newAmount = GetValidAmount();

        Console.Write("Informe a nova categoria da despesa: ");
        string newCategory = Console.ReadLine();

        Expense editedExpense = new Expense(newDescription, newAmount, new Category(newCategory));
        manager.EditExpense(expense, editedExpense);
    }

    static void RemoveExpense(ExpenseManager manager)
    {
        Console.Write("Informe a descrição da despesa que deseja remover: ");
        string descriptionToRemove = Console.ReadLine();

        Expense expenseToRemove = FindExpenseByDescription(manager, descriptionToRemove);

        if (expenseToRemove != null)
        {
            manager.RemoveExpense(expenseToRemove);
            Console.WriteLine("\n Despesa removida com sucesso.");
        }
        else
        {
            Console.WriteLine("Despesa não encontrada.");
        }
    }

    static void DisplayTotalExpenses(ExpenseManager manager)
    {
        decimal totalExpenses = manager.CalculateTotalExpenses();
        Console.WriteLine($"\n Despesas totais: ${totalExpenses}");
    }
}