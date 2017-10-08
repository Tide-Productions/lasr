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
                    entity.addComponent(new Sprite(GameScene.PathTex).setRenderLayer(20));
                    break;
                case CellState.Start:
                    entity.addComponent(new Sprite(GameScene.StartTex).setRenderLayer(20));
                    break;
                case CellState.End:
                    entity.addComponent(new Sprite(GameScene.EndTex).setRenderLayer(20));
                    break;
                case CellState.Other:
                    entity.addComponent(new Sprite(GameScene.OtherTex).setRenderLayer(20));
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
