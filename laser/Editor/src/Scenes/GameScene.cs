using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;
using Nez;
using Nez.Sprites;
using Nez.TextureAtlases;

namespace Editor.Scenes
{
    public class GameScene : BasicScene
    {
        public static StageData StageData;
        public static Texture2D PathTex;
        public static Texture2D StartTex;
        public static Texture2D EndTex;
        public static Texture2D OtherTex;

        public VirtualButton SaveButton;
        public VirtualButton Reset;

        public GameScene()
        {
            setDesignResolution(1280, 720, SceneResolutionPolicy.None);
            Nez.Screen.setSize(1280,720);
            PathTex = Graphics.createSingleColorTexture(16, 16, Color.Gray);
            StartTex = Graphics.createSingleColorTexture(16, 16, Color.Yellow);
            EndTex = Graphics.createSingleColorTexture(16, 16, Color.Green);
            OtherTex = Graphics.createSingleColorTexture(16, 16, Color.Red);

            CreateDefStage();
            CreateStage();

            var cursor = createEntity("cursor", new Vector2(100, 100));
            cursor.addComponent(new Cursor(0, 0));
            cursor.addComponent(new Sprite(content.Load<Texture2D>("cursor")).setRenderLayer(1));
            cursor.scale = new Vector2(2);

            SaveButton = new VirtualButton();
            SaveButton.nodes.Add(new Nez.VirtualButton.KeyboardKey(Keys.Q));
            Reset = new VirtualButton();
            Reset.nodes.Add(new Nez.VirtualButton.KeyboardKey(Keys.E));
        }

        public override void initialize()
        {
            base.initialize();
        }


        public override void update()
        {
            base.update();
            if (SaveButton.isPressed)
            {
                Save.savef();
            }
            if (Reset.isPressed)
            {
                Game1.scene = new GameScene();
            }
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
                    //CreateDefTest();
                    sw.Write(JsonConvert.SerializeObject(StageData));
                }
            }
        }

        private void CreateDefStage()
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

            var ld = StageData.LaserDatas;
            for (var i = 0; i < 21; i++)
            {
                for (var j = 0; j < 15; j++)
                {
                    var laser1 = createEntity("Laser1X" + i + "Y" + j);
                    laser1.addComponent(new Laser(i, j, new Vector2(100, 100), ld[i,j].IsLaser0));
                    var laser2 = createEntity("Laser2X" + i + "Y" + j);
                    laser2.addComponent(new Laser(i, j, new Vector2(100, 100), ld[i, j].IsLaser1));
                    var laser3 = createEntity("Laser3X" + i + "Y" + j);
                    laser3.addComponent(new Laser(i, j, new Vector2(100,100), ld[i, j].IsLaser3));
                }
            }
        }
    }
}
