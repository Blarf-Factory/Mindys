using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class SaveDataPreviewLoader : MonoBehaviour
{
    public GameObject prefab;
    GameData[] gameSaves;

    // Start is called before the first frame update
    void Start()
    {
        gameSaves = LoadGamePreviews();
        Populate();
    }

    GameData[] LoadGamePreviews()
    {
        string path = SaveSystem.getGameDataPath();
        DirectoryInfo dir = new DirectoryInfo(path);
        FileInfo[] info = dir.GetFiles("*.blarf*");
        GameData[] titles = new GameData[info.Length];
 
        for(int i = 0; i < titles.Length; i++)
        {
            titles[i] = SaveSystem.LoadGame(info[i].Name);
        }

        return titles;
    }

    void Populate()
    {
        GameObject obj;

        for(int i = 0; i < gameSaves.Length; i++)
        {
            obj = (GameObject)Instantiate(prefab, transform);
            obj.GetComponentsInChildren<Text>()[0].text = gameSaves[i].crewname;
            obj.GetComponentsInChildren<Text>()[1].text = gameSaves[i].lastplayed.ToString();
            obj.GetComponentsInChildren<Text>()[2].text = gameSaves[i].totaltime.ToString();
        }
    }
}
