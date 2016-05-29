using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardLizard
{
    class HealthGlobe : Component, IUpdateable, ICollisionEnter
    {
        private Transform transform;
        private Animator animator;
        private bool canHeal;
        public HealthGlobe(GameObject gameObject) : base(gameObject)
        {
            animator = (Animator)GameObject.GetComponent("Animator");
            transform = gameObject.Transform;
            canHeal = true;
        }

        public void OnCollisionEnter(Collider other)
        {
            if (other.GameObject.GetComponent("Player") != null && canHeal == true)
            {
                canHeal = false;
                Player.Health += 1;
                GameWorld.Instance.RemoveGameObject(this.GameObject);
            }
        }

        public void Update()
        {
            canHeal = true;
        }
    }
}
