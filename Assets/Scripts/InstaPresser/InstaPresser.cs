using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstaPresser : MonoBehaviour
{
    [SerializeField]
    public List<IngredientData> ingredients;
    [SerializeField]
    public List<RecipeIngredient> ingsRemaining;
    [SerializeField]
    public List<RecipeIngredient> ingsCompleted;
    public List<RecipeIngredient> recipeIngredients;
    public float score;
    public GameObject intakeNode;
    public GameObject outtakeNode;
    public LoadRecipes recipeLoader;
    public GameObject finishedDish;
    // Start is called before the first frame update
    void Start()
    {
        recipeLoader = GameObject.Find("RecipeLoader").GetComponent<LoadRecipes>();
        ingsCompleted = new List<RecipeIngredient>();
        ingsRemaining = new List<RecipeIngredient>();
        SelectRecipe();
    }

    // Update is called once per frame
    void Update()
    {
        //SelectRecipe();
    }

    void SelectRecipe()
    {
        score = 0;
        RecipeData rec = recipeLoader.GetRecipeByName("Double Blarf");
        ingsRemaining = rec.ingredients;
        if (ingsCompleted != null)
        {
            ingsCompleted.Clear();
        }
        

        if (rec == null)
        {
            Debug.Log("RecipeData is null.");
            return;
        }

        

        Debug.Log("Recipe Selected: " + rec.toString());
    }

    public void AddIngredient(IngredientData ingData)
    {
        ingredients.Add(ingData);
        CheckRecipe(ingData);
    }

    void CheckRecipe(IngredientData ingData)
    {
        foreach (RecipeIngredient recIng in ingsRemaining)
        {
            if (recIng.name == ingData.name)
            {
                score += 0.5f;
                if (recIng.cookLevel == ingData.cookLevel)
                {
                    score += 0.5f;
                    ingsCompleted.Add(recIng);
                    ingsRemaining.Remove(recIng);
                    Debug.Log(ingsRemaining);
                    break;
                }
            }
        }
        if (ingsRemaining.Count == 0)
        {
            MakeDish();
        }
    }

    void MakeDish()
    {

        Transform outtakeTrans = outtakeNode.GetComponent<Transform>();
        Instantiate(finishedDish, outtakeTrans.position, outtakeTrans.rotation);
    }
}
