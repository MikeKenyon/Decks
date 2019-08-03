using System;
using System.Collections.Generic;
using System.Text;

namespace Decks.Internal.Serialization
{
    internal static class JsonProperties
    {
        internal const string ElementType = "element";
        internal const string TypeName = "type";
        internal const string Options = "options";

        internal const string NotAvailable = "n/a";

        internal const string Elements = "contents";
        internal const string ShuffleCount = "shuffled";
        internal const string TotalCount = "total";

        internal const string DrawPile = "draw";
        internal const string Discards = "discards";
        internal const string Hands = "hands";
        internal const string Tableau = "tableau";
        internal const string Table = "table";
    }
}
