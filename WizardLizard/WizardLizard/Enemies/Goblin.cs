using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System;

namespace WizardLizard
{
    class Goblin : Component, ILoadable, IUpdateable, IAnimateable, ICollisionEnter, ICollisionExit
    {
        private Director director;
        private Vector2 centering = new Vector2(0, 0);
        private Transform transform;
        private Animator animator;
        private Vector2 goblinPos;
        private float speed = 200;
        private Vector2 velocity;
        private bool goblinCanBeHit;
        private int health;
        private string direction;
        private bool dying = false;
        private bool attacking = false;
        private int chanceToSpawnHealth = 50; //i procent
        private GameObject gameObject;

        public Goblin(GameObject gameObject) : base(gameObject)
        {
            this.gameObject = gameObject;
            animator = (Animator)GameObject.GetComponent("Animator");
            transform = GameObject.Transform;
            health = 1;
            direction = "Left";
        }

        public void Update()
        {
            
            if(dying == false)
            {
                goblinCanBeHit = true;
                goblinPos = new Vector2(transform.Position.X+centering.X, transform.Position.Y+centering.Y);
                if(goblinPos.X > GameWorld.PlayerPos.X)
                {
                    direction = "Left";
                }
                else
                {
                    direction = "Right";
                }
                var range = Math.Sqrt(((goblinPos.X - GameWorld.PlayerPos.X) * (goblinPos.X - GameWorld.PlayerPos.X)) + ((goblinPos.Y - GameWorld.PlayerPos.Y)) * (goblinPos.Y - GameWorld.PlayerPos.Y));
                var xdistance = Math.Sqrt((goblinPos.X - GameWorld.PlayerPos.X) * (goblinPos.X - GameWorld.PlayerPos.X));
                if(attacking == false)
                {
                    if (range >= 400)
                        Idle();

                    if (range < 400)
                        Chase(xdistance);

                    if (range < 10)
                        Attack();
                }
                if (health <= 0)
                {
                    animator.PlayAnimation("Die" + direction);
                    dying = true;
                    Random rnd = new Random();
                    if (rnd.Next(0, 101) <= chanceToSpawnHealth)
                    {
                        director = new Director(new HealthGlobeBuilder());
                        GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(transform.Position.X, transform.Position.Y)));
                    }
                }
            }
        }

        //Idle behaviour
        public void Idle()
        {
            animator.PlayAnimation("Idle"+direction);
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
        public void Attack()
        {
            animator.PlayAnimation("Attack" + direction);
            attacking = true;
        }


        public void Chase(double xdistance)
        {
            Vector2 translation = new Vector2(0, 0);
            Vector2 goblinPos = new Vector2(transform.Position.X + centering.X, transform.Position.Y + centering.Y);
            if (xdistance > 2)
            {
                if (GameWorld.PlayerPos.X > goblinPos.X)
                {
                    translation.X++;
                    animator.PlayAnimation("Run"+direction);
                }
                if (GameWorld.PlayerPos.X < goblinPos.X)
                {
                    translation.X--;
                    animator.PlayAnimation("Run" + direction);
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
                animator.PlayAnimation("Idle"+direction);
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
            animator.CreateAnimation("RunLeft",new Animation(12,0,0,58,90,16,Vector2.Zero));
            animator.CreateAnimation("RunRight", new Animation(12,90,0,58,90,16,Vector2.Zero));
            animator.CreateAnimation("IdleLeft", new Animation(5, 180, 0, 53, 90, 8, Vector2.Zero));
            animator.CreateAnimation("IdleRight", new Animation(5, 270, 0, 53, 90, 8, Vector2.Zero));
            animator.CreateAnimation("DieLeft", new Animation(7, 360, 0, 95, 90, 12, Vector2.Zero));
            animator.CreateAnimation("DieRight", new Animation(7, 450, 0, 95, 90, 12, Vector2.Zero));
            animator.CreateAnimation("AttackLeft", new Animation(9, 540, 0, 70, 90, 16, Vector2.Zero));
            animator.CreateAnimation("AttackRight", new Animation(9, 630, 0, 70, 90, 16, Vector2.Zero));
            animator.PlayAnimation("Idle"+direction);
        }
        public void OnAnimationDone(string animationName)
        {
            if (animationName == "AttackLeft" || animationName == "AttackRight")
            {
                attacking = false;
            }

            if(animationName == "DieLeft" || animationName == "DieRight")
            {
                GameWorld.ObjectsToRemove.Add(GameObject);
            }
        }

        public void OnCollisionEnter(Collider other)
        {
            if (other.GameObject.GetComponent("SolidPlatform") != null)
            {
                Collider collider = (Collider)GameObject.GetComponent("Collider");
                centering = new Vector2(collider.CollisionBox.Width / 2, collider.CollisionBox.Height / 2);

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
