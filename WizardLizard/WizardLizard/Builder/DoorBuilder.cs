using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace WizardLizard
{
    class DoorBuilder : IBuilder
    {
        private GameObject gameObject;

        public void BuildGameObject(Vector2 position)
        {
            throw new NotImplementedException();
        }

        public void BuildGameObject(Vector2 position, int frequency)
        {
            GameObject gameObject = new GameObject();
            gameObject.AddComponent(new SpriteRenderer(gameObject, "PlayerShield", 1f));
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

        public GameObject GetResult()
        {
            return gameObject;
        }
    }
}
