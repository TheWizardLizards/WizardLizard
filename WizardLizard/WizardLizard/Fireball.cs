using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace WizardLizard
{
    class Fireball : Component, ILoadable, IUpdateable, IAnimateable, ICollisionEnter, ICollisionExit
    {
        private Transform transform;
        private Animator animator;
        public Fireball(GameObject gameObject) : base (gameObject)
        {
            animator = (Animator)GameObject.GetComponent("Animator");
            transform = gameObject.Transform;
        }
        public void Update()
        {
        }
        public void LoadContent(ContentManager content)
        {
        }

        public void OnAnimationDone(string animationName)
        {
            throw new NotImplementedException();
        }

        public void OnCollisionEnter(Collider other)
        {
            throw new NotImplementedException();
        }

        public void OnCollisionExit(Collider other)
        {
            throw new NotImplementedException();
        }

    }
}
