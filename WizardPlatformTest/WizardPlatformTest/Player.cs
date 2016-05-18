using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace WizardPlatformTest
{
    class Player : Component, ILoadable, IUpdateable, IAnimateable, ICollisionEnter, ICollisionExit
    {
        private Transform transform;
        private Vector2 velocity;
        private Animator animator;
        private int speed = 200;
        private bool hasJumped;

        public Player(GameObject gameObject) : base(gameObject)
        {
            animator = (Animator)GameObject.GetComponent("Animator");
            transform = gameObject.Transform;

            hasJumped = true;
        }


        public void LoadContent(ContentManager content)
        {

        }
        public void Update()
        {
            KeyboardState keyState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();
            Vector2 translation = Vector2.Zero;
            bool spaceHolding = false;
            if (Pet.Petcontrol == false)
            {
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
                translation += velocity;
                if (keyState.IsKeyDown(Keys.W) && hasJumped == false)
                {
                    translation.Y -= 10f;
                    velocity.Y = -10f;
                    hasJumped = true;
                }
            }
                if (keyState.IsKeyDown(Keys.Space) && spaceHolding == false)
                {
                    Pet.Petcontrol = true;
                }

                float i = 5;
                velocity.Y += 0.15f * i;

                if (velocity.Y > 10)
                {
                    velocity.Y = 10;
                }

                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    //Attack
                }
                //if (mouseState.RightButton == ButtonState.Pressed)
                //{
                //    director = new Director(new FireballBuilder());
                //    GameWorld.ToAdd.Add(director.Construct(new Vector2(transform.Position.X, transform.Position.Y)));
                //}
                //if (mouseState.RightButton == ButtonState.Released && fireballPower > 0)
                //{
                //}

            transform.Translate(translation * GameWorld.DeltaTime * speed);
        }

        public void OnAnimationDone(string animationName)
        {

        }

        public void OnCollisionEnter(Collider other)
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

                if (collider.CollisionBox.Intersects(other.TopLine))
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

        public void OnCollisionExit(Collider other)
        {

        }


    }
}
