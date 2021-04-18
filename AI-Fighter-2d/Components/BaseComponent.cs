using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace AI_Fighter_2d.Components
{
    public abstract class BaseComponent
    {
        public abstract void Draw(SpriteBatch spriteBatch);
        public abstract void Update();
    }
}
