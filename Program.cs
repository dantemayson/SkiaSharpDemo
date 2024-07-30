using SkiaSharp;

SKBitmap originalBitmap = SKBitmap.Decode("welcome.EN.png");

// Create an Alpha8 bitmap as an alpha mask
SKBitmap alphaMask = new SKBitmap(originalBitmap.Width, originalBitmap.Height, SKColorType.Alpha8, SKAlphaType.Premul);

// Set alpha values for each pixel (example: a gradient)
for (int y = 0; y < alphaMask.Height; y++)
{
    for (int x = 0; x < alphaMask.Width; x++)
    {
        byte alphaValue = (byte)(x * 255 / alphaMask.Width);
        alphaMask.SetPixel(x, y, new SKColor(0, 0, 0, alphaValue));
    }
}

// Apply the alpha mask to the original image
for (int y = 0; y < originalBitmap.Height; y++)
{
    for (int x = 0; x < originalBitmap.Width; x++)
    {
        // Get the alpha value from the alpha mask
        byte alphaValue = alphaMask.GetPixel(x, y).Alpha;

        // Apply the alpha value to the original image
        SKColor originalColor = originalBitmap.GetPixel(x, y);
        originalBitmap.SetPixel(x, y, new SKColor(originalColor.Red, originalColor.Green, originalColor.Blue, alphaValue));
    }
}

// Save the result to a new file
using (var stream = System.IO.File.OpenWrite("output_image.png"))
{
    originalBitmap.Encode(SKEncodedImageFormat.Png, 100).SaveTo(stream);
}

// Indicate that the process is complete
Console.WriteLine("Image with alpha mask applied saved to output_image.png");
