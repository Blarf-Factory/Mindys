using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SuperSpatula : MonoBehaviour
{
    private GameObject screen;
    public GameObject StartAnimate;
    private Animator animator;
    private bool holding = false;
    private int mode;

    void Start()
    {
        animator = GetComponent<Animator>();
        screen = GameObject.Find("Screen");
    }

    private void Update()
    {
        if(SceneManager.GetActiveScene().name == "MainMenu")
        {
            mode = 1;
        }
        else
        {
            mode = 0;
        }

        switch (mode)
        {
            case 0:
                //Do Nothing
                break;

            case 1:
                if (Input.GetKeyDown(KeyCode.Space) && !holding)
                {
                    Use();
                    screen.GetComponent<SuperSpatulaScreen>().Invoke("changeScreen", 1f);
                    //Invoke("StartUp", 1f);
                }
                else if (Input.GetKeyDown(KeyCode.Space) && holding)
                {
                    screen.GetComponent<SuperSpatulaScreen>().changeScreen();
                    Invoke("Use", .25f);
                }
                break;
        }
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
}
