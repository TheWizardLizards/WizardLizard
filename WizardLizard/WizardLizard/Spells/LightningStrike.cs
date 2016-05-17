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
