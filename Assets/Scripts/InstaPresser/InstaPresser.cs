using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstaPresser : MonoBehaviour
{
    [SerializeField]
    public List<IngredientData> ingredients;
    public List<RecipeIngredient> ingsRemaining;
    public List<RecipeIngredient> ingsCompleted;
    public List<RecipeIngredient> recipeIngredients;
    public int score;
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
        score = 0;
        RecipeData rec = recipeLoader.GetRecipeByName("Salad");
        ingsRemaining = rec.ingredients;

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
        CheckRecipe(ingData);
    }

    void CheckRecipe(IngredientData ingData)
    {
        foreach (IngredientData ingD in ingredients)
        {
        
        }
    }

    void MakeDish()
    {

    }
}
