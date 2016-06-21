using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace WizardLizard
{
    public class Animator : Component, IUpdateable
    {
        private SpriteRenderer spriteRenderer;
        private int currentIndex = 0;
        private float timeElapsed;
        private float fps;
        private string animationName;
        private Dictionary<string, Animation> animations = new Dictionary<string, Animation>();
        private Rectangle[] rectangles;
        private GameObject gameObject;

        public int CurrentIndex
        {
            get
            {
                return currentIndex;
            }

            set
            {
                currentIndex = value;
            }
        }

        public string AnimationName
        {
            get
            {
                return animationName;
            }

            set
            {
                animationName = value;
            }
        }

        public Animator(GameObject gameObject) : base(gameObject)
        {
            this.gameObject = gameObject;
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
            if (this.AnimationName != animationName)
            {
                //Sets the rectangles
                this.rectangles = animations[animationName].Rectangles;
                //Resets the rectangle
                this.spriteRenderer.Rectangle = rectangles[0];
                //Sets the offset
                this.spriteRenderer.Offset = animations[animationName].Offset;
                //Sets the animation name
                this.AnimationName = animationName;
                //Sets the fps
                this.fps = animations[animationName].Fps;
                //Resets the animation
                timeElapsed = 0;
                CurrentIndex = 0;
            }

        }
        public void Update()
        {
            timeElapsed += GameWorld.DeltaTime;

            CurrentIndex = (int)(timeElapsed * fps);

            if (CurrentIndex > rectangles.Length - 1)
            {
                GameObject.OnAnimationDone(AnimationName);
                timeElapsed = 0;
                CurrentIndex = 0;
            }
            spriteRenderer.Rectangle = rectangles[CurrentIndex];
        }
    }
}
