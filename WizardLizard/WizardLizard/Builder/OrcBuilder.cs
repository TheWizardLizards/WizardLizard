using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
namespace WizardLizard
{
    class OrcBuilder : IBuilder
    {
        private GameObject gameObject;
        public void BuildGameObject(Vector2 position)
        {
            GameObject gameObject = new GameObject();
            gameObject.AddComponent(new SpriteRenderer(gameObject, "orc", 1));
            gameObject.AddComponent(new Collider(gameObject));
            gameObject.Transform.Position = position;
            gameObject.AddComponent(new Orc(gameObject));
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
