using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardLizard
{
    class Button
    {
        Texture2D texture;
        Vector2 position;
        Rectangle rectangle;
        string spriteBlack;
        string spriteRed;
        public Button(Texture2D newTexture, Vector2 newPosition, string spirteOff, string spriteOn)
        {
            texture = newTexture;
            position = newPosition;
            spriteBlack = spirteOff;
            spriteRed = spriteOn;
        }
        
        public bool isClicked;
        public void Update(ContentManager content, MouseState mouse)
        {
            rectangle = new Rectangle((int)position.X, (int)position.Y, 200, 100);
            Rectangle mouseRectangle = new Rectangle(mouse.X, mouse.Y, 1, 1);

            if (mouseRectangle.Intersects(rectangle))
            {
                texture = content.Load<Texture2D>(spriteRed);
                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    isClicked = true;
                }
            }
            else
            {
                texture = content.Load<Texture2D>(spriteBlack);
                isClicked = false;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
        }
    }
}
