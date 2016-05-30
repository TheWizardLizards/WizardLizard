using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System;

namespace WizardLizard
{
    class Goblin : Component, ILoadable, IUpdateable, IAnimateable, ICollisionEnter, ICollisionExit
    {
        private Director director;
        private Transform transform;
        private Animator animator;
        private Vector2 goblinPos;
        private float speed = 200;
        private Vector2 velocity;
        private bool goblinCanBeHit;
        private int health;
        private int chanceToSpawnHealth = 50; //i procent


        public Goblin(GameObject gameObject) : base(gameObject)
        {
            animator = (Animator)GameObject.GetComponent("Animator");
            transform = GameObject.Transform;
            health = 1;
        }

        public void Update()
        {
            
            goblinCanBeHit = true;
            goblinPos = new Vector2(transform.Position.X, transform.Position.Y);
            var range = Math.Sqrt(((goblinPos.X - GameWorld.PlayerPos.X) * (goblinPos.X - GameWorld.PlayerPos.X)) + ((goblinPos.Y - GameWorld.PlayerPos.Y)) * (goblinPos.Y - GameWorld.PlayerPos.Y));
            var xdistance = Math.Sqrt((goblinPos.X - GameWorld.PlayerPos.X) * (goblinPos.X - GameWorld.PlayerPos.X));
            if (range >= 400)
                Idle();

            if (range < 400)
                Chase(xdistance);

            if (range < 10)
                Attack();
            
            if (health <= 0)
            {
                GameWorld.Instance.RemoveGameObject(this.GameObject);
                Random rnd = new Random();
                if (rnd.Next(0, 101) <= chanceToSpawnHealth)
                {
                    director = new Director(new HealthGlobeBuilder());
                    GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(transform.Position.X, transform.Position.Y)));
                }
            }
        }

        //Idle behaviour
        public void Idle()
        {
            animator.PlayAnimation("Idle");
            Vector2 translation = new Vector2(0, 0);
            float i = 5;
            velocity.Y += 0.05f * i;
            if (velocity.Y > 10)
            {
                velocity.Y = 10;
            }
            translation += velocity;

            transform.Translate(translation * GameWorld.DeltaTime * speed);
        }

        //Attacks the player when close enough
        public void Attack() { }


        public void Chase(double xdistance)
        {
            Vector2 translation = new Vector2(0, 0);
            Vector2 goblinPos = new Vector2(transform.Position.X, transform.Position.Y);
            if (xdistance > 2)
            {
                if (GameWorld.PlayerPos.X > goblinPos.X)
                {
                    translation.X++;
                }
                if (GameWorld.PlayerPos.X < goblinPos.X)
                {
                    translation.X--;
                    animator.PlayAnimation("RunLeft");
                }
            }

            float i = 5;
            velocity.Y += 0.05f * i;
            if (velocity.Y > 10)
            {
                velocity.Y = 10;
            }
            translation += velocity;

            transform.Translate(translation * GameWorld.DeltaTime * speed);
            if(translation.X == 0)
            {
                animator.PlayAnimation("Idle");
            }



        }
        public void TakeDamage(int dmg)
        {
            if (goblinCanBeHit == true)
            {
                health = health - dmg;
                goblinCanBeHit = false;
            }
        }

        public void LoadContent(ContentManager content)
        {
            animator.CreateAnimation("Idle",new Animation(5,44,0,25,43,5,Vector2.Zero));
            animator.CreateAnimation("RunLeft", new Animation(14,1,0,28,43,14,Vector2.Zero));
            animator.PlayAnimation("Idle");
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
        public void OnCollisionExit(Collider other) { }
    }
}
