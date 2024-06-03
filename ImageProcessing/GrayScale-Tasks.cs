using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

class GrayScaleTasks
{
    static async Task Main(string[] args)
    {
        string[] imageFiles = Directory.GetFiles("./images", "*.jpg");

        var tasks = imageFiles.Select(imagePath => Task.Run(() => ProcessImage(imagePath)));

        await Task.WhenAll(tasks);
    }

    static void ProcessImage(string imagePath)
    {
        using (Bitmap bitmap = new Bitmap(imagePath))
        {
            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    Color pixel = bitmap.GetPixel(x, y);
                    int grayScale = (int)((pixel.R * 0.3) + (pixel.G * 0.59) + (pixel.B * 0.11));
                    Color grayColor = Color.FromArgb(grayScale, grayScale, grayScale);
                    bitmap.SetPixel(x, y, grayColor);
                }
            }
            string outputPath = Path.Combine(
                "./output",
                Path.GetFileName(imagePath));
            bitmap.Save(outputPath, ImageFormat.Jpeg);
        }
    }
}