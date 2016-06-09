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
            animator = (Animator)GameObject.GetComponent("Animator");

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
            if (Player.Health == 6) animator.PlayAnimation("Healthbar6");

            if (Player.Health == 5) animator.PlayAnimation("Healthbar5");

            if (Player.Health == 4) animator.PlayAnimation("Healthbar4");

            if (Player.Health == 3) animator.PlayAnimation("Healthbar3");

            if (Player.Health == 2) animator.PlayAnimation("Healthbar2");

            if (Player.Health == 1) animator.PlayAnimation("Healthbar1");

            if (Player.Health == 0) animator.PlayAnimation("Healthbar0");

        }
        public void CreateAnimations()
        {
            animator.CreateAnimation("Healthbar6", new Animation(1, 0, 0, 190, 30, 6, Vector2.Zero));
            animator.CreateAnimation("Healthbar5", new Animation(1, 31, 0, 190, 30, 6, Vector2.Zero));
            animator.CreateAnimation("Healthbar4", new Animation(1, 62, 0, 190, 30, 6, Vector2.Zero));
            animator.CreateAnimation("Healthbar3", new Animation(1, 93, 0, 190, 30, 6, Vector2.Zero));
            animator.CreateAnimation("Healthbar2", new Animation(1, 124, 0, 190, 30, 6, Vector2.Zero));
            animator.CreateAnimation("Healthbar1", new Animation(1, 155, 0, 190, 30, 6, Vector2.Zero));
            animator.CreateAnimation("Healthbar0", new Animation(1, 186, 0, 190, 30, 6, Vector2.Zero));
            animator.PlayAnimation("Healthbar6");
        }
    }
}
