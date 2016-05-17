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
        private Vector2 velocity;
        private Animator animator;
        private int john;
        private int speed = 200;
        private bool hasJumped;
        private Director director;

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
                if (keyState.IsKeyDown(Keys.F))
                {
                   director = new Director(new MorphBuilder());
                    GameWorld.GameObjects.Add(director.Construct(new Vector2(10, 10)));
                   
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
