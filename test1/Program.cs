using System;
using System.Collections.Generic;
using System.IO;

namespace FinancialManager
{
    class Program
    {
        static void Main(string[] args)
        {
            FinancialManager manager = new FinancialManager();
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("1. Додати витрати");
                Console.WriteLine("2. Класифікувати витрати");
                Console.WriteLine("3. Встановити бюджет");
                Console.WriteLine("4. Отримати звіт про витрати");
                Console.WriteLine("5. Завершити роботу");

                Console.Write("Виберіть дію: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Введіть суму витрат: ");
                        double amount = Convert.ToDouble(Console.ReadLine());
                        Console.Write("Введіть категорію витрат: ");
                        string category = Console.ReadLine();
                        manager.AddExpense(amount, category);
                        Console.WriteLine("Витрати успішно додано.");
                        break;
                    case "2":
                        Console.WriteLine("Список категорій:");
                        foreach (string cat in manager.GetCategories())
                        {
                            Console.WriteLine(cat);
                        }
                        break;
                    case "3":
                        Console.Write("Введіть бюджет: ");
                        double budget = Convert.ToDouble(Console.ReadLine());
                        manager.SetBudget(budget);
                        Console.WriteLine("Бюджет успішно встановлено.");
                        break;
                    case "4":
                        string report = manager.GetExpenseReport();
                        Console.WriteLine(report);
                        break;
                    case "5":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Неправильний вибір. Спробуйте ще раз.");
                        break;
                }

                Console.WriteLine();
            }
        }
    }

    class FinancialManager
    {
        private List<Expense> expenses;
        private double budget;

        public FinancialManager()
        {
            expenses = new List<Expense>();
            budget = 0.0;
        }

        public void AddExpense(double amount, string category)
        {
            Expense expense = new Expense(amount, category);
            expenses.Add(expense);
        }

        public List<string> GetCategories()
        {
            List<string> categories = new List<string>();
            foreach (Expense expense in expenses)
            {
                if (!categories.Contains(expense.Category))
                {
                    categories.Add(expense.Category);
                }
            }
            return categories;
        }

        public void SetBudget(double amount)
        {
            budget = amount;
        }

        public string GetExpenseReport()
        {
            string report = "Звіт про витрати:\n";
            report += "--------------------------\n";
            double totalExpenses = 0.0;

            foreach (Expense expense in expenses)
            {
                report += $"Сума: {expense.Amount}\tКатегорія: {expense.Category}\n";
                totalExpenses += expense.Amount;
            }

            report += "--------------------------\n";
            report += $"Загальні витрати: {totalExpenses}\n";
            report += $"Бюджет: {budget}\n";
            report += $"Залишок: {budget - totalExpenses}\n";

            // Збереження звіту в текстовий файл
            string fileName = "звіти.txt";
            File.AppendAllText(fileName, report);

            return report;
        }
    }

    class Expense
    {
        public double Amount { get; set; }
        public string Category { get; set; }

        public Expense(double amount, string category)
        {
            Amount = amount;
            Category = category;
        }
    }
}
