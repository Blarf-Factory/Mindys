using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class SaveDataPreviewLoader : MonoBehaviour
{
    public GameObject prefab;
    string[] gameSaves;

    // Start is called before the first frame update
    void Start()
    {
        gameSaves = LoadGamePreviews();
        Populate();
    }

    string[] LoadGamePreviews()
    {
        string path = SaveSystem.getGameDataPath();
        DirectoryInfo dir = new DirectoryInfo(path);
        FileInfo[] info = dir.GetFiles("*.blarf*");
        string[] titles = new string[info.Length];
 
        for(int i = 0; i < titles.Length; i++)
        {
            titles[i] = info[i].Name.Substring(0, info[i].Name.Length - 6);
        }

        return titles;
    }

    void Populate()
    {
        GameObject obj;

        for(int i = 0; i < gameSaves.Length; i++)
        {
            obj = (GameObject)Instantiate(prefab, transform);
            obj.GetComponentsInChildren<Text>()[0].text = gameSaves[i];
        }
    }
}
