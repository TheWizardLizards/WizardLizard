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
        private const float visible = 10;
        private float countdown = visible;
        public PlayerShield(GameObject gameObject) : base(gameObject)
        {
            animator = (Animator)GameObject.GetComponent("Animator");
            transform = gameObject.Transform;
        }
        public void Update()
        {
            animator.PlayAnimation("ShieldAnimation");
            Vector2 translation = Vector2.Zero;
            Vector2 playerPos = new Vector2(GameWorld.PlayerPos.X - 36, GameWorld.PlayerPos.Y - 50);
            if (countdown > 0)
            {
                countdown -= GameWorld.DeltaTime;
            }
            if (countdown <= 0)
            {
                countdown = 0;
                GameWorld.Instance.RemoveGameObject(this.GameObject);
                countdown = visible;
            }


            foreach (GameObject go in GameWorld.GameObjects)
            {
                if (go.GetComponent("Morph") != null)
                {
                    GameWorld.Instance.RemoveGameObject(this.GameObject);
                }
            }
            transform.Position = playerPos;
        }
        public void LoadContent(ContentManager content)
        {
            CreateAnimations();
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
        public void CreateAnimations()
        {
            animator.CreateAnimation("ShieldAnimation", new Animation(3, 0, 0, 144, 200, 6, Vector2.Zero));
            animator.PlayAnimation("ShieldAnimation");
        }
    }
}
