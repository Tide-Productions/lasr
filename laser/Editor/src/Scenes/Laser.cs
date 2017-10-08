using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Nez;
using Nez.Sprites;

namespace Editor.Scenes
{
    public class Laser : Component
    {
        public int X;
        public int Y;

        private bool isActive;

        public bool IsActive
        {
            get => isActive;
            set
            {
                isActive = value;
                //ChangeGraph();
            }
        }

        public int Type;
        private Color color;

        public Laser(int x, int y, Vector2 pos, bool isActive)
        {
            X = x;
            Y = y;
            
            this.isActive = isActive;
        }

        public override void onAddedToEntity()
        {
            base.onAddedToEntity();
            if (entity.name.Contains('1'))
            {
                Type = 1;
                color = Color.Red;
            }
            else if (entity.name.Contains('2'))
            {
                Type = 2;
                color = Color.Green;
            }
            else
            {
                Type = 3;
                color = Color.Blue;
            }
            ChangeGraph();
            entity.transform.position = new Vector2(100 + 16 * 2 * X, 100 + 16 * 2 * Y);
        }

        public void ChangeGraph()
        {
            entity.removeComponent<Sprite>();
            if (isActive)
            {
                if (color == Color.Red)
                {
                    entity.addComponent(
                            new Sprite(Graphics.createSingleColorTexture(5, 5, Color.Red)).setRenderLayer(10));
                }
                else if (color == Color.Green)
                {
                    entity.addComponent(new Sprite(Graphics.createSingleColorTexture(5, 5, Color.Green))
                        .setRenderLayer(10));
                }
                else
                {
                    entity.addComponent(
                        new Sprite(Graphics.createSingleColorTexture(5, 5, Color.Blue)).setRenderLayer(10));
                }
            }
            else
            {
                entity.removeComponent<Sprite>();
            }
            
        }
    }
}
