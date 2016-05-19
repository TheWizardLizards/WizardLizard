using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;


namespace WizardLizard
{
    class Archer : Component, ILoadable, IUpdateable, IAnimateable, ICollisionEnter, ICollisionExit
    {
        
        private Transform transform;
        private Animator animator;
        private Director director;
        private Vector2 archerPos;



        public Archer(GameObject gameObject) : base(gameObject)
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
        private const float delay = 3; // seconds
        private float countdown = delay;
        public float Delay { get; set; }

            

        public void Update()
        {
            archerPos = new Vector2(transform.Position.X, transform.Position.Y);
            var range = archerPos.X - GameWorld.PlayerPos.X;
            if(range < 300)
            {
                
                Timer();
            }
            
        }
        public void shoot()
        {

            director = new Director(new ArrowBuilder());
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(transform.Position.X + 180, transform.Position.Y + 98)));
        }

        public void Timer()
        {
            if (countdown > 0)
            {
                countdown -= GameWorld.DeltaTime;
            }


            if (countdown <= 0)
            {
                shoot();
                countdown = delay;
            }
        }
    }
}
