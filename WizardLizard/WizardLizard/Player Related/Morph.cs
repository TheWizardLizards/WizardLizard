using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace WizardLizard
{
    public class Morph : Component, ILoadable, IUpdateable, IAnimateable, ICollisionEnter, ICollisionExit
    {
        private Transform transform;
        private Animator animator;
        private Vector2 velocity;
        private Director director;
        private bool hasJumped;
        private static bool hasMorphed = false;
        private static bool derp = true;
        private int speed = 1;

        public static bool HasMorphed
        {
            get { return hasMorphed; }
            set { hasMorphed = value; }
        }

        public static bool Derp
        {
            get { return derp; }
            set { derp = value; }
        }

        public Morph(GameObject gameObject) : base(gameObject)
        {
            animator = (Animator)GameObject.GetComponent("Animator");
            transform = gameObject.Transform;
            HasMorphed = true;
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
            
            KeyboardState keyState = Keyboard.GetState();
            Vector2 translation = Vector2.Zero;

                speed = 200;
                translation += velocity;
                if (transform.Position.Y >= 200)
                {
                    hasJumped = false;
                }

                if (keyState.IsKeyDown(Keys.W) && hasJumped == false)
                {
                    translation.Y -= 5f;
                    velocity.Y = -5f;
                    hasJumped = true;
                }

                if (hasJumped == true)
                {
                    float i = 5;
                    velocity.Y += 0.05f * i;
                }

                if (hasJumped == false)
                {
                    velocity.Y = 0f;
                }

                if (keyState.IsKeyDown(Keys.D))
                {
                    translation += new Vector2(1, 0);
                }

                if (keyState.IsKeyDown(Keys.A))
                {
                    translation += new Vector2(-1, 0);
                }

                if (keyState.IsKeyDown(Keys.S))
                {
                    translation += new Vector2(0, 1);
                }

            if (keyState.IsKeyUp(Keys.F))
                {
                 HasMorphed = false;
                }

            if (keyState.IsKeyDown(Keys.F) && HasMorphed == false)
                {
                HasMorphed = true;
                   
                director = new Director(new PlayerBuilder());
                GameWorld.Instance.AddGameObject(director.Construct(this.transform.Position));

                director = new Director(new CompanionBuilder());
                GameWorld.Instance.AddGameObject(director.Construct(this.transform.Position));

                GameWorld.Instance.RemoveGameObject(this.GameObject);
                }

            transform.Translate(translation * GameWorld.DeltaTime * speed);
            GameWorld.PlayerPos = transform.Position;
        }
    }
}

