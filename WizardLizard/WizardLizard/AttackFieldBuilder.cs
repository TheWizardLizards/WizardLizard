using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardLizard
{
    class AttackFieldBuilder : IBuilder
    {
        private GameObject gameObject;
        public void BuildGameObject(Vector2 position)
        {
            throw new NotImplementedException();
        }

        public void BuildGameObject(Vector2 position, string spriteName)
        {
            throw new NotImplementedException();
        }

        public void BuildGameObject(Vector2 position, int frequency)
        {
            throw new NotImplementedException();
        }

        public void BuildGameObject(Vector2 position, int frequency, string spriteName)
        {
            throw new NotImplementedException();
        }

        public void BuildGameObject(Vector2 position, int width, int height)
        {
            throw new NotImplementedException();
        }

        public void BuildGameObject(Vector2 position, int width, int height, string creator)
        {
            GameObject gameObject = new GameObject();
            gameObject.Transform.Position = position;
            gameObject.AddComponent(new AttackField(gameObject, creator));
            gameObject.AddComponent(new Collider(gameObject, width, height));
            this.gameObject = gameObject;
        }

        public GameObject GetResult()
        {
            return gameObject;
        }
    }
}
