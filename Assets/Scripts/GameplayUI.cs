using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayUI : MonoBehaviour
{
    bool paused;
    public GameObject pausedImage;

    // Start is called before the first frame update
    void Start()
    {
        paused = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        // Pausing
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                Screen.lockCursor = true;
                paused = false;
                pausedImage.SetActive(false);
            }
            else
            {
                Screen.lockCursor = false;
                paused = true;
                pausedImage.SetActive(true);
            }

        }

    }
}
