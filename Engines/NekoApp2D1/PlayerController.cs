using NekoEngine;
using NekoEngine.Assets;
using NekoEngine.Components;
using NekoEngine.Physics;
using NekoEngine.Render.OpenGL;
using System.Numerics;

namespace NekoApp2D1
{
    public class PlayerController : Component
    {
        SpriteSheetAnimation ani;
        Single speed = 2f;
        bool onGround;
        bool jumping;
        float maxJump = 0.3f;
        float jump;
        protected override void Setup()
        {
            gameObject.tag = "player";
            AddComponent<SpriteRenderer>();
            ani = new SpriteSheetAnimation(new Texture2D("C3ZwL.png", Texture2D.MagFilter.Nearest));
            ani.framerate = 8;
            AddComponent<Animator>().animation = ani;
            AddComponent<BoxCollder2D>().size = new Vector2(0.1f, 2f);
            AddComponent<Rigidbody2D>();
            Camera.Main.transform.SetParent(transform);
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

            if (Input.GetButtonDown("up") && onGround)
            {
                jumping = true;
            }
            if (Input.GetButtonUp("up"))
            {
                jumping = false;
            }
            
            if (Input.GetKeyDown(VirtualKey.Space))
            {
                var tmp = transform.parent.gameObject.Instantiate(transform.position);
                tmp.AddComponent<BulletController>().left = ani.animation == 1;
            }
            
            if (jumping && jump > 0)
            {
               transform.position += new Vector3(0, speed * 3 * Time.deltaTime, 0);
                jump -= Time.deltaTime;
            }
            else
            {
                jump = maxJump;
            }

            if (!onGround)
            {
                ani.playing = false;
            }

            if (transform.position.Y < -10)
                transform.position = Vector3.Zero;

            if (!ani.playing)
                ani.SetFrame(0);
            else if (ani.frame == 0)
                ani.SetFrame(1);
        }

        protected override void OnTriggerEnter2D(Collision2D collision)
        {
            if(collision.tag == "parcicle")
                Program.Start();
        }

        protected override void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.tag == "ground")
                onGround = true;
            if (collision.tag == "enemy")
                Program.Start();
        }
        protected override void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.tag == "ground")
                onGround = false;
        }
    }
}
