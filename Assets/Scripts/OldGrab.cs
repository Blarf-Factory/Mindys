using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OldGrab : MonoBehaviour
{
  /*  public GameObject playerGO;
    public GameObject leftHand;
    public GameObject rightHand;
    public GameObject dropPoint;
    public GameObject leftHandObj;
    public GameObject rightHandObj;
    public Transform player;
    public Transform playerCamera;
    public float throwStrength = 5f;
    private float grabRange = 2.5f;
    public bool leftHandIsFree = true;
    public bool rightHandIsFree = true;
    private RaycastHit hit;
    public Text useText;

    void Start()
    {
        leftHandObj = null;
        rightHandObj = null;
    }

    void Update()
    {
        if (Physics.SphereCast(playerCamera.position, .1f, playerCamera.forward, out hit, grabRange) &&
                     (hit.collider.gameObject.CompareTag("Grabbable") || hit.collider.GetComponent<FoodContainer>() || hit.collider.GetComponent<StorageContainerLid>()))
        {
            if (hit.collider.GetComponent<StorageContainerLid>())
            {
                useText.text = "(Shift + Click) Open " + hit.collider.GetComponent<StorageContainerLid>().name;
            }
            else if (hit.collider.GetComponent<FoodContainer>())
            {
                useText.text = "(Click) Pickup " + hit.collider.gameObject.name + "\n" + " (Shift + Click) Add Ingredient";
            }
            else
            {
                useText.text = "(Click) Pickup " + hit.collider.gameObject.name;
            }
        }
        else
            useText.text = " ";


        if (Input.GetMouseButtonDown(0) && !Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.R))
        {
            if (leftHandIsFree)
            {
                Pickup("left");
            }
            else
            {
                Drop("left");
            }
        }

        if (Input.GetMouseButtonDown(1) && !Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.R))
        {
            if (rightHandIsFree)
            {
                Pickup("right");
            }
            else
            {
                Drop("right");
            }
        }

        if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.R))
        {
            if (!leftHandIsFree)
            {
                Throw("left");
            }
        }

        if (Input.GetMouseButtonDown(1) && Input.GetKey(KeyCode.R))
        {
            if (!rightHandIsFree)
            {
                Throw("right");
            }
        }


        if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.LeftShift))
        {
            HandUse(leftHandObj);
        }
        if (Input.GetMouseButtonDown(1) && Input.GetKey(KeyCode.LeftShift))
        {
            HandUse(rightHandObj);
        }
    }

    void Pickup(string hand)
    {
        if (Physics.SphereCast(playerCamera.position, .1f, playerCamera.forward, out hit, grabRange) &&
                     hit.collider.gameObject.CompareTag("Grabbable"))
        {
            hit.collider.gameObject.layer = 10;
            if (hand == "left")
            {
                hit.collider.gameObject.transform.parent = leftHand.transform;
                hit.transform.position = leftHand.transform.position;
                hit.transform.rotation = leftHand.transform.rotation;
                leftHandObj = hit.collider.gameObject;
                leftHandIsFree = false;
            }
            else if (hand == "right")
            {
                hit.collider.gameObject.transform.parent = rightHand.transform;
                hit.transform.position = rightHand.transform.position;
                hit.transform.rotation = rightHand.transform.rotation;
                rightHandObj = hit.collider.gameObject;
                rightHandIsFree = false;
            }
            hit.collider.gameObject.GetComponent<Rigidbody>().useGravity = false;
            hit.collider.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    void Drop(string hand)
    {
        if (hand == "left")
        {
            leftHandObj.transform.parent = null;
            if (Physics.Raycast(playerCamera.position, playerCamera.forward, out hit, grabRange))
            {
                leftHandObj.transform.position = hit.point + hit.normal * 0.2f;
                leftHandObj.transform.eulerAngles = hit.normal;
            }
            else
            {
                leftHandObj.transform.position = dropPoint.transform.position;
            }
            leftHandObj.GetComponent<Rigidbody>().isKinematic = false;
            leftHandObj.GetComponent<Rigidbody>().velocity = playerGO.GetComponent<Rigidbody>().velocity;
            leftHandObj.GetComponent<Rigidbody>().angularVelocity = playerGO.GetComponent<Rigidbody>().angularVelocity;
            if (leftHandObj.GetComponent<FoodContainer>())
                leftHandObj.layer = 13;
            else if (leftHandObj.GetComponent<StorageContainer>())
            {
                leftHandObj.layer = 13;
            }
            else
                leftHandObj.layer = 0;
            leftHandObj = null;
            leftHandIsFree = true;
        }
        else if (hand == "right")
        {
            rightHandObj.transform.parent = null;
            if (Physics.Raycast(playerCamera.position, playerCamera.forward, out hit, grabRange))
            {
                rightHandObj.transform.position = hit.point + hit.normal * 0.2f;
                rightHandObj.transform.eulerAngles = hit.normal;
            }
            else
            {
                rightHandObj.transform.position = dropPoint.transform.position;
            }
            rightHandObj.GetComponent<Rigidbody>().isKinematic = false;
            rightHandObj.GetComponent<Rigidbody>().velocity = playerGO.GetComponent<Rigidbody>().velocity;
            rightHandObj.GetComponent<Rigidbody>().angularVelocity = playerGO.GetComponent<Rigidbody>().angularVelocity;
            if (rightHandObj.GetComponent<FoodContainer>())
                rightHandObj.layer = 13;
            else if (rightHandObj.GetComponent<StorageContainer>())
            {
                rightHandObj.layer = 13;
            }
            else
                rightHandObj.layer = 0;
            rightHandObj = null;
            rightHandIsFree = true;
        }
    }

    void Throw(string hand)
    {
        if (hand == "left")
        {
            Debug.Log("throwing!");
            leftHandObj.transform.parent = null;
            leftHandObj.GetComponent<Rigidbody>().isKinematic = false;
            leftHandObj.GetComponent<Rigidbody>().velocity = playerCamera.forward * throwStrength;
            leftHandObj.GetComponent<Rigidbody>().angularVelocity = playerGO.GetComponent<Rigidbody>().angularVelocity;
            leftHandObj = null;
            leftHandIsFree = true;
        }

        if (hand == "right")
        {
            Debug.Log("throwing!");
            rightHandObj.transform.parent = null;
            rightHandObj.GetComponent<Rigidbody>().isKinematic = false;
            rightHandObj.GetComponent<Rigidbody>().velocity = playerCamera.forward * throwStrength;
            rightHandObj.GetComponent<Rigidbody>().angularVelocity = playerGO.GetComponent<Rigidbody>().angularVelocity;
            rightHandObj = null;
            rightHandIsFree = true;
        }
    }
    void HandUse(GameObject hand)
    {
        RaycastHit hit;

        if (Physics.SphereCast(playerCamera.position, .1f, playerCamera.forward, out hit, grabRange))
        {
            if (hit.collider.GetComponent<StorageContainerLid>())
            {
                Debug.Log("Lid Found");
                hit.collider.GetComponent<StorageContainerLid>().OpenContainer();
            }
            else if (hit.collider.GetComponent<FoodContainer>() != null)
            {
                if (hand.GetComponent<IngredientMono>() != null)
                {
                    Debug.Log("Use on FoodContainer");
                    if (hand.transform.parent == leftHand.transform)
                    {
                        MoveToLayer(leftHandObj.transform, 14);
                        leftHandObj.transform.parent = null;
                        hit.collider.GetComponent<FoodContainer>().AddGameObjectToNode(hand);
                        leftHandObj = null;
                        leftHandIsFree = true;
                        hand.GetComponent<Gravity>().gravity = false;
                    }
                    else if (hand.transform.parent == rightHand.transform)
                    {
                        MoveToLayer(rightHandObj.transform, 14);
                        rightHandObj.gameObject.layer = 14;
                        rightHandObj.transform.parent = null;
                        hit.collider.GetComponent<FoodContainer>().AddGameObjectToNode(hand);
                        rightHandObj = null;
                        rightHandIsFree = true;
                        hand.GetComponent<Gravity>().gravity = false;
                    }
                }
            }
        }

        if (hand != null)
        {
            if (hand.name.ToString() == "Knife")
            {
                if (Physics.SphereCast(playerCamera.position, .1f, playerCamera.forward, out hit, grabRange))
                {
                    IngredientMono ingredient = hit.collider.gameObject.GetComponent<IngredientMono>();
                    if (ingredient != null)
                    {
                        if (ingredient.cuttable)
                        {
                            Slice(hit.collider.gameObject, ingredient);
                            ingredient.cuttable = false;
                        }
                    }
                }
            }
        }
    }

    void Slice(GameObject obj, IngredientMono ing)
    {
        Debug.Log("Sliced " + obj.name.ToString());
        obj.transform.GetChild(0).gameObject.SetActive(false);
        obj.transform.GetChild(1).gameObject.SetActive(true);
        ing.cuttable = false;
        if (obj.name == "Cheese")
        {
            obj.name = "Sliced Cheese";
        }
        else
        {
            obj.name = "Chopped " + obj.name;
        }
        ing.name = obj.name;
    }

    void MoveToLayer(Transform root, int layer)
    {
        root.gameObject.layer = layer;
        foreach (Transform child in root)
            MoveToLayer(child, layer);
    } */
}
