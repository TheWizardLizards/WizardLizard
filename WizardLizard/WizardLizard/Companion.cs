using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace WizardLizard
{
    class Companion : Component, ILoadable, IUpdateable, IAnimateable, ICollisionEnter, ICollisionExit
    {
        private Transform transform;
        private Animator animator;
        private Vector2 velocity;
        private bool hasJumped;
        private static bool petcontrol;
        private static bool roar;
        private bool canControle;
        private int speed;
        private bool canInteract;
        private bool haveInteracted;
        private Director director;

        public static bool Petcontrol
        {
            get
            {
                return petcontrol;
            }

            set
            {
                petcontrol = value;
            }
        }

        public static bool Roar
        {
            get
            {
                return roar;
            }

            set
            {
                roar = value;
            }
        }

        public Companion(GameObject gameObject) : base(gameObject)
        {
            animator = (Animator)GameObject.GetComponent("Animator");
            transform = gameObject.Transform;
            petcontrol = false;
            canControle = true;
            speed = 200;
            canInteract = false;
            haveInteracted = true;
            roar = false;
        }
        public void LoadContent(ContentManager content)
        {

        }

        public void OnAnimationDone(string animationName)
        {
            throw new NotImplementedException();
        }
        public void ControlePet(KeyboardState keyState, Vector2 translation)
        {
            speed = 200;
            if (keyState.IsKeyDown(Keys.W) && hasJumped == false)
            {
                translation.Y -= 10f;
                velocity.Y = -10f;
                hasJumped = true;
            }
            float i = 5;
            velocity.Y += 0.15f * i;
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
            if (keyState.IsKeyDown(Keys.R) && roar == false)
            {
                roar = true;
            }
            if (keyState.IsKeyUp(Keys.R) && roar == true)
            {
                roar = false;
            }
            translation += velocity;
            if (keyState.IsKeyDown(Keys.Space) && canControle == true)
            {

                Companion.Petcontrol = false;
                canControle = false;
            }
            if (keyState.IsKeyUp(Keys.Space))
            {
                canControle = true;
            }
            if (keyState.IsKeyDown(Keys.E) && canInteract == true && haveInteracted == true)
            {
                director = new Director(new FireballBuilder());
                GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(transform.Position.X, transform.Position.Y)));
                haveInteracted = false;
                canInteract = false;
            }
            if (keyState.IsKeyUp(Keys.E))
            {
                haveInteracted = true;
            }
            transform.Translate(translation * GameWorld.DeltaTime * speed);
        }
        public void FollowPlayer()
        {
            speed = 5;
            Vector2 playerPos = new Vector2(GameWorld.PlayerPos.X - 25, GameWorld.PlayerPos.Y + 106);
            Vector2 petPos = new Vector2(transform.Position.X, transform.Position.Y);
            Vector2 direction = playerPos - petPos;
            if (speed > direction.Length())
            {
                transform.Position = petPos;
            }
            else
            {
                direction.Normalize();
                transform.Position += direction * speed;
            }
        }
        public void Update()
        {
            KeyboardState keyState = Keyboard.GetState();

            Vector2 translation = Vector2.Zero;

            if (Petcontrol == true)
            {
                ControlePet(keyState, translation);
            }
            else
            {
                FollowPlayer();
            }


        }

        public void OnCollisionEnter(Collider other)
        {
            if (other.GameObject.GetComponent("Lever") != null)
            {
                canInteract = true;
            }

            if (Petcontrol == true)
            {
                if (other.GameObject.GetComponent("SolidPlatform") != null)
                {
                    Collider collider = (Collider)GameObject.GetComponent("Collider");
                    if (collider.CollisionBox.Intersects(other.TopLine) && collider.CollisionBox.Intersects(other.BottomLine))
                    {
                        if (collider.CollisionBox.Intersects(other.RightLine))
                        {
                            Vector2 position = GameObject.Transform.Position;
                            position.Y = other.CollisionBox.X + other.CollisionBox.Width + 1;
                            GameObject.Transform.Position = position;
                        }
                        if (collider.CollisionBox.Intersects(other.LeftLine))
                        {
                            Vector2 position = GameObject.Transform.Position;
                            position.X = other.CollisionBox.X - collider.CollisionBox.Width - 1;
                            GameObject.Transform.Position = position;
                        }
                    }
                    else if (collider.CollisionBox.Intersects(other.TopLine))
                    {
                        Vector2 position = GameObject.Transform.Position;
                        position.Y = other.CollisionBox.Y - collider.CollisionBox.Height - 1;
                        GameObject.Transform.Position = position;
                        hasJumped = false;
                        velocity.Y = 0;
                    }
                    if (collider.CollisionBox.Intersects(other.BottomLine))
                    {
                        Vector2 position = GameObject.Transform.Position;
                        position.Y = other.CollisionBox.Y + other.CollisionBox.Height + 1;
                        GameObject.Transform.Position = position;
                        velocity.Y = 0;
                    }
                }
            }
        }

        public void OnCollisionExit(Collider other)
        {

        }
    }
}
