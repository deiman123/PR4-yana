using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        try
        {
            Console.Write("Введите начало отрезка a: ");
            double a = Convert.ToDouble(Console.ReadLine());

            Console.Write("Введите конец отрезка b: ");
            double b = Convert.ToDouble(Console.ReadLine());

            Console.Write("Введите шаг h: ");
            double h = Convert.ToDouble(Console.ReadLine());

            if (h <= 0)
            {
                throw new ArgumentException("Шаг должен быть положительным числом");
            }

            if (a > b)
            {
                throw new ArgumentException("Начало отрезка не может быть больше конца");
            }

            CalculateAndPrintTable(a, b, h);
        }
        catch (FormatException)
        {
            Console.WriteLine("Ошибка: Введите корректное число");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Непредвиденная ошибка: {ex.Message}");
        }
        finally
        {
            Console.WriteLine("\nПрограмма завершена");
        }
    }

    static void CalculateAndPrintTable(double a, double b, double h)
    {
        List<(double x, double y)> results = new List<(double, double)>();

        for (double x = a; x <= b + h / 2; x += h)
        {
            try
            {
                double y = CalculateFunction(x);
                results.Add((x, y));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при x = {x:F4}: {ex.Message}");
                results.Add((x, double.NaN));
            }
        }

        PrintTable(results);
    }

    static double CalculateFunction(double x)
    {
        if (x < 0)
        {
            throw new ArgumentException("Подкоренное выражение не может быть отрицательным");
        }

        try
        {
            double sqrtX = Math.Sqrt(x);
            double cosX = Math.Cos(x);
            double result = sqrtX * cosX * cosX;

            if (double.IsInfinity(result))
            {
                throw new OverflowException("Результат вышел за пределы допустимых значений");
            }

            if (double.IsNaN(result))
            {
                throw new ArithmeticException("Результат вычисления не определен");
            }

            return result;
        }
        catch (OverflowException)
        {
            throw new OverflowException("Переполнение при вычислении функции");
        }
    }

    static void PrintTable(List<(double x, double y)> results)
    {
        Console.WriteLine("\nТаблица значений функции F(x) = √x · cos²x");
        Console.WriteLine(new string('-', 40));
        Console.WriteLine($"{"x",-15} {"F(x)",-25}");
        Console.WriteLine(new string('-', 40));

        foreach (var item in results)
        {
            if (double.IsNaN(item.y))
            {
                Console.WriteLine($"{item.x,-15:F6} {"не определено",-25}");
            }
            else
            {
                Console.WriteLine($"{item.x,-15:F6} {item.y,-25:F6}");
            }
        }

        Console.WriteLine(new string('-', 40));
        Console.WriteLine($"Всего точек: {results.Count}");
    }
}