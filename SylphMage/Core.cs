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

[assembly: MelonInfo(typeof(SylphMage.Core), "SylphMage", "1.0.0", "jade", null)]
[assembly: MelonGame("Perfectly Generic Team", "The Genesis Project")]

namespace SylphMage
{
    public class Core : MelonMod
    {
        public bool coreupdate = false;

        private void SylphmageCoreUpdate()
        {
            GUIStyle novastyle = new GUIStyle();
            novastyle.richText = true;
            GUI.Label(new Rect(300, 300, 300, 500), "<size=32><color=#f00><b>IMPORTANT: CORE WORKSHOP FUNCTIONALITY MOD \"SYLPHMAGE\" UPDATED; CLOSE TGP TO APPLY THE CHANGE. \n IF THIS MOD IS ABSENT FROM SUBSEQUENT STARTUPS REINSTALL IT</b></color></size>", novastyle);
        }
        private void updateSuccess()
        {
            GUIStyle novastyle = new GUIStyle();
            novastyle.richText = true;
            GUI.Label(new Rect(150, 150, 300, 500), "<size=24><color=#0f0><b>One or more mods have been updated :3c \n close tgp to apply the change</b></color></size>", novastyle);
        }
        private void addSuccess()
        {
            GUIStyle novastyle = new GUIStyle();
            novastyle.richText = true;
            GUI.Label(new Rect(150, 50, 300, 500), "<size=24><color=#0f0><b>One or more mods have been added :3c \n close tgp to apply the change</b></color></size>", novastyle);
        }

        public override void OnLateInitializeMelon()
        {
            if (File.Exists("./Mods/TMPSylphMage.dll"))
            {
                File.Copy("./Mods/TMPSylphMage.dll", "./Mods/SylphMage.dll");
                File.Create("./Mods/TMPCheck");
            }
            if (File.Exists("./Mods/TMPCheck") && !(File.Exists("./Mods/TMPSylphMage.dll")))
            {
                File.Delete("./Mods/TMPCheck");

            }


            string[] getFolders(string location)
            {
                try
                {
                    String[] allfiles = Directory.GetFileSystemEntries(location);
                    LoggerInstance.Msg("the folders are:");
                    foreach (var file in allfiles)
                    {
                        LoggerInstance.Msg(file);

                    }
                    return allfiles;
                }
                catch (DirectoryNotFoundException)
                {
                    LoggerInstance.Msg("err: directory not found");
                    String[] error = new String[] { "error" };
                    return error;
                }
            }
            String[] Folderlist = getFolders("../../workshop/content/1610900");
            if (Folderlist[0] != "error")
            {
                foreach (var candidate in Folderlist)
                {
                    LoggerInstance.Msg("they are: " + candidate);
                    String[] ModList = getFolders(candidate);
                    LoggerInstance.MsgPastel("hiiii");
                    string SylphMageVesrion = "";
                    var modDirectory = new DirectoryInfo("./Mods/");
                    foreach (var file in modDirectory.EnumerateFiles( "SylphMage*"))
                    {
                        if (file.FullName.Remove(0, file.FullName.Length - 3) != "dll")
                        {
                            SylphMageVesrion = file.FullName.Remove(0,file.FullName.Length - 5);
                            LoggerInstance.Msg("sylphmage version is: " + SylphMageVesrion);
                        }
                    }
                        foreach (var Uniquefolder in ModList)
                    {
                        LoggerInstance.Msg("the uniquefolder is: " + Uniquefolder);
                        LoggerInstance.Msg("the candidate is: " + candidate);
                        if (Uniquefolder == candidate + "\\AssetBundles")
                        {
                            foreach (var secondCandidate in Directory.GetFileSystemEntries(Uniquefolder))
                            {


                                if (secondCandidate == Uniquefolder + "\\mod")
                                {
                                    try
                                    {
                                        LoggerInstance.MsgPastel("Success :3");

                                        string symbolicLink = "./Mods";
                                        string filePath = secondCandidate;
                                        string fileName = secondCandidate.Substring(45);
                                        LoggerInstance.Msg("the name of the directory is: " + fileName);
                                        foreach (var item in Directory.GetFileSystemEntries(secondCandidate))
                                        {
                                            LoggerInstance.Msg("the item is: " + item.Substring(item.Length - 3));
                                            if (item.Substring(item.Length - 3) != "dll")
                                            {
                                                string name = item.Remove(0, secondCandidate.Length + 1);
                                                string version = name.Substring(name.Length - 5);
                                                name = name.Remove(name.Length - 6, 6);
                                                LoggerInstance.Msg("the version number is: " + version);
                                                LoggerInstance.Msg("one is: " + "./Mods/" + name + ".dll");
                                                LoggerInstance.Msg("two is: " + "./Mods/" + name + "." + version);
                                                if (File.Exists("./Mods/" + name + ".dll") && !(File.Exists("./Mods/" + name + "." + version)))
                                                {
                                                    try
                                                    {
                                                        var dir = new DirectoryInfo("./Mods/");

                                                        foreach (var file in dir.EnumerateFiles(name + "*"))
                                                        {
                                                            LoggerInstance.Msg("file: ", name + item.Substring(item.Length - 3));
                                                            if (name == "SylphMage" && item.Substring(item.Length - 3) != "dll")
                                                            {
                                                                MelonEvents.OnGUI.Subscribe(SylphmageCoreUpdate, 100);
                                                                coreupdate = true;

                                                            }
                                                            if (item.Substring(item.Length - 5) == version)
                                                            {
                                                                file.Delete();
                                                            }
                                                        }

                                                        LoggerInstance.Msg(name + " has been successfully updated; restart TGP one or two times to apply the change :3c");
                                                        MelonEvents.OnGUI.Subscribe(updateSuccess, 100);
                                                    }
                                                    catch
                                                    {
                                                        LoggerInstance.Msg("was " + name + "even here?");
                                                    }
                                                }
                                                else
                                                {
                                                    LoggerInstance.Msg(name + " exists in the folder :3");
                                                }

                                            }
                                        }
                                        foreach (var item in Directory.GetFileSystemEntries(secondCandidate))
                                        {
                                            string filename = item.Remove(0, 59);
                                            LoggerInstance.Msg("item: " + item);
                                            LoggerInstance.Msg("filename: " + filename);
                                            string name = filename.Remove(filename.Length - 4);
                                            if (filename == "SylphMage.dll" && !(File.Exists("./Mods/"+name+"."+SylphMageVesrion)))
                                            {
                                                LoggerInstance.Msg(name+" : "+SylphMageVesrion);
                                                File.Copy(item, "./Mods/TMP" + filename, true);
                                            }
                                            else
                                            {
                                                if (!(File.Exists("./Mods/" + filename)))
                                                {
                                                    MelonEvents.OnGUI.Subscribe(addSuccess, 100);
                                                }
                                                File.Copy(item, "./Mods/" + filename, true);
                                            }
                                        }
                                    }
                                    catch
                                    {
                                        LoggerInstance.Msg("IMPORTANT: Please screenshot this part of the console and send it to nova if you're seeing this; I done fucked up somehow");
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public override void OnApplicationQuit()
        {
        }
    }
}