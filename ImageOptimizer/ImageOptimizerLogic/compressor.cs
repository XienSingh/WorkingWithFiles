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
	public class Compressor
	{
    public static void CompressImage(string SoucePath, int quality)
    {

      IEnumerable<string> allIamges = Directory.GetFiles(SoucePath, "*.*", SearchOption.AllDirectories);
      int count = 0;
      double origSize = 0;
      double SavedSize = 0;
      foreach(string item in allIamges)
      {
        var FileName = Path.GetFileName(item);
        var extension = Path.GetExtension(FileName);
        var DestPath = item;
        FileStream fs = new FileStream(item, FileMode.Open);
        Image imgPhoto = Image.FromStream(fs);
        Console.WriteLine("------------------------------------------------------------------------------");
        Console.ForegroundColor = ConsoleColor.Cyan;
        double originalImageSize = fs.Length/1000;
        Console.WriteLine($"Image to be optimised: {FileName} original size = {originalImageSize} Kb");
        origSize += originalImageSize;
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
          double optimisedSize = (new FileInfo(DestPath)).Length / 1000;
          Console.WriteLine($"Image optimised: {FileName} original size = {optimisedSize} Kb");
          Console.WriteLine("------------------------------------------------------------------------------");
          SavedSize += optimisedSize;
          Console.ResetColor();

        }
      }
    
    Console.WriteLine("Done");
      Console.WriteLine($"Total number of images processed : {count}");
      Console.WriteLine($"Original Folder size {origSize/1000} Mb");
      Console.WriteLine($"New Folder size {SavedSize/1000} Mb");
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
