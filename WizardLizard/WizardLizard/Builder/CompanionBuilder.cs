﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace WizardLizard
{
    class CompanionBuilder : IBuilder
    {
        private GameObject gameObject;
        public void BuildGameObject(Vector2 position)
        {
            GameObject gameObject = new GameObject();

            gameObject.AddComponent(new SpriteRenderer(gameObject, "Pet", 1f));
            gameObject.AddComponent(new Collider(gameObject));
            gameObject.Transform.Position = new Vector2(10, 10);
            gameObject.Transform.Position = position;
            gameObject.AddComponent(new Companion(gameObject));
            this.gameObject = gameObject;
        }

        public void BuildGameObject(Vector2 position, int frequency)
        {
            throw new NotImplementedException();
        }

        public GameObject GetResult()
        {
            return gameObject;
        }
    }
}
