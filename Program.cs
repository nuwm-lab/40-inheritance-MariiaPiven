using System;


class Triangle
{
    protected double[] x;  // x-координати вершин
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
    public virtual void PrintVertices()
    {
        Console.WriteLine("\nКоординати вершин трикутника:");
        for (int i = 0; i < 3; i++)
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
    public override void PrintVertices()
    {
        Console.WriteLine("\nКоординати вершин чотирикутника:");
        for (int i = 0; i < 4; i++)
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