using System;
using System.Collections.Generic;
using System.Text;

namespace Decks.Samples
{
    public class ExecutionContext<TElement> where TElement : class
    {
        public bool Done { get; set; }
        public string LastCommand { get; set; }
        public Deck<TElement> Deck { get; set; }
        public Configuration.DeckOptions Options { get; set; } = new Configuration.DeckOptions();
    }
}
