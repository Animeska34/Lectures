using NekoEngine;
using NekoEngine.Physics;
using System.Numerics;

namespace NekoApp2D1
{
    internal class Finish : Component
    {
        protected override void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.tag == "Player")
            {
                Debug.Log("Level Complete");
                collision.gameObject.transform.position = Vector3.Zero;
            }
        }
    }
}
