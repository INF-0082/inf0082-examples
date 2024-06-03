using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

class GrayScale
{
    static void Main(string[] args)
    {
        string[] imageFiles = Directory.GetFiles("./images", "*.jpg");
        foreach (string imagePath in imageFiles)
        {
            ProcessImage(imagePath);
        }
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
            string outputPath = Path.Combine("./output", Path.GetFileName(imagePath));
            bitmap.Save(outputPath, ImageFormat.Jpeg);
        }
    }
}