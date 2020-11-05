using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeIngredient
{
    public string name;
    public string cookLevel;
    public bool cut;

    public RecipeIngredient(string name, string cookLevel)
    {
        this.name = name;
        this.cookLevel = cookLevel;
    }
}