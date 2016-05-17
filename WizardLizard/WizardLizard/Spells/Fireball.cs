using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WizardLizard
{
    class Fireball : Component, IUpdateable, ILoadable, IAnimateable, ICollisionEnter, ICollisionExit
    {
        private Transform transform;
        private Animator animator;
        private int speed = 400;
        private bool fireaway = true;
        Vector2 mousePosition;
        MouseState mouseState = Mouse.GetState();
        Vector2 fireballPos;

        public Fireball(GameObject gameObject) : base(gameObject)
        {
            animator = (Animator)GameObject.GetComponent("Animator");
            transform = gameObject.Transform;
        }
        public void LoadContent(ContentManager content)
        {

        }

        public void OnAnimationDone(string animationName)
        {

        }

        public void OnCollisionEnter(Collider other)
        {

        }

        public void OnCollisionExit(Collider other)
        {

        }

        public void Update()
        {
            Vector2 translation = Vector2.Zero;
            if (fireaway == true)
            {
                fireballPos = new Vector2(transform.Position.X, transform.Position.Y);
                foreach (GameObject go in GameWorld.GameObjects)
                {
                    if (go.GetComponent("Aimer") != null)
                    {
                        mousePosition = go.Transform.Position;
                    }

                }


                fireaway = false;
            }
            translation = mousePosition - fireballPos;

            translation.Normalize();

            transform.Translate(translation * GameWorld.DeltaTime * speed);
            
        }
        public void ShootFireball(int speed)
        {
        }
        
    }
}
