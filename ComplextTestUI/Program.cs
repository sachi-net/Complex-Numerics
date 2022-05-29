using ComplexLibrary;
using System;

namespace ComplextTestUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Conj();

            Console.ReadKey();
        }

        static void Cartesian()
        {
            Complex z = new(1, 1);
            Console.WriteLine(z.ToString());
            Console.WriteLine("|z| = {0}, Arg(z) = {1}\n", z.Modulus, z.Argument);

            z = new(Math.Sqrt(3), -1);
            Console.WriteLine(z.ToString());
            Console.WriteLine("|z| = {0}, Arg(z) = {1}\n", z.Modulus, z.Argument);

            z = new(-1, Math.Sqrt(3));
            Console.WriteLine(z.ToString());
            Console.WriteLine("|z| = {0}, Arg(z) = {1}\n", z.Modulus, z.Argument);

            z = new(-1, -1);
            Console.WriteLine(z.ToString());
            Console.WriteLine("|z| = {0}, Arg(z) = {1}\n", z.Modulus, z.Argument);

            z = new(2, -3);
            Console.WriteLine(z.ToString());
            Console.WriteLine("|z| = {0}, Arg(z) = {1}\n", z.Modulus, z.Argument);

            z = new(-3, -4);
            Console.WriteLine(z.ToString());
            Console.WriteLine("|z| = {0}, Arg(z) = {1}\n", z.Modulus, z.Argument);

            z = new(5, 0);
            Console.WriteLine(z.ToString());
            Console.WriteLine("|z| = {0}, Arg(z) = {1}\n", z.Modulus, z.Argument);

            z = new(-5, 0);
            Console.WriteLine(z.ToString());
            Console.WriteLine("|z| = {0}, Arg(z) = {1}\n", z.Modulus, z.Argument);

            z = new(0, 4);
            Console.WriteLine(z.ToString());
            Console.WriteLine("|z| = {0}, Arg(z) = {1}\n", z.Modulus, z.Argument);

            z = new(0, -4);
            Console.WriteLine(z.ToString());
            Console.WriteLine("|z| = {0}, Arg(z) = {1}\n", z.Modulus, z.Argument);
        }

        static void PolarPrimary()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;

            Complex z = new(2, Math.PI / 3, PhaseMode.ArgPrimary);
            Console.WriteLine(z.ToString());
            Console.WriteLine("|z| = {0}, Arg(z) = {1}\n", z.Modulus, z.Argument);

            z = new(2, 2 * Math.PI / 3, PhaseMode.ArgPrimary);
            Console.WriteLine(z.ToString());
            Console.WriteLine("|z| = {0}, Arg(z) = {1}\n", z.Modulus, z.Argument);

            z = new(2, -Math.PI / 3, PhaseMode.ArgPrimary);
            Console.WriteLine(z.ToString());
            Console.WriteLine("|z| = {0}, Arg(z) = {1}\n", z.Modulus, z.Argument);

            z = new(2, -2 * Math.PI / 3, PhaseMode.ArgPrimary);
            Console.WriteLine(z.ToString());
            Console.WriteLine("|z| = {0}, Arg(z) = {1}\n", z.Modulus, z.Argument);

            z = new(2, 0, PhaseMode.ArgPrimary);
            Console.WriteLine(z.ToString());
            Console.WriteLine("|z| = {0}, Arg(z) = {1}\n", z.Modulus, z.Argument);

            z = new(2, Math.PI / 2, PhaseMode.ArgPrimary);
            Console.WriteLine(z.ToString());
            Console.WriteLine("|z| = {0}, Arg(z) = {1}\n", z.Modulus, z.Argument);

            z = new(2, -Math.PI / 2, PhaseMode.ArgPrimary);
            Console.WriteLine(z.ToString());
            Console.WriteLine("|z| = {0}, Arg(z) = {1}\n", z.Modulus, z.Argument);

            z = new(2, -Math.PI, PhaseMode.ArgPrimary);
            Console.WriteLine(z.ToString());
            Console.WriteLine("|z| = {0}, Arg(z) = {1}\n", z.Modulus, z.Argument);
        }

        static void PolarSecondary()
        {
            Console.ForegroundColor = ConsoleColor.Blue;

            Complex z = new(2, Math.PI / 3, PhaseMode.ArgSecondary);
            Console.WriteLine(z.ToString());
            Console.WriteLine("|z| = {0}, Arg(z) = {1}\n", z.Modulus, z.Argument);

            z = new(2, 2 * Math.PI / 3, PhaseMode.ArgSecondary);
            Console.WriteLine(z.ToString());
            Console.WriteLine("|z| = {0}, Arg(z) = {1}\n", z.Modulus, z.Argument);

            z = new(2, 5 * Math.PI / 3, PhaseMode.ArgSecondary);
            Console.WriteLine(z.ToString());
            Console.WriteLine("|z| = {0}, Arg(z) = {1}\n", z.Modulus, z.Argument);

            z = new(2, 4 * Math.PI / 3, PhaseMode.ArgSecondary);
            Console.WriteLine(z.ToString());
            Console.WriteLine("|z| = {0}, Arg(z) = {1}\n", z.Modulus, z.Argument);

            z = new(2, 0, PhaseMode.ArgSecondary);
            Console.WriteLine(z.ToString());
            Console.WriteLine("|z| = {0}, Arg(z) = {1}\n", z.Modulus, z.Argument);

            z = new(2, Math.PI / 2, PhaseMode.ArgSecondary);
            Console.WriteLine(z.ToString());
            Console.WriteLine("|z| = {0}, Arg(z) = {1}\n", z.Modulus, z.Argument);

            z = new(2, 3 * Math.PI / 2, PhaseMode.ArgSecondary);
            Console.WriteLine(z.ToString());
            Console.WriteLine("|z| = {0}, Arg(z) = {1}\n", z.Modulus, z.Argument);

            z = new(2, Math.PI, PhaseMode.ArgSecondary);
            Console.WriteLine(z.ToString());
            Console.WriteLine("|z| = {0}, Arg(z) = {1}\n", z.Modulus, z.Argument);
        }

        static void Add()
        {
            Complex a = new(1, 5);
            Complex b = new(2, -Math.PI / 2, PhaseMode.ArgPrimary);
            Console.WriteLine((a + b).ToString());
        }

        static void Subs()
        {
            Complex a = new(1, 5);
            Complex b = new(2, -Math.PI / 2, PhaseMode.ArgPrimary);
            Console.WriteLine(a.ToString());
            Console.WriteLine(b.ToString());
            Console.WriteLine((a - b).ToString());
        }

        static void Mult()
        {
            Complex a = new(1, 5);
            Complex b = new(2, -Math.PI / 2, PhaseMode.ArgPrimary);
            Console.WriteLine(a.ToString());
            Console.WriteLine(b.ToString());
            Console.WriteLine((a * b).ToString());

            Complex x = new(2, 5 * Math.PI / 3, PhaseMode.ArgSecondary);
            Complex y = new(2, 5);
            Console.WriteLine(x.ToString());
            Console.WriteLine(y.ToString());
            Console.WriteLine((x * y).ToString());
            Complex r = new(2 + 5 * Math.Sqrt(3), 5 - 2 * Math.Sqrt(3));
            Console.WriteLine(r.ToString());
            Console.WriteLine((x * y).Equals(r));
        }

        static void Div()
        {
            Complex a = new(1, 5);
            Complex b = new(2, -Math.PI / 2, PhaseMode.ArgPrimary);
            Console.WriteLine(a.ToString());
            Console.WriteLine(b.ToString());
            Console.WriteLine((a / b).ToString());

            Complex x = new(2, 5 * Math.PI / 3, PhaseMode.ArgSecondary);
            Complex y = new(2, 5);
            Console.WriteLine(x.ToString());
            Console.WriteLine(y.ToString());
            Console.WriteLine((x / y).ToString());
        }

        static void Pow()
        {
            Complex a = new(1, 5);
            Console.WriteLine(a.ToString());
            Console.WriteLine((a ^ 3).ToString());
            Console.WriteLine();

            Complex x = new(2, 5 * Math.PI / 3, PhaseMode.ArgSecondary);
            Console.WriteLine(x.ToString());
            Console.WriteLine((x ^ 3).ToString());
            Console.WriteLine();

            Complex y = new(2, 5 * Math.PI / 3, PhaseMode.ArgSecondary);
            Console.WriteLine(y.ToString());
            Console.WriteLine((y ^ 5).ToString());
            Console.WriteLine(y.Sqrt().ToString());
        }

        static void Conj()
        {
            Complex a = new(1, 5);
            Console.WriteLine(a.ToString());
            Console.WriteLine(a.Conjugate().ToString());
            Console.WriteLine(a.Negate().ToString());
            Console.WriteLine();

            Complex x = new(2, 5 * Math.PI / 3, PhaseMode.ArgSecondary);
            Console.WriteLine(x.ToString());
            Console.WriteLine(x.Conjugate().ToString());
            Console.WriteLine(x.Negate().ToString());
        }

        static void Equals()
        {
            Complex a = new(1, Math.Sqrt(3));
            Complex b = new(2, Math.PI / 3, PhaseMode.ArgPrimary);
            Console.WriteLine(a.ToString());
            Console.WriteLine(b.ToString());
            Console.WriteLine(a.Equals(b));
        }
    }
}
