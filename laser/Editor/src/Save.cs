using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Editor.Scenes;
using Newtonsoft.Json;

namespace Editor
{
    public class Save
    {
        public static void savef()
        {
            var sfd = new SaveFileDialog();
            sfd.Filter = "Json|*.json";
            sfd.Title = "Save Stage as Json";
            sfd.FileName = "default.json";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                using (var sr = new StreamWriter(sfd.OpenFile()))
                {
                    sr.Write(JsonConvert.SerializeObject(GameScene.StageData));
                }
            }
        }
    }
}
