using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{



    // Revise these to have data contained within IngredientData class
    public string name;
    public bool cookable;
    public bool cuttable;
    public float cookTime;
    public float burnTime;
    public bool raw;
    public bool cooked; 
    public bool burnt;
    public List<MaterialChanger> mats;
    public bool cooking;
    public float currentTime;
    public GameObject cutPrefab;
    public bool activateCut; // remove later  


    // Start is called before the first frame update
    void Start()
    {
        activateCut = false; // remove later
        cooking = false;
        raw = true;
        cooked = false;
        burnt = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!burnt && cooking)
        {
            currentTime += Time.deltaTime;

            if (raw && currentTime >= cookTime)
            {
                currentTime = 0;
                raw = false;
                cooked = true;

                foreach (MaterialChanger mat in mats)
                {
                    mat.SetCookedMat();
                }

            }
            else if (cooked && currentTime >= burnTime)
            {

                cooked = false;
                burnt = true;
                foreach (MaterialChanger mat in mats)
                {
                    mat.SetBurntMat();
                }
            }

        }

        if (activateCut)
        {
            Cut();
        }
    }

    public void Cut()
    {
        if (!cuttable)
            return;
        
        Instantiate(cutPrefab, this.GetComponent<Transform>().position, Quaternion.identity);

        if (cooked)
        {
            foreach (MaterialChanger mat in mats)
            {
                mat.SetCookedMat();
                Debug.Log("Set cooked");
            }
        }
        else if (burnt)
        {
            foreach (MaterialChanger mat in mats)
            {
                mat.SetBurntMat();
            }
        }

    }

    public void LoadValues(IngredientData ing)
    {
        this.name = ing.name;
        this.cookable = ing.cookable;
        this.cuttable = ing.cuttable;
        this.cookTime = ing.cookTime;
        this.burnTime = ing.burnTime;
    }
}
