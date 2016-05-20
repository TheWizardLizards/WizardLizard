using Microsoft.Xna.Framework;

namespace WizardLizard
{
    class ArcherBuilder : IBuilder
    {
        private GameObject gameObject;
        public void BuildGameObject(Vector2 position)
        {
            GameObject gameObject = new GameObject();
            gameObject.AddComponent(new SpriteRenderer(gameObject, "archer", 1));
            gameObject.Transform.Position = position;
            gameObject.AddComponent(new Archer(gameObject));
            this.gameObject = gameObject;
        }

        public GameObject GetResult()
        {
            return gameObject;
        }
    }
}
