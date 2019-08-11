using Microsoft.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Decks.Samples.Commands
{
    public class ShuffleCommand<TElement> : CommandLineApplication where TElement : class
    {
        public ShuffleCommand(ExecutionContext<TElement> context) : base(true)
        {
            Name = "Shuffle";
            Description = "Shuffles the contents of the deck.";
            Context = context;

            OnExecute(() =>
            {
                var deck = Context.Deck;

                deck.DrawPile.Shuffle(true);
                deck.Deal();

                Out.WriteLine("Shuffled");

                return 0;
            });
        }

        public ExecutionContext<TElement> Context { get; }
    }
}
