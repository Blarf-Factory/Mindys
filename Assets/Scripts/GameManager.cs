using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject ServerObject;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        if (ServerObject)
        {
            player.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
