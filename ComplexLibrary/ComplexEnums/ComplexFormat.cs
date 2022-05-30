/// <summary>
/// Complex number format to be used in string notation.
/// </summary>
public enum ComplexFormat
{
    /// <summary>
    /// Cartesian format as Z = x + iy.
    /// </summary>
    Cartesian,

    /// <summary>
    /// Polar format as Z = r[Cos(φ) + i.Sin(φ)].
    /// </summary>
    Polar,

    /// <summary>
    /// Exponential format as Z = re^(iφ).
    /// </summary>
    Exponential
}