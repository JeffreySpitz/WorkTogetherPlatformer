using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem
{

    public static void SavePlayer(int next_level)
    {
        string save_path = Path.Combine(Application.persistentDataPath, "player_data.bin");
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(save_path, FileMode.Create);

        PlayerData data = new PlayerData(next_level);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer()
    {
        string save_path = Path.Combine(Application.persistentDataPath, "player_data.bin");
        if (File.Exists(save_path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(save_path, FileMode.Open);
            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.Log("Save file not found in " + save_path);
            return null;
        }
    }
}
