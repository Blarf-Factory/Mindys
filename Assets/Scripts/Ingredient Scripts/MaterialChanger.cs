using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialChanger : MonoBehaviour
{
    public Material rawMat;
    public Material cookedMat;
    public Material burntMat;
    Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame

    public void SetRawMat()
    {
        if (rend != null){
            rend.material = rawMat;
        }
    }

    public void SetCookedMat()
    {
        if (rend != null){
            Debug.Log("is it working? YES");
            Debug.Log(rend);
            rend.material = cookedMat;
        }
    }

    public void SetBurntMat()
    {
        if (rend != null){
            rend.material = burntMat;
        }
    }
}
