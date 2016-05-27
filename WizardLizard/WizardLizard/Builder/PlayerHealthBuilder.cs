using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace WizardLizard
{
    class PlayerHealthBuilder : IBuilder
    {
        private GameObject gameObject;
        public void BuildGameObject(Vector2 position)
        {
            GameObject gameObject = new GameObject();
            gameObject.AddComponent(new SpriteRenderer(gameObject, "HealthSheet", 1f));
            gameObject.AddComponent(new Animator(gameObject));
            gameObject.Transform.Position = new Vector2(10, 10);
            gameObject.AddComponent(new PlayerHealth(gameObject));
            this.gameObject = gameObject;
        }

        public void BuildGameObject(Vector2 position, string spriteName)
        {
            throw new NotImplementedException();
        }

        public void BuildGameObject(Vector2 position, int frequency)
        {
            throw new NotImplementedException();
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
