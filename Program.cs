using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace LabWork
{
    // Simple point struct to hold coordinates
    public readonly struct Point
    {
        public double X { get; }
        public double Y { get; }
        public Point(double x, double y) { X = x; Y = y; }
        public override string ToString() => $"({X}, {Y})";
    }

    internal static class GeometryUtils
    {
        public const double Epsilon = 1e-9;

        public static bool IsZero(double v) => Math.Abs(v) < Epsilon;
        public static double Distance(Point a, Point b)
        {
            double dx = b.X - a.X, dy = b.Y - a.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }
        public static double Cross(Point a, Point b, Point c)
        {
            // (b - a) x (c - a)
            return (b.X - a.X) * (c.Y - a.Y) - (b.Y - a.Y) * (c.X - a.X);
        }
    }

    // Base polygon model with ordering and area via shoelace
    public abstract class Polygon
    {
        private readonly List<Point> _vertices = new();
        public IReadOnlyList<Point> Vertices => _vertices;
        protected abstract int ExpectedVertexCount { get; }

        public void SetVertices(IEnumerable<Point> points)
        {
            if (points is null) throw new ArgumentException("Список вершин не може бути null.");
            var list = points.ToList();
            if (list.Count != ExpectedVertexCount)
                throw new ArgumentException($"Очікується {ExpectedVertexCount} вершин(и), отримано {list.Count}.");

            // Duplicate check (epsilon)
            for (int i = 0; i < list.Count; i++)
                for (int j = i + 1; j < list.Count; j++)
                    if (GeometryUtils.Distance(list[i], list[j]) < GeometryUtils.Epsilon)
                        throw new ArgumentException("Деякі вершини співпадають.");

            // Order CCW around centroid to ensure consistent area and validation
            var cx = list.Average(p => p.X);
            var cy = list.Average(p => p.Y);
            list = list
                .OrderBy(p => Math.Atan2(p.Y - cy, p.X - cx))
                .ToList();

            _vertices.Clear();
            _vertices.AddRange(list);

            ValidateAfterOrdering();
        }

        protected virtual void ValidateAfterOrdering() { }

        public virtual double CalculateArea()
        {
            // Shoelace formula (requires ordered polygon without self-intersections)
            double sum = 0;
            int n = _vertices.Count;
            for (int i = 0; i < n; i++)
            {
                var a = _vertices[i];
                var b = _vertices[(i + 1) % n];
                sum += a.X * b.Y - a.Y * b.X;
            }
            return Math.Abs(sum) / 2.0;
        }
    }

    /// <summary>
    /// Triangle with non-collinearity validation
    /// </summary>
    public sealed class Triangle : Polygon
    {
        protected override int ExpectedVertexCount => 3;

        protected override void ValidateAfterOrdering()
        {
            var a = Vertices[0];
            var b = Vertices[1];
            var c = Vertices[2];
            double area2 = GeometryUtils.Cross(a, b, c);
            if (GeometryUtils.IsZero(area2))
                throw new ArgumentException("Точки трикутника лежать на одній прямій.");
        }
    }

    /// <summary>
    /// Convex quadrilateral with convexity validation
    /// </summary>
    public sealed class ConvexQuadrilateral : Polygon
    {
        protected override int ExpectedVertexCount => 4;

        protected override void ValidateAfterOrdering()
        {
            // Strict convexity: consistent cross product signs and no collinear adjacent triples
            int n = Vertices.Count;
            double? sign = null;
            for (int i = 0; i < n; i++)
            {
                var a = Vertices[i];
                var b = Vertices[(i + 1) % n];
                var c = Vertices[(i + 2) % n];
                double cross = GeometryUtils.Cross(a, b, c);
                if (GeometryUtils.IsZero(cross))
                    throw new ArgumentException("Суміжні вершини дають колінеарність — фігура не опукла.");
                double s = Math.Sign(cross);
                sign ??= s;
                if (Math.Sign(cross) != sign)
                    throw new ArgumentException("Чотирикутник не опуклий або вершини подані у невірному порядку.");
            }
        }
    }

    internal static class ConsoleUI
    {
        public static List<Point> ReadVertices(int count, string label)
        {
            var pts = new List<Point>(count);
            for (int i = 0; i < count; i++)
            {
                double x = ReadDouble($"Введіть координату x{i + 1} ({label}): ");
                double y = ReadDouble($"Введіть координату y{i + 1} ({label}): ");
                pts.Add(new Point(x, y));
            }
            return pts;
        }

        public static void PrintVertices(IReadOnlyList<Point> vertices, string title)
        {
            Console.WriteLine(title);
            for (int i = 0; i < vertices.Count; i++)
                Console.WriteLine($"Вершина {i + 1}: {vertices[i]}");
        }

        private static double ReadDouble(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                var s = Console.ReadLine();
                if (double.TryParse(s, NumberStyles.Float, CultureInfo.CurrentCulture, out double v))
                    return v;
                Console.WriteLine("Некоректне число. Спробуйте ще раз.");
            }
        }
    }

    internal static class Program
    {
        private static void Main()
        {
            // Triangle workflow
            Console.WriteLine("Робота з трикутником:");
            var triangle = new Triangle();
            while (true)
            {
                try
                {
                    var triPts = ConsoleUI.ReadVertices(3, "трикутника");
                    triangle.SetVertices(triPts);
                    break;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Помилка: {ex.Message}\n");
                }
            }
            ConsoleUI.PrintVertices(triangle.Vertices, "\nКоординати вершин трикутника:");
            Console.WriteLine($"Площа трикутника: {triangle.CalculateArea():F2}");

            // Quadrilateral workflow
            Console.WriteLine("\nРобота з чотирикутником:");
            var quad = new ConvexQuadrilateral();
            while (true)
            {
                try
                {
                    var quadPts = ConsoleUI.ReadVertices(4, "чотирикутника");
                    quad.SetVertices(quadPts);
                    break;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Помилка: {ex.Message}\n");
                }
            }
            ConsoleUI.PrintVertices(quad.Vertices, "\nКоординати вершин чотирикутника:");
            Console.WriteLine($"Площа чотирикутника: {quad.CalculateArea():F2}");
        }
    }
}
