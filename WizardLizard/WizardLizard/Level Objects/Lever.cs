using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardLizard
{
    class Lever : Component, ILoadable, IUpdateable, IAnimateable, ICollisionEnter, ICollisionExit
    {
        private Transform transform;
        private Animator animator;
        private int frequency;

        public int Frequency
        {
            get
            {
                return frequency;
            }

            set
            {
                frequency = value;
            }
        }

        public Lever(GameObject gameObject, int frequency) : base(gameObject)
        {
            Frequency = frequency;
            animator = (Animator)GameObject.GetComponent("Animator");
            transform = gameObject.Transform;
        }
        public void LoadContent(ContentManager content)
        {

        }

        public void Update()
        {

        }

        public void interaction(Player player)
        {
            Collider other = (Collider)player.GameObject.GetComponent("Collider");
            Collider me = (Collider)this.GameObject.GetComponent("Collider");
            if (me.CollisionBox.Intersects(other.CollisionBox))
            {
                if (frequency > 50)
                {
                    if (GameWorld.spawnList.ContainsKey(frequency))
                    {
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
                                GameWorld.ObjectsToRemove.Add(go);
                            }
                        }
                    }
                }
            }
        }

        public void OnAnimationDone(string animationName)
        {
            throw new NotImplementedException();
        }

        public void OnCollisionEnter(Collider other)
        {
           
        }

        public void OnCollisionExit(Collider other)
        {

        }

    }
}
