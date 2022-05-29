using System;

namespace ComplexLibrary
{
    public class Complex : IEquatable<Complex>
    {
        private readonly double eps = 1E-12;
        public readonly double E = Math.E;
        public readonly double Pi = Math.PI;

        public double Real { get; }
        public double Imaginary { get; }
        public double Argument { get; }
        public double Modulus { get; }

        public Complex(double real, double imaginary)
        {
            Real = real;
            Imaginary = imaginary;

            if (real is 0 && imaginary is 0)
            {
                Modulus = 0;
                Argument = 0;
                return;
            }

            Modulus = Math.Sqrt(Math.Pow(real, 2) + Math.Pow(imaginary, 2));
            Quadrant? quadrant = ComplexResolver.GetQuadrant(real, imaginary);
            Argument = ComplexResolver.ResolvePhase(real, imaginary, quadrant);
        }

        public Complex(double magnitude, double argument, PhaseMode phaseMode = PhaseMode.ArgPrimary)
        {
            if (phaseMode is PhaseMode.ArgPrimary && !(argument >= -Math.PI && argument <= Math.PI))
            {
                throw new Exception();
            }

            if (phaseMode is PhaseMode.ArgSecondary && !(argument >= 0 && argument <= 2 * Math.PI))
            {
                throw new Exception();
            }

            Real = ComplexResolver.ResolveDecimalErrors(magnitude * Math.Cos(argument));
            Imaginary = ComplexResolver.ResolveDecimalErrors(magnitude * Math.Sin(argument));

            Modulus = magnitude;
            Quadrant? quadrant = ComplexResolver.GetQuadrant(Real, Imaginary);
            Argument = ComplexResolver.ResolvePhase(Real, Imaginary, quadrant);
        }

        public override string ToString()
        {
            double Re = Real is 0 ? Math.Abs(Real) : Real;
            double Im = Imaginary is 0 ? Math.Abs(Imaginary) : Imaginary;

            return $"({Re}, {Im})";
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Complex);
        }

        public bool Equals(Complex other)
        {
            if (other is null || this is null)
                return false;

            return Math.Abs(Real - other.Real) <= eps && Math.Abs(Imaginary - other.Imaginary) <= eps;
        }

        public static Complex operator +(Complex x, Complex y)
        {
            if (x is null || y is null)
            {
                throw new Exception();
            }

            return new(x.Real + y.Real, x.Imaginary + y.Imaginary);
        }

        public static Complex operator -(Complex x, Complex y)
        {
            if (x is null || y is null)
            {
                throw new Exception();
            }

            return new(x.Real - y.Real, x.Imaginary - y.Imaginary);
        }

        public static Complex operator *(Complex x, Complex y)
        {
            if (x is null || y is null)
            {
                throw new Exception();
            }

            return new(x.Real*y.Real-x.Imaginary*y.Imaginary, x.Real*y.Imaginary + y.Real*x.Imaginary);
        }

        public static Complex operator /(Complex x, Complex y)
        {
            if (x is null || y is null)
            {
                throw new Exception();
            }

            if (y.Modulus is 0)
            {
                throw new Exception();
            }

            double Re = (x.Real * y.Real + x.Imaginary * y.Imaginary) / Math.Pow(y.Modulus, 2);
            double Im = (x.Imaginary * y.Real - x.Real * y.Imaginary) / Math.Pow(y.Modulus, 2);

            return new(Re, Im);
        }

        public static Complex operator ^(Complex x, int e)
        {
            if (x is null)
            {
                throw new Exception();
            }

            double Re = Math.Cos(e * x.Argument) * Math.Pow(x.Modulus, e);
            double Im = Math.Sin(e * x.Argument) * Math.Pow(x.Modulus, e);

            Re = ComplexResolver.ResolveDecimalErrors(Re);
            Im = ComplexResolver.ResolveDecimalErrors(Im);

            return new(Re, Im);
        }

        public static bool operator ==(Complex x, Complex y)
        {
            return x.Equals(y);
        }

        public static bool operator !=(Complex x, Complex y)
        {
            return !x.Equals(y);
        }

        public Complex Negate()
        {
            return new(-Real, -Imaginary);
        }

        public Complex Conjugate()
        {
            return new(Real, -Imaginary);
        }

        public Complex Sqrt()
        {
            if (this is null)
            {
                throw new Exception();
            }

            double Re = Math.Cos(Argument / 2) * Math.Sqrt(Modulus);
            double Im = Math.Sin(Argument / 2) * Math.Sqrt(Modulus);

            Re = ComplexResolver.ResolveDecimalErrors(Re);
            Im = ComplexResolver.ResolveDecimalErrors(Im);

            return new(Re, Im);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Math.Round(Real, 10), Math.Round(Imaginary, 10));
        }
    }
}
