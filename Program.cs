using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace Image_Bury
{
    enum RGB { 
        R = 0,
        G = 1,
        B = 2
    }
    class Program
    {
        static void Main(string[] args)
        {
            var fileInfo = new FileInfo(Path.GetFullPath(args[0]));
            var imageInfo = new FileInfo(Path.GetFullPath(args[1]));
            int bitsPerPixel = 1;
            if (args.Length > 1)
                if (int.TryParse(args[2], out int parsedInt))
                    bitsPerPixel = parsedInt;
            

            if (!fileInfo.Exists || !imageInfo.Exists) 
                throw new Exception("One of the files does not exist!");

            var fileBitLen = fileInfo.Length * 8;
            var imageBitLen = fileInfo.Length * 8;


            using (FileStream imageStream = new FileStream(imageInfo.FullName, FileMode.Open, FileAccess.Read))
            using (BitStream fileStream = new BitStream(new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read)))
            using (var image = new Bitmap(imageStream))
            {
                RGB targetColour = RGB.R;
                for (int y = 0; y < image.Height; y++) {
                    for (int x = 0; x < image.Width; x++) {
                        var px = image.GetPixel(x, y);
                        px.
                        fileStream.ReadByte()
                    }
                }
            }
        }

        static Color BuryDataInColor(Color color, bool[] data, RGB initialChannel = RGB.R)
        {
            RGB targetColour = initialChannel;
            Dictionary<RGB, int> channelPositions = new Dictionary<RGB, int>() {
                { RGB.R, 0 }, { RGB.G, 0 }, { RGB.B, 0 }
            };

            byte R = color.R, G = color.G, B = color.B;

            void assignBit(ref byte input, bool bit, int position)
            {
                if (bit)
                    input = (byte)(input | (1 << position));
                else 
                    input = (byte)(input & ~(1 << position));
            }

            for (int i = 0; i < data.Length; i++) {
                switch (targetColour)
                {
                    case RGB.R:
                        assignBit(ref R, data[i], channelPositions[targetColour]);
                        break;
                    case RGB.G:
                        break;
                    case RGB.B:
                        break;
                }

                targetColour = (RGB)(((int)targetColour + 1) % 3);
            }
        }
    }
}
