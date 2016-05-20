using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace WizardLizard
{
    class Orc : Component, ILoadable, IUpdateable, IAnimateable, ICollisionEnter, ICollisionExit
    {
        private Transform transform;
        private Animator animator;
        private float speed;
        private Vector2 orcPos;



        public Orc(GameObject gameObject) : base(gameObject)
        {
            animator = (Animator)GameObject.GetComponent("Animator");
            transform = GameObject.Transform;
        }

        public void Update()
        {
            orcPos = new Vector2(transform.Position.X, transform.Position.Y);
            var range = orcPos.X - GameWorld.PlayerPos.X;
            if (range <= 500)
                Chase();

        }

        public void Chase()
        {
            speed = 2;
            Vector2 playerPos = new Vector2(GameWorld.PlayerPos.X, GameWorld.PlayerPos.Y - 50);
            orcPos = new Vector2(transform.Position.X, transform.Position.Y);
            Vector2 direction = playerPos - orcPos;
            foreach (GameObject go in GameWorld.GameObjects)
            {
                if (go.GetComponent("Morph") != null)
                    playerPos = go.Transform.Position;
            }


            if (speed > direction.Length())
            {
                transform.Position = orcPos;
            }
            else
            {
                direction.Normalize();
                transform.Position += direction * speed;
            }
        }
        public void LoadContent(ContentManager content) { }
        public void OnAnimationDone(string animationName) { }
        public void OnCollisionEnter(Collider other) { }
        public void OnCollisionExit(Collider other) { }
    }
}
