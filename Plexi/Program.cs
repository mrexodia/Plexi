using System.Linq;

namespace Plexi
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var filters = new Processor[]
            {
                new Identity(),
                new Negative(),
                new Greenblind()
            };

            Processor filter = null;
            if (args.Length >= 1)
                filter = filters.First(name => string.Compare(args[0], name.ToString(), true) == 0);
            if (filter == null)
                filter = filters.Last();

            var input = Plexi.ReadBitmapFromConsole();
            var output = filter.Process(input);
            Plexi.WriteBitmapToConsole(output);
        }
    }
}