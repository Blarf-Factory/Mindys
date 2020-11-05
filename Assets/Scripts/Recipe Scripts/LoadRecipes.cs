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
            XmlNode priceNode = prefabNode.NextSibling;

            string ingPathPattern = "//recipes/recipe/ingredients/ingredient";

            XmlNodeList ingNodeList = doc.SelectNodes(ingPathPattern);

            List<RecipeIngredient> recIngs = new List<RecipeIngredient>();

            foreach (XmlNode ingNode in ingNodeList)
            {
                XmlNode ingNameNode = ingNode.FirstChild;
                XmlNode ingCookLevelNode = ingNameNode.NextSibling;

                string ingName = ingNameNode.InnerXml;
                string ingCookLevel = ingCookLevelNode.InnerXml;
                
                recIngs.Add(new RecipeIngredient(ingName, ingCookLevel));
            }


            int id = int.Parse(idNode.InnerXml);
            string name = nameNode.InnerXml;
            string description = descriptionNode.InnerXml;
            string container = containerNode.InnerXml;
            string prefab = prefabNode.InnerXml;
            float price = float.Parse(priceNode.InnerXml);
            
            allRecipes.Add(new RecipeData(id, name, description, container, prefab, price, recIngs));

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

