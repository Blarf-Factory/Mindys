using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class IngredientData
{
    public int id;
    public string name;
    public string description;
    public string prefab;
    public float baseCost; 
    public bool cookable;
    public bool cuttable;
    public float cookTime;
    public float burnTime;
    public int cookLevel;

    public IngredientData(int id, string name, string description, string prefab, float baseCost, bool cookable, 
                            bool cuttable, float cookTime, float burnTime, int cookLevel)
    {
        this.id = id;
        this.name = name;
        this.description = description;
        this.prefab = prefab;
        this.cookable = cookable;
        this.cuttable = cuttable;
        this.cookTime = cookTime;
        this.burnTime = burnTime;
        this.cookLevel = cookLevel;
    }


        public string toString() 
        {
            return "-- Ingredient Data -- \n" +
            "ID: " + id + "\n" +
            "Name: " + name + "\n" +
            "Description: " + description + "\n" +
            "Base Prefab Name:  " + prefab + "\n" +
            "Cookable: " + cookable + "\n" +
            "Cuttable: " + cuttable + "\n" +
            "Cook Time: " + cookTime + "\n" +
            "Burn Time: " + burnTime + "\n" +
            "Cook Level: " + cookLevel;
        }

        public RecipeIngredient ConvertToRecIng(IngredientData ingD)
        {
            return new RecipeIngredient(ingD.name, ingD.cookLevel);
        }
}
