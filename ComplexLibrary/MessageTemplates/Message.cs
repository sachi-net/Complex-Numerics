namespace ComplexLibrary.MessageTemplates
{
    internal static class Message
    {
        internal const string INVALID_RADIUS = "Radius cannot be negative.";
        internal const string INVALID_ARGUMENT = "Complex argument is not in range [#RANGE] for the selected phase-mode \"[#PHASE]\"";
        internal const string COMPLEX_NOT_INITIALIZED = "Complex number object is not initialized.";
        internal const string COMPLEX_NOT_INITIALIZED_NAMED = "Complex number object \"[#NAME]\" is not initialized.";
        internal const string INVALID_CONVERSION = "Complex number cannot be converted into a real instance because Imaginary component exists.";
        internal const string ZERO_DIVISION_ERROR = "Cannot divide complex number by zero.";
    }
}
