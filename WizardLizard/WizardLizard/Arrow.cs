using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;

namespace WizardLizard
{
    class Arrow : Component, IUpdateable, ILoadable, IAnimateable, ICollisionEnter, ICollisionExit
    {

        private float speed = 400;
        Transform transform;
        Animator animator;
        private bool shot = true;
        Vector2 arrowPos;
        Vector2 playerPos;
        

        public Arrow(GameObject gameObject) : base(gameObject)
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
            if(shot == true)
            {
                arrowPos = new Vector2(transform.Position.X, transform.Position.Y);
                foreach (GameObject go in GameWorld.GameObjects)
                {
                    if (go.GetComponent("Player") != null)
                        playerPos = go.Transform.Position;
                }
                shot = false;
            }

            translation = playerPos - arrowPos;
            translation.Normalize();
            transform.Translate(translation * GameWorld.DeltaTime * speed);

        }

    }
}
