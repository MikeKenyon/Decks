using Microsoft.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Decks.Samples.Commands
{
    public class ExitCommand<TElement> : CommandLineApplication where TElement : class
    {
        private ExecutionContext<TElement> Context { get; set; }

        public ExitCommand(ExecutionContext<TElement> context) : base(true)
        {
            Name = "Exit";
            Description = "Quits this application.";
            Context = context;

            OnExecute(() => {
                context.Done = true;
                return 0;
            });
        }


    }
}
