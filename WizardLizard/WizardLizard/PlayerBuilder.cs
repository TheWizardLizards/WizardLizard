using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace WizardLizard
{
    class PlayerBuilder : IBuilder
    {
        private GameObject gameObject;
        public void BuildGameObject(Vector2 position)
        {
            GameObject gameObject = new GameObject();
            gameObject.AddComponent(new SpriteRenderer(gameObject, "Hero", 1f));
            gameObject.Transform.Position = new Vector2(50,50);
            gameObject.AddComponent(new Player(gameObject));
            this.gameObject = gameObject;
        }
        
        public GameObject GetResult()
        {
            return gameObject;
        }
    }
}
