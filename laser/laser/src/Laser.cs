using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;
using Nez.Textures;

namespace laser
{
    public class Laser : Component, IUpdatable
    {
        public int X;
        public int Y;
        public bool IsActive;
        public int Type;

        public Laser(int x, int y, bool isActive)
        {
            X = x;
            Y = y;
            IsActive = isActive;
        }

        public override void onAddedToEntity()
        {
            base.onAddedToEntity();

            var texture = entity.scene.content.Load<Texture2D>("Images/laser/laser_sheet");
            var subtextures = Subtexture.subtexturesFromAtlas(texture, 16, 16);
            entity.scale = new Vector2(2);

            if (entity.name.Contains("Laser1"))
            {
                entity.addComponent(new Sprite(subtextures[4]).setColor(Color.Red));
            }
            else if (entity.name.Contains("Laser2"))
            {
                entity.addComponent(new Sprite(subtextures[4]).setColor(Color.Green));
            }
            else
            {
                entity.addComponent(new Sprite(subtextures[4]).setColor(Color.Blue));
            }

            entity.transform.position = new Vector2(100 + 16 * 2 * X, 100 + 16 * 2 * Y);
        }

        public void update()
        {
            //TODO: Player - Laser interaction
        }
    }
}
