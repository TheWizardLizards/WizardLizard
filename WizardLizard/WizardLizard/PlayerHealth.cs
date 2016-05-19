using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace WizardLizard
{
    class PlayerHealth : Component, IUpdateable, ILoadable, IAnimateable
    {
        private Animator animator;
        public PlayerHealth(GameObject gameObject) : base(gameObject)
        {
            animator = new Animator(gameObject);
            //animator = (Animator)GameObject.GetComponent("Animator");

        }
        public void LoadContent(ContentManager content)
        {
            CreateAnimations();
        }

        public void OnAnimationDone(string animationName)
        {
        }

        public void Update()
        {
            if (Player.Health == 4)
            {
                animator.PlayAnimation("Healthbar4");
            }
            if (Player.Health == 3)
            {
                animator.PlayAnimation("Healthbar3");
            }
            if (Player.Health == 2)
            {
                animator.PlayAnimation("Healthbar2");
            }
            if (Player.Health == 1)
            {
                animator.PlayAnimation("Healthbar1");
            }
            if (Player.Health == 0)
            {
                animator.PlayAnimation("Healthbar0");
            }
        }
        public void CreateAnimations()
        {
            animator.CreateAnimation("Healthbar4", new Animation(1, 0, 0, 158, 38, 6, Vector2.Zero));
            animator.CreateAnimation("Healthbar3", new Animation(1, 43, 0, 158, 38, 6, Vector2.Zero));
            animator.CreateAnimation("Healthbar2", new Animation(1, 86, 0, 158, 38, 6, Vector2.Zero));
            animator.CreateAnimation("Healthbar1", new Animation(1, 128, 0, 158, 38, 6, Vector2.Zero));
            animator.CreateAnimation("Healthbar0", new Animation(1, 169, 0, 158, 38, 6, Vector2.Zero));
            animator.PlayAnimation("Healthbar4");
        }
    }
}
