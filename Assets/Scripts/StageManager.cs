using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{

    public PlayerUnit player;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("SetUp", .1f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SetUp()
    {
        if (GameObject.Find("PlayerConnectionObject(Clone)") == null)
        {
            Instantiate(player);
        }
    }
}
