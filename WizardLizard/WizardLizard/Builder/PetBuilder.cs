using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace WizardLizard
{
    class PetBuilder : IBuilder
    {
        private GameObject gameObject;
        public void BuildGameObject(Vector2 position)
        {
            GameObject gameObject = new GameObject();

            gameObject.AddComponent(new SpriteRenderer(gameObject, "MoveableBox.png", 1f));
            //gameObject.AddComponent(new Collider(gameObject));
            gameObject.Transform.Position = new Vector2(10, 10);
            gameObject.AddComponent(new Pet(gameObject));
            this.gameObject = gameObject;
        }

        public GameObject GetResult()
        {
            return gameObject;
        }
    }
}
