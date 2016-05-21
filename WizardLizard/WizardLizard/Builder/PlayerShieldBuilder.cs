using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace WizardLizard
{
    class PlayerShieldBuilder : IBuilder
    {
        private GameObject gameObject;
        public void BuildGameObject(Vector2 position)
        {
            GameObject gameObject = new GameObject();
            gameObject.AddComponent(new SpriteRenderer(gameObject, "PlayerShield", 1f));
            gameObject.AddComponent(new Collider(gameObject));
            gameObject.Transform.Position = new Vector2(50, 50);
            gameObject.AddComponent(new PlayerShield(gameObject));
            this.gameObject = gameObject;
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
