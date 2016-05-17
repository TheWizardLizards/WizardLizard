using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardLizard
{
    class Morph : Component, ILoadable, IUpdateable, IAnimateable, ICollisionEnter, ICollisionExit
    {
        private Transform transform;
        private Animator animator;
        private Vector2 velocity;
        private bool hasJumped;
        private static bool hasMorphed = false;
        private static bool derp = true;
        private int speed = 1;
        private int bo = 0;

        public static bool HasMorphed
        {
            get
            {
                return hasMorphed;
            }

            set
            {
                hasMorphed = value;
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

        public Morph(GameObject gameObject) : base(gameObject)
        {
            
            animator = (Animator)GameObject.GetComponent("Animator");
            transform = gameObject.Transform;
        }
        public void LoadContent(ContentManager content)
        {

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

        public void Update()
        {
            KeyboardState keyState = Keyboard.GetState();
            Vector2 translation = Vector2.Zero;

            if (HasMorphed == true)
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
                    bo = 1;
                }
                if (keyState.IsKeyDown(Keys.A))
                {
                    translation += new Vector2(-1, 0);
                    bo = 1;
                }
                if (keyState.IsKeyDown(Keys.S))
                {
                    translation += new Vector2(0, 1);
                    bo = 1;
                }
                if (keyState.IsKeyDown(Keys.F) && bo == 1)
                {
                    Morph.HasMorphed = false;
                    bo = 0;
                }

            }
        }
    }
}

