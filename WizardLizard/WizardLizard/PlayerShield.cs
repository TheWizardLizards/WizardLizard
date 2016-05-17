using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace WizardLizard
{
    class PlayerShield : Component, IUpdateable, ILoadable, IAnimateable, ICollisionEnter, ICollisionExit
    {
        private Animator animator;
        private Transform transform;
        Vector2 playerPos;
        public PlayerShield(GameObject gameObject) : base(gameObject)
        {
            animator = (Animator)GameObject.GetComponent("Animator");
            transform = gameObject.Transform;
        }
        public void Update()
        {
            Vector2 translation = Vector2.Zero;

            foreach (GameObject go in GameWorld.GameObjects)
            {
                if (go.GetComponent("Player") != null)
                {
                    playerPos = go.Transform.Position;
                }
            }
            transform.Position = playerPos;
            //transform.Translate(translation * GameWorld.DeltaTime * speed);

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
        
    }
}
