using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace WizardLizard
{
    class Aimer : Component, IUpdateable, ILoadable
    {
        private Transform transform;
        private Animator animator;

        public Aimer(GameObject gameObject) : base (gameObject)
        {
            animator = (Animator)GameObject.GetComponent("Animator");
            transform = gameObject.Transform;
        }
        public void LoadContent(ContentManager content)
        {

        }

        public void Update()
        {
            MouseState mouseState = Mouse.GetState();
            transform.Position = new Vector2(mouseState.X , mouseState.Y);
        }
        
    }
}
