using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LoadGameUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LoadGamePreviews();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadGamePreviews()
    {
        string path = SaveSystem.getGameDataPath();
        DirectoryInfo dir = new DirectoryInfo(path);
        FileInfo[] info = dir.GetFiles("*.blarf*");
 
        foreach (FileInfo f in info)
        {
            Debug.Log(removeFileExe(f.Name));
        }
    }

    private string removeFileExe(string s)
    {
        return s.Substring(0, s.Length - 6);
    }
}
