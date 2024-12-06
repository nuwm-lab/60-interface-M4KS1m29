using System;

abstract class AbstractHalf
{
    protected double CoefficientX1, CoefficientX2, ConstantTerm;

    // Конструктор
    protected AbstractHalf(double coefficientX1, double coefficientX2, double constantTerm)
    {
        CoefficientX1 = coefficientX1;
        CoefficientX2 = coefficientX2;
        ConstantTerm = constantTerm;
    }

    // Абстрактний метод для виведення коефіцієнтів
    public abstract void DisplayCoefficients();

    // Абстрактний метод для перевірки, чи належить точка
    public abstract bool IsPointIn(double x1, double x2, double? x3 = null);
}

class HalfPlane : AbstractHalf
{
    public HalfPlane(double coefficientX1, double coefficientX2, double constantTerm) : base(coefficientX1, coefficientX2, constantTerm)
    {
        Console.WriteLine("HalfPlane створено");
    }

    public override void DisplayCoefficients()
    {
        Console.WriteLine($"Рiвняння пiвплощини: {CoefficientX1} * x1 + {CoefficientX2} * x2 <= {ConstantTerm}");
    }

    public override bool IsPointIn(double x1, double x2, double? x3 = null)
    {
        return (CoefficientX1 * x1 + CoefficientX2 * x2) <= ConstantTerm;
    }
}

class HalfSpace : AbstractHalf
{
    private double CoefficientX3;

    public HalfSpace(double coefficientX1, double coefficientX2, double coefficientX3, double constantTerm) : base(coefficientX1, coefficientX2, constantTerm)
    {
        CoefficientX3 = coefficientX3;
        Console.WriteLine("HalfSpace створено");
    }

    public override void DisplayCoefficients()
    {
        Console.WriteLine($"Рiвняння пiвпростору: {CoefficientX1} * x1 + {CoefficientX2} * x2 + {CoefficientX3} * x3 <= {ConstantTerm}");
    }

    public override bool IsPointIn(double x1, double x2, double? x3 = null)
    {
        if (x3 == null)
        {
            throw new ArgumentException("Для перевірки точки в півпросторі потрібен третій параметр (x3).");
        }
        return (CoefficientX1 * x1 + CoefficientX2 * x2 + CoefficientX3 * x3.Value) <= ConstantTerm;
    }
}

class Program
{
    static void Main(string[] args)
    {
        try
        {
            Console.WriteLine("Оберіть тип об'єкта: 1 - HalfPlane, 2 - HalfSpace");
            if (!int.TryParse(Console.ReadLine(), out int userChoice) || (userChoice != 1 && userChoice != 2))
            {
                Console.WriteLine("Невірний вибір. Завершення програми.");
                return;
            }

            AbstractHalf obj;

            Console.WriteLine("Введіть коефіцієнти:");
            Console.Write("a1: ");
            double a1 = double.Parse(Console.ReadLine());
            Console.Write("a2: ");
            double a2 = double.Parse(Console.ReadLine());
            double a3 = 0;
            if (userChoice == 2)
            {
                Console.Write("a3: ");
                a3 = double.Parse(Console.ReadLine());
            }
            Console.Write("c: ");
            double c = double.Parse(Console.ReadLine());

            if (userChoice == 1)
            {
                obj = new HalfPlane(a1, a2, c);
                Console.WriteLine("Ви вибрали HalfPlane.");
            }
            else
            {
                obj = new HalfSpace(a1, a2, a3, c);
                Console.WriteLine("Ви вибрали HalfSpace.");
            }

            obj.DisplayCoefficients();

            Console.WriteLine("Введіть координати точки для перевірки:");
            Console.Write("x1: ");
            double x1 = double.Parse(Console.ReadLine());
            Console.Write("x2: ");
            double x2 = double.Parse(Console.ReadLine());
            double? x3 = null;
            if (userChoice == 2)
            {
                Console.Write("x3: ");
                x3 = double.Parse(Console.ReadLine());
            }

            bool isIn = obj.IsPointIn(x1, x2, x3);
            Console.WriteLine($"Точка належить області: {isIn}");
        }
        catch (FormatException)
        {
            Console.WriteLine("Помилка введення: введіть коректні числові значення.");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Невідома помилка: {ex.Message}");
        }

        Console.WriteLine("Натисніть будь-яку клавішу для виходу.");
        Console.ReadKey();
    }
}