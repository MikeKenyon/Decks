using Microsoft.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Decks.Samples.Commands
{
    public class DescribeCommand<TElement> : CommandLineApplication where TElement : class
    {
        public DescribeCommand(ExecutionContext<TElement> context) : base(true)
        {
            Name = "Describe";
            Description = $"Displays detailed information about a component.";

            Commands.Add(new DescribeDeckCommand<TElement>(context));
        }
    }
}