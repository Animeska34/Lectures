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
        Scene scene;
        public override void OnLoad()
        {
            //Change window title
            Neko.title = AppDomain.CurrentDomain.FriendlyName;
            //Creating scene and attaching camera
            scene = new Scene();
            var c = scene.root.Instantiate(new Vector3(0, 0, -10)).AddComponent<Camera>();
            c.SetMain();
            c.orthographic = true;
            //Write your logic here
            var player = scene.root.Instantiate();
            player.AddComponent<PlayerController>();
            player.transform.localScale = new Vector3(2f, 2f, 0);

            InstantiateCyclePlatform(new Vector3(0, -3, 1));
            InstantiatePlatform(new Vector3(5, -4, 1));
            InstantiatePlatform(new Vector3(10, -3, 1));
            scene.Start();

        }

        void InstantiatePlatform(Vector3 pos)
        {
            var tmp = scene.root.Instantiate();
            tmp.transform.position = pos;
            tmp.AddComponent<SpriteRenderer>().sprite = new Sprite("Platform.png");
            tmp.AddComponent<BoxCollder2D>();
            tmp.tag = "ground";
        }
        void InstantiateCyclePlatform(Vector3 pos)
        {
            var tmp = scene.root.Instantiate();
            tmp.transform.position = pos;
            tmp.AddComponent<SpriteRenderer>().sprite = new Sprite("CyclePlatform.png");
            tmp.AddComponent<CycleCollder2D>().radius = 1.1f;
            tmp.tag = "ground";
        }
    }
}