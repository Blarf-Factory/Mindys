using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerUnit : MonoBehaviour
{
    public GameObject cameraObj;
    public float movementSpeed = 5f;
    public float ZeroGMoveSpeed = 40f;
    public float xSensitivity = 5f;
    public float ySensitivity = 5f;
    public float sprintSpeed = 1.5f;
    public float jumpHeight = 1f;
    public float gravity = 9.8f;
    public Rigidbody playerRB;
    public bool magboots = true;
    public bool reorient = false;
    public float reorientSpeed = 75f;
    private Quaternion targetRotation;
    
    /*
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
    */

    Vector3 velocity;
    Vector3 estPostion;
    public float latency = 1;
    public float smoothingFactor = 10;

    // Update is called once per frame
    void Update()
    {
        /*
        //velocity = new Vector3(0, 0, 0);
        if (!hasAuthority)
        {
            estPostion = estPostion + (velocity * Time.deltaTime);
            transform.position = estPostion;                 //Vector3.Lerp(transform.position, estPostion, (Time.deltaTime * smoothingFactor));
            return;
        }

        if (!hasAuthority)
        {
            estPostion = estPostion + (velocity * Time.deltaTime);
            transform.position = estPostion;                 //Vector3.Lerp(transform.position, estPostion, (Time.deltaTime * smoothingFactor));
            return;
        }
        */

        transform.Translate(velocity * Time.deltaTime);

        if (magboots)
        {
            MagBootsControls();
        }
        else
        {
            ZeroGravControls();
        }

        if (reorient) // if magboots are activated, reorient player towards floor
        {
            SetOrientation();
        }
        else
        {
            if (magboots)
            {
                MagBootsCameraControls();
            }
            else
            {
                ZeroGCameraControls();
            }
        }

        cameraObj.SetActive(true);
    }


    void MagBootsCameraControls()
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
    void ZeroGCameraControls()
    {


        float camPitch = ySensitivity * Input.GetAxisRaw("Vertical") * Time.deltaTime; // get mouse pitch
        float camYaw = xSensitivity * Input.GetAxisRaw("Horizontal") * Time.deltaTime; // get mouse yaw

        this.transform.Rotate(-camPitch, camYaw, 0f);
    }


	void MagBootsControls()
    {
        velocity = new Vector3(0, 0, 0);
        float modifier = 1; // used for sprint and other effects



        if (Vector3.Dot(this.transform.up, Vector3.down) > 0)
        {
            playerRB.AddForce(new Vector3(0f, gravity * Time.deltaTime, 0f), ForceMode.Force);
        }
        else
        {
            playerRB.AddForce(new Vector3(0f, -gravity * Time.deltaTime, 0f), ForceMode.Force);
        }

        velocity.y = playerRB.velocity.y;


        Debug.Log("L x-axis: " + Input.GetAxisRaw("Strafe"));
        Debug.Log("L y-axis: " + Input.GetAxisRaw("Walk"));
        float xInput = Input.GetAxisRaw("Strafe");
        float yInput = Input.GetAxisRaw("Walk");

        if (yInput != 0)
        {
            if (yInput > 0 && Input.GetButton("Sprint"))
            {
                modifier = sprintSpeed;
            }
            velocity += Vector3.forward * yInput * movementSpeed * modifier;

        }
        if (xInput != 0)
        {
            
            velocity += Vector3.right * xInput * movementSpeed;
        }

        if (Input.GetButtonDown("Y"))
        {
            magboots = false;
            OrientPlayerToCamera();
        }

        //CmdUpdateVelocity(velocity, transform.position);
    }

    void ZeroGravControls()
    {
        float xInput = Input.GetAxisRaw("Strafe");
        float yInput = Input.GetAxisRaw("Walk");
        float zInput = Input.GetAxisRaw("UpDown");
        velocity = new Vector3(0, 0, 0);

        if (yInput != 0)
        {
            playerRB.AddRelativeForce(new Vector3(0f, 0f, ZeroGMoveSpeed * yInput * Time.deltaTime), ForceMode.Force);
        }

        if (xInput != 0)
        {
            playerRB.AddRelativeForce(new Vector3(ZeroGMoveSpeed * xInput * Time.deltaTime, 0f, 0f), ForceMode.Force);
        }
        if (zInput != 0)
        {
            playerRB.AddRelativeForce(new Vector3(0f, ZeroGMoveSpeed * zInput * Time.deltaTime, 0f), ForceMode.Force);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            this.transform.Rotate(0f, 0f, ZeroGMoveSpeed);
        }
        if (Input.GetKey(KeyCode.E))
        {
            this.transform.Rotate(0f, 0f, -ZeroGMoveSpeed);
        }

        if (Input.GetButtonDown("Y"))
        {
            magboots = true;
            reorient = true;
        }

        velocity += playerRB.velocity;

        //CmdUpdateVelocity(velocity, transform.position);
    }

    void OrientPlayerToCamera()
    {
        float x;
        if (Vector3.Dot(this.transform.up, Vector3.down) > 0) // find if player is upsidedown
            x = -cameraObj.transform.eulerAngles.x; // rotate down
        else
            x = cameraObj.transform.eulerAngles.x; // rotate up

        this.transform.RotateAround(cameraObj.transform.position, this.transform.right, x); // rotate player around the camera
        cameraObj.transform.localEulerAngles = new Vector3(0f,0f,0f); // reset the local rotation of the camera to 0
    }
    void SetOrientation()
    {
        
        float yRot = this.transform.localEulerAngles.y;
            if (Vector3.Dot(this.transform.up, Vector3.down) > 0)
            {
                targetRotation = Quaternion.Euler(0f,yRot,180f);
            }
            else
            {
                targetRotation = Quaternion.Euler(0f,yRot,0f);
            }
            this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation,
                                                            targetRotation,
                                                            reorientSpeed * Time.deltaTime);
            if (targetRotation.eulerAngles == this.transform.eulerAngles)
            {
                Debug.Log("Completed Orientation Change");
                reorient = false;
            }
    }
    /*
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
    */
}
