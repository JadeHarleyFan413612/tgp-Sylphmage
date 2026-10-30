using HarmonyLib;
using MelonLoader;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UnityEngine;

[assembly: MelonInfo(typeof(SylphMageUpdateHandler.Core), "SylphMageUpdateHandler", "1.0.0", "jade", null)]
[assembly: MelonGame("Perfectly Generic Team", "The Genesis Project")]
namespace SylphMageUpdateHandler
{
    public class Core : MelonMod
    {
        static string updaterUpdate = "updatersyphMage update";

        public override void OnInitializeMelon()
        {
            LoggerInstance.Msg("Initialized.");

            var modDirectory = new DirectoryInfo("../../workshop/content/1610900/3750025727/AssetBundles/mod/hidden/");

            foreach (var item in modDirectory.EnumerateFiles("SylphMage*"))
            {
                LoggerInstance.Msg(item.Name);
                LoggerInstance.Msg(item.Name.Remove(0, item.Name.Length - 5).Remove(1, 4));
            }
            if (!File.Exists("./Mods/SylphMage.dll"))
            {
                MelonEvents.OnGUI.Subscribe(SylphmageMissing, 100);

                try
                {
                    foreach (var file in modDirectory.EnumerateFiles("SylphMage*"))
                    {
                        File.Copy(file.FullName, "./Mods/" + file.Name, true);
                    }
                    MelonEvents.OnGUI.Subscribe(SylphmageSuccess, 100);
                }
                catch
                {
                    MelonEvents.OnGUI.Subscribe(SylphmageFailure, 100);
                }
            }
            else
            {
                string[] exists = System.IO.Directory.GetFiles("./Mods/", "SylphMagexx.0*", System.IO.SearchOption.TopDirectoryOnly);
                if (exists.Length > 0)
                    LoggerInstance.Msg("hiiiiiiiiiiiii :3");
                LoggerInstance.Msg("nothing to do uwu");
            }
        }
        public override void OnApplicationQuit()
        {
            var sylphMageDirectory = new DirectoryInfo("./Mods/");
            string[] exists = System.IO.Directory.GetFiles("./Mods/", "SylphMage.0*", System.IO.SearchOption.TopDirectoryOnly);
            var melonSylphMage = new FileInfo(sylphMageDirectory + "SylphMage.dll");
            var workshopSylphMage = new FileInfo("../../workshop/content/1610900/3750025727/AssetBundles/mod/hidden/SylphMage.dll");


            if ((exists.Length > 0 || melonSylphMage.LastWriteTime.Ticks < workshopSylphMage.LastWriteTime.Ticks) && !File.Exists("./Mods/"+updaterUpdate))
            {
                foreach (var SylphMage in sylphMageDirectory.EnumerateFiles("SylphMage*"))
                {
                    if (SylphMage.Name != "SylphMageUpdateHandler.dll" )
                        SylphMage.Delete();
                }
            }

            }
        private void SylphmageMissing()
        {
            GUIStyle novastyle = new GUIStyle();
            novastyle.richText = true;
            GUI.Label(new Rect(300, 300, 300, 500), "<size=32><color=#f00><b>IMPORTANT: CORE WORKSHOP FUNCTIONALITY MOD \"SYLPHMAGE\" WILL NOW. \n UNDERGO AN ATTEMPTED UPDATE.</b></color></size>", novastyle);
        }
        private void SylphmageSuccess()
        {
            GUIStyle novastyle = new GUIStyle();
            novastyle.richText = true;
            GUI.Label(new Rect(300, 300, 300, 500), "<size=32><color=#0f0><b>\n\nSYLPHMAGE SUCCESFULLY UPDATED. PLEASE RESTART TGP</b></color></size>", novastyle);
        }
        private void SylphmageFailure()
        {
            GUIStyle novastyle = new GUIStyle();
            novastyle.richText = true;
            GUI.Label(new Rect(300, 300, 300, 500), "<size=32><color=#f00><b>\n\nUPDATE FAILED. REMOVE ALL FILES LABLED 'SYLPHMAGE' FROM\n THE MELONLOADER MOD FOLDER AND GET THE LATEST SYLPHMAGE RELEASE FROM THE GITHUB ", novastyle);
        }

        }
    }
