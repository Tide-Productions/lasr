using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Nez;

namespace laser.Scenes
{
    public class BasicScene : Scene
    {
        public const int SCREEN_SPACE_RENDER_LAYER = 999;
        public UICanvas canvas;

        public BasicScene()
        {
            addRenderer(new DefaultRenderer());
            clearColor = Color.Black;
        }
    }
}
