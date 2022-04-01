using NekoEngine;
using NekoEngine.Assets;
using NekoEngine.Components;
using NekoEngine.Render.OpenGL;
using System.Numerics;

namespace NekoApp2D1
{
    public class PlayerController : Component
    {
        SpriteSheetAnimation ani;
        Single speed = 1.5f;
        protected override void Setup()
        {
            AddComponent<SpriteRenderer>();
            ani = new SpriteSheetAnimation(new Texture2D("C3ZwL.png", Texture2D.MagFilter.Nearest));
            ani.framerate = 8;
            AddComponent<Animator>().animation = ani;
            Input.map.Add("up", VirtualKey.W);
            Input.map.Add("down", VirtualKey.S);
            Input.map.Add("left", VirtualKey.A);
            Input.map.Add("right", VirtualKey.D);
        }
        protected override void Update()
        {
            ani.playing = false;
            if (Input.GetButton("left"))
            {
                transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
                ani.SetAnimation(1);
                ani.playing = true;
            }
            if (Input.GetButton("right"))
            {
                transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
                ani.SetAnimation(3);
                ani.playing = true;
            }
            if (Input.GetButton("up"))
            {
                transform.position += new Vector3(0, speed * Time.deltaTime, 0);
                if (!ani.playing)
                {
                    ani.SetAnimation(0);
                    ani.playing = true;
                }
            }
            if (Input.GetButton("down"))
            {
                transform.position -= new Vector3(0, speed * Time.deltaTime, 0);
                if (!ani.playing)
                {
                    ani.SetAnimation(2);
                    ani.playing = true;
                }
            }
            if (!ani.playing)
                ani.SetFrame(0);
            else if (ani.frame == 0)
                ani.SetFrame(1);
        }
    }
}
