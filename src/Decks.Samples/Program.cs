using System;

namespace Decks.Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            DoLoop<Elements.SpaceSector>();
        }

        private static void DoLoop<TElement>() where TElement : class
        {
            var context = new ExecutionContext<TElement>();
            var root = new InteractiveCommand<TElement>(context);

            root.Welcome();

            while (!context.Done)
            {
                root.ProcessCommand();
                root = new InteractiveCommand<TElement>(context);
            }
        }
    }
}
