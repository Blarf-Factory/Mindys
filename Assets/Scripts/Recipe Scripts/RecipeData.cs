using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeData
{
    public int id;
    public string name;
    public string description;
    public string container;
    public string prefab;
    public float price;
    public List<RecipeIngredient> ingredients;

    public RecipeData(int id, string name, string description, string container, string prefab, float price, List<RecipeIngredient> ingredients)
    {
        this.id = id;
        this.name = name;
        this.description = description;
        this.container = container;
        this.prefab = prefab;
        this.price = price;
        this.ingredients = ingredients;
        

    }

        public string toString() 
        {
            string s = "-- Ingredient Data -- \n" +
            "ID: " + id + "\n" +
            "Name: " + name + "\n" +
            "Description: " + description + "\n" +
            "Container Name: " + container + "\n" +
            "Prefab Name: " + prefab + "\n";
            
            foreach (RecipeIngredient ing in ingredients)
            {
                s += "Ingredient Name: " + ing.name + "\n";
                s += "Cook Level: " + ing.cookLevel + "\n";
                s += "Cut: " + ing.cut + "\n";
            }

            return s;
        }

    
}
