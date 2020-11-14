using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSavePreview : MonoBehaviour
{
    public void SelectSaveDataHelper()
    {
        GameObject.Find("LoadGameUI").GetComponent<LoadGameUI>().SelectSaveData(this.gameObject);
    }
}
