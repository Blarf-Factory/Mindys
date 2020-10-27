using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SuperSpatula : MonoBehaviour
{
    private GameObject screen;
    public GameObject MainMenuObject;
    public GameObject PauseMenuObject;
    public GameObject StartUpLogo;
    public GameObject StartAnimate;
    public bool ownedByPlayer;
    private Animator animator;
    private bool holding = false;
    private int mode;
    private AudioSource sound;
    public AudioClip slide;
    public AudioClip startSound;
    private bool viewScreen = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        screen = GameObject.Find("Screen");
        sound = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(SceneManager.GetActiveScene().name == "MainMenu")
        {
            if (Input.GetKeyDown(KeyCode.Space) && !holding)
            {
                Use();
                Invoke("changeScreenHelper", 1f);
                //Invoke("StartUp", 1f);
            }
            else if (Input.GetKeyDown(KeyCode.Space) && holding)
            {
                changeScreenHelper();
                Invoke("Use", .25f);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Tab) && !holding)
            {
                Use();
                Invoke("changeScreenHelper", 1f);
            }
            else if (Input.GetKeyDown(KeyCode.Space) && holding)
            {
                changeScreenHelper();
                Invoke("Use", .25f);
            }
        }
    }

    public void changeScreen(int mode)
    {
        sound.clip = slide;
        sound.Play();
        if(!viewScreen)
        {
            openScreen();
            switch(mode)
            {
                //-- MainMenu Mode --//
                case 0:
                    bootUp();
                break;

                //-- Pause Mode --//
                case 1:
                    openPauseMenu();
                break;
            }
        }
        else
        {
            closeScreen();
        }
    }

    private void changeScreenHelper()
    {
        changeScreen(0);
    }

    public void openScreen()
    {
        screen.GetComponent<SuperSpatulaScreen>().animator.SetBool("viewScreen", true);
        viewScreen = true;
        GameObject.Find("ScreenObject").GetComponent<Light>().enabled = true;
    }

    public void closeScreen()
    {
        GameObject.Find("ScreenObject").GetComponent<Light>().enabled = false;
        screen.GetComponent<SuperSpatulaScreen>().animator.SetBool("viewScreen", false);
        viewScreen = false;
    }

    public void Use()
    {
        if (!holding)
        {
            animator.SetBool("holding", true);
            holding = true;
        }
        else if (holding)
        {
            animator.SetBool("holding", false);
            holding = false;
        }
    }

    public void StartUp()
    {
        StartAnimate.SetActive(true);
    }
    public void bootUp()
    {
        StartUpLogo.SetActive(true);
        Invoke("startUpSound", 2f);
        Invoke("OpenMainMenu", 3f);
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

    public void openPauseMenu()
    {
        PauseMenuObject.SetActive(true);
    }
}
