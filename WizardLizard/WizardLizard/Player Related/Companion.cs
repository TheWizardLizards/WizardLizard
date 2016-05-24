﻿using System;
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
        private bool shiftControle;
        private int speed = 200;
        private bool canInteract;
        private bool haveInteracted;
        private Director director;
        private Lever lastknownLever;

        public static bool companionControle
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
            shiftControle = true;
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
            if (keyState.IsKeyUp(Keys.Space))
            {
                shiftControle = true;
            }
            if (keyState.IsKeyDown(Keys.Space) && shiftControle == true)
            {
                Companion.companionControle = false;
                shiftControle = false;
            }
            if (keyState.IsKeyDown(Keys.E) && canInteract == true && haveInteracted == true)
            {
                if (lastknownLever != null)
                {
                    lastknownLever.interaction(this.GameObject);
                }
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
            Vector2 playerPos = new Vector2(GameWorld.PlayerPos.X - 20, GameWorld.PlayerPos.Y + 50);
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

            if (companionControle == true)
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

            if (companionControle == true)
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
                }
            }

            if (other.GameObject.GetComponent("Lever") != null)
            {
                canInteract = true;
                lastknownLever = (Lever)other.GameObject.GetComponent("Lever");
            }
        }

        public void OnCollisionExit(Collider other)
        {

        }
    }
}
