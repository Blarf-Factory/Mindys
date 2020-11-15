using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSavePreview : MonoBehaviour
{
    
        float r, g, b;
    void Start()
    {
        r = GetComponent<Image>().color.r;
        g = GetComponent<Image>().color.g;
        b = GetComponent<Image>().color.b;
    }
    void Update()
    {            
        if( this.gameObject.Equals(GameObject.Find("LoadGameUI").GetComponent<LoadGameUI>().selectedSave) )
        {
            GetComponent<Image>().color = new Color(r, g, b, 1f);
        }
        else
        {
            GetComponent<Image>().color = new Color(r, g, b, 0.35f);
        }
    }
    public void SelectSaveDataHelper()
    {
        GameObject.Find("LoadGameUI").GetComponent<LoadGameUI>().SelectSaveData(this.gameObject);
    }
}
