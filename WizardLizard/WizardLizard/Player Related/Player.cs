using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace WizardLizard
{
    class Player : Component, ILoadable, IUpdateable, IAnimateable, ICollisionEnter, ICollisionExit
    {
        private SoundEffect jumpSound, attackSound;
        private Vector2 velocity;
        private Transform transform;
        private Animator animator;
        private bool shiftControle;
        private int speed = 200;
        private static int health;
        private static bool hasJumped;
        private bool fireball = true;
        private bool lightning = true;
        private bool shield = true;
        private bool attack = true;
        private bool attacking = false;
        private Director director;
        private bool canInteract = false;
        private bool haveInteracted = true;
        private bool playerCanBeHit;
        private bool shooting = false;
        private Lever lastknownLever;
        private string direction;
        private Vector2 centering = new Vector2(0, 0);
        private const float fireballCooldown = 3; // seconds
        private float fireballCountdown = fireballCooldown;
        private const float lightningstrikCooldown = 3; // seconds
        private float lightningstrikeCountdown = lightningstrikCooldown;
        private const float shieldCooldown = 10; // seconds
        private float shieldCountdown = shieldCooldown;
        public float Delay { get; set; }


        public static int Health
        {
            get { return health; }
            set { health = value; }
        }

        public static bool HasJumped
        {
            get
            {
                return hasJumped;
            }

            set
            {
                hasJumped = value;
            }
        }

        public Player(GameObject gameObject) : base(gameObject)
        {
            animator = (Animator)GameObject.GetComponent("Animator");
            transform = gameObject.Transform;
            health = 6;
            hasJumped = true;
            shiftControle = true;
            direction = "Right";
        }
        public void LoadContent(ContentManager content)
        {
            jumpSound = content.Load<SoundEffect>("PlayerJump");
            attackSound = content.Load<SoundEffect>("PlayerAttack");
            animator.CreateAnimation("IdleRight", new Animation(1, 0, 22, 64, 100, 1, Vector2.Zero));
            animator.CreateAnimation("IdleLeft", new Animation(1, 100, 24, 64, 100, 1, Vector2.Zero));
            animator.CreateAnimation("DieRight", new Animation(8, 0, 0, 109, 100, 16, new Vector2(22, 0)));
            animator.CreateAnimation("DieLeft", new Animation(8, 100, 0, 109, 100, 16, new Vector2(24, 0)));
            animator.CreateAnimation("AttackLeft", new Animation(19, 200, 0, 110, 119, 57, new Vector2(22,0)));
            animator.CreateAnimation("AttackRight", new Animation(19, 319, 0, 110, 119, 57, new Vector2(37,0)));
            animator.CreateAnimation("CastFireRight", new Animation(13, 438, 0, 83, 112, 26, new Vector2(13,0)));
            animator.CreateAnimation("CastFireLeft", new Animation(13, 550, 0, 83, 112, 26, new Vector2(13, 0)));
            animator.CreateAnimation("CastLightRight", new Animation(13, 662, 0, 83, 112, 26, new Vector2(13, 0)));
            animator.CreateAnimation("CastLightLeft", new Animation(13, 774, 0, 83, 112, 26, new Vector2(13, 0)));
            animator.CreateAnimation("RunRight", new Animation(23, 886, 0, 107, 100, 46, new Vector2(0, 0)));
            animator.CreateAnimation("RunLeft", new Animation(23, 986, 0, 107, 100, 46, new Vector2(0, 0)));
            animator.PlayAnimation("IdleRight");
        }
        public void Update()
        {
            
            playerCanBeHit = true;

            KeyboardState keyState = Keyboard.GetState();

            Vector2 translation = Vector2.Zero;

            MouseState mouseState = Mouse.GetState();

            PlayerController(keyState, translation, mouseState);
            GameWorld.PlayerPos = new Vector2(transform.Position.X + centering.X, transform.Position.Y + centering.Y);
            if(attacking == true)
            {
                if(animator.CurrentIndex >= 14)
                {
                    director = new Director(new AttackFieldBuilder());
                    if(direction == "Right")
                    {
                        GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(transform.Position.X+37, transform.Position.Y+8), 38, 72, "Player"));
                        attacking = false;
                    }
                    if(direction == "Left")
                    {
                        GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(transform.Position.X-22, transform.Position.Y+8), 38, 72, "Player"));
                        attacking = false;
                    }
                }
            }
        }

        public void OnAnimationDone(string animationName)
        {
            animator.PlayAnimation("Idle"+direction);
            if(animationName == "Attack" + direction)
            {
                attacking = false;
            }
        }
        private void MeleeAttack(MouseState mouseState)
        {
            if (mouseState.LeftButton == ButtonState.Pressed && attack == true && shooting == false)
            {
                attackSound.Play();
                attack = false;
                animator.PlayAnimation("Attack" + direction);
                attacking = true;
            }
            if(mouseState.LeftButton == ButtonState.Released)
            {
                attack = true;
            }
        }
        private void Interact(KeyboardState keyState)
        {
            //press E to interact
            if (keyState.IsKeyDown(Keys.E) && canInteract == true && haveInteracted == true)
            {
                if(lastknownLever != null)
                {
                    lastknownLever.Interaction(this.GameObject);
                }
                haveInteracted = false;
                canInteract = false;
            }
            if (keyState.IsKeyUp(Keys.E))
            {
                haveInteracted = true;
            }

        }
        private void CreateShield(KeyboardState keyState)
        {
            foreach (GameObject go in GameWorld.GameObjects)
            {
                if (go.GetComponent("PlayerShield") != null)
                {
                    shieldCountdown = shieldCooldown;
                }
            }
            //Creates a shield around the player
            if (shieldCountdown > 0)
            {
                shieldCountdown -= GameWorld.DeltaTime;
            }

            if (shieldCountdown <= 0)
            {
                if (keyState.IsKeyDown(Keys.Q) && shield == true)
                {
                    director = new Director(new PlayerShieldBuilder());
                    GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(transform.Position.X, transform.Position.Y)));
                    shield = false;
                }
                if (keyState.IsKeyUp(Keys.Q))
                {
                    shield = true;
                }
            }

        }
        private void ShootLighting(KeyboardState keyState, MouseState mouseState)
        {
            if (lightningstrikeCountdown > 0)
            {
                lightningstrikeCountdown -= GameWorld.DeltaTime;
            }

            if (lightningstrikeCountdown <= 0)
            {
                lightningstrikeCountdown = 0;
                //Shoots a lightningstrike from above towards the moueses position
                if (keyState.IsKeyDown(Keys.R) && lightning == true && shooting == false)
                {
                    shooting = true;
                    animator.PlayAnimation("CastLight" + direction);
                }
                if (keyState.IsKeyUp(Keys.R))
                {
                    lightning = true;
                }
            }
            if(animator.CurrentIndex >= 12 && shooting == true && animator.AnimationName == "CastLight"+direction)
            {
                director = new Director(new LightningStrikeBuilder());
                GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(mouseState.X - 51, -956)));
                lightning = false;
                shooting = false;
                lightningstrikeCountdown = lightningstrikCooldown;
            }
        }

        private void ShootFireball(MouseState mouseState)
        {
            if (fireballCountdown > 0)
            {
                fireballCountdown -= GameWorld.DeltaTime;
            }

            if (fireballCountdown <= 0)
            {
                fireballCountdown = 0;
                //Shoots a fireball towards the moueses position at the given time the button has been pressed
                if (mouseState.RightButton == ButtonState.Pressed && fireball == true && shooting == false)
                {
                    shooting = true;
                    animator.PlayAnimation("CastFire" + direction);
                }
                if (mouseState.RightButton == ButtonState.Released)
                {
                    fireball = true;
                }
            }
            if(animator.CurrentIndex >= 12 && shooting == true && animator.AnimationName == "CastFire" + direction)
            {
                director = new Director(new FireballBuilder());
                //Updates the fireballs spawn position
                GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(transform.Position.X + 10, transform.Position.Y + 20)));
                shooting = false;
                fireball = false;
                fireballCountdown = fireballCooldown;
            }
        }

        private void ShiftToCompanion(KeyboardState keyState)
        {
            if (keyState.IsKeyDown(Keys.Space) && shiftControle == true)
            {
                Companion.CompanionControle = true;
                shiftControle = false;
            }
            if (keyState.IsKeyUp(Keys.Space))
            {
                shiftControle = true;
            }
        }
        private void Jump(KeyboardState keyState, Vector2 translation)
        {
            if (keyState.IsKeyDown(Keys.W) && hasJumped == false)
            {
                jumpSound.Play();
                translation.Y -= 5f;
                velocity.Y = -5f;
                hasJumped = true;
            }
        }
        private void PlayerController(KeyboardState keyState, Vector2 translation, MouseState mouseState)
        {
            if (Companion.CompanionControle == false)
            {
                if(animator.AnimationName != "CastFire" + direction && animator.AnimationName != "CastLight" + direction && animator.AnimationName != "Attack"+direction)
                {
                    if (keyState.IsKeyDown(Keys.D))
                    {
                        translation += new Vector2(1, 0);
                        direction = "Right";
                        animator.PlayAnimation("Run" + direction);
                    }
                    if (keyState.IsKeyDown(Keys.A))
                    {
                        translation += new Vector2(-1, 0);
                        direction = "Left";
                        animator.PlayAnimation("Run" + direction);
                    }

                    if (translation.X == 0)
                    {
                        animator.PlayAnimation("Idle" + direction);
                    }
                }
                Jump(keyState, translation);

                ShiftToCompanion(keyState);

                ShootFireball(mouseState);

                ShootLighting(keyState, mouseState);

                CreateShield(keyState);

                Interact(keyState);

                MeleeAttack(mouseState);
            }

            float i = 5;
            velocity.Y += 0.05f * i;
            translation += velocity;

            if (velocity.Y > 10)
            {
                velocity.Y = 10;
            }

            MorphPlayer(keyState);

            transform.Translate(translation * GameWorld.DeltaTime * speed);

        }
        private void MorphPlayer(KeyboardState keyState)
        {

            if (keyState.IsKeyUp(Keys.F))
            {
                Morph.HasMorphed = false;
            }


            if (keyState.IsKeyDown(Keys.F) && Morph.HasMorphed == false)
            {
                Morph.HasMorphed = true;
                director = new Director(new MorphBuilder());
                GameWorld.Instance.AddGameObject(director.Construct(this.transform.Position));

                GameWorld.Instance.RemoveGameObject(this.GameObject);

                foreach (GameObject go in GameWorld.GameObjects)
                {
                    if (go.GetComponent("Companion") != null)
                    {
                        GameWorld.Instance.RemoveGameObject(go);
                    }
                }

            }
        }

        public void PlayerHit(int dmg)
        {
            if (playerCanBeHit == true)
            {
                Health = Health - dmg;
                playerCanBeHit = false;
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
                        hasJumped = false;
                        if (velocity.Y > 0)
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
                        hasJumped = false;
                        if (velocity.Y > 0)
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
                        hasJumped = false;
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

                //                             Old Collision !!![PLEASE DONT REMOVE]!!!


                //if (collider.CollisionBox.Intersects(other.TopLine) && collider.CollisionBox.Intersects(other.BottomLine))
                //{
                //    if (collider.CollisionBox.Intersects(other.RightLine))
                //    {
                //        Vector2 position = GameObject.Transform.Position;
                //        position.Y = other.CollisionBox.X + other.CollisionBox.Width;
                //        GameObject.Transform.Position = position;
                //    }
                //    if (collider.CollisionBox.Intersects(other.LeftLine))
                //    {
                //        Vector2 position = GameObject.Transform.Position;
                //        position.X = other.CollisionBox.X - collider.CollisionBox.Width;
                //        GameObject.Transform.Position = position;
                //    }
                //}
                //else if (collider.CollisionBox.Intersects(other.TopLine))
                //{
                //    Vector2 position = GameObject.Transform.Position;
                //    position.Y = other.CollisionBox.Y - collider.CollisionBox.Height;
                //    GameObject.Transform.Position = position;
                //    hasJumped = false;
                //    velocity.Y = 0;
                //}
                //if (collider.CollisionBox.Intersects(other.BottomLine))
                //{
                //    Vector2 position = GameObject.Transform.Position;
                //    position.Y = other.CollisionBox.Y + other.CollisionBox.Height;
                //    GameObject.Transform.Position = position;
                //    velocity.Y = 0;
                //}
            }
            if (other.GameObject.GetComponent("Lever") != null)
            {
                canInteract = true;
                lastknownLever = (Lever)other.GameObject.GetComponent("Lever");
            }
            if(other.GameObject.GetComponent("NonSolidPlatform") != null)
            {
                Collider collider = (Collider)GameObject.GetComponent("Collider");
                 
                if (collider.CollisionBox.Intersects(other.TopLine))
                {
                    if (velocity.Y > 0)
                    {
                        int top = Math.Max(collider.CollisionBox.Top, other.CollisionBox.Top);
                        int left = Math.Max(collider.CollisionBox.Left, other.CollisionBox.Left);
                        int width = Math.Min(collider.CollisionBox.Right, other.CollisionBox.Right) - left;
                        int height = Math.Min(collider.CollisionBox.Bottom, other.CollisionBox.Bottom) - top;
                        if (width > height)
                        {
                            if (collider.CollisionBox.Y + collider.CollisionBox.Height - 20 < other.TopLine.Y)
                            {
                                Vector2 position = GameObject.Transform.Position;
                                position.Y = other.CollisionBox.Y - collider.CollisionBox.Height;
                                GameObject.Transform.Position = position;
                                hasJumped = false;
                                velocity.Y = 0;
                            }
                        }
                    }
                }
            }
        }
        public void OnCollisionExit(Collider other)
        {

        }

    }
}
