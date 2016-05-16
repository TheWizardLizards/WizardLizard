using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;

namespace WizardPlatformTest
{
    class SolidPlatform : Component, ILoadable, IUpdateable, IAnimateable, ICollisionEnter, ICollisionExit
    {
        private Transform transform;
        private Animator animator;

        public SolidPlatform(GameObject gameObject) : base(gameObject)
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
            
        }
    }
}
