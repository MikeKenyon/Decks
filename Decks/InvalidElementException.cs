using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Decks
{
    public class InvalidElementException : Exception
    {
        public InvalidElementException() : this("The element isn't in a place to do this.")
        {
        }

        public InvalidElementException(string message) : base(message)
        {
        }

        public InvalidElementException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidElementException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
