using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace JumpTest
{
    public class TestBullet
    {
        Texture2D texture;

        Vector2 position;
        Vector2 velocity;

        public TestBullet( Vector2 newposition)
        {
            texture = newTexture;
            position = newposition;
           
        }
