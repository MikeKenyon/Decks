using Microsoft.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Decks.Samples.Commands
{
    public class NewDeckCommand<TElement> : CommandLineApplication where TElement : class
    {
        private ExecutionContext<TElement> Context { get; set; }

        public NewDeckCommand(ExecutionContext<TElement> context) : base(true)
        {
            Name = "Deck";
            Description = $"Creates a new deck of {typeof(TElement).Name}.";
            Context = context;

            OnExecute(() =>
            {
                Context.Deck = new Deck<TElement>(Context.Options);
                Out.WriteLine("New deck created!");
                CheckInitialize();
                return 0;
            });
        }

        private void CheckInitialize()
        {
            var method = typeof(TElement).GetMethod("Initialize", BindingFlags.Static | BindingFlags.Public);
            if (method != null)
            {
                Out.WriteLine("Using standardized initialization ...");
                method.Invoke(null, new object[] { Context.Deck });
                Out.WriteLine("Initialized.");
            }
            else
            {
                Out.WriteLine("No initialization found.");
            }
        }
    }
}
