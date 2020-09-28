using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetworkObject : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (hasAuthority == false)
        {
            //GetComponent<PlayerUnit>().enabled = false;
            
            return;
        }

        PrevLocation = transform.position;
        PrevRotation = transform.rotation;
    }

    public override void OnStartAuthority()
    {
        if (!hasAuthority)
        {
            camera.SetActive(false);
            GetComponent<PlayerUnit>().enabled = false;
        }
    }

    Vector3 velocity;
    Vector3 estPostion;
    Vector3 PrevLocation;
    Quaternion estRotation;
    Quaternion PrevRotation;
    public float latency = 1;
    public float smoothingFactor = 10;

    public GameObject camera;

    // Update is called once per frame
    void Update()
    {
        if (!hasAuthority)
        {

            camera.SetActive(false);
            GetComponent<PlayerUnit>().enabled = false;
            /**
            estPostion = estPostion + (velocity * Time.deltaTime);
            transform.position = estPostion;                 //Vector3.Lerp(transform.position, estPostion, (Time.deltaTime * smoothingFactor));
            
            transform.rotation = estRotation;                 //Vector3.Lerp(transform.position, estPostion, (Time.deltaTime * smoothingFactor));
            */
            return;
        }

        transform.Translate(velocity * Time.deltaTime);

        if (PrevLocation != transform.position)
        {
            CmdUpdateVelocity(velocity, transform.position);
        }

        if (PrevRotation != transform.rotation)
        {
            CmdUpdateRotation(transform.rotation);
        }

        PrevLocation = transform.position;
        PrevRotation = transform.rotation;
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

    [Command]
    void CmdUpdateRotation(Quaternion r)
    {
        transform.rotation = r;

        RpcUpdateRotation(transform.rotation);
    }

    [ClientRpc]
    void RpcUpdateRotation(Quaternion r)
    {
        if (hasAuthority)
        {
            return;
        }
        
        estRotation = r;
    }
}
