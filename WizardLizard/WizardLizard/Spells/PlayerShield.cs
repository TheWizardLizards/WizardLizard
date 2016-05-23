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
        private int visible = 0;
        public PlayerShield(GameObject gameObject) : base(gameObject)
        {
            animator = (Animator)GameObject.GetComponent("Animator");
            transform = gameObject.Transform;
        }
        public void Update()
        {
            Vector2 translation = Vector2.Zero;
            visible++;
            Vector2 playerPos = new Vector2(GameWorld.PlayerPos.X - 36, GameWorld.PlayerPos.Y - 50);

            //if (visible >= 100)
            //{
            //    foreach (GameObject go in GameWorld.GameObjects)
            //    {
            //        if (go.GetComponent("PlayerShield") != null)
            //        {
            //            visible++;
            //            if (visible >= 50)
            //            {
            //                GameWorld.ObjectsToRemove.Add(go);
            //                visible = 0;
            //            }
            //        }
            //    }
            //}
            transform.Position = playerPos;
        }
        public void LoadContent(ContentManager content)
        {
        }

        public void OnAnimationDone(string animationName)
        {
        }

        public void OnCollisionEnter(Collider other)
        {
            if (other.GameObject.GetComponent("Arrow") != null)
            {
                GameWorld.Instance.RemoveGameObject(this.GameObject);
            }
        }

        public void OnCollisionExit(Collider other)
        {
        }
        
    }
}
