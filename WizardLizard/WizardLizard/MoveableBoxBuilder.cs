using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace WizardLizard
{
    class MoveableBoxBuilder : IBuilder
    {
        private GameObject gameObject;
        public void BuildGameObject(Vector2 position)
        {
            GameObject gameObject = new GameObject();
            gameObject.AddComponent(new SpriteRenderer(gameObject, "Crate01", 1f));
            gameObject.Transform.Position = position;
            gameObject.AddComponent(new SolidPlatform(gameObject));
            gameObject.AddComponent(new MoveableBox(gameObject));
            gameObject.AddComponent(new Collider(gameObject));
            this.gameObject = gameObject;
        }

        public void BuildGameObject(Vector2 position, int frequency)
        {

        }

        public void BuildGameObject(Vector2 position, int width, int height)
        {

        }

        public GameObject GetResult()
        {
            return gameObject;
        }
    }
}
