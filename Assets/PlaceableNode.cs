using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableNode : MonoBehaviour
{

    public GameObject placedObj;

    // Start is called before the first frame update
    void Start()
    {
        placedObj = null;
    }

    public bool CheckIfFull()
    {
        if (placedObj == null)
            return false;
        else
            return true;
    }

    public void ReceiveObj(GameObject obj)
    {
        if (placedObj == null)
        {
            placedObj = obj;
            placedObj.transform.position = this.transform.position;
            placedObj.transform.rotation = this.transform.rotation;
            placedObj.transform.parent = this.transform;
        }
    }

    public GameObject SendObj()
    {
        GameObject tempObj = placedObj;
        placedObj = null;

        tempObj.transform.parent = null;
        
        return tempObj;
    }
    
}
