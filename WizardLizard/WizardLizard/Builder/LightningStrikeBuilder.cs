using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace WizardLizard
{
    class LightningStrikeBuilder : IBuilder
    {
        private GameObject gameObject;
        public void BuildGameObject(Vector2 position)
        {
            GameObject gameObject = new GameObject();
            gameObject.AddComponent(new SpriteRenderer(gameObject, "LightningStrike", 1f));
            gameObject.Transform.Position = position;
            //gameObject.AddComponent(new Collider(gameObject));
            gameObject.AddComponent(new LightningStrike(gameObject));
            this.gameObject = gameObject;
        }
        public GameObject GetResult()
        {
            return gameObject;
        }
    }
}
