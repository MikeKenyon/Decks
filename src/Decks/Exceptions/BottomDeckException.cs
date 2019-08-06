using System;
using System.Runtime.Serialization;

namespace Decks
{
    /// <summary>
    /// An exception thrown when you try to deal a card and another card cannot be dealt.
    /// </summary>
    public class BottomDeckException : Exception
    {
        /// <summary>
        /// Typical message for getting bottom-decked.
        /// </summary>
        public BottomDeckException() : base("Top deck is empty, no cards left to deal.")
        {
        }

        /// <summary>
        /// Custom message for getting bottom-decked.
        /// </summary>
        /// <param name="message">The custom message.</param>
        public BottomDeckException(string message) : base(message)
        {
        }

        /// <summary>
        /// Generates a bottom-decking based off of some other error occuring.
        /// </summary>
        /// <param name="message">Custom message for the bottom-decking.</param>
        /// <param name="innerException">What caused this.</param>
        public BottomDeckException(string message, Exception innerException) : base(message, innerException)
        {
        }
        /// <summary>
        /// Handles serialization of this exception over the wire.
        /// </summary>
        /// <param name="info">The serialization bucket.</param>
        /// <param name="context">The context of the serialization run.</param>
        protected BottomDeckException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
