using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class SaveSystem  
{
    private static readonly string SAVE_FOLDER = Application.dataPath + "/Saves/";

    public static void Init()
    {
        if (!Directory.Exists(SAVE_FOLDER))
        {
            Directory.CreateDirectory(SAVE_FOLDER);
        }
    }

    public static void Save(string  saveString, string saveFile)
    {
        File.WriteAllText(SAVE_FOLDER + saveFile, saveString);
    }

    public static string Load(string saveFile)
    {
        if(File.Exists(SAVE_FOLDER+saveFile))
        {
            string saveString = File.ReadAllText(SAVE_FOLDER+saveFile);
            return saveString;
        }
        return null;
    }
}
