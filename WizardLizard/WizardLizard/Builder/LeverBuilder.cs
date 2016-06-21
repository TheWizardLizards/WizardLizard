using Microsoft.Xna.Framework;
using System;

namespace WizardLizard
{
    public class LeverBuilder : IBuilder
    {
        private GameObject gameObject;
        public void BuildGameObject(Vector2 position)
        {

        }

        public void BuildGameObject(Vector2 position, string spriteName)
        {

        }

        public void BuildGameObject(Vector2 position, int frequency)
        {
            GameObject gameObject = new GameObject();
            gameObject.AddComponent(new SpriteRenderer(gameObject, "LeverSpriteSheet", 1f));
            gameObject.AddComponent(new Animator(gameObject));
            gameObject.AddComponent(new Collider(gameObject));
            gameObject.Transform.Position = position;
            gameObject.AddComponent(new Lever(gameObject,frequency));
            this.gameObject = gameObject;
        }

        public void BuildGameObject(Vector2 position, int frequency, string spriteName)
        {
            throw new NotImplementedException();
        }

        public void BuildGameObject(Vector2 position, int width, int height)
        {

        }

        public void BuildGameObject(Vector2 position, int width, int height, string creator)
        {
            throw new NotImplementedException();
        }

        public GameObject GetResult()
        {
            return gameObject;
        }
    
    }
}
