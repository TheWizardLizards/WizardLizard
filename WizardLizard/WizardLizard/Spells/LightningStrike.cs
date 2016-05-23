using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace WizardLizard
{
    class LightningStrike : Component, IUpdateable, ILoadable, IAnimateable, ICollisionEnter, ICollisionExit
    {
        private Transform transform;
        private Animator animator;
        private float visible = 0;
        public LightningStrike(GameObject gameObject) : base(gameObject)
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
        }

        public void OnCollisionExit(Collider other)
        {
        }
        public void Update()
        {
            visible++;
            if (visible >= 50)
            {
                foreach (GameObject go in GameWorld.GameObjects)
                {
                    if (go.GetComponent("LightningStrike") != null)
                    {
                        GameWorld.ObjectsToRemove.Add(go);
                        visible = 0;
                    }
                }
            }
        }
    }
}
