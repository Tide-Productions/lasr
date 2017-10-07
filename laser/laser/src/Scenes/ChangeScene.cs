using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nez;

namespace laser.Scenes
{
    public class ChangeScene : BasicScene
    {

        public string Next;
        public float Time;

        public ChangeScene(string next)
        {
            setDesignResolution(1280, 720, SceneResolutionPolicy.None);
            Screen.setSize(1280, 720);
            Next = next;
            Time = 0f;
        }

        public override void update()
        {
            base.update();
            Time += Nez.Time.deltaTime;

            if (Time > 1.5f)
            {
                Core.scene = new GameScene(Next);
            }
        }
    }
}
