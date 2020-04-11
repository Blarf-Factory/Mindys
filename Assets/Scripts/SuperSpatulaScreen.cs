using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperSpatulaScreen : MonoBehaviour
{
    private Animator animator;
    private bool viewScreen = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        //this.transform.localScale = new Vector3(0f, 1f, 1f);
        //this.transform.position = new Vector3(-0.37f, 0f, 0f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !viewScreen)
        {
            animator.SetBool("viewScreen", true);
            viewScreen = true;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && viewScreen)
        {
            animator.SetBool("viewScreen", false);
            viewScreen = false;
        }
    }
}
