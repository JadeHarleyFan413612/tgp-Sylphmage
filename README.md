# tgp-Sylphmage
Small melonloader helper tool to facilitate the retrieval of melonloader mods uploaded to the tgp workshop.
it also facilitates auto-updating of tgp workshop mods

# why sylphmage?
I generally classpect myself as both sylph and mage of space (hence sylphmage) and I felt like making this program was particularly sylphmage of spacey of me :p 
                           (oh also I suck at naming things so it was an easy way out)

# how do I use this mod?
well I mean I said so on the workshop page for this silly, but if you want a recap:
1. Install [the melonloader installer](https://github.com/LavaGang/MelonLoader.Installer/releases)
2. use it to install melonloader to tgp
3. go to your steam library
4. right click on tgp
5. select "properties"
6. select "installed files"
7. select "browse" on the top right corner of the screen
8. Put SylphMage.dll and SylphMage.x.x.x (where x.x.x is the version number) into the "Mods" folder
9. subscribe to the sylphmage workshop mod
10. restart tgp :3

# how do I use this to upload my melonloader mods to the TGP workshop?
**NOTE:** at the moment **this tool can only support standalone DLL melonloader mods** (i.e. no libraries); if your mod requires userlibs, or requires non-dll files please wait until I add that functionality (or you can contribute to this project and add it yourself :3 

the process is about the same as uploading a normal tgp workshop mod with a few key differences:
after creating the inital folder that houses your mod files as per usual with tgp workshop mods you will want to create an "AssetBundles" folder and in that folder a "mod" folder (the names are case sensitive); from here place your yourModName.dll file in this folder alongside a yourModName.x.x.x file (the number at the end is the version number; each x *must be* a single digit ranging from 0 to 9, and I would recommend setting it to 0.0.1 initially but you can do as you wish). If you have more than one dll file they must all have an associated "dllFilename.x.x.x" file. the name.x.x.x files can be any file but for simplicity's sake I usually make them empty text files (do note that they can NOT have an extension besides .x.x.x).
all your melonloader mods *must* be in the mod folder; sub-folders are currently not supported.
when you want to update your workshop mod you must replace the yourModName.dll file with an identically named file, and increase the associated version number. When this happens sylphMage will detect the change and automatically retrieve the updated version.

# how do I contribute?
You'll have to use Visual Studio 2022 either on windows or winboat or something. if there's any errors with file references I don't entirely know how to fix that but let me know and we can try to figure it out.
