﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace WizardLizard
{
    public class Lever : Component, ILoadable, IUpdateable, IAnimateable, ICollisionEnter, ICollisionExit
    {
        private SoundEffect leverSound, magicDoorSound;
        private Transform transform;
        private Animator animator;
        private int frequency;

        public int Frequency
        {
            get { return frequency; }
            set { frequency = value; }
        }

        public Lever(GameObject gameObject, int frequency) : base(gameObject)
        {
            Frequency = frequency;
            animator = (Animator)GameObject.GetComponent("Animator");
            transform = gameObject.Transform;
        }
        public void LoadContent(ContentManager content)
        {
            leverSound = content.Load<SoundEffect>("LeverUsed");
            magicDoorSound = content.Load<SoundEffect>("MagicDoorSound");
            CreateAnimations();
        }

        public void CreateAnimations()
        {
            animator.CreateAnimation("Idle", new Animation(1, 0, 0, 150, 52, 1, Vector2.Zero));
            animator.CreateAnimation("Activated", new Animation(4, 0, 0, 150, 52, 8, Vector2.Zero));
            animator.PlayAnimation("Idle");
        }

        public void Update()
        {

        }

        public void Interaction(GameObject target)
        {
            Collider other = (Collider)target.GetComponent("Collider");
            Collider me = (Collider)this.GameObject.GetComponent("Collider");
            if (me.CollisionBox.Intersects(other.CollisionBox))
            {
                if (frequency > 50)
                {
                    if (GameWorld.spawnList.ContainsKey(frequency))
                    {
                        animator.PlayAnimation("Activated");
                        leverSound.Play();
                        GameWorld.ObjectToAdd.Add(GameWorld.spawnList[frequency]);
                        GameWorld.spawnList.Remove(frequency);
                    }
                }
                else
                {
                    foreach (GameObject go in GameWorld.GameObjects)
                    {
                        if (go.GetComponent("Door") != null)
                        {
                            Door door = (Door)go.GetComponent("Door");
                            if (door.Frequency == Frequency)
                            {
                                animator.PlayAnimation("Activated");
                                magicDoorSound.Play();
                                leverSound.Play();
                                GameWorld.ObjectsToRemove.Add(go);
                            }
                        }
                    }
                }
            }
        }

        public void OnAnimationDone(string animationName)
        {
            animator.PlayAnimation("Idle");
        }

        public void OnCollisionEnter(Collider other)
        {
           
        }

        public void OnCollisionExit(Collider other)
        {

        }

    }
}
