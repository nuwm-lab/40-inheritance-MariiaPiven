<<<<<<< HEAD

<<<<<<< HEAD

class Triangle
{
    protected double[] x;  // x-координати вершин312321
    protected double[] y;  // y-координати вершин

    public Triangle()
    {
        x = new double[3];
        y = new double[3];
    }

    // Метод для задання координат вершин
    public virtual void SetVertices()
    {
        for (int i = 0; i < 3; i++)
        {
            Console.WriteLine($"Введіть координати {i + 1}-ї вершини:");
            Console.Write($"x{i + 1} = ");
            x[i] = double.Parse(Console.ReadLine() ?? "0");
            Console.Write($"y{i + 1} = ");
            y[i] = double.Parse(Console.ReadLine() ?? "0");
        }
    }

    // Метод для виведення координат вершин
=======
using System;

/// <summary>
/// Базовий клас для трикутника.
/// </summary>
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
>>>>>>> 845a1cd7df4008d61e33a313a59d631cd29f9ab0
    public virtual void PrintVertices()
    {
        Console.WriteLine("\nКоординати вершин трикутника:");
        for (int i = 0; i < 3; i++)
<<<<<<< HEAD
        {
            Console.WriteLine($"Вершина {i + 1}: ({x[i]}, {y[i]})");
        }
    }

    // Метод для обчислення площі (формула Герона)
    public virtual double CalculateArea()
    {
        // Обчислюємо довжини сторін
        double a = Math.Sqrt(Math.Pow(x[1] - x[0], 2) + Math.Pow(y[1] - y[0], 2));
        double b = Math.Sqrt(Math.Pow(x[2] - x[1], 2) + Math.Pow(y[2] - y[1], 2));
        double c = Math.Sqrt(Math.Pow(x[0] - x[2], 2) + Math.Pow(y[0] - y[2], 2));

        // Півпериметр
        double p = (a + b + c) / 2;

        // Формула Герона
        return Math.Sqrt(p * (p - a) * (p - b) * (p - c));
    }
}

class ConvexQuadrilateral : Triangle
{
    public ConvexQuadrilateral()
    {
        x = new double[4];  // Розширюємо масиви для 4 вершин
        y = new double[4];
    }

    // Перевантажений метод для задання координат вершин
    public override void SetVertices()
    {
        for (int i = 0; i < 4; i++)
        {
            Console.WriteLine($"Введіть координати {i + 1}-ї вершини:");
            Console.Write($"x{i + 1} = ");
            x[i] = double.Parse(Console.ReadLine() ?? "0");
            Console.Write($"y{i + 1} = ");
            y[i] = double.Parse(Console.ReadLine() ?? "0");
        }
    }

    // Перевантажений метод для виведення координат вершин
=======
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

>>>>>>> 845a1cd7df4008d61e33a313a59d631cd29f9ab0
    public override void PrintVertices()
    {
        Console.WriteLine("\nКоординати вершин чотирикутника:");
        for (int i = 0; i < 4; i++)
<<<<<<< HEAD
        {
            Console.WriteLine($"Вершина {i + 1}: ({x[i]}, {y[i]})");
        }
    }

    // Перевантажений метод для обчислення площі (метод діагоналей)
    public override double CalculateArea()
    {
        // Обчислюємо довжини діагоналей
        double d1 = Math.Sqrt(Math.Pow(x[2] - x[0], 2) + Math.Pow(y[2] - y[0], 2));
        double d2 = Math.Sqrt(Math.Pow(x[3] - x[1], 2) + Math.Pow(y[3] - y[1], 2));

        // Обчислюємо кут між діагоналями
        double cosAlpha = (
            (x[2] - x[0]) * (x[3] - x[1]) +
            (y[2] - y[0]) * (y[3] - y[1])
        ) / (d1 * d2);

        double sinAlpha = Math.Sqrt(1 - cosAlpha * cosAlpha);

        // Площа чотирикутника = (d1 * d2 * sin(alpha)) / 2
        return (d1 * d2 * sinAlpha) / 2;
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Створюємо трикутник
        Console.WriteLine("Робота з трикутником:");
        Triangle triangle = new Triangle();
        triangle.SetVertices();
        triangle.PrintVertices();
        double triangleArea = triangle.CalculateArea();
        Console.WriteLine($"Площа трикутника: {triangleArea:F2}");

        // Створюємо чотирикутник
        Console.WriteLine("\nРобота з чотирикутником:");
        ConvexQuadrilateral quadrilateral = new ConvexQuadrilateral();
        quadrilateral.SetVertices();
        quadrilateral.PrintVertices();
        double quadrilateralArea = quadrilateral.CalculateArea();
        Console.WriteLine($"Площа чотирикутника: {quadrilateralArea:F2}");
    }
}
=======
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

>>>>>>> 845a1cd7df4008d61e33a313a59d631cd29f9ab0
=======

using System;

/// <summary>
/// Базовий клас для трикутника.
/// </summary>
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
>>>>>>> main
