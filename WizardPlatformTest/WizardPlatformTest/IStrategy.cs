using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizardPlatformTest
{
    interface IStrategy
    {
        void Update(ref Direction direction);
    }
}
