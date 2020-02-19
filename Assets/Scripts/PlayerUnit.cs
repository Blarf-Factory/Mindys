using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerUnit : NetworkBehaviour
{
    public GameObject camera;

    // Start is called before the first frame update
    void Start()
    {
        if (!hasAuthority)
        {
            return;
        }
    }

    Vector3 velocity;
    Vector3 estPostion;
    public float latency = 1;
    public float smoothingFactor = 10;

    // Update is called once per frame
    void Update()
    {
        //velocity = new Vector3(0, 0, 0);
        if (!hasAuthority)
        {
            estPostion = estPostion + (velocity * Time.deltaTime);
            transform.position = estPostion;                 //Vector3.Lerp(transform.position, estPostion, (Time.deltaTime * smoothingFactor));
            return;
        }
        //GetComponent<Camera>().SetActive(true);
        camera.SetActive(true);

        transform.Translate(velocity * Time.deltaTime);

		MagBootsControls();
        
    }

	void MagBootsControls()
    {
        if (Input.GetKey(KeyCode.W))
        {
            //this.transform.Translate(Vector3.forward * Time.deltaTime);
            velocity = new Vector3(0, 0, 1);
            CmdUpdateVelocity(velocity, transform.position);
        }
        if (Input.GetKey(KeyCode.S))
        {
            //this.transform.Translate(Vector3.back * Time.deltaTime);
            velocity = new Vector3(0, 0, -1);
            CmdUpdateVelocity(velocity, transform.position);
        }
        if (Input.GetKey(KeyCode.A))
        {
            //this.transform.Translate(Vector3.left * Time.deltaTime);
            velocity = new Vector3(-1, 0, 0);
            CmdUpdateVelocity(velocity, transform.position);
        }
        if (Input.GetKey(KeyCode.D))
        {
            //this.transform.Translate(Vector3.right * Time.deltaTime);
            velocity = new Vector3(1, 0, 0);
            CmdUpdateVelocity(velocity, transform.position);
        }
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
        if(hasAuthority)
        {
            return;
        }

        //transform.position = p;

        velocity = v;
        estPostion = p + (velocity * (latency));
    }
}
