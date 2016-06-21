using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace WizardLizard
{
    public class Aimer : Component, IUpdateable, ILoadable
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
