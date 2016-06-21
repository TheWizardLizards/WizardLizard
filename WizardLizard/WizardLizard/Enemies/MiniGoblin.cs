using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace WizardLizard
{
    public class MiniGoblin : Component, ILoadable, IUpdateable, IAnimateable, ICollisionEnter, ICollisionExit
    {
        private Transform transform;
        private Animator animator;
        private Vector2 miniGoblinPos;
        private Vector2 companionPos;
        private float speed;
        private Vector2 distToCompanion;


        public MiniGoblin(GameObject gameObject) : base(gameObject)
        {
            animator = (Animator)GameObject.GetComponent("Animator");
            transform = GameObject.Transform;
            speed = 200;
        }

        public void Update()
        {
            Vector2 translation = Vector2.Zero;

            miniGoblinPos = new Vector2(transform.Position.X, transform.Position.Y);

            foreach (GameObject go in GameWorld.GameObjects)
            {
                if (go.GetComponent("Companion") != null)
                {
                    companionPos = go.Transform.Position;
                }
            }



            distToCompanion = miniGoblinPos - companionPos;
            if (Companion.Roar == true)
            {

                if (distToCompanion.Length() < 300)
                {
                    if (companionPos.X > transform.Position.X)
                    {
                            translation = new Vector2(-1, 0);
                        
                    }
                    else if (companionPos.X < transform.Position.X)
                    {
                            translation = new Vector2(1, 0);
                        
                    }
                }
            }
            transform.Translate(translation * GameWorld.DeltaTime * speed);

        }

        public void LoadContent(ContentManager content) { }
        public void OnAnimationDone(string animationName) { }
        public void OnCollisionEnter(Collider other) { }
        public void OnCollisionExit(Collider other) { }
    }
}