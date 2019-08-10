using Microsoft.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Decks.Samples.Commands
{
    public class DescribeDeckCommand<TElement> : CommandLineApplication where TElement : class
    {
        public DescribeDeckCommand(ExecutionContext<TElement> context) : base(true)
        {
            Name = "Deck";
            Description = "Displays detailed information about a deck.";
            Context = context;

            OnExecute(() =>
            {
                var ser = new JsonSerializerSettings { Formatting = Formatting.Indented };
                var text = JsonConvert.SerializeObject(Context.Deck, ser);
                Out.WriteLine(text);
                return 0;
            });
        }

        public ExecutionContext<TElement> Context { get; }
    }
}