using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using laser.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nez;
using Nez.Textures;
using Nez.Sprites;

namespace laser
{
    public class Player : Component, IUpdatable
    {
        enum Animations
        {
            WalkUp,
            WalkDown,
            WalkRight,
            WalkLeft
        }

        private Sprite<Animations> _animation;

        public VirtualIntegerAxis _xAxisInput;
        public VirtualIntegerAxis _yAxisInput;

        public VirtualIntegerAxis _xAxisAction;
        public VirtualIntegerAxis _yAxisAction;

        public VirtualButton _actionButton;
        public VirtualButton _UpButton;
        public VirtualButton _DownButton;
        public VirtualButton _LeftButton;
        public VirtualButton _RightButton;


        public bool moving = false;
        public Vector2 dir;
        private int count;

        public int X;
        public int Y;

        public override void onAddedToEntity()
        {
            base.onAddedToEntity();
            var texture = entity.scene.content.Load<Texture2D>("Images/player_walk/player_sheet");
            var subtextures = Subtexture.subtexturesFromAtlas(texture, 16, 16);

            _animation = entity.addComponent(new Sprite<Animations>(subtextures[0]));

            entity.scale = new Vector2(2);

            _animation.addAnimation(Animations.WalkUp, new SpriteAnimation(new List<Subtexture>()
            {
                subtextures[0],
                subtextures[1]
            }));
            _animation.addAnimation(Animations.WalkRight, new SpriteAnimation(new List<Subtexture>()
            {
                subtextures[2],
                subtextures[3]
            }));
            _animation.addAnimation(Animations.WalkDown, new SpriteAnimation(new List<Subtexture>()
            {
                subtextures[4],
                subtextures[5]
            }));
            _animation.addAnimation(Animations.WalkLeft, new SpriteAnimation(new List<Subtexture>()
            {
                subtextures[6],
                subtextures[7]
            }));

            setupInput();
        }

        void setupInput()
        {
            _xAxisInput = new VirtualIntegerAxis();
            _xAxisInput.nodes.Add(new Nez.VirtualAxis.GamePadLeftStickX());
            _xAxisInput.nodes.Add(new Nez.VirtualAxis.KeyboardKeys(VirtualInput.OverlapBehavior.TakeNewer, Keys.A, Keys.R));

            _yAxisInput = new VirtualIntegerAxis();
            _yAxisInput.nodes.Add(new Nez.VirtualAxis.GamePadLeftStickY());
            _yAxisInput.nodes.Add(new Nez.VirtualAxis.KeyboardKeys(VirtualInput.OverlapBehavior.TakeNewer, Keys.W, Keys.S));

            _xAxisAction = new VirtualIntegerAxis();
            _xAxisAction.nodes.Add(new Nez.VirtualAxis.GamePadRightStickX());
            _xAxisAction.nodes.Add(new Nez.VirtualAxis.KeyboardKeys(VirtualInput.OverlapBehavior.TakeNewer, Keys.Left, Keys.Right));

            _yAxisAction = new VirtualIntegerAxis();
            _yAxisAction.nodes.Add(new Nez.VirtualAxis.GamePadRightStickY());
            _yAxisAction.nodes.Add(new Nez.VirtualAxis.KeyboardKeys(VirtualInput.OverlapBehavior.TakeNewer, Keys.Up, Keys.Down));

            _actionButton = new VirtualButton();
            _actionButton.nodes.Add(new Nez.VirtualButton.KeyboardKey(Keys.Space));
            _actionButton.nodes.Add(new Nez.VirtualButton.GamePadButton(0, Buttons.A));

            _UpButton = new VirtualButton();
            _UpButton.nodes.Add(new Nez.VirtualButton.KeyboardKey(Keys.W));
            _DownButton = new VirtualButton();
            _DownButton.nodes.Add(new Nez.VirtualButton.KeyboardKey(Keys.S));
            _LeftButton = new VirtualButton();
            _LeftButton.nodes.Add(new Nez.VirtualButton.KeyboardKey(Keys.A));
            _RightButton = new VirtualButton();
            _RightButton.nodes.Add(new Nez.VirtualButton.KeyboardKey(Keys.D));
        }

        public void update()
        {

            if (moving)
            {
                this.move();
                return;
            }

            var move = Vector2.Zero;

            if (GameScene.StageData.MapCells[X, Y].State == CellState.End)
            {
                Core.scene = new ChangeScene(GameScene.StageData.Next);
            }

            if (_UpButton.isPressed)
                move -= new Vector2(0,1);
            if(_DownButton.isPressed)
                move += new Vector2(0,1);
            if(_LeftButton.isPressed)
                move -= new Vector2(1,0);
            if(_RightButton.isPressed)
                move += new Vector2(1,0);


            var animation = Animations.WalkDown;

            if (move.X < 0)
                animation = Animations.WalkLeft;
            else if (move.X > 0)
                animation = Animations.WalkRight;

            if (move.Y < 0)
                animation = Animations.WalkUp;
            else if (move.Y > 0)
                animation = Animations.WalkDown;

            if (move != Vector2.Zero)
            {
                if (!_animation.isAnimationPlaying(animation))
                    _animation.play(animation);
                    
                var data = GameScene.StageData.MapCells;

                var left = X > 0 ? data[X-1, Y].State : CellState.Other;
                var right = X < 20 ? data[X + 1, Y].State : CellState.Other;
                var up = Y > 0 ? data[X, Y - 1].State : CellState.Other;
                var down = Y < 14 ? data[X, Y + 1].State : CellState.Other;

                if (_UpButton.isPressed && up != CellState.Other)
                {
                    dir = new Vector2(0,-1);
                    moving = true;
                    count = 0;
                    Y--;

                } else if (_DownButton && down != CellState.Other)
                {
                    dir = new Vector2(0,1);
                    moving = true;
                    count = 0;
                    Y++;
                }
                else if (_LeftButton && left != CellState.Other)
                {
                    dir = new Vector2(-1, 0);
                    moving = true;
                    count = 0;
                    X--;
                }
                else if (_RightButton && right != CellState.Other)
                {
                    dir = new Vector2(1,0);
                    moving = true;
                    count = 0;
                    X++;
                }

            }
            else
            {
                _animation.stop();
            }
        }

        private void move()
        {
            if (count < 8)
            {
                dir.Normalize();
                entity.transform.position += dir * 4;
                count++;
            }
            else
            {
                count = 20;
                moving = false;
                dir = Vector2.Zero;
            }
        }
    }
}
