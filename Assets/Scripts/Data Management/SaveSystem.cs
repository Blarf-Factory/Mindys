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
    FileStream dataStream = new FileStream(getGameDataPath() + "/" + saveData.crewname + ".blurf", FileMode.Create);

    BinaryFormatter converter = new BinaryFormatter();
    converter.Serialize(dataStream, saveData);

    dataStream.Close();
  }

  public static GameData LoadGame(GameData saveData)
  {
    if(File.Exists(getGameDataPath() + "/" + saveData.crewname + ".blurf"))
    {
      // File exists 
      FileStream dataStream = new FileStream(getGameDataPath() + "/" + saveData.crewname + ".blurf", FileMode.Open);

      BinaryFormatter converter = new BinaryFormatter();
      saveData = converter.Deserialize(dataStream) as GameData;

      dataStream.Close();
      return saveData;
    }
    else
    {
    // File does not exist
    Debug.LogError("Save file not found in " + getGameDataPath() + "/" + saveData.crewname + ".blurf");
    return null;
    }
  }

  public static string getGameDataPath()
    {
        string gameDataPath;
        #if UNITY_STANDALONE_WIN
            gameDataPath =
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments).Replace("\\", "/");
            gameDataPath += "/My Games/Mindys/";
        #else
            savedGamesPath = Application.persistentDataPath + "/";
        #endif
        return gameDataPath;
    }
}
