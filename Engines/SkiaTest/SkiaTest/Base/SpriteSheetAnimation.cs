using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiaTest.Base
{
    internal class SpriteSheetAnimation
    {
        public SKImage CurrentFrame { get; private set; }
        public SpriteSheet SpriteSheet { get; private set; }
        public UInt32 CurrentAnimation { get; set; } = 0;
        public UInt32 Framerate { get; set; } = 8;
        public bool IsPlaying { get; set; } = true;
        private UInt32 frameID = 0;
        private Single timeFrom = 0;
        public void Update()
        {
            if (IsPlaying)
            {
                if (timeFrom > 1f / Framerate)
                {
                    if (SpriteSheet.Image.Width <= frameID * SpriteSheet.Size + SpriteSheet.Size)
                        frameID = 0;
                    NextFrame();
                    timeFrom = 0;
                }
                else
                    timeFrom += App.deltaTime;
            }
        }
        public void NextFrame()
        {
            var tmp = SpriteSheet.Get(new SKPoint(frameID++, CurrentAnimation));
            if (tmp != null)
                CurrentFrame = tmp;
            else
                frameID = 0;
        }
        public void Render(SKPoint position)
        {
            App.canvas.DrawImage(CurrentFrame, position);
        }
        public SpriteSheetAnimation(SpriteSheet spriteSheet, UInt32 frameRate = 8)
        {
            SpriteSheet = spriteSheet;
            Framerate = frameRate;
            CurrentAnimation = 0;
            CurrentFrame = SpriteSheet.Get(0);
        }
        public SpriteSheetAnimation(SKImage image, Int32 size = 64, UInt32 frameRate = 8) : this(new SpriteSheet(image, size), frameRate) { }
        public SpriteSheetAnimation(string relPath, Int32 size = 64, UInt32 frameRate = 8) : this(new SpriteSheet(relPath, size), frameRate) { }
    }
}
