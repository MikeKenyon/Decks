using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Decks
{
    /// <summary>
    /// An exception thrown when you try to deal a card and another card cannot be dealt.
    /// </summary>
    public class BottomDeckException : Exception
    {
        public BottomDeckException() : base("Top deck is empty, no cards left to deal.")
        {
        }

        public BottomDeckException(string message) : base(message)
        {
        }

        public BottomDeckException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BottomDeckException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
