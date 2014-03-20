using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyMelody
{
    class Character
    {
        Animation animated;
        Vector2 position;
        Boolean isActive;
        int health;

        public int Width
        {
            get { return animated.FrameWidth; }
        }
        
        public void Initialize(Animation anima, Vector2 pos)
        {

        }
    }
}
