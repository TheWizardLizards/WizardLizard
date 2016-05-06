using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizardLizard
{
    public class Collider : Component, IDrawable, ILoadable, IUpdateable
    {
        private SpriteRenderer spriterenderer;
        private Texture2D texture2D;
        private Rectangle collisionBox;
        private List<Collider> otherColliders = new List<Collider>();
        private bool doCollisionChecks;

        public Collider(GameObject gameObject) : base(gameObject)
        {
            GameWorld.Instance.Colliders.Add(this);
        }
        public Rectangle CollisionBox
        {
            get
            {
                return new Rectangle
                    (
                    (int)(GameObject.Transform.Position.X - spriterenderer.Offset.X),
                    (int)(GameObject.Transform.Position.Y - spriterenderer.Offset.Y),
                    spriterenderer.Rectangle.Width,
                    spriterenderer.Rectangle.Height
                    );
            }
        }

        public bool DoCollisionChecks
        {
            set
            {
                doCollisionChecks = value;
            }
        }

        public void LoadContent(ContentManager content)
        {
            spriterenderer = (SpriteRenderer)GameObject.GetComponent("SpriteRenderer");
            texture2D = content.Load<Texture2D>("CollisionTexture");

        }

        public void Update()
        {
            CheckCollision();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle topLine = new Rectangle(CollisionBox.X, CollisionBox.Y, CollisionBox.Width, 1);
            Rectangle bottomLine = new Rectangle(CollisionBox.X, CollisionBox.Y + CollisionBox.Height, CollisionBox.Width, 1);
            Rectangle rightLine = new Rectangle(CollisionBox.X + CollisionBox.Width, CollisionBox.Y, 1, CollisionBox.Height);
            Rectangle leftLine = new Rectangle(CollisionBox.X, CollisionBox.Y, 1, CollisionBox.Height);

            spriteBatch.Draw(texture2D, topLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(texture2D, bottomLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(texture2D, rightLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(texture2D, leftLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);

        }

        private void CheckCollision()
        {
            foreach (Collider other in GameWorld.Instance.Colliders)
            {
                if (other != this)
                {
                    if(other != null)
                    {
                        if (CollisionBox.Intersects(other.CollisionBox))
                        {
                            if (!otherColliders.Contains(other))
                            {
                                otherColliders.Add(this);
                                GameObject.OnCollisionEnter(other);
                                other.GameObject.OnCollisionEnter(this);
                            }
                        }
                    }
                    else
                    {
                        if (otherColliders.Contains(this))
                        {
                            otherColliders.Remove(this);
                            GameObject.OnCollisionExit(this);
                        }
                    }
                }
            }
        }
    }
}
