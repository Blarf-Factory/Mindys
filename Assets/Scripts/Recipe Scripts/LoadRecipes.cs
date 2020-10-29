using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;

// ----- Loads XML data into the ingredient prefabs. Also generates a list of all the ingredients available to the game. -----

public class LoadRecipes : MonoBehaviour
{
    TextAsset rawXML;
    List<RecipeData> allRecipes;
    public List<GameObject> prefabs;
    public bool doneLoading = false;

    // Start is called before the first frame update
    void Start()
    {
        allRecipes = new List<RecipeData>();

        rawXML = Resources.Load<TextAsset>("recipes");

        string data = rawXML.text;
        ParseXML(data);
        //LoadRecipeValues();

        doneLoading = true;
    }

    // Update is called once per frame
    void ParseXML(string data)
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(new StringReader(data));

        string xmlPathPattern = "//recipes/recipe";
        XmlNodeList recNodeList = doc.SelectNodes(xmlPathPattern);

        Debug.Log(recNodeList.Count);
        foreach (XmlNode node in recNodeList)
        {
            XmlNode idNode = node.FirstChild;
            XmlNode nameNode = idNode.NextSibling;
            XmlNode descriptionNode = nameNode.NextSibling;
            XmlNode containerNode = descriptionNode.NextSibling;
            XmlNode prefabNode = containerNode.NextSibling;

            string ingPathPattern = "//recipes/recipe/ingredients/ingredient";

            XmlNodeList ingNodeList = doc.SelectNodes(ingPathPattern);

            List<RecipeIngredient> recIngs = new List<RecipeIngredient>();

            foreach (XmlNode ingNode in ingNodeList)
            {
                XmlNode ingNameNode = ingNode.FirstChild;
                XmlNode ingCookLevelNode = ingNameNode.NextSibling;
                XmlNode ingCutNode = ingCookLevelNode.NextSibling;
                
                string ingName = ingNameNode.InnerXml;
                string ingCookLevel = ingCookLevelNode.InnerXml;
                bool ingCut = ingCutNode.InnerXml == "true";
                
                
                recIngs.Add(new RecipeIngredient(ingName, ingCookLevel, ingCut));
            }


            int id = int.Parse(idNode.InnerXml);
            string name = nameNode.InnerXml;
            string description = descriptionNode.InnerXml;
            string container = containerNode.InnerXml;
            string prefab = prefabNode.InnerXml;
            
            allRecipes.Add(new RecipeData(id, name, description, container, prefab, recIngs));

        }

        

        foreach (RecipeData rec in allRecipes)
        {  
            Debug.Log(rec.toString());
        }

    }

    void LoadRecipeValues()
    {
        foreach (RecipeData rec in allRecipes)
        {
            foreach (GameObject g in prefabs)
            {
                if (g.name == rec.prefab)
                {
                   // g.GetComponent<Recipe>().LoadValues(rec);
                    break;
                }
            }
        }
    }

    public RecipeData GetRecipeByName(string name)
    {
        foreach (RecipeData rec in allRecipes)
        {
            if (rec.name == name)
            {
                return rec;
            }
        }
        return null;
    }

    public GameObject GetIngredientPrefab(string ingName)
    {
            foreach (GameObject g in prefabs)
            {
                if (g.name == ingName)
                {
                    return g;
                }
            }
            return null;
        }
    }
public class RecipeData
{
    public int id;
    public string name;
    public string description;
    public string container;
    public string prefab;
    public List<RecipeIngredient> ingredients;

    public RecipeData(int id, string name, string description, string container, string prefab, List<RecipeIngredient> ingredients)
    {
        this.id = id;
        this.name = name;
        this.description = description;
        this.container = container;
        this.prefab = prefab;
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

public class RecipeIngredient
{
    public string name;
    public string cookLevel;
    public bool cut;

    public RecipeIngredient(string name, string cookLevel, bool cut)
    {
        this.name = name;
        this.cookLevel = cookLevel;
        this.cut = cut;
    }
}