using System;

namespace ComplexLibrary
{
    internal class ComplexResolver
    {
        internal static Quadrant? GetQuadrant(double Re, double Im)
        {
            if (Re >= 0 && Im > 0)
                return Quadrant.Quadrant1;
            
            if (Re < 0 && Im > 0)
                return Quadrant.Quadrant2;

            if (Re < 0 && Im < 0)
                return Quadrant.Quadrant3;

            if (Re > 0 && Im <= 0)
                return Quadrant.Quadrant4;

            return null;
        }

        internal static double ResolvePhase(double Re, double Im, Quadrant? quadrant)
        {
            double angle = Math.Atan(Im / Re);

            if (Im is 0 && Re > 0)
            {
                return 0;
            }

            if (Im is 0 && Re < 0)
                return Math.PI;

            if (Im > 0 && Re is 0)
                return Math.PI / 2;

            if (Im < 0 && Re is 0)
                return 3 * Math.PI / 2;

            switch (quadrant)
            {
                case Quadrant.Quadrant1:
                    return angle;
                case Quadrant.Quadrant2:
                case Quadrant.Quadrant3:
                    return Math.PI + angle;
                case Quadrant.Quadrant4:
                    return 2 * Math.PI + angle;
                default:
                    return angle;
            }
        }

        internal static double ResolveDecimalErrors(double value, double epsilon = 1E-12)
        {
            double offset = Math.Abs(Math.Round(value) - value);
            return offset < epsilon ? Math.Round(value) : value;
        }
    }
}
