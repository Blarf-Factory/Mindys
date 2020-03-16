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
    public List<Material> rawMat;
    public List<Material> cookedMat;
    public List<Material> burntMat;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
