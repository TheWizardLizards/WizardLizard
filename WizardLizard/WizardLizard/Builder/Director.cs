using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizardLizard
{
    class Director
    {
        private IBuilder builder;
        public Director(IBuilder builder)
        {
            this.builder = builder;
        }
        public GameObject Construct(Vector2 position)
        {
            builder.BuildGameObject(position);
            return builder.GetResult();
        }
        public GameObject Construct(Vector2 position, int frequency)
        {
            builder.BuildGameObject(position, frequency);
            return builder.GetResult();
        }
        public GameObject Construct(Vector2 position, int frequency, string spriteName)
        {
            builder.BuildGameObject(position, frequency, spriteName);
            return builder.GetResult();
        }
        public GameObject Construct(Vector2 position, int width, int height)
        {
            builder.BuildGameObject(position, width, height);
            return builder.GetResult();
        }
        public GameObject Construct(Vector2 position, string spriteName)
        {
            builder.BuildGameObject(position, spriteName);
            return builder.GetResult();
        }
        public GameObject Construct(Vector2 position, int width, int height, string creator)
        {
            builder.BuildGameObject(position, width,height, creator);
            return builder.GetResult();
        }
    }
}
