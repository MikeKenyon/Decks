using Microsoft.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Decks.Samples.Commands
{
    public class OptionsDisplayCommand<TElement> : CommandLineApplication where TElement : class
    {
        private const int Level = 4;
        private const int ColumnWidth = 60;

        public OptionsDisplayCommand(ExecutionContext<TElement> context)
        {
            Name = "Display";
            Description = "Displays the current options.";
            ExtendedHelpText = "This displays all of the options information currently in use.";

            Context = context;

            OnExecute(() =>
            {
                DisplayOptions(Context.Options);
                return 0;
            });
        }

        private void DisplayOptions(INotifyPropertyChanged context, int inset = 0)
        {
            var type = context.GetType();
            var query = from p in type.GetProperties()
                        orderby p.Name
                        select p;
            foreach (var property in query)
            {
                if (IsSkipped(property.Name))
                {
                    continue;
                }
                if(typeof(INotifyPropertyChanged).IsAssignableFrom(property.PropertyType))
                {
                    Write(inset, property.Name, string.Empty);
                    DisplayOptions(((INotifyPropertyChanged)property.GetValue(context)), inset + Level);
                }
                else
                {
                    Write(inset, property.Name, property.GetValue(context));
                }
            }
        }

        private bool IsSkipped(string name)
        {
            switch(name)
            {
                case "Events":
                case "IsNotifying":
                    return true;
                default:
                    return false;
            }
        }

        private void Write(int inset, string name, object value)
        {
            var orig = Console.ForegroundColor;

            Console.Write(string.Empty.PadLeft(inset));

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write(name.PadRight(ColumnWidth - inset));
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(value ?? "<null>");

            Console.ForegroundColor = orig;
        }

        public ExecutionContext<TElement> Context { get; }
    }
}
