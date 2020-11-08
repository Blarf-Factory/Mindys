﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RecipeIngredient
{
    public string name;
    public int cookLevel;

    public RecipeIngredient(string name, int cookLevel)
    {
        this.name = name;
        this.cookLevel = cookLevel;
    }
}