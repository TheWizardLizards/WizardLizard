using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizardLizard
{
    class Animator : Component, IUpdateable
    {
        private SpriteRenderer spriteRenderer;
        private int currentIndex = 0;
        private float timeElapsed;
        private float fps;
        private string animationName;
        private Dictionary<string, Animation> animations = new Dictionary<string, Animation>();
        private Rectangle[] rectangles;
        private GameObject gameObject;
        public Animator(GameObject gameObject) : base(gameObject)
        {
            animations = new Dictionary<string, Animation>();
            fps = 8;
            this.spriteRenderer = (SpriteRenderer)gameObject.GetComponent("SpriteRenderer");
        }
        public void CreateAnimation(string name, Animation animation)
        {
            animations.Add(name, animation);
        }
        public void PlayAnimation(string animationName)
        {
            //Checks if it’s a new animation
            if (this.animationName != animationName)
            {
                //Sets the rectangles
                this.rectangles = animations[animationName].Rectangles;
                //Resets the rectangle
                this.spriteRenderer.Rectangle = rectangles[0];
                //Sets the offset
                this.spriteRenderer.Offset = animations[animationName].Offset;
                //Sets the animation name
                this.animationName = animationName;
                //Sets the fps
                this.fps = animations[animationName].Fps;
                //Resets the animation
                timeElapsed = 0;
                currentIndex = 0;
            }

        }
        public void Update()
        {
            timeElapsed += GameWorld.DeltaTime;

            currentIndex = (int)(timeElapsed * fps);

            if (currentIndex > rectangles.Length - 1)
            {
                GameObject.OnAnimationDone(animationName);
                timeElapsed = 0;
                currentIndex = 0;
            }
            spriteRenderer.Rectangle = rectangles[currentIndex];
        }
    }
}
