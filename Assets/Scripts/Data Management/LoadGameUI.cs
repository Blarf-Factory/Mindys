using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class LoadGameUI : MonoBehaviour
{
    public GameObject selectedSave;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void SelectSaveData(GameObject g)
    {
        selectedSave = g;
    }

    public void StartFromSave()
    {
        GameObject.Find("GlobalData").GetComponent<GlobalData>().Data = SaveSystem.LoadGame(selectedSave.GetComponentsInChildren<Text>()[0].text);
        Debug.Log(selectedSave.GetComponentsInChildren<Text>()[0].text);
    }

    public void DeleteSave()
    {
        File.Delete(SaveSystem.getGameDataPath() + selectedSave.GetComponentsInChildren<Text>()[0].text + ".blarf");
    }
}
