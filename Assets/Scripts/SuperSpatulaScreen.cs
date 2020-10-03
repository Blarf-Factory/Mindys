using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperSpatulaScreen : MonoBehaviour
{
    private Animator animator;
    private bool viewScreen = false;
    public GameObject MainMenuObject;
    public GameObject PauseMenuObject;
    public GameObject StartUpLogo;
    private bool first;

    private AudioSource sound;
    public AudioClip slide;
    public AudioClip startSound;

    void Start()
    {
        animator = GetComponent<Animator>();
        sound = GetComponent<AudioSource>();
        first = true;
    }

    public void changeScreen(bool pause)
    {
        sound.clip = slide;
        sound.Play();
        if (!viewScreen)
        {
            animator.SetBool("viewScreen", true);
            viewScreen = true;
            GameObject.Find("ScreenObject").GetComponent<Light>().enabled = true;
            if (first && !pause)
            {
                StartUp();
                first = false;
            } else if(first && pause)
            {
                openPauseMenu();
            }
        }
        else if (viewScreen)
        {
            GameObject.Find("ScreenObject").GetComponent<Light>().enabled = false;
            animator.SetBool("viewScreen", false);
            viewScreen = false;
        }
    }

    public void StartUp()
    {
        StartUpLogo.SetActive(true);
        Invoke("startUpSound", 2f);
        Invoke("OpenMainMenu", 3f);
    }

    public void openPauseMenu()
    {
        PauseMenuObject.SetActive(true);
    }

    private void OpenMainMenu()
    {
        MainMenuObject.SetActive(true);
    }

    public void startUpSound()
    {
        sound.clip = startSound;
        sound.Play();
    }
}
