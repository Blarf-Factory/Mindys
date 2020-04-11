using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperSpatula : MonoBehaviour
{
    private Animator animator;
    private bool holding = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !holding)
        {
            animator.SetBool("holding", true);
            holding = true;
        }
        else if (Input.GetKeyDown(KeyCode.Q) && holding)
        {
            animator.SetBool("holding", false);
            holding = false;
        }
    }
}
