using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using Nez;
using Nez.Sprites;
using Nez.TextureAtlases;

namespace laser.Scenes
{
    public class GameScene : BasicScene
    {
        public static StageData StageData;

        public GameScene(string stagePath)
        {
            setDesignResolution(1280, 720, SceneResolutionPolicy.None);
            Screen.setSize(1280,720);

            var player = this.createEntity("player", new Vector2(Screen.width / 2, Screen.height / 2));
            player.addComponent(new Player());

            GetStageData(stagePath);

            CreateStage();

        }

        public override void initialize()
        {
            base.initialize();
        }


        public override void update()
        {
            base.update();
        }

        private void GetStageData(string stageDataPath)
        {
            var def = stageDataPath == "default";

            if (!def)
                using (var sr = new StreamReader(new FileStream(stageDataPath, FileMode.Open, FileAccess.Read, FileShare.None)))
                {
                    StageData = JsonConvert.DeserializeObject<StageData>(sr.ReadToEnd());
                }
            else
            {
                using (var sw = new StreamWriter(new FileStream("test.json", FileMode.Create, FileAccess.Write)))
                {
                    CreateDefTest();
                    sw.Write(JsonConvert.SerializeObject(StageData));
                }
            }
        }

        private void CreateDefTest()
        {
            var ld = new LaserData[21, 15];
            var mc = new Cell[21, 15];

            for (var j = 0; j < 21; j++)
            {
                for (var k = 0; k < 15; k++)
                {
                    ld[j, k].X = j;
                    ld[j, k].Y = k;

                    mc[j, k].X = j;
                    mc[j, k].Y = k;
                }
            }


            StageData = new StageData()
            {
                LaserDatas = ld,
                MapCells = mc,
                TowerData = new[] { 1, 2, 3 },
                Next = "test.json"
            };
        }

        private void CreateStage()
        {
            var mc = StageData.MapCells;
            for (var i = 0; i < 21; i++)
            {
                for (var j = 0; j < 15; j++)
                {
                    var cell = createEntity("CellX" + i + "Y" + j);
                    cell.addComponent(new Tile(i, j, new Vector2(100, 100), mc[i,j].State));
                }
            }

            var td = StageData.TowerData;

            for (var i = 0; i < td[0]; i++)
            {
                var tower = createEntity("Tower0-" + i);
                tower.addComponent(new Tower(0));
            }
            for (var i = 0; i < td[1]; i++)
            {
                var tower = createEntity("Tower1-" + i);
                tower.addComponent(new Tower(1));
            }
            for (var i = 0; i < td[2]; i++)
            {
                var tower = createEntity("Tower2-" + i);
                tower.addComponent(new Tower(2));
            }

            findEntity("player").getComponent<Player>().AddTowers();

            var ld = StageData.LaserDatas;

            for (var i = 0; i < 21; i++)
            {
                for (var j = 0; j < 15; j++)
                {
                    if (ld[i, j].IsLaser0 || ld[i, j].IsLaser1 || ld[i, j].IsLaser3)
                    {
                        if (ld[i, j].IsLaser0)
                        {
                            var laser1 = createEntity("Laser1X" + i + "Y" + j);
                            laser1.addComponent(new Laser(i, j, true));
                        }
                        if (ld[i, j].IsLaser1)
                        {
                            var laser2 = createEntity("Laser2X" + i + "Y" + j);
                            laser2.addComponent(new Laser(i, j, true));
                        }
                        if (ld[i, j].IsLaser3)
                        {

                            var laser3 = createEntity("Laser3X" + i + "Y" + j);
                            laser3.addComponent(new Laser(i, j, true));
                        }

                    }
                }
            }
        }
    }
}
