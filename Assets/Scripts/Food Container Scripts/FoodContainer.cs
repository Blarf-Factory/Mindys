using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodContainer : MonoBehaviour
{
    public List<GameObject> iNodes; // ingredient nodes
    public GameObject iNodesParent;
    public int iIndex;



    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void InitializeNodes()
    {
        iNodesParent = this.transform.Find("Nodes").gameObject;
        bool moreChildren = true;
        try
        {
            iNodesParent.transform.GetChild(0);
        }
        catch (UnityException e)
        {
            moreChildren = false;
        }

        for (int i = 0; moreChildren; i++)
        {
            nodes.Add(iNodesParent.transform.GetChild(i).gameObject);
            try
            {
                iNodesParent.transform.GetChild(i + 1);
            }
            catch (UnityException e)
            {
                moreChildren = false;
            }
        }
    }
}
