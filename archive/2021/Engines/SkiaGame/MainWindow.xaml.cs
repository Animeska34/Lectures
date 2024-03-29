﻿using System;
using Windows.System;
using Microsoft.UI.Xaml;

using SS2DE;
using SS2DE.Assets;
using SS2DE.Components;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SkiaGame
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        class PlayerController : Component
        {
            SpriteSheetAnimation ani;
            Single speed = 75;
            protected override void Setup()
            {
                AddComponent<SpriteRenderer>();
                ani = Asset.Load<SpriteSheetAnimation>("C3ZwL.png");
                AddComponent<Animator>().Animation = ani;
                Input.map.Add("up", VirtualKey.W);
                Input.map.Add("down", VirtualKey.S);
                Input.map.Add("left", VirtualKey.A);
                Input.map.Add("right", VirtualKey.D);
            }
            protected override void Update()
            {
                ani.Playing = false;
                if (Input.GetButton("left"))
                {
                    transform.position.x -= speed * Time.deltaTime;
                    ani.SetAnimation(1);
                    ani.Playing = true;
                }
                if (Input.GetButton("right"))
                {
                    transform.position.x += speed * Time.deltaTime;
                    ani.SetAnimation(3);
                    ani.Playing = true;
                }
                if (Input.GetButton("up"))
                {
                    transform.position.y += speed * Time.deltaTime;
                    if (!ani.Playing)
                    {
                        ani.SetAnimation(0);
                        ani.Playing = true;
                    }
                }
                if (Input.GetButton("down"))
                {
                    transform.position.y -= speed * Time.deltaTime;
                    if (!ani.Playing)
                    {
                        ani.SetAnimation(2);
                        ani.Playing = true;
                    }
                }
                if (!ani.Playing)
                    ani.SetFrame(0);
            }
        }
        public MainWindow()
        {
            this.InitializeComponent();
            Core.Start(xamlCanvas);
            Scene scene = new Scene();
            scene.root.Instantiate().AddComponent<Camera>().SetMain();
            var player = scene.root.Instantiate();
            player.AddComponent<PlayerController>();
            player.transform.scale = new Vector2(2f, 2f);
            player.transform.position = new Vector2(500, -500);
            GameObject name = player.Instantiate();
            name.AddComponent<TextRenderer>().Text = "Player";
            name.transform.position = new Vector2(15, 0);
            scene.Start();
        }
    }
}
