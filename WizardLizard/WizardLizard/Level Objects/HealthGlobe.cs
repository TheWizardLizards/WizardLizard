using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace WizardLizard
{
    public class HealthGlobe : Component, IUpdateable, ICollisionEnter, ILoadable
    {
        private SoundEffect healthUpSound;
        private Transform transform;
        private Animator animator;
        private bool canHeal;
        public HealthGlobe(GameObject gameObject) : base(gameObject)
        {
            animator = (Animator)GameObject.GetComponent("Animator");
            transform = gameObject.Transform;
            canHeal = true;
        }

        public void LoadContent(ContentManager content)
        {
            healthUpSound = content.Load<SoundEffect>("HealthUpSound");
        }

        public void OnCollisionEnter(Collider other)
        {
            if (other.GameObject.GetComponent("Player") != null && canHeal == true)
            {
                if (Player.Health < 6)
                {
                    canHeal = false;
                    Player.Health += 1;
                    healthUpSound.Play();
                    GameWorld.Instance.RemoveGameObject(this.GameObject);
                }
            }
        }

        public void Update()
        {
            canHeal = true;
        }

    }
}
