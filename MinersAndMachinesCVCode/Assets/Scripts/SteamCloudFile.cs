using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SteamCloudFile
{
    private const string filename = "/SteamCloud_MinersAndMachines.sav";

    public static void SaveData(SteamCloudPrefs steamCloudPrefs)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + filename, FileMode.Create);

        bf.Serialize(stream, steamCloudPrefs);
        stream.Close();
    }

    public static SteamCloudPrefs LoadData()
    {
        if(File.Exists(Application.persistentDataPath + filename))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + filename, FileMode.Open);

            if (stream.Length > 0)
            {
                SteamCloudPrefs steamCloudPrefs = bf.Deserialize(stream) as SteamCloudPrefs;
                stream.Close();
                return steamCloudPrefs;
            }
            else
            {
                stream.Close();
                return null;
            }
        }
        else
        {
            Debug.LogError("Bad save file");
            return null;
        }
    }

}