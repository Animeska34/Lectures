using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Windows.Input;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using SkiaSharp.Views.Windows;
using SkiaSharp;
using System.Reflection;
using System.Diagnostics;
using Windows.System;
using Windows.UI.Core;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SkiaTest
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {

        SKCanvas canvas;
        SKPoint max;

        SKImage image;
        //Base.SpriteSheet spriteSheet;
        Base.SpriteSheetAnimation ani;

        SKPoint playerPosition = new(0,0);
        Single playerStep = 64;

        Single carScale = 1f;
        Single carSpeed = 5f;
        Single carXSpeed = -0.65f;
        Int16 carDirection = 0;
        Single carScaleSpeed = 0.0045f;
        SKPoint carPosition;
        SKPoint carMaxPosition;
        public MainWindow()
        {
            this.InitializeComponent();
            CompositionTarget.Rendering += Update;
            xamlCanvas.IsTabStop = true;
            xamlCanvas.KeyDown += OnKeyDown;
            xamlCanvas.KeyUp += OnKeyUp;
            

            image = SKImage.FromEncodedData(AppDomain.CurrentDomain.BaseDirectory + "Images\\sprite.png");
            ani = new("Images\\sprite.png", 64,4);
        }

        class DebugText
        {
            public String text;
            public float x;
            public float y;
            public DebugText(String _text, float _x, float _y)
            {
                text = _text;
                x = _x; y = _y;
            }
            public DebugText(String _text, SKPoint point)
            {
                text = _text;
                x = point.X; y = point.Y;
            }
        }
        List<DebugText> debugInfo = new List<DebugText>();
        void DebugTextAdd(String _text, float _x, float _y)
        {
            debugInfo.Add(new(_text, _x, _y));
        }
        void DebugTextAdd(String _text, SKPoint point)
        {
            debugInfo.Add(new(_text, point));
        }
        void RenDebugText()
        {
            SKPaint debugText = new();
            debugText.Color = SKColors.Red;
            debugText.TextSize = 32;
            debugText.Typeface = SKTypeface.FromFamilyName("UD Digi Kyokasho NP-R");
            canvas.DrawText("(" + max.X + ", " + max.Y + ")", 50, 50, debugText);
            foreach (var item in debugInfo)
            {
                canvas.DrawText(item.text, item.x, item.y, debugText);
            }
        }
        void RenGrid(Int32 cellSz = 25)
        {
            SKPaint debugGrid = new();
            SKColor col = new SKColor(0, 0, 0, 25);
            debugGrid.Color = col;

            for (int x = 0; x < max.X; x += cellSz)
            {
                SKPoint start = new SKPoint(x, 0);
                SKPoint end = new SKPoint(x, max.Y);
                canvas.DrawLine(start, end, debugGrid);
            }

            for (int y = 0; y < max.Y; y += cellSz)
            {
                SKPoint start = new SKPoint(0, y);
                SKPoint end = new SKPoint(max.X, y);
                canvas.DrawLine(start, end, debugGrid);
            }
        }
        private void AddPoly(SKPoint[] points, SKPath path, Boolean printPointNumber)
        {
            path.AddPoly(points);
            if (printPointNumber)
            {
                for (int i = 0; i < points.Length; i++)
                {
                    debugInfo.Add(new DebugText(i.ToString(), points[i]));
                }
            }
        }

        private void Update(object sender, object args)
        {
            RenderingEventArgs rae = args as RenderingEventArgs;
            App.deltaTime = rae.RenderingTime.Milliseconds / 1000f;
            xamlCanvas.Invalidate();
        }
        private void SKXamlCanvas_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {

            debugInfo = new();
            canvas = e.Surface.Canvas;
            App.canvas = canvas;
            //max = new(canvas.LocalClipBounds.Width, canvas.LocalClipBounds.Height);
            max = new(2860, 2017);
            SKPoint center = new(canvas.LocalClipBounds.Width / 2, canvas.LocalClipBounds.Height / 2);
            float scale = 1 / (max.Y + max.X);
            canvas.Clear(SKColors.DarkGray);
            SKPaint black = new();
            black.Color = SKColors.Black;
            SKPaint red = new();
            red.Color = SKColors.Red;
            red.TextSize = (max.Y + max.X) / 100;
            red.Typeface = SKTypeface.FromFamilyName("UD Digi Kyokasho NP-R");
            /*
            SKPoint p1 = new(max.X * 1 / 3, max.Y * 1 / 3);
            SKPoint p2 = new(max.X * 2 / 3, max.Y * 2 / 3);
            canvas.DrawRect(max.X * 1 / 3, max.Y * 1 / 3, p1.X, p1.Y, black);
            canvas.DrawLine(p1, p2, red);

            p1 = new(max.X * 2 / 3, max.Y * 1 / 3);
            p2 = new(max.X * 1 / 3, max.Y * 2 / 3);
            canvas.DrawLine(p1, p2, red);

            p1 = new(max.X/2, max.Y * 1 / 3);
            p2 = new(max.X/2, max.Y * 2 / 3);
            canvas.DrawLine(p1, p2, red);

            p1 = new(max.X * 2 / 3, max.Y / 2);
            p2 = new(max.X * 1 / 3, max.Y / 2);
            canvas.DrawLine(p1, p2, red);
            canvas.DrawText("TestImage?", max.X / 2, max.Y / 2, red);
            */
            SKPaint wall = new();
            wall.Color = SKColors.LightGray;
            SKPaint wallShadow = new();
            wallShadow.Color = SKColors.Gray;
            SKPaint outline = new();
            outline.Color = SKColors.Black;
            outline.Style = SKPaintStyle.Stroke;
            SKPaint window = new();
            window.Color = SKColor.Parse("#AEC5C5");
            SKPaint windowShadow = new();
            windowShadow.Color = SKColor.Parse("#9db9b9");
            SKPaint sky = new();
            sky.Shader = SKShader.CreateLinearGradient(new SKPoint(0, 0), new SKPoint(0, max.Y), new[] { SKColor.Parse("#2198FE"), SKColor.Parse("#9BD1FF") }, SKShaderTileMode.Decal);
            SKPaint road = new();
            road.Color = SKColor.Parse("#47494A");
            SKPaint roadLine = new();
            roadLine.Color = SKColor.Parse("#FFFFFF");
            SKPaint shadow = new();
            SKColor colorShadow = new SKColor(0, 0, 0, 125);
            shadow.Color = colorShadow;




            //H1
            SKPoint crrH1 = new();
            crrH1.X = center.X - 200;
            crrH1.Y = center.Y + 100;
            SKPath path;

            path = new SKPath();
            AddPoly(
new SKPoint[] {
                    new(crrH1.X + 250, crrH1.Y), //LeftUp
                    new(crrH1.X + 000, max.Y),//LeftBottom
                    
                    new(crrH1.X + 550, max.Y), //RightBottom
}, path, false);
            canvas.DrawPath(path, road);
            canvas.DrawPath(path, outline);


            path = new SKPath();
            AddPoly(
new SKPoint[] {
                    new(crrH1.X + 250, crrH1.Y), //LeftUp
                    new(crrH1.X + 270, max.Y),//LeftBottom
                    
                    new(crrH1.X + 280, max.Y), //RightBottom
}, path, false);
            canvas.DrawPath(path, roadLine);

            //RenCar
            carMaxPosition = new SKPoint(0, crrH1.Y);
            RenCar(crrH1);
            //canvas.DrawPath(path, outline);
            //H2
            path = new SKPath();
            AddPoly(
                new SKPoint[] {
                    new(crrH1.X + 150, crrH1.Y + 125),
                    new(crrH1.X - 150, crrH1.Y + 50),
                    new(crrH1.X + 250, crrH1.Y + 25),
                    new(crrH1.X + 290, crrH1.Y + 40),
                    new(crrH1.X + 320, crrH1.Y + 90),

            }, path, true);

            canvas.DrawPath(path, shadow);

            canvas.DrawRect(0, crrH1.Y, max.X, -max.Y, sky);
            canvas.DrawRect(crrH1.X - 150, crrH1.Y - 125, 300, 250, wall);
            canvas.DrawRect(crrH1.X - 150, crrH1.Y - 125, 300, 250, outline);
            path = new SKPath();
            AddPoly(
                new SKPoint[] {
                    new(crrH1.X + 150, crrH1.Y + 125),
                    new(crrH1.X + 150, crrH1.Y - 125),

                    new(crrH1.X + 200, crrH1.Y - 50),
                    new(crrH1.X + 200, crrH1.Y + 50)
            }, path, false);

            canvas.DrawPath(path, wallShadow);
            canvas.DrawPath(path, outline);

            for (int y = -100; y < 100; y += 55)
            {
                for (int x = -117; x < 120; x += 65)
                {
                    canvas.DrawRect(crrH1.X + x, crrH1.Y + y, 40, 30, window);
                    canvas.DrawRect(crrH1.X + x, crrH1.Y + y, 40, 30, outline);
                    canvas.DrawLine(new SKPoint(crrH1.X + x + 25, crrH1.Y + y + 20), new SKPoint(crrH1.X + x + 10, crrH1.Y + y + 5), roadLine);
                    canvas.DrawLine(new SKPoint(crrH1.X + x + 20, crrH1.Y + y + 25), new SKPoint(crrH1.X + x + 10, crrH1.Y + y + 15), roadLine);
                }
            }
            path = new SKPath();
            float xl = 0f;
            float xr = 0f;
            float yu = 0f;
            float yb = 0f;
            float y1f = 0f;
            float y2f = 0f;
            float addiction = 2f;
            for (int i = 0; i < 4; i++)
            {

                AddPoly(
                    new SKPoint[] {
                    new(crrH1.X + 160 + xl, crrH1.Y - 60 + yb + y1f * addiction),
                    new(crrH1.X + 160 + xl, crrH1.Y - 90 + yu + y1f * addiction),

                    new(crrH1.X + 165 + xr, crrH1.Y - 80 + yu + y1f * addiction),
                    new(crrH1.X + 165 + xr, crrH1.Y - 57 + yb + y1f * addiction),

                }, path, true);
                AddPoly(
        new SKPoint[] {
                    new(crrH1.X + 160 + xl, crrH1.Y - 13 + yb + y1f),
                    new(crrH1.X + 160 + xl, crrH1.Y - 45 + yu + y1f),

                    new(crrH1.X + 165 + xr, crrH1.Y - 37 + yu + y1f),
                    new(crrH1.X + 165 + xr, crrH1.Y - 13 + yb + y1f),

    }, path, false);

                AddPoly(
    new SKPoint[] {
                    new(crrH1.X + 160 + xl, crrH1.Y + 9 + yu + y2f), //LeftUp
                    new(crrH1.X + 160 + xl, crrH1.Y + 40 + yb + y2f),//LeftBottom

                    new(crrH1.X + 165 + xr, crrH1.Y + 34 + yb + y2f), //RightBottom
                    new(crrH1.X + 165 + xr, crrH1.Y + 10 + yu + y2f), //RightUp
    }, path, false);
                AddPoly(
    new SKPoint[] {
                    new(crrH1.X + 160 + xl, crrH1.Y + 61 + yu + y2f * addiction), //LeftUp
                    new(crrH1.X + 160 + xl, crrH1.Y + 90 + yb + y2f * addiction),//LeftBottom

                    new(crrH1.X + 165 + xr, crrH1.Y + 81 + yb + y2f * addiction), //RightBottom
                    new(crrH1.X + 165 + xr, crrH1.Y + 57 + yu + y2f * addiction), //RightUp
    }, path, false);
                xl += 10;
                xr += 8;
                yu += 3;
                yb += -5;
                y1f += 7;
                y2f += -5;
            }

            //Column 2
            canvas.DrawPath(path, windowShadow);
            canvas.DrawPath(path, outline);

            //SKRectI limit = new(0,64,64,128);
            //SKImage cropped = image.Subset(limit);
            //canvas.DrawImage(cropped, 100,100);
            ani.Render(playerPosition);
            ani.Update();

            //RenDebugText();
            //RenGrid();
        }

        private void RenCar(SKPoint imageCenter)
        {
            if (carPosition.Y == 0 && carPosition.X == 0)
                carPosition = new SKPoint(400, max.Y - 50);
            SKPaint mainColor = new SKPaint();
            mainColor.Color = SKColors.Blue;
            SKPaint tiersColor = new SKPaint();
            tiersColor.Color = SKColors.Black;

            canvas.DrawRect(imageCenter.X + carPosition.X - 75 * carScale, carPosition.Y, 150 * carScale, -50 * carScale, mainColor);
            canvas.DrawRect(imageCenter.X + carPosition.X - 70 * carScale, carPosition.Y, 20 * carScale, 25 * carScale, tiersColor);
            canvas.DrawRect(imageCenter.X + carPosition.X + 50 * carScale, carPosition.Y, 20 * carScale, 25 * carScale, tiersColor);

            carPosition.Y -= carSpeed * carDirection;
            carPosition.X += carXSpeed * carDirection;
            carScale -= carScaleSpeed * carDirection;

            if (carPosition.Y < carMaxPosition.Y)
            {
                carPosition = new SKPoint(400, carMaxPosition.Y);
                carScale = 1f;
            }
        }

        void OnKeyDown(Object sender, KeyRoutedEventArgs e)
        {
            switch (e.Key)
            {
                case VirtualKey.Up:
                case VirtualKey.W:
                    playerPosition.Y -= playerStep;
                    ani.CurrentAnimation = 0;
                    ani.IsPlaying = true;
                    break;
                case VirtualKey.Down:
                case VirtualKey.S:
                    playerPosition.Y += playerStep;
                    ani.CurrentAnimation = 2;
                    ani.IsPlaying = true;
                    break;
                case VirtualKey.Left:
                case VirtualKey.A:
                    playerPosition.X -= playerStep;
                    ani.CurrentAnimation = 1;
                    ani.IsPlaying = true;
                    break;
                case VirtualKey.Right:
                case VirtualKey.D:
                    playerPosition.X += playerStep;
                    ani.CurrentAnimation = 3;
                    ani.IsPlaying = true;
                    break;
            }
        }

        void OnKeyUp(Object sender, KeyRoutedEventArgs e)
        {
            switch (e.Key)
            {
                case VirtualKey.Up:
                case VirtualKey.W:
                case VirtualKey.Down:
                case VirtualKey.S:
                case VirtualKey.Left:
                case VirtualKey.A:
                case VirtualKey.Right:
                case VirtualKey.D:
                    ani.IsPlaying = false;
                    break;
            }
        }

        /*
        private void myButton_Click(object sender, RoutedEventArgs e)
        {
            myButton.Content = "Clicked";
        }
        */
    }
}
