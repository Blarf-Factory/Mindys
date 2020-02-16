using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerConnectionObject : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (isLocalPlayer == false)
        {
            return;
        }

        CmdSpawn();
    }

    public GameObject PlayerUnit;

    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer == false)
        {
            return;
        }
        
    }

    GameObject myPlayerUnit;

    [Command]
    void CmdSpawn()
    {
        GameObject player = Instantiate(PlayerUnit);

        myPlayerUnit = player;
        
        NetworkServer.SpawnWithClientAuthority(player, connectionToClient);
    }
}
