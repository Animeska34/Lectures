using NekoEngine;
using NekoEngine.Assets;
using NekoEngine.Components;
using NekoEngine.Extensions;
using System.Numerics;

namespace NekoApp2D1
{
    public class Program : NekoProgram
    {
        private static void Main()
        {
            //Starting NekoEngine
            new Program();
        }
        public override void OnLoad()
        {
            //Change window title
            Neko.title = AppDomain.CurrentDomain.FriendlyName;
            //Creating scene and attaching camera
            Scene scene = new Scene();
            var c = scene.root.Instantiate(new Vector3(0, 0, -10)).AddComponent<Camera>();
            c.SetMain();
            c.orthographic = true;
            //Write your logic here
            var player = scene.root.Instantiate();
            player.AddComponent<PlayerController>();
            player.transform.localScale = new Vector3(2f, 2f, 0);

            scene.Start();

        }
    }
}