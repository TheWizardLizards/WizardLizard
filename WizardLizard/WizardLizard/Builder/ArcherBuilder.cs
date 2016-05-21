using System;
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
            gameObject.AddComponent(new Collider(gameObject));
            gameObject.Transform.Position = position;
            gameObject.AddComponent(new Archer(gameObject));
            this.gameObject = gameObject;
        }

        public void BuildGameObject(Vector2 position, int frequency)
        {
            
        }

        public void BuildGameObject(Vector2 position, int width, int height)
        {
            throw new NotImplementedException();
        }

        public GameObject GetResult()
        {
            return gameObject;
        }
    }
}
