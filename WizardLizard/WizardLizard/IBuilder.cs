using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizardLizard
{
    interface IBuilder
    {
        GameObject GetResult();
        void BuildGameObject(Vector2 position);
        void BuildGameObject(Vector2 position, string name);
        void BuildGameObject(Vector2 position, string name, string spriteName);
    }
}
