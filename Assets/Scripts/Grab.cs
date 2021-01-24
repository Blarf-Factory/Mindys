using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Grab : MonoBehaviour
{
    public GameObject player;
    public GameObject rightHand;
    public GameObject dropPoint;
    public GameObject rightHandObj;
    public Transform playerTransform;
    public Transform playerCam;
    public float throwStrength = 5f;
    private float grabRange = 2.5f;
    public bool rightHandIsFree = true;
    private RaycastHit hit;
    public Text useText;

    // Start is called before the first frame update
    void Start()
    {
        rightHandObj = null;
        rightHandIsFree = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.SphereCast(playerCam.position, .1f, playerCam.forward, out hit, grabRange) &&
                     (hit.collider.gameObject.CompareTag("Grabbable") || hit.collider.gameObject.CompareTag("Placeable Surface")))
        {
            GameObject hitObj = hit.collider.gameObject;
            if (hitObj.CompareTag("Grabbable"))
            {
                if (rightHandIsFree)
                {
                    useText.text = "Pickup " + hit.collider.gameObject.name + " (LMB)";
                }
                else
                {
                    useText.text = " ";
                }
            }
            else if (hitObj.CompareTag("Placeable Surface"))
            {
                if (!rightHandIsFree)
                {
                    useText.text = "Place " + rightHandObj.name + " into " + hit.collider.gameObject.name + " (LMB)";
                }
                else
                {
                    useText.text = "Need Item";
                }
                
            }
            
        }
        else
             useText.text = " ";

        if (Input.GetMouseButtonDown(0))
        {
            if (rightHandIsFree)
            {
                Pickup();
            }
            else
            {
                Drop();
            }
        }

    }

    void Pickup()
    {
        if (Physics.SphereCast(playerCam.position, .1f, playerCam.forward, out hit, grabRange) &&
                     (hit.collider.gameObject.CompareTag("Grabbable") || hit.collider.gameObject.CompareTag("Grabbable Container")))
        {
            GameObject hand;
            GameObject heldObj;

            hand = rightHand;
            heldObj = rightHandObj;
            rightHandIsFree = false;
            Debug.Log(hit.transform.gameObject.name + " Picked Up");

            hit.collider.gameObject.layer = 10;

            hit.collider.gameObject.transform.parent = hand.transform;
            hit.transform.position = hand.transform.position;
            hit.transform.rotation = hand.transform.rotation;
            heldObj = hit.collider.gameObject;

            hit.collider.gameObject.GetComponent<Rigidbody>().useGravity = false;
            hit.collider.gameObject.GetComponent<Rigidbody>().isKinematic = true;

            rightHandObj = heldObj;
        }
    }

    void Drop()
    {
        GameObject heldObj;

        heldObj = rightHandObj;
        rightHandIsFree = true;

        heldObj.transform.parent = null;


        if (Physics.Raycast(playerCam.position, playerCam.forward, out hit, grabRange))
        {
            if (hit.collider.tag == "Intake")
            {
                Debug.Log("Intake Drop.");

                InstaPresser insta = hit.collider.transform.parent.GetComponent<InstaPresser>();
                if (insta == null)
                {
                    Debug.Log("Instapresser not found...");
                }
                IngredientData ingData = heldObj.GetComponent<Ingredient>().data;
                if (ingData == null)
                {
                    Debug.Log("Ingredient data null.");
                }
                insta.AddIngredient(ingData); // this line is awful 
                // Drop ingredient into intake
                Destroy(heldObj);

            }
            else
            {
                heldObj.transform.position = hit.point + hit.normal * 0.2f;
                heldObj.transform.eulerAngles = hit.normal;
            }
            
        }

        else
        {
            heldObj.transform.position = dropPoint.transform.position;
        }
        Rigidbody heldObjRB = heldObj.GetComponent<Rigidbody>();
        Rigidbody playerRB = player.GetComponent<Rigidbody>();

        heldObjRB.isKinematic = false;
        heldObjRB.velocity = playerRB.velocity;
        heldObjRB.angularVelocity = playerRB.angularVelocity;

        heldObj.layer = 0;
        heldObj = null;
        
        

    }
}
