using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstaPresser : MonoBehaviour
{
    [SerializeField]
    public List<IngredientData> ingredients;
    public GameObject intakeNode;
    public GameObject outtakeNode;
    public LoadRecipes recipeLoader;
    // Start is called before the first frame update
    void Start()
    {
        recipeLoader = GameObject.Find("RecipeLoader").GetComponent<LoadRecipes>();
    }

    // Update is called once per frame
    void Update()
    {
        //SelectRecipe();
    }

    void SelectRecipe()
    {
        RecipeData rec = recipeLoader.GetRecipeByName("Salad");

        if (rec == null)
        {
            Debug.Log("RecipeData is null.");
            return;
        }

        

        Debug.Log("Recipe: " + rec.toString());
    }

    public void AddIngredient(IngredientData ingData)
    {
        ingredients.Add(ingData);
    }
}
