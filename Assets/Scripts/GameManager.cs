using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject Settings;
    public PlayerUnit player;
    // Start is called before the first frame update
    void Start()
    {
        if(SceneManager.GetActiveScene().name == "MainMenu")
        {
            Settings.GetComponent<SettingsMenu>().LoadSettings();
        }
        else
        {
            Invoke("stageStart", .1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void stageStart()
    {
        if (GameObject.Find("PlayerConnectionObject(Clone)") == null)
        {
            Instantiate(player);
        }
    }
}
