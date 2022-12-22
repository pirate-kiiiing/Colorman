using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    private static SaveData data;
    public static SaveData Data 
    {
        get
        {
            if (data != null) return data;

            data = Load();
            
            return data;
        }
        set { }
    }

    private static string path = Application.persistentDataPath + "/data.colorman";

    public static void Save()
    {
        var formatter = new BinaryFormatter();
        var stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, Data);
        stream.Close();
    }

    private static SaveData Load()
    {
        if (File.Exists(path) == false)
        {
            return new SaveData
            {
                Level = 1,
                RateUsTimer = DateTime.Now,
            };
        }

        var formatter = new BinaryFormatter();
        var stream = new FileStream(path, FileMode.Open);

        try
        {
            var data = formatter.Deserialize(stream) as SaveData;

            return data;
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to deserialize {nameof(SaveData)}");
            Debug.LogError(e.ToString());

            throw;
        }
        finally
        {
            stream.Close();
        }
    }
}

[Serializable]
public class SaveData
{
    public int Level { get; set; }
    public DateTime RateUsTimer { get; set; }
}
