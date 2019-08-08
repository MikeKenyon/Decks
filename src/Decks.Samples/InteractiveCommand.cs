using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.CommandLineUtils;

namespace Decks.Samples
{
    public class InteractiveCommand : CommandLineApplication
    {
        public InteractiveCommand(ExecutionContext context) : base(true)
        {
            Commands.Add(new Commands.ExitCommand(context));
            Commands.Add(new Commands.HelpCommand(this));
        }

        internal void Welcome()
        {
            Out.WriteLine("******************************************************");
            Out.WriteLine("**                                                  **");
            Out.WriteLine("**      Welcome to Decks Sample Application!        **");
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
                Unhandled(command);
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

        private string ReadCommand()
        {
            return Console.ReadLine();
        }

        private void ShowPrompt()
        {
            Console.Write("> ");
        }
    }
}
