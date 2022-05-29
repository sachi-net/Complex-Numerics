/// <summary>
/// Method of sign-convention when defining argument
/// </summary>
public enum PhaseMode
{
    /// <summary>
    /// Restrict phase in argument of complex number Z within range -π &lt; Arg(Z) ≤ π
    /// </summary>
    ArgPrimary,
    /// <summary>
    /// Angle phase in argument of complex number Z within range 0 ≤ arg(Z) &lt; 2π
    /// </summary>
    ArgSecondary
}
