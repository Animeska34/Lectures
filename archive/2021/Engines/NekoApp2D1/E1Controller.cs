using NekoEngine;
using NekoEngine.Assets;
using NekoEngine.Components;
using NekoEngine.Render.OpenGL;
using System.Numerics;

namespace NekoApp2D1
{
    public class E1Controller : Component
    {
        float rotationSpeed = 100f;

        GameObject parcicles;
        private void AddParcicle(Vector3 v)
        {
            var tmp = parcicles.Instantiate();
            tmp.transform.position = transform.position + v + new Vector3(0,0,-2);
            tmp.AddComponent<SpriteRenderer>().sprite = new Sprite(new Texture2D("ray.png", Texture2D.MagFilter.Nearest));
            tmp.tag = "parcicle";
            var col = tmp.AddComponent<CycleCollder2D>();
            col.radius = 0.5f;
            col.isTrigger = true;
        }
        //Calls once, when component created
        protected override void Setup()
        {
            AddComponent<SpriteRenderer>().sprite = new Sprite(new Texture2D("E1.png", Texture2D.MagFilter.Nearest));
            parcicles = gameObject.Instantiate();
            AddParcicle(new Vector3(1.5f, 0, 0));
            AddParcicle(new Vector3(-1.5f, 0, 0));
            AddParcicle(new Vector3(0, 1.5f, 0));
            AddParcicle(new Vector3(0, -1.5f, 0));
            gameObject.tag = "enemy";
            AddComponent<CycleCollder2D>().radius = 1f;
            //transform.GetChilds()[0].GetChilds()[0].gameObject.AddComponent<GameObjectDebug>();
        }
        //Calls on every frame update
        protected override void Update()
        {
            var rot = new Vector3(0, 0, rotationSpeed * Time.deltaTime);

            parcicles.transform.localEulerAngles += rot;
            foreach (var item in parcicles.transform.GetChilds())
            {
                item.localEulerAngles += rot * 10;
            }
        }
    }
}