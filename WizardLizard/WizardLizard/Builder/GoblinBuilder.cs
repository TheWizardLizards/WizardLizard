﻿using Microsoft.Xna.Framework;

namespace WizardLizard
{
    class GoblinBuilder: IBuilder
    {
        private GameObject gameObject;
        public void BuildGameObject(Vector2 position)
        {
            GameObject gameObject = new GameObject();
            gameObject.AddComponent(new SpriteRenderer(gameObject, "goblin", 1));
            gameObject.Transform.Position = position;
            gameObject.AddComponent(new Collider(gameObject));
            gameObject.AddComponent(new Goblin(gameObject));
            this.gameObject = gameObject;
        }

        public GameObject GetResult()
        {
            return gameObject;
        }
    }
}
