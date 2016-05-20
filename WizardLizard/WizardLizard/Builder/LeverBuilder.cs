using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardLizard
{
    class LeverBuilder : IBuilder
    {
        private GameObject gameObject;
        public void BuildGameObject(Vector2 position)
        {
            throw new NotImplementedException();
        }

        public void BuildGameObject(Vector2 position, int frequency)
        {
            GameObject gameObject = new GameObject();
            gameObject.AddComponent(new SpriteRenderer(gameObject, "MoveableBox", 1f));
            gameObject.AddComponent(new Collider(gameObject));
            gameObject.Transform.Position = position;
            gameObject.AddComponent(new Lever(gameObject,frequency));
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
