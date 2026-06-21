using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using UnityEngine;
using MelonLoader;
using HarmonyLib;
using System.Collections;

namespace SylphMage
{
    public class mod : MelonMod
    {
        private static void CopyFilesRecursively(string sourcePath, string targetPath)
        {
            //Now Create all of the directories
            foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(dirPath.Replace(sourcePath, targetPath));
            }

            //Copy all the files & Replaces any files with the same name
            foreach (string newPath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
            {
                try
                {


                    File.Copy(newPath, newPath.Replace(sourcePath, targetPath), true);
                } catch
                {
                    Melon<mod>.Logger.Msg("but it was already here");
                }
            }
        }

        public override void OnInitializeMelon()
        { 
             string[] getFolders(string location)
            {
            try
            {
            String[] allfiles = Directory.GetFileSystemEntries(location);
            LoggerInstance.Msg("the folders are:");
            foreach(var file in allfiles)
            {
                LoggerInstance.Msg(file);
        
            }
                    return allfiles;
            } catch (DirectoryNotFoundException)
            {
                LoggerInstance.Msg("err: directory not found");
                    String[] error = new String[] { "error" };
                    return error;
            }
            }
            String[] Folderlist = getFolders("../../workshop/content/1610900");
            if (Folderlist[0] != "error")
            {
                foreach(var candidate in Folderlist)
                {
                    LoggerInstance.Msg("they are: "+candidate);
                    String[] ModList = getFolders(candidate);
                    LoggerInstance.MsgPastel("hiiii");
                    foreach(var secondCandidate in ModList)
                    {
                        LoggerInstance.Msg("the candidate file is: " + secondCandidate);
                        if (secondCandidate == candidate + "\\mod")
                        try{
                            LoggerInstance.MsgPastel("Success :3");

                            string symbolicLink = "./Mods";
                            string filePath = secondCandidate;
                            string fileName = secondCandidate.Substring(31);
                            LoggerInstance.Msg("the name of the file is: " + fileName);
                            foreach (var item in Directory.GetFileSystemEntries(secondCandidate))
                            {
                                LoggerInstance.Msg("the item is: " + item.Substring(item.Length - 3));
                                if (item.Substring(item.Length - 3) != "dll")
                                {
                                    string name = item.Remove(0, secondCandidate.Length + 1);
                                    string version = name.Substring(name.Length - 5);
                                    name = name.Remove(name.Length - 6, 6);
                                    if (File.Exists("./Mods/" + name + ".dll") && !(File.Exists("./Mods/" + name +"."+ version)))
                                    {
                                        try
                                        {
                                            var dir = new DirectoryInfo("./Mods");

                                            foreach (var file in dir.EnumerateFiles(name + "*"))
                                            {
                                                file.Delete();
                                            }
                                            if (name == "sylphMage")
                                                {
                                                    LoggerInstance.Msg("IMPORTANT: CORE WORKSHOP FUNCTIONALITY MOD UPDATED; IF THIS MOD IS ABSCENT FROM SUBSEQUENT STARTUPS REINSTALL IT");
                                                }
                                                LoggerInstance.Msg(name + " has been successfuly updated/added; restart TGP one or two times to apply the change :3c");
                                        }
                                        catch
                                        {
                                            LoggerInstance.Msg("was " + name + "even here?");
                                        }
                                        }
                                        else
                                        {
                                            LoggerInstance.Msg(name+" exists in the folder :3");
                                        }

                                    }
                                }
                            foreach (var item in Directory.GetFileSystemEntries(secondCandidate))
                                {
                                    CopyFilesRecursively(filePath, symbolicLink);
                                }
                            } catch
                            {
                                LoggerInstance.Msg("IMPORTANT: Please screenshot this part of the console and send it to nova if you're seeing this; I done fucked up somehow");
                            }
                        }
                    }
                }
            }
        }
    }