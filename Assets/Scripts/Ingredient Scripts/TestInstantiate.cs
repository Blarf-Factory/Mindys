using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInstantiate : MonoBehaviour
{
    // Start is called before the first frame update

    bool doOnce = true;
    public List<GameObject> prefabs;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (doOnce && GameObject.Find("IngredientLoader").GetComponent<LoadIngredients>().doneLoading)
        {
            doOnce = false;
            foreach (GameObject g in prefabs)
            {
                Instantiate(g, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));
            }
        }
    }
}
