using System.Linq;
using System.Drawing;

namespace Plexi
{
    public class Negative : Processor
    {
        public override Color Transform(Color c)
        {
            return Color.FromArgb(255 - c.R, 255 - c.G, 255 - c.B);
        }
    }

    public class Identity : Processor
    {
        public override Color Transform(Color c)
        {
            return c;
        }
    }

    public class Greenblind : Processor
    {
        public override Color Transform(Color c)
        {
            return Color.FromArgb(c.R, 0, c.B);
        }
    }

    public class Shift : Processor
    {
        public override Color Transform(Color c)
        {
            return Color.FromArgb(c.B, c.R, c.G);
        }
    }

    public class Rotate : Processor
    {
        public override Color[,] Process(Color[,] image)
        {
            int w = image.GetLength(0), h = image.GetLength(1);
            var newImage = new Color[h, w];
            for (var x = 0; x < w; x++)
                for (var y = 0; y < h; y++)
                    newImage[y, x] = image[x, h-y-1];
            return newImage;
        }
    }

    public static class Program
    {
        public static void Main(string[] args)
        {
            //Available Processor instances.
            var processors = new Processor[]
            {
                new Identity(),
                new Negative(),
                new Greenblind(),
                new Shift(),
                new Rotate()
            };

            //Get the Processor to use from the command line arguments or take the last one.
            Processor processor = null;
            if (args.Length >= 1)
                processor = processors.First(name => string.Compare(args[0], name.ToString(), true) == 0);
            if (processor == null)
                processor = processors.Last();

            //Read, process and write the image.
            var input = Plexi.ReadBitmapFromConsole();
            var output = processor.Process(input);
            Plexi.WriteBitmapToConsole(output);
        }
    }
}