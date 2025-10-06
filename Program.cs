
using System;

/// <summary>
/// Базовий клас для трикутника.
/// </summary>1
public class Triangle
{
    private double[] _x;
    private double[] _y;

    public Triangle()
    {
        _x = new double[3];
        _y = new double[3];
    }

    /// <summary>
    /// Введення координат вершин з перевіркою.
    /// </summary>
    public virtual void InputVertices()
    {
        for (int i = 0; i < 3; i++)
        {
            _x[i] = ReadDouble($"x{i + 1}");
            _y[i] = ReadDouble($"y{i + 1}");
        }
    }

    /// <summary>
    /// Виведення координат вершин.
    /// </summary>
    public virtual void PrintVertices()
    {
        Console.WriteLine("\nКоординати вершин трикутника:");
        for (int i = 0; i < 3; i++)
            Console.WriteLine($"Вершина {i + 1}: ({_x[i]}, {_y[i]})");
    }

    /// <summary>
    /// Обчислення площі трикутника (формула Герона).
    /// </summary>
    public virtual double CalculateArea()
    {
        double a = Distance(_x[0], _y[0], _x[1], _y[1]);
        double b = Distance(_x[1], _y[1], _x[2], _y[2]);
        double c = Distance(_x[2], _y[2], _x[0], _y[0]);
        double p = (a + b + c) / 2;
        return Math.Sqrt(p * (p - a) * (p - b) * (p - c));
    }

    protected double ReadDouble(string name)
    {
        double value;
        while (true)
        {
            Console.Write($"Введіть координату {name}: ");
            if (double.TryParse(Console.ReadLine(), out value))
                return value;
            Console.WriteLine("Некоректне значення! Введіть число.");
        }
    }

    protected double Distance(double x1, double y1, double x2, double y2)
    {
        return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
    }
}

/// <summary>
/// Клас для опуклого чотирикутника.
/// </summary>
public class ConvexQuadrilateral : Triangle
{
    private double[] _x;
    private double[] _y;

    public ConvexQuadrilateral()
    {
        _x = new double[4];
        _y = new double[4];
    }

    public override void InputVertices()
    {
        for (int i = 0; i < 4; i++)
        {
            _x[i] = ReadDouble($"x{i + 1}");
            _y[i] = ReadDouble($"y{i + 1}");
        }
    }

    public override void PrintVertices()
    {
        Console.WriteLine("\nКоординати вершин чотирикутника:");
        for (int i = 0; i < 4; i++)
            Console.WriteLine($"Вершина {i + 1}: ({_x[i]}, {_y[i]})");
    }

    public override double CalculateArea()
    {
        // Compute area by splitting quadrilateral into two triangles: (p1,p2,p3) + (p1,p3,p4)
        double area1 = Math.Abs(((_x[1] - _x[0]) * (_y[2] - _y[0]) - (_y[1] - _y[0]) * (_x[2] - _x[0])) ) / 2.0;
        double area2 = Math.Abs(((_x[2] - _x[0]) * (_y[3] - _y[0]) - (_y[2] - _y[0]) * (_x[3] - _x[0])) ) / 2.0;
        return area1 + area2;
    }
}

public class Program
{
    public static void Main()
    {
        Console.WriteLine("Робота з трикутником:");
        Triangle triangle = new Triangle();
        triangle.InputVertices();
        triangle.PrintVertices();
        Console.WriteLine($"Площа трикутника: {triangle.CalculateArea():F2}");

        Console.WriteLine("\nРобота з чотирикутником:");
        ConvexQuadrilateral quad = new ConvexQuadrilateral();
        quad.InputVertices();
        quad.PrintVertices();
        Console.WriteLine($"Площа чотирикутника: {quad.CalculateArea():F2}");
    }
}
