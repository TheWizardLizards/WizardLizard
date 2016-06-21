namespace WizardLizard
{
    public class NonSolidPlatform : Component
    {
        private Transform transform;
        private Animator animator;
        public NonSolidPlatform(GameObject gameObject) : base(gameObject)
        {
            animator = (Animator)GameObject.GetComponent("Animator");
            transform = gameObject.Transform;
        }
    }
}
