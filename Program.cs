using System;
using System.Threading;

abstract class AbstractHalf
{
    protected double a1, a2, b;

    // Конструктор
    protected AbstractHalf(double a1, double a2, double b)
    {
        this.a1 = a1;
        this.a2 = a2;
        this.b = b;
    }

 
    public abstract void DisplayCoefficients();

    // Абстрактний метод для перевірки, чи належить точка
    public abstract bool IsPointIn(double x1, double x2);

    // Деструктор
    ~AbstractHalf()
    {
        Console.WriteLine("AbstractHalf знищується");
    }
}

class HalfPlane : AbstractHalf
{
    public HalfPlane(double a1, double a2, double b) : base(a1, a2, b)
    {
        Console.WriteLine("HalfPlane створено");
    }

    public override void DisplayCoefficients()
    {
        Console.WriteLine($"Рiвняння пiвплощини: {a1} * x1 + {a2} * x2 <= {b}");
    }

    public override bool IsPointIn(double x1, double x2)
    {
        return (a1 * x1 + a2 * x2) <= b;
    }

    // Деструктор
    ~HalfPlane()
    {
        Console.WriteLine("HalfPlane знищується");
    }
}


class HalfSpace : AbstractHalf
{
    private double a3;

    public HalfSpace(double a1, double a2, double a3, double b) : base(a1, a2, b)
    {
        this.a3 = a3;
        Console.WriteLine("HalfSpace створено");
    }

    public override void DisplayCoefficients()
    {
        Console.WriteLine($"Рiвняння пiвпростору: {a1} * x1 + {a2} * x2 + {a3} * x3 <= {b}");
    }

    public bool IsPointInHalfSpace(double x1, double x2, double x3)
    {
        return (a1 * x1 + a2 * x2 + a3 * x3) <= b;
    }

    public override bool IsPointIn(double x1, double x2)
    {
        throw new NotImplementedException("IsPointIn methd is not applicable for HalfSpace. Use IsPointInHalfSpace instead.");
    }

    // Деструктор
    ~HalfSpace()
    {
        Console.WriteLine("HalfSpace знищується");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Оберіть тип об'єкта: 1 - HalfPlane, 2 - HalfSpace");
        string userChoose = Console.ReadLine();

        AbstractHalf obj;

        if (userChoose == "1")
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

        if (userChoose == "1")
        {
            Console.WriteLine("Точка (1, 1) належить пiвплощинi: " + obj.IsPointIn(1, 1));
        }
        else
        {
            HalfSpace halfSpaceObj = obj as HalfSpace;
            Console.WriteLine("Точка (1, 1, 1) належить пiпростору: " + halfSpaceObj.IsPointInHalfSpace(1, 1, 1));
        }

        obj = null;
        GC.Collect();
        GC.WaitForPendingFinalizers();



        Thread.Sleep(1000);

        Console.ReadKey();
    }
}