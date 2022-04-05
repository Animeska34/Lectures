﻿using NekoEngine;
using NekoEngine.Assets;
using NekoEngine.Components;
using NekoEngine.Render.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace NekoApp2D1
{
    class BulletController : Component
    {
        static Sprite sprite;

        float speed = 5f;
        public bool left;
        public float lifetime = 1f;
        protected override void Setup()
        {
            AddComponent<SpriteRenderer>().sprite = new Sprite(new Texture2D("bullet.png", Texture2D.MagFilter.Nearest));
            AddComponent<BoxCollder2D>().isTrigger = true;
            AddComponent<Rigidbody2D>().useGravity = false;
            transform.position += new Vector3(0,0,2);
        }
        protected override void Update()
        {
            lifetime -= Time.deltaTime;
            if (lifetime < 0)
            {
                Destroy();
            }
            if (left)
            {
                transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
                transform.localScale = new Vector3(-1.5f, 1.5f, 1.5f);
            }
            else
            {
                transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
                transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            }
        }

        protected override void OnDestroy()
        {

        }
    }
}