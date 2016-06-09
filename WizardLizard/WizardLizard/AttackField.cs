using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;

namespace WizardLizard
{
    public class AttackField : Component, ILoadable, IUpdateable, ICollisionEnter, ICollisionExit
    {
        private Transform transform;
        private string attacker;
        private float timer;
        private int deadline;
        private bool hit = false;

        public string Attacker
        {
            get
            {
                return attacker;
            }

            set
            {
                attacker = value;
            }
        }

        public AttackField(GameObject gameObject, string attacker) : base(gameObject)
        {
            Attacker = attacker;
            transform = gameObject.Transform;
        }
        public void LoadContent(ContentManager content)
        {
            timer = 0;
            if (Attacker == "Orc")
            {
                // (frames involved / totalframes) / (FPS / totalframes) gives us time in seconds
                deadline = (5 / 22)/(22/22);
            }
            if(Attacker == "Goblin")
            {
                // (frames involved / totalframes) / (FPS / totalframes) gives us time in seconds
                deadline = (3 / 9) / (16 / 9);
            }
            if(Attacker == "Player")
            {
                // (frames involved / totalframes) / (FPS / totalframes) gives us time in seconds
                deadline = (4 / 19) / (57 / 19);
            }
        }

        public void OnCollisionEnter(Collider other)
        {
            if(hit == false)
            {
                if (Attacker == "Player")
                {
                    if (other.GameObject.GetComponent("Orc") != null)
                    {
                        Orc orc = (Orc)other.GameObject.GetComponent("Orc");
                        orc.TakeDamage(1);
                        GameWorld.ObjectsToRemove.Add(this.GameObject);
                        hit = true;
                    }
                    if (other.GameObject.GetComponent("Goblin") != null)
                    {
                        Goblin goblin = (Goblin)other.GameObject.GetComponent("Goblin");
                        goblin.TakeDamage(1);
                        GameWorld.ObjectsToRemove.Add(this.GameObject);
                        hit = true;
                    }
                    if (other.GameObject.GetComponent("Archer") != null)
                    {
                        Archer archer = (Archer)other.GameObject.GetComponent("Archer");
                        archer.TakeDamage(1);
                        GameWorld.ObjectsToRemove.Add(this.GameObject);
                        hit = true;
                    }
                }
                if (Attacker == "Orc" || Attacker == "Goblin")
                {
                    if (other.GameObject.GetComponent("Player") != null)
                    {
                        Player player = (Player)other.GameObject.GetComponent("Player");
                        if (Attacker == "Orc")
                        {
                            player.PlayerHit(4);
                            GameWorld.ObjectsToRemove.Add(this.GameObject);
                            hit = true;
                        }
                        else if (Attacker == "Goblin")
                        {
                            player.PlayerHit(1);
                            GameWorld.ObjectsToRemove.Add(this.GameObject);
                            hit = true;
                        }
                    }
                }
            }
        }

        public void OnCollisionExit(Collider other)
        {

        }

        public void Update()
        {
            timer += GameWorld.DeltaTime;
            if(deadline <= timer)
            {
                GameWorld.ObjectsToRemove.Add(this.GameObject);
            }
        }
    }
}
