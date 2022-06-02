using ComplexLibrary.Exceptions;
using ComplexLibrary.MessageTemplates;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ComplexLibrary
{
    /// <summary>
    /// Defines a complex number with complex numerical arithmetics.
    /// </summary>
    public class Complex : IEquatable<Complex>
    {
        private readonly double eps = 1E-12;

        /// <summary>
        /// Mathematical constant (Euler's number) e.
        /// </summary>
        public static readonly double E = Math.E;
        /// <summary>
        /// Mathematical constant π.
        /// </summary>
        public static readonly double Pi = Math.PI;

        /// <summary>
        /// Real component of this complex number.
        /// </summary>
        public double Real { get; }

        /// <summary>
        /// Imaginary component of this complex number.
        /// </summary>
        public double Imaginary { get; }

        /// <summary>
        /// Argument (phase) of this complex number represented in Arg-Secondary (0 ≤ arg(Z) &lt; 2π) convention.
        /// </summary>
        public double Argument { get; }

        /// <summary>
        /// Modulus (magnitude or radius) of this complex number.
        /// </summary>
        public double Modulus { get; }

        /// <summary>
        /// Initializes an instance of new Complex object with cartesian (Argand) coordinates.
        /// </summary>
        /// <param name="real">Real (x or Re(Z)) component of the complex number.</param>
        /// <param name="imaginary">Imaginary (y or Im(Z)) component of the complex number.</param>
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

        /// <summary>
        /// Initializes an instance of new Complex object with polar coordinates.
        /// </summary>
        /// <param name="radius">Radius (modulus) of the complex number.</param>
        /// <param name="argument">Argument (phase) of the complex number in radians.</param>
        /// <param name="phaseMode">Sign-convention of argument in either Arg(Z): (-π, π] or arg(Z): [0, 2π).</param>
        /// <exception cref="ArgumentException">Throws when the radius is negative.</exception>
        /// <exception cref="ArgumentException">Throws when the complex argument does not match with the given phase-mode.</exception>
        public Complex(double radius, double argument, PhaseMode phaseMode)
        {
            if (radius < 0)
            {
                throw new ArgumentException(Message.INVALID_RADIUS);
            }

            if (phaseMode is PhaseMode.ArgPrimary && !(argument > -Math.PI && argument <= Math.PI))
            {
                throw new ArgumentException(Message.INVALID_ARGUMENT
                    .Replace("[#RANGE]", "(-π, π]")
                    .Replace("[#PHASE]", phaseMode.ToString()));
            }

            if (phaseMode is PhaseMode.ArgSecondary && !(argument >= 0 && argument < 2 * Math.PI))
            {
                throw new ArgumentException(Message.INVALID_ARGUMENT
                    .Replace("[#RANGE]", "[0, 2π)")
                    .Replace("[#PHASE]", phaseMode.ToString()));
            }

            Real = ComplexResolver.ResolveDecimalErrors(radius * Math.Cos(argument));
            Imaginary = ComplexResolver.ResolveDecimalErrors(radius * Math.Sin(argument));

            Modulus = radius;
            Quadrant? quadrant = ComplexResolver.GetQuadrant(Real, Imaginary);
            Argument = ComplexResolver.ResolvePhase(Real, Imaginary, quadrant);
        }

        /// <summary>
        /// Convert the complex number into a string notation.
        /// </summary>
        /// <returns>String notation of this complex number in cartesian coordinates.</returns>
        /// <exception cref="ComplexNotInitializedException">Throws when this complex object is not initialized.</exception>
        public override string ToString()
        {
            if (this is null)
            {
                throw new ComplexNotInitializedException(Message.COMPLEX_NOT_INITIALIZED);
            }

            double Re = (Real is 0 || Math.Abs(Real) < eps) ? Math.Abs(Real) : Real;
            double Im = (Imaginary is 0 || Math.Abs(Imaginary) < eps) ? Math.Abs(Imaginary) : Imaginary;

            return $"({Re}, {Im})";
        }

        /// <summary>
        /// Convert the complex number into a string notation with given decimal precision.
        /// </summary>
        /// <param name="precision">Number of decimals to in string notation.</param>
        /// <returns>String notation of this complex number in cartesian coordinates.</returns>
        /// <exception cref="ComplexNotInitializedException">Throws when this complex object is not initialized.</exception>
        public string ToString(byte precision)
        {
            if (this is null)
            {
                throw new ComplexNotInitializedException(Message.COMPLEX_NOT_INITIALIZED);
            }

            double Re = (Real is 0 || Math.Abs(Real) < eps) ? Math.Abs(Real) : Real;
            double Im = (Imaginary is 0 || Math.Abs(Imaginary) < eps) ? Math.Abs(Imaginary) : Imaginary;

            return $"({Math.Round(Re, precision)}, {Math.Round(Im, precision)})";
        }

        /// <summary>
        /// Convert the complex number into the given standard notation with decimal precision.
        /// </summary>
        /// <param name="format">Complex notation either Cartesian, Polar or Exponential.</param>
        /// <param name="precision">Number of decimals to in string notation.</param>
        /// <returns>String notation of this complex number with given standard format (arg(Z): [0, 2π) convention is used for Polar and Exponential formats).</returns>
        /// <exception cref="ComplexNotInitializedException">Throws when this complex object is not initialized.</exception>
        public string ToString(ComplexFormat format, byte precision = 4)
        {
            if (this is null)
            {
                throw new ComplexNotInitializedException(Message.COMPLEX_NOT_INITIALIZED);
            }

            if (format is ComplexFormat.Cartesian)
            {
                double Re = Math.Round(Real, precision);
                double Im = Math.Round(Imaginary, precision);
                IDictionary<Complex, string> cases = new Dictionary<Complex, string>
                {
                    { new (0, 0), "0"},
                    { new (0, 1), "i" },
                    { new (0, -1), "-i" },
                    { new (1, 0), "1" },
                    { new (-1, 0), "-1" },
                    { new (1, 1), "1+i" },
                    { new (1, -1), "1-i" },
                    { new (-1, 1), "-1+i" },
                    { new (-1, -1), "-1-i" }
                };

                string complex = cases.ContainsKey(this) ? 
                    cases.Where(x => x.Key.Equals(this)).FirstOrDefault().Value :
                    (Real is 0 || Math.Abs(Real) < eps) && Imaginary is not 0 ? $"{Im}i" :
                    Real is not 0 && (Imaginary is 0 || Math.Abs(Imaginary) < eps) ? $"{Re}" :
                    Real is not 0 && Imaginary is 1 ? $"{Re}+i" :
                    Real is not 0 && Imaginary is -1 ? $"{Re}-i" :
                    Imaginary > 0 ? $"{Re}+{Im}i" : $"{Re}{Im}i";

                return complex.Replace("+", " + ").Replace("-", " - ").Trim();
            }

            if (format is ComplexFormat.Polar)
            {
                double mod = Math.Round(Modulus, precision);
                double arg = Math.Round(Argument, precision);

                if (mod is 0)
                    return "0";

                string modStr = mod is 1 ? string.Empty : mod.ToString();

                string complex =
                    Math.Abs(Argument) % Math.PI is 0 or < 1E-12 ? $"{modStr}.Cos({arg})" :
                    Math.Abs(Argument) % (Math.PI / 2) is 0 or < 1E-12 ? $"{modStr}i.Sin({arg})" :
                    mod is 1 ? $"Cos({arg})+i.Sin({arg})" : 
                    $"{modStr}[Cos({arg})+i.Sin({arg})]";

                return complex.Replace("+", " + ").Replace("-", " - ").Trim();
            }

            if (format is ComplexFormat.Exponential)
            {
                double mod = Math.Round(Modulus, precision);
                double arg = Math.Round(Argument, precision);

                if (mod is 0)
                    return "0";

                string modStr = mod is 1 ? string.Empty : mod.ToString();
                string argStr = arg is 1 ? string.Empty : arg.ToString();

                string complex = arg is 0 ? modStr : $"{modStr}e{arg}i";

                return complex.Replace("+", " + ").Replace("-", " - ").Trim();
            }

            return ToString(precision);
        }

        /// <summary>
        /// Convert this Complex to double precision floating-point number.
        /// </summary>
        /// <returns>Value of this complex number as real.</returns>
        /// <exception cref="InvalidCastException">Throws when this complex object is not a pure real number.</exception>
        public double ToReal()
        {
            if (Imaginary is not 0)
            {
                throw new InvalidCastException(Message.INVALID_CONVERSION);
            }

            return Real;
        }

        /// <summary>
        /// Convert double-precision real number to a complex number.
        /// </summary>
        /// <param name="n">Double-precision real number n.</param>
        /// <returns>Complex number by provided real number.</returns>
        public static Complex ToComplex(double n)
        {
            return new(n, 0);
        }

        /// <summary>
        /// Calculates complex roots of quadratic equation ax² + bx + c = 0.
        /// </summary>
        /// <param name="a">Coefficient a of x squared (x²).</param>
        /// <param name="b">Coefficient b of x (x¹).</param>
        /// <param name="c">Coefficient c as constant (x⁰).</param>
        /// <returns>Array of roots in complex form.</returns>
        /// <exception cref="InvalidOperationException">Throws when coefficent a is 0 in the quadratic equation.</exception>
        public static Complex[] FindQuadraticRoots(double a, double b, double c)
        {
            if (a is 0)
            {
                throw new InvalidOperationException(Message.BAD_EQUATION);
            }

            double dx = b * b - 4 * a * c;
            double d = 2 * a;

            Complex r1 = null, r2 = null;

            if(dx > 0)
            {
                r1 = new(-b / d + Math.Sqrt(dx) / d, 0);
                r2 = new(-b / d - Math.Sqrt(dx) / d, 0);
            }

            if (dx is 0)
            {
                r1 = new(-b / d, 0);
                return new Complex[] { r1 };
            }

            if (dx < 0)
            {
                dx = Math.Abs(dx);
                r1 = new(-b / d, Math.Sqrt(dx) / d);
                r2 = new(-b / d, -Math.Sqrt(dx) / d);
            }

            return new Complex[] { r1, r2 };
        }

        /// <summary>
        /// Evaluate the equality of this complex number with the given object.
        /// </summary>
        /// <param name="obj">Object to be compare with this complex number.</param>
        /// <returns>Return true if the objects are equals and otherwise false.</returns>
        public override bool Equals(object obj)
        {
            return Equals(obj as Complex);
        }

        /// <summary>
        /// Evaluate the equality of this complex number with the given complex number.
        /// </summary>
        /// <param name="other">Complex number to be compare with this complex number.</param>
        /// <returns>Return true if the objects are equals and otherwise false.</returns>
        public bool Equals(Complex other)
        {
            if (other is null || this is null)
                return false;

            return Math.Abs(Real - other.Real) <= eps && Math.Abs(Imaginary - other.Imaginary) <= eps;
        }

        /// <summary>
        /// Add two complex numbers.
        /// </summary>
        /// <param name="x">Complex number x.</param>
        /// <param name="y">Complex number y.</param>
        /// <returns>Complex result of the addion of complex number x and y.</returns>
        /// <exception cref="ComplexNotInitializedException">Throws when the x or y complex objects are not initialized.</exception>
        public static Complex operator +(Complex x, Complex y)
        {
            if (x is null)
            {
                throw new ComplexNotInitializedException(Message.COMPLEX_NOT_INITIALIZED_NAMED
                    .Replace("[#NAME]", nameof(x)));
            }

            if (y is null)
            {
                throw new ComplexNotInitializedException(Message.COMPLEX_NOT_INITIALIZED_NAMED
                    .Replace("[#NAME]", nameof(y)));
            }

            return new(x.Real + y.Real, x.Imaginary + y.Imaginary);
        }

        /// <summary>
        /// Add complex number to a real number.
        /// </summary>
        /// <param name="x">Complex number x.</param>
        /// <param name="n">Double-precision real number n.</param>
        /// <returns>Complex result of the addion of complex number x and real number n.</returns>
        /// <exception cref="ComplexNotInitializedException">Throws when the current complex object is not initialized.</exception>
        public static Complex operator +(Complex x, double n)
        {
            if (x is null)
            {
                throw new ComplexNotInitializedException(Message.COMPLEX_NOT_INITIALIZED_NAMED
                    .Replace("[#NAME]", nameof(x)));
            }

            return new(x.Real + n, x.Imaginary);
        }

        /// <summary>
        /// Add real number to complex number.
        /// </summary>
        /// <param name="x">Complex number x.</param>
        /// <param name="n">Double-precision real number n.</param>
        /// <returns>Complex result of the addion of complex number x and real number n.</returns>
        /// <exception cref="ComplexNotInitializedException">Throws when the current complex object is not initialized.</exception>
        public static Complex operator +(double n, Complex x)
        {
            if (x is null)
            {
                throw new ComplexNotInitializedException(Message.COMPLEX_NOT_INITIALIZED_NAMED
                    .Replace("[#NAME]", nameof(x)));
            }

            return new(x.Real + n, x.Imaginary);
        }

        /// <summary>
        /// Subtract two complex numbers.
        /// </summary>
        /// <param name="x">Complex number x.</param>
        /// <param name="y">Complex number y.</param>
        /// <returns>Complex result of the subtraction of complex number y from x.</returns>
        /// <exception cref="ComplexNotInitializedException">Throws when the x or y complex objects are not initialized.</exception>
        public static Complex operator -(Complex x, Complex y)
        {
            if (x is null)
            {
                throw new ComplexNotInitializedException(Message.COMPLEX_NOT_INITIALIZED_NAMED
                    .Replace("[#NAME]", nameof(x)));
            }

            if (y is null)
            {
                throw new ComplexNotInitializedException(Message.COMPLEX_NOT_INITIALIZED_NAMED
                    .Replace("[#NAME]", nameof(y)));
            }

            return new(x.Real - y.Real, x.Imaginary - y.Imaginary);
        }

        /// <summary>
        /// Subtract a real number from complex number.
        /// </summary>
        /// <param name="x">Complex number x.</param>
        /// <param name="n">Double-precision real number n.</param>
        /// <returns>Complex result of the subtraction of real number n from complex number x.</returns>
        /// <exception cref="ComplexNotInitializedException">Throws when the current complex object is not initialized.</exception>
        public static Complex operator -(Complex x, double n)
        {
            if (x is null)
            {
                throw new ComplexNotInitializedException(Message.COMPLEX_NOT_INITIALIZED_NAMED
                    .Replace("[#NAME]", nameof(x)));
            }

            return new(x.Real - n, x.Imaginary);
        }

        /// <summary>
        /// Subtract complex number from a real number.
        /// </summary>
        /// <param name="x">Complex number x.</param>
        /// <param name="n">Double-precision real number n.</param>
        /// <returns>Complex result of the subtraction of complex number x from real number n.</returns>
        /// <exception cref="ComplexNotInitializedException">Throws when the current complex object is not initialized.</exception>
        public static Complex operator -(double n, Complex x)
        {
            if (x is null)
            {
                throw new ComplexNotInitializedException(Message.COMPLEX_NOT_INITIALIZED_NAMED
                    .Replace("[#NAME]", nameof(x)));
            }

            return new(n - x.Real, -x.Imaginary);
        }

        /// <summary>
        /// Negate current complex number.
        /// </summary>
        /// <param name="x">Current cumplex number.</param>
        /// <returns>Negative of current complex number.</returns>
        /// <exception cref="ComplexNotInitializedException">Throws when the current complex object is not initialized.</exception>
        public static Complex operator -(Complex x)
        {
            if (x is null)
            {
                throw new ComplexNotInitializedException(Message.COMPLEX_NOT_INITIALIZED_NAMED
                    .Replace("[#NAME]", nameof(x)));
            }

            return x.Negate();
        }

        /// <summary>
        /// Multiply two complex numbers.
        /// </summary>
        /// <param name="x">Complex number x.</param>
        /// <param name="y">Complex number y.</param>
        /// <returns>Complex result of the multiplication of complex number x and y.</returns>
        /// <exception cref="ComplexNotInitializedException">Throws when the x or y complex objects are not initialized.</exception>
        public static Complex operator *(Complex x, Complex y)
        {
            if (x is null)
            {
                throw new ComplexNotInitializedException(Message.COMPLEX_NOT_INITIALIZED_NAMED
                    .Replace("[#NAME]", nameof(x)));
            }

            if (y is null)
            {
                throw new ComplexNotInitializedException(Message.COMPLEX_NOT_INITIALIZED_NAMED
                    .Replace("[#NAME]", nameof(y)));
            }

            return new(x.Real*y.Real-x.Imaginary*y.Imaginary, x.Real*y.Imaginary + y.Real*x.Imaginary);
        }

        /// <summary>
        /// Multiply a real number by complex number.
        /// </summary>
        /// <param name="x">Complex number x.</param>
        /// <param name="n">Double-precision real number n.</param>
        /// <returns>Complex result of the multiplication of complex number x by real number n.</returns>
        /// <exception cref="ComplexNotInitializedException">Throws when the current complex object is not initialized.</exception>
        public static Complex operator *(double n, Complex x)
        {
            if (x is null)
            {
                throw new ComplexNotInitializedException(Message.COMPLEX_NOT_INITIALIZED_NAMED
                    .Replace("[#NAME]", nameof(x)));
            }

            return new(n * x.Real, n * x.Imaginary);
        }

        /// <summary>
        /// Multiply complex number by a real number.
        /// </summary>
        /// <param name="x">Complex number x.</param>
        /// <param name="n">Double-precision real number n.</param>
        /// <returns>Complex result of the multiplication of complex number x by real number n.</returns>
        /// <exception cref="ComplexNotInitializedException">Throws when the current complex object is not initialized.</exception>
        public static Complex operator *(Complex x, double n)
        {
            if (x is null)
            {
                throw new ComplexNotInitializedException(Message.COMPLEX_NOT_INITIALIZED_NAMED
                    .Replace("[#NAME]", nameof(x)));
            }

            return n * x;
        }

        /// <summary>
        /// Divide complex number by another.
        /// </summary>
        /// <param name="x">Complex number x.</param>
        /// <param name="y">Complex number y.</param>
        /// <returns>Complex result of the division of complex number x by y.</returns>
        /// <exception cref="ComplexNotInitializedException">Throws when the x or y complex objects are not initialized.</exception>
        public static Complex operator /(Complex x, Complex y)
        {
            if (x is null)
            {
                throw new ComplexNotInitializedException(Message.COMPLEX_NOT_INITIALIZED_NAMED
                    .Replace("[#NAME]", nameof(x)));
            }

            if (y is null)
            {
                throw new ComplexNotInitializedException(Message.COMPLEX_NOT_INITIALIZED_NAMED
                    .Replace("[#NAME]", nameof(y)));
            }

            if (y.Modulus is 0)
            {
                throw new DivideByZeroException(Message.ZERO_DIVISION_ERROR);
            }

            double Re = (x.Real * y.Real + x.Imaginary * y.Imaginary) / Math.Pow(y.Modulus, 2);
            double Im = (x.Imaginary * y.Real - x.Real * y.Imaginary) / Math.Pow(y.Modulus, 2);

            return new(Re, Im);
        }

        /// <summary>
        /// Divide complex number by a real number.
        /// </summary>
        /// <param name="x">Complex number x.</param>
        /// <param name="n">Double-precision real number n.</param>
        /// <returns>Complex result of the division of complex number x by real number n.</returns>
        /// <exception cref="ComplexNotInitializedException">Throws when the current complex object is not initialized.</exception>
        /// <exception cref="DivideByZeroException">Throws when try divide complex number by zero.</exception>
        public static Complex operator /(Complex x, double n)
        {
            if (x is null)
            {
                throw new ComplexNotInitializedException(Message.COMPLEX_NOT_INITIALIZED_NAMED
                    .Replace("[#NAME]", nameof(x)));
            }

            if (n is 0)
            {
                throw new DivideByZeroException(Message.ZERO_DIVISION_ERROR);
            }

            return new(x.Real / n, x.Imaginary / n);
        }

        /// <summary>
        /// Raise the complex number to the power of provided real number.
        /// </summary>
        /// <param name="x">Complex number x.</param>
        /// <param name="e">Exponent value (power) e.</param>
        /// <returns>Complex result of the exponential of complex number x to the power of e.</returns>
        /// <exception cref="ComplexNotInitializedException">Throws when the current complex object is not initialized.</exception>
        public static Complex operator ^(Complex x, int e)
        {
            if (x is null)
            {
                throw new ComplexNotInitializedException(Message.COMPLEX_NOT_INITIALIZED_NAMED
                    .Replace("[#NAME]", nameof(x)));
            }

            double Re = Math.Cos(e * x.Argument) * Math.Pow(x.Modulus, e);
            double Im = Math.Sin(e * x.Argument) * Math.Pow(x.Modulus, e);

            Re = ComplexResolver.ResolveDecimalErrors(Re);
            Im = ComplexResolver.ResolveDecimalErrors(Im);

            return new(Re, Im);
        }

        /// <summary>
        /// Calculate the conjugate of this complex number.
        /// </summary>
        /// <param name="x">Complex number x.</param>
        /// <returns>Conjugate of complex number x.</returns>
        /// <exception cref="ComplexNotInitializedException">Throws when the current complex object is not initialized.</exception>
        public static Complex operator ~(Complex x)
        {
            if (x is null)
            {
                throw new ComplexNotInitializedException(Message.COMPLEX_NOT_INITIALIZED_NAMED
                    .Replace("[#NAME]", nameof(x)));
            }

            return x.Conjugate();
        }

        /// <summary>
        /// Evaluate the equality of two complex numbers.
        /// </summary>
        /// <param name="x">Complex number x.</param>
        /// <param name="y">Complex number y.</param>
        /// <returns>Return true if the objects are equals and otherwise false.</returns>
        public static bool operator ==(Complex x, Complex y)
        {
            return x.Equals(y);
        }

        /// <summary>
        /// Evaluate the inequality of two complex numbers.
        /// </summary>
        /// <param name="x">Complex number x.</param>
        /// <param name="y">Complex number y.</param>
        /// <returns>Return true if the objects are not equals and otherwise false.</returns>
        public static bool operator !=(Complex x, Complex y)
        {
            return !x.Equals(y);
        }

        /// <summary>
        /// Negate this complex number.
        /// </summary>
        /// <returns>Negative of this complex number.</returns>
        /// <exception cref="InvalidCastException">Throws when this complex object is not a pure real number.</exception>
        public Complex Negate()
        {
            if (this is null)
            {
                throw new ComplexNotInitializedException(Message.COMPLEX_NOT_INITIALIZED);
            }

            return new(-Real, -Imaginary);
        }

        /// <summary>
        /// Calculate the conjugate of this complex number.
        /// </summary>
        /// <returns>Conjugate of this complex number.</returns>
        /// <exception cref="InvalidCastException">Throws when this complex object is not a pure real number.</exception>
        public Complex Conjugate()
        {
            if (this is null)
            {
                throw new ComplexNotInitializedException(Message.COMPLEX_NOT_INITIALIZED);
            }

            return new(Real, -Imaginary);
        }

        /// <summary>
        /// Calculate the reciprocal (multiplicative inverse) of this complex number.
        /// </summary>
        /// <returns>Inverse of the complex number.</returns>
        /// <exception cref="InvalidCastException">Throws when this complex object is not a pure real number.</exception>
        /// <exception cref="DivideByZeroException">Throws when the modulus of this complex number is zero which causes the zero division.</exception>
        public Complex Reciprocal()
        {
            if (this is null)
            {
                throw new ComplexNotInitializedException(Message.COMPLEX_NOT_INITIALIZED);
            }

            if (Modulus is 0)
            {
                throw new DivideByZeroException(Message.ZERO_DIVISION_ERROR);
            }

            return new Complex(Real, -Imaginary) / Math.Pow(Modulus, 2);
        }

        /// <summary>
        /// Calculate the square root of this complex number.
        /// </summary>
        /// <returns>Complex result of square root of this complex number.</returns>
        /// <exception cref="InvalidCastException">Throws when this complex object is not a pure real number.</exception>
        public Complex Sqrt()
        {
            if (this is null)
            {
                throw new ComplexNotInitializedException(Message.COMPLEX_NOT_INITIALIZED);
            }

            double Re = Math.Cos(Argument / 2) * Math.Sqrt(Modulus);
            double Im = Math.Sin(Argument / 2) * Math.Sqrt(Modulus);

            Re = ComplexResolver.ResolveDecimalErrors(Re);
            Im = ComplexResolver.ResolveDecimalErrors(Im);

            return new(Re, Im);
        }

        /// <summary>
        /// Calculate the hashcode of this complex number.
        /// </summary>
        /// <returns>Hash value of this complex number.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Math.Round(Real, 10), Math.Round(Imaginary, 10));
        }
    }
}
