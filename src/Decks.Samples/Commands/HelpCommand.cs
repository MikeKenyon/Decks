using Microsoft.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Decks.Samples.Commands
{
    public class HelpCommand : CommandLineApplication
    {
        public HelpCommand(CommandLineApplication root) : base(true)
        {
            Name = "Help";
            Description = "Gets help information about this application.";

            CommandArg = Argument("Command", "A command to get more detail on.");

            Root = root;

            OnExecute(() =>
            {
                Root.ShowHelp(CommandArg.Value);
                return 0;
            });
        }

        public CommandArgument CommandArg { get; set; }

        public CommandLineApplication Root { get; }
    }
}
