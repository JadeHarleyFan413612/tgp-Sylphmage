using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelonLoader;
using System.IO;

namespace SylphMage_TMP_handler
{
    public class TMPHandler : MelonMod
    {
        public override void OnApplicationQuit()
        {
            base.OnApplicationQuit();
            if (File.Exists("./Mods/TMPSylphMage.dll") && File.Exists("./Mods/TMPCheck"))
            {
                File.Delete("./Mods/TMPSylphMage.dll");
                File.Delete("./Mods/TMPCheck");
            }

        }
    }
}
