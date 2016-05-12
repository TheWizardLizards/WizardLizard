using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace JumpTest
{
    class TestPlayer
    {
        Texture2D texture;
        TestBullet bullet;
        Vector2 position;
        Vector2 velocity;

        bool hasJumped;

        public TestPlayer(Texture2D newTexture, Vector2 newposition)
        {
            texture = newTexture;
            position = newposition;
            hasJumped = true;
            bullet = new TestBullet(new Vector2(10,10));
        }
        public void Update(GameTime gametime)
        {
            position += velocity;
            if (Keyboard.GetState().IsKeyDown(Keys.C))
            {
                Game1.bullets.Add(bullet);
            }
                if (Keyboard.GetState().IsKeyDown(Keys.A)) velocity.X = 3f;
            else if(Keyboard.GetState().IsKeyDown(Keys.D)) velocity.X = -3f; else velocity.X = 0f;

            if(Keyboard.GetState().IsKeyDown(Keys.Space) && hasJumped == false)
            {
                position.Y -= 10f;
                velocity.Y = -10f;
                hasJumped = true;
            }
            if(hasJumped == true)
            {
                float i = 1;
                velocity.Y += 0.15f * i;
            }
            if (position.Y + texture.Height >= 600)
                hasJumped = false;

            if (hasJumped == false)
                velocity.Y = 0f;
        }
        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(texture, position, Color.White);
        }
            
    }
}
