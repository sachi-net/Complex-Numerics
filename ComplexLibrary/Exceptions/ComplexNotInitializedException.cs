using System;

namespace ComplexLibrary.Exceptions
{
    /// <summary>
    /// Exception when the complex number is not initialized before usage.
    /// </summary>
    public class ComplexNotInitializedException : Exception
    {
        /// <summary>
        /// Initialize an instance of ComplexNotInitializedException.
        /// </summary>
        public ComplexNotInitializedException()
        {

        }

        /// <summary>
        /// Initialize an instance of ComplexNotInitializedException with specified message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for this exception.</param>
        public ComplexNotInitializedException(string message) : base(message)
        {

        }

        /// <summary>
        /// Initialize an instance of ComplexNotInitializedException with specified message and exception that cause this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for this exception.</param>
        /// <param name="inner">The exception that is the cause of the current exception, or a null reference.</param>
        public ComplexNotInitializedException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
