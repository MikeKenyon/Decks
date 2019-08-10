using Microsoft.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Decks.Samples.Commands
{
    public class NewCommand<TElement> : CommandLineApplication where TElement : class
    {
        public NewCommand(ExecutionContext<TElement> context) : base(true)
        {
            Name = "New";
            Description = "Create a new element in the system.";

            Commands.Add(new NewDeckCommand<TElement>(context));
        }
    }
}
