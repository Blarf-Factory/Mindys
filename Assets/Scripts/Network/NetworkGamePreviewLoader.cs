using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class NetworkGamePreviewLoader : MonoBehaviour
{
    public GameObject prefab;
    private string[] names;
    private string[] ips;


    // Start is called before the first frame update
    void Start()
    {
        RefreshNetworkGames();
    }

    void RefreshNetworkGames()
    {
        LoadNetworkAddressbook();
        PopulateFriends();
    }

    void LoadNetworkAddressbook()
    {
        //Get Addressbook from local drive
        string[] addressbook;
        if(File.Exists(SaveSystem.getGameDataPath() + "/addressbook.blarf"))
        {
            // File exists 
            FileStream file = new FileStream(SaveSystem.getGameDataPath() + "/addressbook.blarf", FileMode.Open);
            addressbook = file.ToString().Split('\n');
            file.Close();

            //Get GameData from Network using Addressbook
            names = new string[addressbook.Length];
            ips    = new string[addressbook.Length];
            for(int i = 0; addressbook.Length == 0; i++){
                names[i] = addressbook[i].Substring(20);
                ips[i]   = addressbook[i].Substring(20, (addressbook[i].Length - 1));
            }
        }
        else
        {
            // File does not exist
            Debug.LogError("Save file not found in " + SaveSystem.getGameDataPath() + "/addressbook.blarf");
        }
    }

    void PopulateFriends()
    {
        GameObject obj;

        for(int i = 0; i < names.Length; i++)
        {
            obj = (GameObject)Instantiate(prefab, transform);
            obj.GetComponentsInChildren<Text>()[0].text = name[i].ToString();
        }
    }
}
