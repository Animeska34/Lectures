using NekoEngine;

namespace NekoApp2D1
{
    public class GameObjectDebug : Component
    {
        //Calls once, when component created
        protected override void Setup()
        {

        }
        //Calls on every frame update
        protected override void Update()
        {
            Debug.Log(transform.position.ToString());
        }
    }
}