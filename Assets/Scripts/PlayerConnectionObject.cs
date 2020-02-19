using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerConnectionObject : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        CmdSpawn();
    }

    public GameObject PlayerUnit;

    [SyncVar]
    public string PlayerName = "Loser";

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
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

    [Command]
    void CmdChangePlayerName(string n)
    {
        PlayerName = n;
        //RpcChangePlayerName(PlayerName);
    }

    [ClientRpc]
    void RpcChangePlayerName(string n)
    {
        PlayerName = n;
    }
}
