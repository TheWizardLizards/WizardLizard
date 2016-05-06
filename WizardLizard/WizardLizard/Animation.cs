using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace WizardLizard
{
    class Animation 
    {
        private float fps;
        private Vector2 offset;
        private Rectangle[] rectangles;

        public float Fps
        {
            get
            {
                return fps;
            }
        }

        public Vector2 Offset
        {
            get
            {
                return offset;
            }
        }

        public Rectangle[] Rectangles
        {
            get
            {
                return rectangles;
            }
        }
        
        public Animation(int frames, int yPos, int xStratFrame, int widht, int height, float fps, Vector2 offset)
        {
            rectangles = new Rectangle[frames];
            fps = 5;
            for (int i = 0; i < frames; i++)
            {
                rectangles[i] = new Rectangle((i + xStratFrame) * widht, yPos, widht, height);
            }

       
            this.fps = fps;
            this.offset = offset;
        }
    }
}
