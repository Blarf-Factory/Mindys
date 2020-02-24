using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerUnit : NetworkBehaviour
{
    public GameObject cameraObj;
    public float movementSpeed = 5;
    public float mouseSensitivityX = 5;
    public float mouseSensitivityY = 5;

    // Start is called before the first frame update
    void Start()
    {
        if (!hasAuthority)
        {
            return;
        }
    }

    private void Awake()
    {
        
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
        

        transform.Translate(velocity * Time.deltaTime);

		MagBootsControls();
        CameraControls();
        cameraObj.SetActive(true);
    }


    void CameraControls()
    {
        float pitch = 0;
        float yaw = 0;
        float viewRange = 200;

        pitch = mouseSensitivityY * Input.GetAxisRaw("Mouse Y"); // get mouse pitch
        yaw = mouseSensitivityX * Input.GetAxisRaw("Mouse X"); // get mouse yaw

        this.transform.Rotate(0f, yaw, 0f); // turns player
        cameraObj.transform.Rotate(-pitch, 0f, 0f); // moves camera up and down

        // if (cameraObj.transform.localEulerAngles.x > viewRange // check to see if camera view is outside range
        //     && cameraObj.transform.localEulerAngles.x < 180)
        // {
        //     cameraObj.transform.localEulerAngles = new Vector3(viewRange, 0, 0); // reset camera position to inside range
        // }

        // if (cameraObj.transform.localEulerAngles.x < viewRange + 200
        //             && cameraObj.transform.localEulerAngles.x > 180) // check to see if camera view is outside range
        // {
        //     cameraObj.transform.localEulerAngles = new Vector3(-viewRange, 0, 0); // reset camera position to inside range
        // }
    }

	void MagBootsControls()
    {
        velocity = new Vector3(0, 0, 0);

        if (Input.GetKey(KeyCode.W))
        {
            //this.transform.Translate(Vector3.forward * Time.deltaTime);
            velocity += Vector3.forward * movementSpeed;
            CmdUpdateVelocity(velocity, transform.position);
        }
        if (Input.GetKey(KeyCode.S))
        {
            //this.transform.Translate(Vector3.back * Time.deltaTime);
            velocity += Vector3.back * movementSpeed;
            CmdUpdateVelocity(velocity, transform.position);
        }
        if (Input.GetKey(KeyCode.A))
        {
            //this.transform.Translate(Vector3.left * Time.deltaTime);
            velocity += Vector3.left * movementSpeed;
            CmdUpdateVelocity(velocity, transform.position);
        }
        if (Input.GetKey(KeyCode.D))
        {
            //this.transform.Translate(Vector3.right * Time.deltaTime);
            velocity += Vector3.right * movementSpeed;

        }   
        CmdUpdateVelocity(velocity, transform.position);
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
