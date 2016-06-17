using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace WizardLizard
{
    public class PlayerHealth : Component, IUpdateable, ILoadable, IAnimateable
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
            switch (Player.Health)
            {
                case 1:
                    animator.PlayAnimation("Healthbar1");
                    break;
                case 2:
                    animator.PlayAnimation("Healthbar2");
                    break;
                case 3:
                    animator.PlayAnimation("Healthbar3");
                    break;
                case 4:
                    animator.PlayAnimation("Healthbar4");
                    break;
                case 5:
                    animator.PlayAnimation("Healthbar5");
                    break;
                case 6:
                    animator.PlayAnimation("Healthbar6");
                    break;
                case 0:
                    animator.PlayAnimation("Healthbar0");
                    break;
            }
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
