﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkObject : NetworkBehaviour
{

    public GameObject ServerObject;

    // Start is called before the first frame update
    void Start()
    {
        if ( !ServerObject )
        {
            (GetComponent<NetworkObject>() as MonoBehaviour).enabled = false;
        }
        else if ( !hasAuthority )
        {
            (GetComponent<PlayerUnit>() as MonoBehaviour).enabled = false;
        }

        PrevLocation = transform.position;
    }

    Vector3 velocity;
    Vector3 estPostion;
    Vector3 PrevLocation;
    public float latency = 1;
    public float smoothingFactor = 10;

    // Update is called once per frame
    void Update()
    {
        if (!hasAuthority)
        {
            estPostion = estPostion + (velocity * Time.deltaTime);
            transform.position = estPostion;                 //Vector3.Lerp(transform.position, estPostion, (Time.deltaTime * smoothingFactor));
            return;
        }

        transform.Translate(velocity * Time.deltaTime);

        if (PrevLocation != transform.position)
        {
            CmdUpdateVelocity(velocity, transform.position);
        }

        PrevLocation = transform.position;
    }
    
    [Command]
    void CmdUpdateVelocity(Vector3 v, Vector3 p)
    {
        transform.position = p;
        velocity = v;

        RpcUpdateVelocity(velocity, transform.position);
    }

    [ClientRpc]
    void RpcUpdateVelocity(Vector3 v, Vector3 p)
    {
        if (hasAuthority)
        {
            return;
        }

        //transform.position = p;

        velocity = v;
        estPostion = p + (velocity * (latency));
    }
}
