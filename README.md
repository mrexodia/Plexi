# Plexi

Simple image processing framework. Tested on Windows (Visual Studio) and OSX (Xamarin Studio).

- Plexi: Console application with pipes.
- INFOIBX: GUI application.

## Documentation

The framework ([Plexi.cs](https://github.com/mrexodia/Plexi/blob/master/Plexi/Plexi.cs)) allows you to easily define image [processors](https://github.com/mrexodia/Plexi/blob/master/Plexi/Plexi.cs#L30) that can be applied to a [System.Drawing.Bitmap](https://msdn.microsoft.com/en-us/library/system.drawing.bitmap(v=vs.110).aspx) class. There are currently two methods in the `Processor` class that you can override: `Transform` and `Process`.

## Example processors

The `Transform` method will be called for every color in your input image, the `Negative` processor will return the negative for every color in your image and after running this processor you will have the negative image.

```c#
public class Negative : Processor
{
    public override Color Transform(Color c)
    {
        return Color.FromArgb(255 - c.R, 255 - c.G, 255 - c.B);
    }
}
```

The `RotateRight` method will be called with the image as a two-dimensional array (width * height) of colors. This processor will rotate the input image 90 degrees to the right.

```c#
public class RotateRight : Processor
{
    public override Color[,] Process(Color[,] image)
    {
        int w = image.GetLength(0), h = image.GetLength(1);
        var newImage = new Color[h, w];
        for (var x = 0; x < w; x++)
            for (var y = 0; y < h; y++)
                newImage[y, x] = image[x, h - y - 1];
        return newImage;
    }
}
```

## INFOIBV (GUI)

Add your processors to the pipeline by adding your own processors in  [Form1.cs line 66](https://github.com/mrexodia/Plexi/blob/master/INFOIBV/Form1.cs#L66).

## Plexi (command line)

The Plexi project ([Program.cs](https://github.com/mrexodia/Plexi/blob/master/Plexi/Program.cs)) is a simple command-line interface that works with pipes (for Linux/OSX users). The following command will execite a pipeline of the `Negative` and `Rotate` processors (defined in [Program.cs line 57](https://github.com/mrexodia/Plexi/blob/master/Plexi/Program.cs#L57)) on `images/lena_color.jpg`:

```bash
mono bin/Debug/Plexi.exe Negative,Rotate < images/lena_color.jpg > output.png
```

The [test](https://github.com/mrexodia/Plexi/blob/master/Plexi/test) script for OSX allows you to quickly execute a pipeline on an image and view the results (this does the same as the command above):

```bash
./test images/lena_color.jpg Negative,Rotate
```
