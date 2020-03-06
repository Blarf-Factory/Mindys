using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    public bool floor = true;
    public bool gravity = false;
    public float gravAmount = 15f;
    public bool airlock = false;
    private Rigidbody rb;
    private Vector3 direction;

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gravity)
        {
            if (floor)
                ApplyGravity(-1f);
            else
                ApplyGravity(1f);
        }
        else if (airlock)
        {
            ApplyVacuum(direction);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Floor Grav Trigger")
        {
            gravity = true;
            floor = true;
        }
        else if (col.gameObject.name == "Ceiling Grav Trigger")
        {
            gravity = true;
            floor = false;
        }
        else if (col.gameObject.tag == "Airlock")
        {
            Debug.Log(this.gameObject.name + " is in the airlock");
            gravity = false;
            airlock = true;
            direction = col.transform.forward;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.name == "Floor Gravity")
        {
            Debug.Log(this.gameObject.name + " exited floor gravity");
            gravity = false;
            
        }
        else if (col.gameObject.name == "Ceiling Gravity")
        {
            Debug.Log(this.gameObject.name + " exited ceiling gravity");
            gravity = false;
            
        }
        else if (col.gameObject.tag == "Airlock")
        {
            Debug.Log(this.gameObject.name + " is leaving the airlock");
            airlock = false;
        }
    }

    void ApplyGravity(float direction)
    {
        rb.AddForce(new Vector3(0f,direction * gravAmount,0f), ForceMode.Acceleration);
    }

    void ApplyVacuum(Vector3 direction)
    {
        rb.AddForce(direction * 15f, ForceMode.Acceleration);
        Debug.Log("Applyed Vacuum");
    }
}
