using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInstantiate : MonoBehaviour
{
    // Start is called before the first frame update

    public List<GameObject> prefabs;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InstantiateIngredients(List<GameObject> prefabs)
    {
        float i = 0f;

        foreach (GameObject g in prefabs)
        {
            Instantiate(g, new Vector3(i, 0, 0), Quaternion.Euler(0, 0, 0));
            i += 0.3f;
        }
    }
}
