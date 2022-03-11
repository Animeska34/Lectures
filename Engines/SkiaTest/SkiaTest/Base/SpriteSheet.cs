using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiaTest.Base
{
    internal class SpriteSheet
    {
        public SKImage Image { get; private set; }
        public Int32 Size { get; private set; }

        public SKImage Get(SKPoint point)
        {
            SKPoint pos = new SKPoint(point.X * Size, point.Y * Size);
            return DirectGet(pos);

        }
        private SKImage DirectGet(SKPoint pos)
        {
            if (Image.Width >= pos.X + Size && Image.Height >= pos.Y + Size)
            {
                SKRectI limit = new((Int32)pos.X, (Int32)pos.Y, (Int32) pos.X + Size, (Int32)pos.Y + Size);
                return Image.Subset(limit);
            }
            else
            {
                return null;
            }
        }
        public SKImage Get(UInt32 id)
        {
            SKPoint pos = new SKPoint(0,0);
            pos.X = (Int32)id * Size;
            if(pos.X > Image.Width)
            {
                pos.Y = (Int32) (pos.X / Image.Width) * Size;
            }
            return DirectGet(pos);
        }
        public SpriteSheet(SKImage image, Int32 size = 64)
        {
            Image = image;
            Size = size;
        }
        public SpriteSheet(byte[] data, Int32 size = 64) : this(SKImage.FromEncodedData(data), size) { }
        public SpriteSheet(String relPath, Int32 size = 64) : this(SKImage.FromEncodedData(AppDomain.CurrentDomain.BaseDirectory + relPath), size) { }

    }
}
