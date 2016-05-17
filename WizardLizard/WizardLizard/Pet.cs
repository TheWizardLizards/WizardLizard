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
    class Pet : Component, ILoadable, IUpdateable, IAnimateable, ICollisionEnter, ICollisionExit
    {
        private Transform transform;
        private Animator animator;
        private Vector2 velocity;
        private bool hasJumped;
        private static bool petcontrol = false;
        private bool canControle = true;
        private static bool derp = true;
        private int speed = 1;
        private int bo = 0;

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

        public static bool Derp
        {
            get
            {
                return derp;
            }

            set
            {
                derp = value;
            }
        }

        public Pet(GameObject gameObject) : base(gameObject)
        {
            animator = (Animator)GameObject.GetComponent("Animator");
            transform = gameObject.Transform;
        }
        public void LoadContent(ContentManager content)
        {

        }

        public void OnAnimationDone(string animationName)
        {
            throw new NotImplementedException();
        }

        public void OnCollisionEnter(Collider other)
        {
            throw new NotImplementedException();
        }

        public void OnCollisionExit(Collider other)
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            KeyboardState keyState = Keyboard.GetState();
            Vector2 translation = Vector2.Zero;

            if (Petcontrol == true)
            {
                speed = 200;
                translation += velocity;
                if (transform.Position.Y >= 200)
                {
                    hasJumped = false;
                }
                if (keyState.IsKeyDown(Keys.W) && hasJumped == false)
                {
                    translation.Y -= 10f;
                    velocity.Y = -10f;
                    hasJumped = true;
                }
                if (hasJumped == true)
                {
                    float i = 5;
                    velocity.Y += 0.15f * i;
                }

                if (hasJumped == false)
                {
                    velocity.Y = 0f;
                }
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
                if (keyState.IsKeyDown(Keys.Space) && canControle == true)
                {

                    Pet.Petcontrol = false;
                    canControle = false;
                }
                if (keyState.IsKeyUp(Keys.Space))
                {
                    canControle = true;
                }
            }
            else
            {
                speed = 5;
                Vector2 playerPos = new Vector2(GameWorld.PlayerPos.X + 98, GameWorld.PlayerPos.Y + 150);
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
            transform.Translate(translation * GameWorld.DeltaTime * speed);

        }
    }
}
