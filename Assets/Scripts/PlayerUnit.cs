using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerUnit : NetworkBehaviour
{
    public GameObject cameraObj;
    public float movementSpeed = 5f;
    public float xSensitivity = 5f;
    public float ySensitivity = 5f;
    public float sprintSpeed = 1.5f;

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
        float upperViewLimit = 280;
        float lowerViewLimit = 80;
        
        float camPitch = cameraObj.transform.localEulerAngles.x;
        float camYaw = cameraObj.transform.localEulerAngles.y;

        pitch = ySensitivity * Input.GetAxisRaw("Vertical") * Time.deltaTime; // get mouse pitch
        yaw = xSensitivity * Input.GetAxisRaw("Horizontal") * Time.deltaTime; // get mouse yaw

        this.transform.Rotate(0f, yaw, 0f); // turns player
        cameraObj.transform.Rotate(-pitch, 0f, 0f); // moves camera up and down

        if ( (camPitch < upperViewLimit || camYaw != 0) && camPitch > 180)
        {
           cameraObj.transform.localEulerAngles = new Vector3(upperViewLimit + 0.001f, 0, 0); // reset camera position to inside range
        }

        if ( (camPitch > lowerViewLimit || camYaw != 0) && camPitch < 180) // check to see if camera view is outside range
        {
           cameraObj.transform.localEulerAngles = new Vector3(lowerViewLimit - 0.001f, 0, 0); // reset camera position to inside range
        }
    }

	void MagBootsControls()
    {
        velocity = new Vector3(0, 0, 0);
        float modifier = 1; // used for sprint and other effects

        Debug.Log("L x-axis: " + Input.GetAxisRaw("Strafe"));
        Debug.Log("L y-axis: " + Input.GetAxisRaw("Walk"));
        float xInput = Input.GetAxisRaw("Strafe");
        float yInput = Input.GetAxisRaw("Walk");


        if (yInput != 0)
        {
            
            //this.transform.Translate(Vector3.forward * Time.deltaTime);
            if (yInput > 0 && Input.GetButton("Sprint"))
            {
                modifier = sprintSpeed;
            }
            velocity += Vector3.forward * yInput * movementSpeed * modifier;
            
            Debug.Log("Velocity: " + velocity + " yInput: " + yInput);

            CmdUpdateVelocity(velocity, transform.position);
        }
        if (xInput != 0)
        {
            
            //this.transform.Translate(Vector3.forward * Time.deltaTime);
            velocity += Vector3.right * xInput * movementSpeed;
            CmdUpdateVelocity(velocity, transform.position);
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
