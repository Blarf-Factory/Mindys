using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public string name;
    public bool cookable;
    public bool cuttable;
    public float cookTime;
    public float burnTime;
    public bool cut;
    public bool raw;
    public bool cooked; 
    public bool burnt;
    public List<MaterialChanger> unCutMats;
    public List<MaterialChanger> cutMats;
    public List<MaterialChanger> mats;
    public bool cooking;
    public float currentTime;


    // Start is called before the first frame update
    void Start()
    {
        cooking = false;
        raw = true;
        cooked = false;
        burnt = false;
        mats = unCutMats;
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
    }

    public void Cut()
    {
        cut = true;
        mats = cutMats;
        
        // TODO: switch gameobjects

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
