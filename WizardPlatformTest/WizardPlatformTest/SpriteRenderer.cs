using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizardPlatformTest
{
    class SpriteRenderer : Component, IDrawable, ILoadable 
    {
        private Color color;
        private Rectangle rectangle;
        private Texture2D sprite;
        private Vector2 offset;
        private string spriteName;
        private float layerDepth;

        public Rectangle Rectangle
        {
            get
            {
                return rectangle;
            }
            set
            {
                rectangle = value;
            }
        }

        public Vector2 Offset
        {
            get
            {
                return offset;
            }

            set
            {
                offset = value;
            }
        }

        public Texture2D Sprite
        {
            get
            {
                return sprite;
            }

            set
            {
                sprite = value;
            }
        }

        public Color Color
        {
            get
            {
                return color;
            }

            set
            {
                color = value;
            }
        }

        public SpriteRenderer(GameObject gameObject, string spriteName, float layerDepth) : base (gameObject)
        {
            this.spriteName = spriteName;
            this.layerDepth = layerDepth;
        }
        public void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>(spriteName);
            rectangle = new Rectangle(0,0,sprite.Width,sprite.Height);
            this.color = Color.White;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, GameObject.Transform.Position, rectangle, color, 0, offset, 1, SpriteEffects.None, 1f);
        }
    }
}
