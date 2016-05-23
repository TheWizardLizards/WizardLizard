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
            CreateAnimations();
            foreach (GameObject go in GameWorld.GameObjects)
            {
                if (go.GetComponent("Aimer") != null)
                {
                    mousePosition = go.Transform.Position;
                }

            }
            fireballPos = new Vector2(transform.Position.X, transform.Position.Y);
        }

        public void OnAnimationDone(string animationName)
        {

        }

        public void OnCollisionEnter(Collider other)
        {
            if (other.GameObject.GetComponent("Goblin") != null)
            {
                Goblin goblin = (Goblin)other.GameObject.GetComponent("Goblin");
                GameWorld.ObjectsToRemove.Add(this.GameObject);
                goblin.TakeDamage(1);
            }
            if (other.GameObject.GetComponent("Orc") != null)
            {
                Orc orc = (Orc)other.GameObject.GetComponent("Orc");
                GameWorld.ObjectsToRemove.Add(this.GameObject);
                orc.TakeDamage(1);
            }
            if (other.GameObject.GetComponent("Archer") != null)
            {
                Archer archer = (Archer)other.GameObject.GetComponent("Archer");
                GameWorld.ObjectsToRemove.Add(this.GameObject);
                archer.TakeDamage(1);
            }
            if (other.GameObject.GetComponent("SolidPlatform") != null)
            {
                GameWorld.ObjectsToRemove.Add(this.GameObject);
            }
        }

        public void OnCollisionExit(Collider other)
        {

        }

        public void Update()
        {
            animator.PlayAnimation("FireballYo");
            Vector2 translation = Vector2.Zero;
            translation = mousePosition - fireballPos;

            translation.Normalize();

            transform.Translate(translation * GameWorld.DeltaTime * speed);

            if (transform.Position.X > 1600 || transform.Position.X < 0 || transform.Position.Y > 900 || transform.Position.Y < 0)
            {
                GameWorld.ObjectsToRemove.Add(this.GameObject);
            }
        }
        public void CreateAnimations()
        {
            animator.CreateAnimation("FireballYo", new Animation(4, 0, 0, 50, 50, 32, Vector2.Zero));
            animator.PlayAnimation("FireballYo");
        }
        
    }
}
