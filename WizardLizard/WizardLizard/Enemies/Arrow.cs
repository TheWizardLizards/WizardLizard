using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;

namespace WizardLizard
{
    public class Arrow : Component, IUpdateable, ILoadable, IAnimateable, ICollisionEnter, ICollisionExit
    {
        private float speed = 1216;
        Transform transform;
        Animator animator;
        Vector2 arrowPos;
        Vector2 translation;

        public Arrow(GameObject gameObject) : base(gameObject)
        {
            animator = (Animator)GameObject.GetComponent("Animator");
            transform = gameObject.Transform;
        }


        public void Update()
        {
            translation.Normalize();
            transform.Translate(translation * GameWorld.DeltaTime * speed);
            if(transform.Position.X > 1600 || transform.Position.X < 0 || transform.Position.Y > 900 || transform.Position.Y < 0)
            {
                GameWorld.ObjectsToRemove.Add(this.GameObject);
            }
        }

        public void LoadContent(ContentManager content)
        {

            arrowPos = new Vector2(transform.Position.X, transform.Position.Y);
            translation = GameWorld.PlayerPos - arrowPos;
        }

        public void OnAnimationDone(string animationName)
        {

        }
        public void OnCollisionEnter(Collider other)
        {
            if (other.GameObject.GetComponent("Player") != null)
            {
                Player player = (Player)other.GameObject.GetComponent("Player");
                GameWorld.ObjectsToRemove.Add(this.GameObject);
                player.PlayerHit(1);
            }
            if (other.GameObject.GetComponent("PlayerShield") != null)
            {
                GameWorld.ObjectsToRemove.Add(this.GameObject);
            }
            if (other.GameObject.GetComponent("SolidPlatform") != null)
            {
                GameWorld.ObjectsToRemove.Add(this.GameObject);
            }
        }

        public void OnCollisionExit(Collider other)
        {

        }

    }
}
