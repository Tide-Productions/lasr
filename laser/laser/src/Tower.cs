using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Nez;
using Nez.Sprites;

namespace laser
{ 
    public class Tower : Component, IUpdatable
    {
        public Player Player;
        public TowerState State;
        public int Id;
        public int X;
        public int Y;
        public Sprite Sprite;

        public enum TowerState
        {
            Free,
            Shadow,
            Placed
        }

        public Tower(int id)
        {
            Id = id;
            X = -1;
            Y = -1;
        }

        public override void onAddedToEntity()
        {
            base.onAddedToEntity();
            Player = entity.scene.entities.findEntity("player").getComponent<Player>();
            State = TowerState.Free;
            this.setEnabled(false);
            Sprite = entity.getComponent<Sprite>();
        }

        public void shadow(int x, int y)
        {
            X = x;
            Y = y;
            State = TowerState.Shadow;
        }

        public void place()
        {
            if (State != TowerState.Shadow)
                return;

        }

        public void update()
        {
            if (State == TowerState.Shadow)
            {
                
            }
            else if (State == TowerState.Placed)
            {
                
            }
            else
            {
                Sprite.setColor(new Color(Color.White, 255));
            }
        }
    }
}
