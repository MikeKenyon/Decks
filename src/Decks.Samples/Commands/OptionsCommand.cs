using Microsoft.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Decks.Samples.Commands
{
    public class OptionsCommand<TElement> : CommandLineApplication where TElement : class
    {
        public OptionsCommand(ExecutionContext<TElement> context)
        {
            Name = "Options";
            Description = "Manipulates the options used to create a deck.";
            Context = context;

            Commands.Add(new OptionsDisplayCommand<TElement>(context));
        }

        public ExecutionContext<TElement> Context { get; }
    }
}
