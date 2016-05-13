using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace WizardLizard
{
    class Player : Component, ILoadable, IUpdateable, IAnimateable, ICollisionEnter, ICollisionExit
    {
        private Transform transform;
        private Vector2 position;
        private Vector2 velocity;
        private Animator animator;
        private int john;
        private int speed = 200;
        bool hasJumped;

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
            if (Pet.Petcontrol == false)
            {
                if (keyState.IsKeyDown(Keys.W))
                {

                }

                if (keyState.IsKeyDown(Keys.D))
                {
                    translation += new Vector2(1, 0);
                    john = 1;
                }
                if (keyState.IsKeyDown(Keys.A))
                {
                    translation += new Vector2(-1, 0);
                    john = 1;

                }
                if (keyState.IsKeyDown(Keys.S))
                {
                    translation += new Vector2(0, 1);
                    john = 1;

                }
                if (keyState.IsKeyDown(Keys.Space) && john == 1)
                {
                    john = 0;
                    Pet.Petcontrol = true;
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
            }

            //position += velocity;
            //if 
            //(keyState.IsKeyDown(Keys.A)) velocity.X = 3f;
            //else if 
            //(keyState.IsKeyDown(Keys.D)) velocity.X = -3f;
            //else
            //velocity.X = 0f;

            //if (keyState.IsKeyDown(Keys.W) && hasJumped == false)
            //{
            //    position.Y -= 10f;
            //    velocity.Y = -10f;
            //    hasJumped = true;
            //}
            //if (hasJumped == true)
            //{
            //    float i = 1;
            //    velocity.Y += 0.15f * i;
            //}
            //if (position.Y  >= 600)
            //{
            //    hasJumped = false;
            //}

            //if (hasJumped == false)
            //{
            //    velocity.Y = 0f;
            //}
            transform.Translate(translation * GameWorld.DeltaTime * speed);
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


    }
}
