using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Editor.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nez;
using Nez.Sprites;

namespace Editor
{
    public class Cursor : Component, IUpdatable
    {
        public int X;
        public int Y;

        public VirtualButton _UpButton;
        public VirtualButton _DownButton;
        public VirtualButton _LeftButton;
        public VirtualButton _RightButton;
        public VirtualButton _PathButton;
        public VirtualButton _LaserButton1;
        public VirtualButton _LaserButton2;
        public VirtualButton _LaserButton3;

        public Cursor(int x, int y)
        {
            X = x;
            Y = y;
            SetupControlls();
        }

        public override void onAddedToEntity()
        {
            base.onAddedToEntity();
        }

        public void SetupControlls()
        {
            _UpButton = new VirtualButton();
            _UpButton.nodes.Add(new Nez.VirtualButton.KeyboardKey(Keys.W));
            _DownButton = new VirtualButton();
            _DownButton.nodes.Add(new Nez.VirtualButton.KeyboardKey(Keys.S));
            _LeftButton = new VirtualButton();
            _LeftButton.nodes.Add(new Nez.VirtualButton.KeyboardKey(Keys.A));
            _RightButton = new VirtualButton();
            _RightButton.nodes.Add(new Nez.VirtualButton.KeyboardKey(Keys.D));
            _PathButton = new VirtualButton();
            _PathButton.nodes.Add(new Nez.VirtualButton.KeyboardKey(Keys.P));

            _LaserButton1 = new VirtualButton();
            _LaserButton1.nodes.Add(new Nez.VirtualButton.KeyboardKey(Keys.U));
            _LaserButton2 = new VirtualButton();
            _LaserButton2.nodes.Add(new Nez.VirtualButton.KeyboardKey(Keys.I));
            _LaserButton3 = new VirtualButton();
            _LaserButton3.nodes.Add(new Nez.VirtualButton.KeyboardKey(Keys.O));
        }

        public void update()
        {
            this.transform.position = new Vector2(100 + 16 * 2 * X, 100 + 16 * 2 * Y);

            if (_UpButton.isPressed && Y > 0)
                Y--;
            if (_DownButton.isPressed && Y < 14)
                Y++;
            if (_LeftButton.isPressed && X > 0)
                X--;
            if (_RightButton.isPressed && X < 20)
                X++;

            if (_PathButton.isPressed)
            {
                var tileState = GameScene.StageData.MapCells[X, Y].State;
                switch (tileState)
                {
                    case CellState.Other:
                        var cellO = new Cell();
                        cellO.State = CellState.Path;
                        cellO.X = X;
                        cellO.Y = Y;
                        GameScene.StageData.MapCells[X,Y] = cellO;
                        var cellEO = entity.scene.entities.findEntity("CellX" + X + "Y" + Y);
                        cellEO.removeComponent<Sprite>();
                        cellEO.addComponent(new Sprite(GameScene.PathTex).setRenderLayer(20));
                        break;
                    case CellState.Path:
                        var cellP = new Cell();
                        cellP.State = CellState.Start;
                        cellP.X = X;
                        cellP.Y = Y;
                        GameScene.StageData.MapCells[X, Y] = cellP;
                        var cellEP = entity.scene.entities.findEntity("CellX" + X + "Y" + Y);
                        cellEP.removeComponent<Sprite>();
                        cellEP.addComponent(new Sprite(GameScene.StartTex).setRenderLayer(20));
                        break;
                    case CellState.Start:
                        var cellS = new Cell();
                        cellS.State = CellState.End;
                        cellS.X = X;
                        cellS.Y = Y;
                        GameScene.StageData.MapCells[X, Y] = cellS;
                        var cellES = entity.scene.entities.findEntity("CellX" + X + "Y" + Y);
                        cellES.removeComponent<Sprite>();
                        cellES.addComponent(new Sprite(GameScene.EndTex).setRenderLayer(20));
                        break;
                    case CellState.End:
                        var cellE = new Cell();
                        cellE.State = CellState.Other;
                        cellE.X = X;
                        cellE.Y = Y;
                        GameScene.StageData.MapCells[X, Y] = cellE;
                        var cellEE = entity.scene.entities.findEntity("CellX" + X + "Y" + Y);
                        cellEE.removeComponent<Sprite>();
                        cellEE.addComponent(new Sprite(GameScene.OtherTex).setRenderLayer(20));
                        break;
                }
            }

            if (_LaserButton1.isPressed)
            {
                var laser = entity.scene.entities.findEntity("Laser1X" + X + "Y" + Y).getComponent<Laser>();
                laser.IsActive = !laser.IsActive;
                var ld = GameScene.StageData.LaserDatas;

                if (laser.IsActive)
                {
                    laser.entity.addComponent(new Sprite(Graphics.createSingleColorTexture(5, 5, Color.Orange))
                        .setRenderLayer(10).setLocalOffset(new Vector2(-5,0)));
                    ld[X, Y].IsLaser0 = true;
                }
                else
                {
                    laser.entity.removeComponent<Sprite>();
                    ld[X, Y].IsLaser0 = false;
                }
            }

            if (_LaserButton2.isPressed)
            {
                var laser = entity.scene.entities.findEntity("Laser2X" + X + "Y" + Y).getComponent<Laser>();
                laser.IsActive = !laser.IsActive;
                var ld = GameScene.StageData.LaserDatas;
                if (laser.IsActive)
                {
                    laser.entity.addComponent(new Sprite(Graphics.createSingleColorTexture(5, 5, Color.Green))
                        .setRenderLayer(10).setLocalOffset(new Vector2(0,0)));
                    ld[X, Y].IsLaser1 = true;
                }
                else
                {
                    laser.entity.removeComponent<Sprite>();
                    ld[X, Y].IsLaser1 = false;
                }
            }

            if (_LaserButton3.isPressed)
            {
                var laser = entity.scene.entities.findEntity("Laser3X" + X + "Y" + Y).getComponent<Laser>();
                laser.IsActive = !laser.IsActive;
                var ld = GameScene.StageData.LaserDatas;
                if (laser.IsActive)
                {
                    laser.entity.addComponent(new Sprite(Graphics.createSingleColorTexture(5, 5, Color.Blue))
                        .setRenderLayer(10).setLocalOffset(new Vector2(5,0)));
                    ld[X, Y].IsLaser3 = true;
                }
                else
                {
                    laser.entity.removeComponent<Sprite>();
                    ld[X, Y].IsLaser3 = false;
                }
            }
        }
    }
}
