using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class NetworkUI : NetworkManager
{
    bool SetUpnp = false;
    public GameObject NetworkGameManager;
    public void StartUpHost()
    {
        if (SetUpnp) { GetComponent<Upnp>().enabled = true; }
        SetPort();
        NetworkManager.singleton.StartHost();
    }

    public void JoinGame()
    {
        SetIPAddress();
        SetPort();
        NetworkManager.singleton.StartClient();
    }

    void SetIPAddress()
    {
        string ipAddress = GameObject.Find("InputFieldIPAddress").transform.Find("Text").GetComponent<Text>().text;
        singleton.networkAddress = ipAddress;
    }

    void SetPort()
    {
    //    NetworkManager.singleton.networkPort = 7777;
    }
}

