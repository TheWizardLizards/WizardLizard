using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace WizardLizard
{
    public class NonSolidPlatformBuilder : IBuilder
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

        }

        public void BuildGameObject(Vector2 position, int frequency, string spriteName)
        {
            throw new NotImplementedException();
        }

        public void BuildGameObject(Vector2 position, int width, int height)
        {
            GameObject gameObject = new GameObject();
            gameObject.Transform.Position = position;
            gameObject.AddComponent(new NonSolidPlatform(gameObject));
            gameObject.AddComponent(new Collider(gameObject, width, height));
            this.gameObject = gameObject;
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
