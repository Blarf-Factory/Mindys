using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public IngredientData data;
    public bool raw;
    public bool cooked; 
    public bool burnt;
    public int cookLevel; // 0 - raw, 1 - cooked, 2 - burnt

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
        cookLevel = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (cookLevel < 2 && cooking)
        {
            currentTime += Time.deltaTime;

            if (raw && currentTime >= data.cookTime)
            {
                currentTime = 0;
                Cook();

            }
            else if (cookLevel == 1 && currentTime >= data.burnTime)
            {
                Burn();
            }

        }

        if (activateCut)
        {
            Cut();
        }
    }

    public void Cook()
    {
        cookLevel = 1; // set cooked
        foreach (MaterialChanger mat in mats)
        {
            mat.SetCookedMat();
        }
    }

    public void Burn()
    {
        cookLevel = 2; // set burnt
        foreach (MaterialChanger mat in mats)
        {
            mat.SetBurntMat();
        }
    }

    public void Cut()
    {
        if (!data.cuttable)
            return;
        
        GameObject newCutPrefab = (GameObject)Instantiate(cutPrefab, this.GetComponent<Transform>().position, Quaternion.identity);
        Ingredient newIng = newCutPrefab.GetComponent<Ingredient>();

        if (cookLevel == 1)
        {
            newIng.Cook();
        }
        else if (cookLevel == 2)
        {
            newIng.Burn();
        }

    }

    public void LoadValues(IngredientData ing) // Load values into ingredient
    {
        data = ing;
    }
}
