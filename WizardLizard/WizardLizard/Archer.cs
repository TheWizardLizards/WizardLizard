using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;

namespace WizardLizard
{
    class Archer : Component, ILoadable, IUpdateable, IAnimateable, ICollisionEnter, ICollisionExit
    {
        
        private Transform transform;
        private Animator animator;
        private Director director;
        private Vector2 archerPos;
        private const float delay = 3; // seconds
        private float countdown = delay;
        public float Delay { get; set; }
        private Vector2 velocity;
        private int speed = 100;


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
            if (other.GameObject.GetComponent("SolidPlatform") != null)
            {
                Collider collider = (Collider)GameObject.GetComponent("Collider");

                int top = Math.Max(collider.CollisionBox.Top, other.CollisionBox.Top);
                int left = Math.Max(collider.CollisionBox.Left, other.CollisionBox.Left);
                int width = Math.Min(collider.CollisionBox.Right, other.CollisionBox.Right) - left;
                int height = Math.Min(collider.CollisionBox.Bottom, other.CollisionBox.Bottom) - top;

                var intersectingRectangle = new Rectangle(left, top, width, height);

                if (collider.CollisionBox.Intersects(other.TopLine) && collider.CollisionBox.Intersects(other.RightLine))
                {
                    if (width > height)
                    {
                        Vector2 position = GameObject.Transform.Position;
                        position.Y = other.CollisionBox.Y - collider.CollisionBox.Height;
                        GameObject.Transform.Position = position;
                        velocity.Y = 0;
                    }
                    else
                    {
                        Vector2 position = GameObject.Transform.Position;
                        position.X = other.CollisionBox.X + other.CollisionBox.Width;
                        GameObject.Transform.Position = position;
                    }
                }
                else if (collider.CollisionBox.Intersects(other.TopLine) && collider.CollisionBox.Intersects(other.LeftLine))
                {
                    if (width > height)
                    {
                        Vector2 position = GameObject.Transform.Position;
                        position.Y = other.CollisionBox.Y - collider.CollisionBox.Height;
                        GameObject.Transform.Position = position;
                        velocity.Y = 0;
                    }
                    else
                    {
                        Vector2 position = GameObject.Transform.Position;
                        position.X = other.CollisionBox.X - collider.CollisionBox.Width;
                        GameObject.Transform.Position = position;
                    }
                }
                else if (collider.CollisionBox.Intersects(other.BottomLine) && collider.CollisionBox.Intersects(other.LeftLine))
                {
                    if (width > height)
                    {
                        Vector2 position = GameObject.Transform.Position;
                        position.Y = other.CollisionBox.Y + other.CollisionBox.Height;
                        GameObject.Transform.Position = position;
                        velocity.Y = 0;
                    }
                    else
                    {
                        Vector2 position = GameObject.Transform.Position;
                        position.X = other.CollisionBox.X - collider.CollisionBox.Width;
                        GameObject.Transform.Position = position;
                    }
                }
                else if (collider.CollisionBox.Intersects(other.BottomLine) && collider.CollisionBox.Intersects(other.RightLine))
                {
                    if (width > height)
                    {
                        Vector2 position = GameObject.Transform.Position;
                        position.Y = other.CollisionBox.Y + other.CollisionBox.Height;
                        GameObject.Transform.Position = position;
                        velocity.Y = 0;
                    }
                    else
                    {
                        Vector2 position = GameObject.Transform.Position;
                        position.X = other.CollisionBox.X + other.CollisionBox.Width;
                        GameObject.Transform.Position = position;
                    }
                }
                else
                {
                    if (collider.CollisionBox.Intersects(other.TopLine))
                    {
                        Vector2 position = GameObject.Transform.Position;
                        position.Y = other.CollisionBox.Y - collider.CollisionBox.Height;
                        GameObject.Transform.Position = position;
                        velocity.Y = 0;
                    }
                    else if (collider.CollisionBox.Intersects(other.BottomLine))
                    {
                        Vector2 position = GameObject.Transform.Position;
                        position.Y = other.CollisionBox.Y + other.CollisionBox.Height;
                        GameObject.Transform.Position = position;
                        velocity.Y = 0;
                    }
                    else if (collider.CollisionBox.Intersects(other.LeftLine))
                    {
                        Vector2 position = GameObject.Transform.Position;
                        position.X = other.CollisionBox.X - collider.CollisionBox.Width;
                        GameObject.Transform.Position = position;
                    }
                    else if (collider.CollisionBox.Intersects(other.RightLine))
                    {
                        Vector2 position = GameObject.Transform.Position;
                        position.X = other.CollisionBox.X + other.CollisionBox.Width;
                        GameObject.Transform.Position = position;
                    }
                }
            }
        }
        public void OnCollisionExit(Collider other)
        {

        }

            

        public void Update()
        {
            Vector2 translation = new Vector2(0, 0);
            float i = 5;
            velocity.Y += 0.05f * i;
            if (velocity.Y > 10)
            {
                velocity.Y = 10;
            }
            translation += velocity;

            transform.Translate(translation * GameWorld.DeltaTime * speed);
            archerPos = new Vector2(transform.Position.X, transform.Position.Y);
            var range = Math.Sqrt(((archerPos.X - GameWorld.PlayerPos.X) * (archerPos.X - GameWorld.PlayerPos.X)) + ((archerPos.Y - GameWorld.PlayerPos.Y)) * (archerPos.Y - GameWorld.PlayerPos.Y));
            if (range < 800)
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
