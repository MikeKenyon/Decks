using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Decks
{
    /// <summary>
    /// Expresses that an operation has been attempted which is inappropriate not because of what
    /// you're attempting to do, but because the element it is acting on wasn't an appropriate 
    /// target for it.
    /// </summary>
    public class InvalidElementException : ArgumentException
    {
        /// <summary>
        /// Basic invalid element setup.
        /// </summary>
        public InvalidElementException() : this("The element isn't in a place to do this.")
        {
        }
        /// <summary>
        /// Exception with a custom message.
        /// </summary>
        /// <param name="message">The custom message.</param>
        public InvalidElementException(string message) : base(message)
        {
        }

        /// <summary>
        /// The custom message when an invalid element exception is caused by something else.
        /// </summary>
        /// <param name="message">The custom message.</param>
        /// <param name="innerException">The causing exception.</param>
        public InvalidElementException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// A constructor used internally by the serialization engine.
        /// </summary>
        /// <param name="info">The serialization bucket.</param>
        /// <param name="context">The context for serialization.</param>
        protected InvalidElementException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
