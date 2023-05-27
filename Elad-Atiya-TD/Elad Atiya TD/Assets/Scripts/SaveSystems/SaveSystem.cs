using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveData ()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/savedata.EladAtiya";
        FileStream stream = new FileStream(path, FileMode.Create);

        DataToSave data = new DataToSave();

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static DataToSave LoadData()
    {
        string path = Application.persistentDataPath + "/savedata.EladAtiya";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            DataToSave data = formatter.Deserialize(stream) as DataToSave;

            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
