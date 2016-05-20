using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace WizardLizard
{
    class Goblin : Component, ILoadable, IUpdateable, IAnimateable, ICollisionEnter, ICollisionExit
    {
        private Transform transform;
        private Animator animator;
        private Vector2 goblinPos;
        private float speed;


        public Goblin(GameObject gameObject) : base(gameObject)
        {
            animator = (Animator)GameObject.GetComponent("Animator");
            transform = GameObject.Transform;
        }

        public void Update()
        {
            goblinPos = new Vector2(transform.Position.X, transform.Position.Y);
            var range = goblinPos.X - GameWorld.PlayerPos.X;
            if (range >= 300)
                Idle();

            if (range < 300)
                Chase();

            if (range < 10)
                Attack();
            
        }
        
        //Idle behaviour
        public void Idle() { }
        
        //Attacks the player when close enough
        public void Attack() { }


        public void Chase()
        {
            speed = 2;
            Vector2 playerPos = new Vector2(GameWorld.PlayerPos.X, GameWorld.PlayerPos.Y - 30);
            Vector2 goblinPos = new Vector2(transform.Position.X, transform.Position.Y);
            Vector2 direction = playerPos - goblinPos;
            foreach(GameObject go in GameWorld.GameObjects)
            {
                if (go.GetComponent("Morph") != null)
                    playerPos = go.Transform.Position;
            }
            

            if (speed > direction.Length())
            {
                transform.Position = goblinPos;
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
