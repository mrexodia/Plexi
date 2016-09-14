using System;
using System.Drawing;
using System.Linq;

namespace Plexi
{
	abstract class Filter
	{
		public virtual Bitmap Process(Bitmap b)
		{
			var image = new Color[b.Width, b.Height];
			for (var x = 0; x < b.Width; x++)
				for (var y = 0; y < b.Height; y++)
					image[x, y] = b.GetPixel(x, y);
			Process(image);
			var result = new Bitmap(b.Width, b.Height);
			for (var x = 0; x < b.Width; x++)
				for (var y = 0; y < b.Height; y++)
					result.SetPixel(x, y, image[x, y]);
			return result;
		}

		public virtual void Process(Color[,] image)
		{
			for (var x = 0; x < image.GetLength(0); x++)
				for (var y = 0; y < image.GetLength(1); y++)
					image[x, y] = Transform(image[x, y]);
		}

		public virtual Color Transform(Color c)
		{
			return c;
		}

		public override string ToString()
		{
			return this.GetType().Name;
		}
	}

	class Negative : Filter
	{
		public override Color Transform(Color c)
		{
			return Color.FromArgb(255 - c.R, 255 - c.G, 255 - c.B); // Negative color
		}
	}

	class Original : Filter
	{
		public override Color Transform(Color c)
		{
			return c;
		}
	}

	class Colorblind : Filter
	{
		public override Color Transform(Color c)
		{
			return Color.FromArgb(c.R, 0, c.B);
		}
	}

	public static class Plexi
	{
		public static void Main(string[] args)
		{
			var filters = new Filter[]
			{
				new Original(),
				new Negative(),
				new Colorblind()
			};
			Filter filter = null;
			if (args.Length >= 1)
				filter = filters.First(name => string.Compare(args[0], name.ToString(), true) == 0);
			if (filter == null)
				filter = filters.Last();
			var input = new Bitmap(Console.OpenStandardInput());
			var output = filter.Process(input);
			output.Save(Console.OpenStandardOutput(), System.Drawing.Imaging.ImageFormat.Png);
		}
	}
}
