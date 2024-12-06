using System;
using System.Threading;

abstract class AbstractHalf
{
    protected double coefficientX1, coefficientX2, constantTerm;

    // Конструктор
    protected AbstractHalf(double coefficientX1, double coefficientX2, double constantTerm)
    {
        this.coefficientX1 = coefficientX1;
        this.coefficientX2 = coefficientX2;
        this.constantTerm = constantTerm;
    }

    // Абстрактний метод для виведення коефіцієнтів
    public abstract void DisplayCoefficients();

    // Абстрактний метод для перевірки, чи належить точка
    public abstract bool IsPointIn(double x1, double x2);
}

class HalfPlane : AbstractHalf
{
    public HalfPlane(double coefficientX1, double coefficientX2, double constantTerm) : base(coefficientX1, coefficientX2, constantTerm)
    {
        Console.WriteLine("HalfPlane створено");
    }

    public override void DisplayCoefficients()
    {
        Console.WriteLine($"Рiвняння пiвплощини: {coefficientX1} * x1 + {coefficientX2} * x2 <= {constantTerm}");
    }

    public override bool IsPointIn(double x1, double x2)
    {
        return (coefficientX1 * x1 + coefficientX2 * x2) <= constantTerm;
    }
}

class HalfSpace : AbstractHalf
{
    private double coefficientX3;

    public HalfSpace(double coefficientX1, double coefficientX2, double coefficientX3, double constantTerm) : base(coefficientX1, coefficientX2, constantTerm)
    {
        this.coefficientX3 = coefficientX3;
        Console.WriteLine("HalfSpace створено");
    }

    public override void DisplayCoefficients()
    {
        Console.WriteLine($"Рiвняння пiвпростору: {coefficientX1} * x1 + {coefficientX2} * x2 + {coefficientX3} * x3 <= {constantTerm}");
    }

    public override bool IsPointIn(double x1, double x2)
    {
        throw new NotImplementedException("Цей метод не може бути викликаний для HalfSpace без третього параметра.");
    }

    public bool IsPointIn(double x1, double x2, double x3)
    {
        return (coefficientX1 * x1 + coefficientX2 * x2 + coefficientX3 * x3) <= constantTerm;
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

            if (userChoice == 1)
            {
                obj = new HalfPlane(1, 2, 5);
                Console.WriteLine("Ви вибрали HalfPlane.");
            }
            else
            {
                obj = new HalfSpace(1, 2, 3, 10);
                Console.WriteLine("Ви вибрали HalfSpace.");
            }

            obj.DisplayCoefficients();

            if (userChoice == 1)
            {
                Console.WriteLine("Точка (1, 1) належить пiвплощинi: " + obj.IsPointIn(1, 1));
            }
            else
            {
                if (obj is HalfSpace halfSpaceObj)
                {
                    Console.WriteLine("Точка (1, 1, 1) належить пiпростору: " + halfSpaceObj.IsPointIn(1, 1, 1));
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Сталася помилка: {ex.Message}");
        }

        Console.WriteLine("Натисніть будь-яку клавішу для виходу.");
        Console.ReadKey();
    }
}
