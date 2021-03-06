﻿using Microsoft.Xna.Framework;

namespace WizardLizard
{
    public class Animation
    {
        private float fps;
        private Vector2 offset;
        private Rectangle[] rectangles;

        public float Fps
        {
            get { return fps; }
        }

        public Vector2 Offset
        {
            get            {                return offset;            }
        }

        public Rectangle[] Rectangles
        {
            get { return rectangles; }
        }

        public Animation(int frames, int yPos, int xStratFrame, int widht, int height, float fps, Vector2 offset)
        {
            rectangles = new Rectangle[frames];
            for (int i = 0; i < frames; i++)
            {
                rectangles[i] = new Rectangle((i * widht)+ xStratFrame, yPos, widht, height);
            }


            this.fps = fps;
            this.offset = offset;
        }
    }
}
