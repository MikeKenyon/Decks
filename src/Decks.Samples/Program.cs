using System;

namespace Decks.Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new ExecutionContext();
            var root = new InteractiveCommand(context);

            root.Welcome();

            while(!context.Done)
            {
                root.ProcessCommand();
            }
        }
    }
}
