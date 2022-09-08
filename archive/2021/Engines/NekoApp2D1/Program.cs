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
        static Scene scene;
        public override void OnLoad()
        {
            //Change window title
            Neko.title = AppDomain.CurrentDomain.FriendlyName;

            Input.map.Add("up", VirtualKey.W);
            Input.map.Add("left", VirtualKey.A);
            Input.map.Add("right", VirtualKey.D);

            Start();
        }

        public static void Start()
        {
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
            InstantiateFinishPlatform(new Vector3(15, -5, 1));
            InstantiateE1(new Vector3(10, -2, 1));
            scene.Start();
        }

        static void InstantiateE1(Vector3 pos)
        {
            var tmp = scene.root.Instantiate();
            tmp.transform.position = pos;
            tmp.AddComponent<E1Controller>();
        }

        static void InstantiatePlatform(Vector3 pos)
        {
            var tmp = scene.root.Instantiate();
            tmp.transform.position = pos;
            tmp.AddComponent<SpriteRenderer>().sprite = new Sprite("Platform.png");
            tmp.AddComponent<BoxCollder2D>();
            tmp.tag = "ground";
        }
        static void InstantiateCyclePlatform(Vector3 pos)
        {
            var tmp = scene.root.Instantiate();
            tmp.transform.position = pos;
            tmp.AddComponent<SpriteRenderer>().sprite = new Sprite("CyclePlatform.png");
            tmp.AddComponent<CycleCollder2D>().radius = 1.1f;
            tmp.tag = "ground";
        }

        static void InstantiateFinishPlatform(Vector3 pos)
        {
            var tmp = scene.root.Instantiate();
            tmp.transform.position = pos;
            tmp.AddComponent<SpriteRenderer>().sprite = new Sprite("Platform.png");
            tmp.AddComponent<BoxCollder2D>();
            tmp.AddComponent<Finish>();
            tmp.tag = "ground";
        }

    }
}