using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Settings;
    // Start is called before the first frame update
    void Start()
    {
        Settings.GetComponent<SettingsMenu>().LoadSettings();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
