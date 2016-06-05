using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System;

namespace WizardLizard
{
    class Orc : Component, ILoadable, IUpdateable, IAnimateable, ICollisionEnter, ICollisionExit
    {
        Director director;
        private Transform transform;
        private Animator animator;
        private float speed = 200;
        private Vector2 orcPos;
        private Vector2 velocity;
        private bool orcCanBeHit;
        private bool attacking = false;
        private bool dying = false;
        private string direction = "Left";
        private Vector2 centering = new Vector2(0, 0);
        private int health;
        private int chanceToSpawnHealth = 50; //I Procent


        public Orc(GameObject gameObject) : base(gameObject)
        {
            animator = (Animator)GameObject.GetComponent("Animator");
            transform = GameObject.Transform;
            health = 8;
        }

        public void Update()
        {
            orcCanBeHit = true;
            orcPos = new Vector2(transform.Position.X + centering.X, transform.Position.Y + centering.X);
            var range = Math.Sqrt(((orcPos.X - GameWorld.PlayerPos.X) * (orcPos.X - GameWorld.PlayerPos.X)) + ((orcPos.Y - GameWorld.PlayerPos.Y)) * (orcPos.Y - GameWorld.PlayerPos.Y));
            var xdistance = Math.Sqrt((orcPos.X - GameWorld.PlayerPos.X) * (orcPos.X - GameWorld.PlayerPos.X));
            if(dying == false)
            {
                if (attacking == false)
                {
                    if (range <= 500)
                    {
                        Chase(xdistance);
                    }
                    if (range <= 100)
                    {
                        Attack();
                    }
                    else
                    {
                        Idle();
                    }
                }
                if (attacking == true && animator.AnimationName == "Attack" + direction)
                {
                    if (15 <= animator.CurrentIndex && animator.CurrentIndex <= 19)
                    {
                        director = new Director(new AttackFieldBuilder());
                        if (direction == "Right")
                        {
                            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(transform.Position.X, transform.Position.Y), 38, 72, "Orc"));
                            attacking = false;
                        }
                        else if (direction == "Left")
                        {
                            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(transform.Position.X+15, transform.Position.Y+98), 90, 122, "Orc"));
                            attacking = false;
                        }
                    }
                }
            }
            if (health <= 0)
            {
                animator.PlayAnimation("Die" + direction);
                dying = true;
            }
        }
        public void TakeDamage(int dmg)
        {
            if (orcCanBeHit == true)
            {
                health = health - dmg;
                orcCanBeHit = false;
            }
        }
        public void Idle()
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
        }

        public void Chase(double xdistance)
        {
            Vector2 translation = new Vector2(0, 0);
            if(xdistance > 15)
            {
                if (GameWorld.PlayerPos.X > orcPos.X)
                {
                    translation.X++;
                    direction = "Right";
                    animator.PlayAnimation("Walk"+direction);
                }
                if (GameWorld.PlayerPos.X < orcPos.X)
                {
                    translation.X--;
                    direction = "Left";
                    animator.PlayAnimation("Walk"+direction);
                }
            }
            if(translation.X == 0)
            {
                animator.PlayAnimation("Idle" + direction);
            }
            float i = 5;
            velocity.Y += 0.05f * i;
            if (velocity.Y > 10)
            {
                velocity.Y = 10;
            }
            translation += velocity;

            transform.Translate(translation * GameWorld.DeltaTime * speed);
        }

        public void Attack()
        {
            if(attacking == false)
            {
                animator.PlayAnimation("Attack" + direction);
                attacking = true;
            }

        }

        public void LoadContent(ContentManager content)
        {
            CreateAnimations();
        }

        public void CreateAnimations()
        {
            animator.CreateAnimation("IdleRight", new Animation(8,0,0,145,150,10,Vector2.Zero));
            animator.CreateAnimation("IdleLeft", new Animation(8, 0, 1160, 145, 150, 10, Vector2.Zero));
            animator.CreateAnimation("AttackLeft", new Animation(22, 150, 0, 271, 220, 22, Vector2.Zero));
            animator.CreateAnimation("AttackRight", new Animation(22, 370, 0, 271, 220, 22, Vector2.Zero));
            animator.CreateAnimation("WalkLeft", new Animation(11, 590, 0, 181, 160, 11, Vector2.Zero));
            animator.CreateAnimation("WalkRight", new Animation(11, 590, 1991, 181, 160, 11, Vector2.Zero));
            animator.CreateAnimation("DieLeft", new Animation(9, 750, 0, 275, 200, 9, Vector2.Zero));
            animator.CreateAnimation("DieRight", new Animation(9, 750, 2475, 275, 200, 9, Vector2.Zero));
            animator.PlayAnimation("IdleLeft");
        }

        public void OnAnimationDone(string animationName)
        {
            if(animationName == "Attack" + direction)
            {
                attacking = false;
            }
            if(animationName == "Die" + direction)
            {
                GameWorld.Instance.RemoveGameObject(this.GameObject);
                Random rnd = new Random();
                if (rnd.Next(0, 101) <= chanceToSpawnHealth)
                {
                    director = new Director(new HealthGlobeBuilder());
                    GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(transform.Position.X, transform.Position.Y)));
                }
            }
            if(dying == false)
            animator.PlayAnimation("Idle"+direction);
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
