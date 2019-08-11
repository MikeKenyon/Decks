using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.Extensions.CommandLineUtils;

namespace Decks.Samples
{
    public class InteractiveCommand<TElement> : CommandLineApplication where TElement : class
    {
        public ExecutionContext<TElement> Context { get; }

        public InteractiveCommand(ExecutionContext<TElement> context) : base(true)
        {
            Commands.Add(new Commands.DescribeCommand<TElement>(context));
            Commands.Add(new Commands.ExitCommand<TElement>(context));
            Commands.Add(new Commands.HelpCommand(this));
            Commands.Add(new Commands.NewCommand<TElement>(context));
            Commands.Add(new Commands.OptionsCommand<TElement>(context));
            Commands.Add(new Commands.ShuffleCommand<TElement>(context));
            Context = context;
        }

        internal void Welcome()
        {
            Out.WriteLine("******************************************************");
            Out.WriteLine("**                                                  **");
            Out.WriteLine("**      Welcome to Decks Sample Application!        **");
            Out.WriteLine($"**      (Element Type: {typeof(TElement).Name})".PadRight(52) + "**");
            Out.WriteLine("**                                                  **");
            Out.WriteLine("******************************************************");
        }

        public void ProcessCommand()
        {
            ShowPrompt();
            var command = ReadCommand();
            try
            {
                Execute(command);
            }
            catch (CommandParsingException)
            { 
                Unhandled(Context.LastCommand);
            }
        }

        private void Unhandled(string command)
        {
            Error($"Didn't understand '{command}'.");
        }

        private static void Error(string error)
        {
            var old = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(error);
            Console.ForegroundColor = old;
        }

        private string[] ReadCommand()
        {
            var text = Console.ReadLine();
            Context.LastCommand = text;
            return SplitArguments(text);
        }
        public static string[] SplitArguments(string commandLine)
        {
            var parmChars = commandLine.ToCharArray();
            var inSingleQuote = false;
            var inDoubleQuote = false;
            for (var index = 0; index < parmChars.Length; index++)
            {
                if (parmChars[index] == '"' && !inSingleQuote)
                {
                    inDoubleQuote = !inDoubleQuote;
                    parmChars[index] = '\n';
                }
                if (parmChars[index] == '\'' && !inDoubleQuote)
                {
                    inSingleQuote = !inSingleQuote;
                    parmChars[index] = '\n';
                }
                if (!inSingleQuote && !inDoubleQuote && parmChars[index] == ' ')
                {
                    parmChars[index] = '\n';
                }
            }
            return (new string(parmChars)).Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
        }

        private void ShowPrompt()
        {
            Console.Write("> ");
        }
    }
}
