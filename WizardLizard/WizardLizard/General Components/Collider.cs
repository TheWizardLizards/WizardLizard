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
        private List<Collider> otherColliders = new List<Collider>();
        private bool doCollisionChecks;
        private Rectangle topLine;
        private Rectangle bottomLine;
        private Rectangle rightLine;
        private Rectangle leftLine;
        private int width;
        private int height;

        public Collider(GameObject gameObject) : base(gameObject)
        {
            GameWorld.Instance.Colliders.Add(this);
        }
        public Collider(GameObject gameObject, int width, int height) : base(gameObject)
        {
            GameWorld.Instance.Colliders.Add(this);
            this.width = width;
            this.height = height;
        }
        public Rectangle CollisionBox
        {
            get
            {
                if(spriterenderer != null)
                {
                    return new Rectangle
                        (
                        (int)(GameObject.Transform.Position.X - spriterenderer.Offset.X),
                        (int)(GameObject.Transform.Position.Y - spriterenderer.Offset.Y),
                        spriterenderer.Rectangle.Width,
                        spriterenderer.Rectangle.Height
                        );
                }else
                {
                    return new Rectangle((int)GameObject.Transform.Position.X, (int)GameObject.Transform.Position.Y, width, height);
                }

            }
        }

        public bool DoCollisionChecks
        {
            set
            {
                doCollisionChecks = value;
            }
        }

        public Rectangle TopLine
        {
            get
            {
                return topLine;
            }

            set
            {
                topLine = value;
            }
        }

        public Rectangle BottomLine
        {
            get
            {
                return bottomLine;
            }

            set
            {
                bottomLine = value;
            }
        }

        public Rectangle RightLine
        {
            get
            {
                return rightLine;
            }

            set
            {
                rightLine = value;
            }
        }

        public Rectangle LeftLine
        {
            get
            {
                return leftLine;
            }

            set
            {
                leftLine = value;
            }
        }

        public void LoadContent(ContentManager content)
        {
            if(GameObject.GetComponent("SpriteRenderer") != null)
            {
                spriterenderer = (SpriteRenderer)GameObject.GetComponent("SpriteRenderer");
            }
            texture2D = content.Load<Texture2D>("CollisionTexture");

        }

        public void Update()
        {
            CheckCollision();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            TopLine = new Rectangle(CollisionBox.X, CollisionBox.Y, CollisionBox.Width, 1);
            BottomLine = new Rectangle(CollisionBox.X, CollisionBox.Y + CollisionBox.Height, CollisionBox.Width, 1);
            RightLine = new Rectangle(CollisionBox.X + CollisionBox.Width, CollisionBox.Y, 1, CollisionBox.Height);
            LeftLine = new Rectangle(CollisionBox.X, CollisionBox.Y, 1, CollisionBox.Height);

            spriteBatch.Draw(texture2D, TopLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(texture2D, BottomLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(texture2D, RightLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(texture2D, LeftLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);

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
                            }
                        }
                        else
                        {
                            if (otherColliders.Contains(this))
                            {
                                otherColliders.Remove(this);
                                GameObject.OnCollisionExit(other);
                            }
                        }
                    }
                }
            }
        }
    }
}
