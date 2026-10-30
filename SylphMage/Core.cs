using MelonLoader;
using UnityEngine;
using static MelonLoader.MelonLogger;

[assembly: MelonInfo(typeof(SylphMage.Core), "SylphMage", "1.0.0", "jade", null)]
[assembly: MelonGame("Perfectly Generic Team", "The Genesis Project")]

namespace SylphMage
{
    public class Core : MelonMod
    {
        static string coreModDir = "./Mods/";
        static string tgpWorkshopDir = "../../workshop/content/1610900";
        static string[] workshopModList = Directory.GetFileSystemEntries(tgpWorkshopDir);
        static string sylphMageString = "SylphMage";
        static string updaterString = "SylphMageUpdateHandler.dll";
        static string updaterUpdate = "updatersyphMage update";
        public override void OnInitializeMelon()
        {
            LoggerInstance.Msg("SylphMage initialized. :3");
        }
        public override void OnLateInitializeMelon()
        {
            if(File.Exists(coreModDir+updaterString) && File.Exists(coreModDir + updaterUpdate)){
                File.Delete(coreModDir+updaterUpdate);
            }
            
            foreach (string workshopMod in workshopModList) {
                string melonloaderModDir = workshopMod + "\\AssetBundles\\mod";

                if (Directory.Exists(melonloaderModDir)) {
                    var modDirInfo = new DirectoryInfo(melonloaderModDir);

                    foreach (var file in modDirInfo.GetFiles()) {
                        LoggerInstance.Msg(file.Name);
                        if (file.Name.Length > sylphMageString.Length){
                            if(file.Name.Remove(sylphMageString.Length) != sylphMageString || file.Name == updaterString){
                                LoggerInstance.Msg("again: "+file.Name);
                                if (!File.Exists(coreModDir+file.Name))
                                {
                                    file.CopyTo(coreModDir+file.Name,true);
                                    MelonEvents.OnGUI.Subscribe(modChangeToast, 100);
                                }
                                if (File.Exists(coreModDir + file.Name)){
                                    var fileInModDir = new FileInfo(coreModDir + file.Name);

                                    if (fileInModDir.LastWriteTime.Ticks < file.LastWriteTime.Ticks) {
                                        fileInModDir.Delete();
                                        if(file.Name == updaterString)
                                        {
                                            File.Create(coreModDir + updaterUpdate);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void modChangeToast()
        {
            GUIStyle novastyle = new GUIStyle();
            novastyle.richText = true;
            GUI.Label(new Rect(150, 50, 300, 500), "<size=24><color=#0f0><b>One or more mods have been added/updated :3c \n close tgp to apply the change</b></color></size>", novastyle);
        }
    }
}