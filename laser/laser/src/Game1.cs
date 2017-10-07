using laser.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nez;

namespace laser
{
    public class Game1 : Core
    {
        public Game1() : base(width: 1280, height: 720, isFullScreen: false, enableEntitySystems: true, contentDirectory: "Content", windowTitle: "LAS??R")
        { }

        protected override void Initialize()
        {
            base.Initialize();

            scene = new GameScene();
        }
    }
}
