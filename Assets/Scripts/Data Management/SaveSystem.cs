using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Environment = System.Environment;

public static class SaveSystem
{
  public static void SaveGame(GameData saveData)
  {
    FileStream dataStream = new FileStream(getGameDataPath() + "/" + saveData.crewname + ".blarf", FileMode.Create);

    BinaryFormatter converter = new BinaryFormatter();
    converter.Serialize(dataStream, saveData);

    dataStream.Close();
  }

  public static GameData LoadGame(string saveData)
  {
    if(File.Exists(getGameDataPath() + "/" + saveData + ".blarf"))
    {
      // File exists 
      FileStream dataStream = new FileStream(getGameDataPath() + "/" + saveData + ".blarf", FileMode.Open);

      BinaryFormatter converter = new BinaryFormatter();
      GameData Data = converter.Deserialize(dataStream) as GameData;

      dataStream.Close();
      return Data;
    }
    else
    {
    // File does not exist
    Debug.LogError("Save file not found in " + getGameDataPath() + "/" + saveData + ".blarf");
    return null;
    }
  }

  public static string getGameDataPath()
    {
        string gameDataPath;
        #if UNITY_STANDALONE_WIN
            gameDataPath =
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments).Replace("\\", "/");
            gameDataPath += "/My Games/Mindys/Saves";
        #else
            savedGamesPath = Application.persistentDataPath + "/";
        #endif
        return gameDataPath;
    }
}
