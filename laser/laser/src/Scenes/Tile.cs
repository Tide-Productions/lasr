using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Nez;
using Nez.Sprites;

namespace laser.Scenes
{
    public class Tile : Component, IUpdatable
    {
        public Vector2 Offset;
        public int X;
        public int Y;
        public CellState State;

        public Tile(int x, int y, Vector2 offset, CellState type)
        {
            Offset = offset;
            X = x;
            Y = y;
            State = type;
        }

        public override void onAddedToEntity()
        {
            base.onAddedToEntity();
            switch (State)
            {
                case CellState.Path:
                    entity.addComponent(new Sprite(Graphics.createSingleColorTexture(16, 16, Color.Gray)));
                    break;
                case CellState.Start:
                    entity.addComponent(new Sprite(Graphics.createSingleColorTexture(16, 16, Color.Yellow)));
                    entity.scene.entities.findEntity("player").transform.position =
                        Offset + new Vector2(X * 16 * 2, Y * 16 * 2);
                    var player = entity.scene.entities.findEntity("player").getComponent<Player>();
                    player.X = X;
                    player.Y = Y;
                    break;
                case CellState.End:
                    entity.addComponent(new Sprite(Graphics.createSingleColorTexture(16, 16, Color.Green)));
                    break;
                case CellState.Other:
                    //entity.addComponent(new Sprite(Graphics.createSingleColorTexture(16, 16, Color.Red)));
                    break;
            }
            entity.scale = new Vector2(2);
            entity.transform.position = Offset + new Vector2(X * 16 * entity.scale.X, Y * 16 * entity.scale.Y);
        }

        public void update()
        {
            
        }
    }
}
