using System;
using Microsoft.Xna.Framework;

namespace WizardLizard
{
    public class DoorBuilder : IBuilder
    {
        private GameObject gameObject;

        public void BuildGameObject(Vector2 position)
        {
            throw new NotImplementedException();
        }

        public void BuildGameObject(Vector2 position, int frequency)
        {
            GameObject gameObject = new GameObject();
            gameObject.AddComponent(new SpriteRenderer(gameObject, "MagicDoor", 1f));
            gameObject.Transform.Position = position;
            gameObject.AddComponent(new SolidPlatform(gameObject));
            gameObject.AddComponent(new Door(gameObject, frequency));
            gameObject.AddComponent(new Collider(gameObject));
            this.gameObject = gameObject;
        }

        public void BuildGameObject(Vector2 position, string spriteName)
        {
            throw new NotImplementedException();
        }

        public void BuildGameObject(Vector2 position, int frequency, string spriteName)
        {
            GameObject gameObject = new GameObject();
            gameObject.AddComponent(new SpriteRenderer(gameObject, spriteName, 1f));
            gameObject.Transform.Position = position;
            gameObject.AddComponent(new SolidPlatform(gameObject));
            gameObject.AddComponent(new Door(gameObject, frequency));
            gameObject.AddComponent(new Collider(gameObject));
            this.gameObject = gameObject;
        }

        public void BuildGameObject(Vector2 position, int width, int height)
        {
            throw new NotImplementedException();
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
