using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace WizardLizard
{
    public class LightningStrike : Component, IUpdateable, ILoadable, IAnimateable, ICollisionEnter, ICollisionExit
    {
        private SoundEffect lightningSound;
        private Transform transform;
        private int speed = 2000;
        private Animator animator;
        public LightningStrike(GameObject gameObject) : base(gameObject)
        {
            animator = (Animator)GameObject.GetComponent("Animator");
            transform = gameObject.Transform;
        }
        public void LoadContent(ContentManager content)
        {
            lightningSound = content.Load<SoundEffect>("LightningStrikeShoot");
            lightningSound.Play();
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
                goblin.TakeDamage(6);
            }
            if (other.GameObject.GetComponent("Orc") != null)
            {
                Orc orc = (Orc)other.GameObject.GetComponent("Orc");
                GameWorld.ObjectsToRemove.Add(this.GameObject);
                orc.TakeDamage(6);
            }
            if (other.GameObject.GetComponent("Archer") != null)
            {
                Archer archer = (Archer)other.GameObject.GetComponent("Archer");
                GameWorld.ObjectsToRemove.Add(this.GameObject);
                archer.TakeDamage(6);
            }
            if (other.GameObject.GetComponent("SolidPlatform") != null
                || other.GameObject.GetComponent("Door") != null
                || other.GameObject.GetComponent("Lever") != null
                || other.GameObject.GetComponent("MoveableBox") != null
                || other.GameObject.GetComponent("NonSolidPlatform") != null)
            {
                GameWorld.ObjectsToRemove.Add(this.GameObject);
            }
        }

        public void OnCollisionExit(Collider other)
        {
        }
        public void Update()
        {
            Vector2 translation = new Vector2(0, 1);
            transform.Translate(translation * GameWorld.DeltaTime * speed);
        }
    }
}
