using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageOptimizerLogic
{
	public class compressor
	{
    public static void CompressImage(string SoucePath, int quality)
    {
      int count = 0;
      List<string> Directories = Directory.GetDirectories(SoucePath).ToList();
      List<List<string>> SecondLvlDir = new List<List<string>>();
      List<List<string>> images = new List<List<string>>();
      foreach (string path in Directories)
      {
        SecondLvlDir.Add(Directory.GetDirectories(path).ToList());
      }
      foreach (List<string> paths in SecondLvlDir)
			{
				foreach (string item in paths) { 
        images.Add(Directory.EnumerateFiles(item).ToList());
        }
      }  
      foreach(List<string> items in images)
      {
        foreach(string imgPath in items) 
        { 
        var FileName = Path.GetFileName(imgPath);
        var extension = Path.GetExtension(FileName);
        var DestPath = imgPath;
        FileStream fs = new FileStream(imgPath, FileMode.Open);
        Image imgPhoto = Image.FromStream(fs);
          Console.WriteLine("------------------------------------------------------------------------------");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"Image to be optimised: {FileName} original size = {fs.Length/1000} Kb");

          fs.Close();
        using (Bitmap bmp1 = new Bitmap(imgPhoto))
        {
            ImageFormat imageFormat = null;
            switch (extension)
            {
              case "jpeg":
              case "jpg":
                imageFormat = ImageFormat.Jpeg;
                break;
              case "png":
                imageFormat = ImageFormat.Png;
                break;
              case "bmp":
                imageFormat = ImageFormat.Bmp;
                break;
              case "gif":
                imageFormat = ImageFormat.Gif;
                break;
               default:
                imageFormat = ImageFormat.Jpeg;
                break;
						}
            Console.ForegroundColor = ConsoleColor.Magenta;
          Console.WriteLine($"Image Name: {FileName}");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Processing");
          ImageCodecInfo jpgEncoder = GetEncoder(imageFormat);
          System.Drawing.Imaging.Encoder QualityEncoder = System.Drawing.Imaging.Encoder.Quality;
          EncoderParameters myEncoderParameters = new EncoderParameters(1);
          EncoderParameter myEncoderParameter = new EncoderParameter(QualityEncoder, quality);
          myEncoderParameters.Param[0] = myEncoderParameter;
          bmp1.Save(DestPath, jpgEncoder, myEncoderParameters);
            count++;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Image optimised: {FileName} original size = {(new FileInfo(DestPath)).Length/1000} Kb");
            Console.WriteLine("------------------------------------------------------------------------------");

            Console.ResetColor();

					}
        }
			}
      Console.WriteLine("Done");
      Console.WriteLine($"Total number of images processed : {count}");
      Console.WriteLine(DateTime.Now);
    }
    private static ImageCodecInfo GetEncoder(ImageFormat format)
    {
      ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
      foreach (ImageCodecInfo codec in codecs)
      {
        if (codec.FormatID == format.Guid)
        {
          return codec;
        }
      }
      return null;
    }
  }

}
