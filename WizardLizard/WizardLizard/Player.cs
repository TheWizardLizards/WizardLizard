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
    class Player : Component, ILoadable, IUpdateable, IDrawable, IAnimateable, ICollisionEnter, ICollisionExit 
    {
      
        Vector2 position;
        Vector2 velocity;
        bool hasJumped;
        
        public Player()
        {
            
          
            hasJumped = true;
        }


        public void LoadContent(ContentManager content)
        {
            
        }
        public void Update()
        {
            position += velocity;
            if (Keyboard.GetState().IsKeyDown(Keys.A)) velocity.X = 3f;
            else if (Keyboard.GetState().IsKeyDown(Keys.D)) velocity.X = -3f; else velocity.X = 0f;

            if (Keyboard.GetState().IsKeyDown(Keys.Space) && hasJumped == false)
            {
                position.Y -= 10f;
                velocity.Y = -10f;
                hasJumped = true;
            }
            if (hasJumped == true)
            {
                float i = 1;
                velocity.Y += 0.15f * i;
            }
            if (position.Y  >= 600)
                hasJumped = false;

            if (hasJumped == false)
                velocity.Y = 0f;
        }

        public void Draw(SpriteBatch spriteBatch)
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

       
    }
}
