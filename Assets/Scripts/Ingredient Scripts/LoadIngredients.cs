﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;

// ----- Loads XML data into the ingredient prefabs. Also generates a list of all the ingredients available to the game. -----

public class LoadIngredients : MonoBehaviour
{
    TextAsset rawXML;
    List<IngredientData> allIngredients;
    public List<GameObject> prefabs;
    public bool doneLoading = false;

    // Start is called before the first frame update
    void Start()
    {
        allIngredients = new List<IngredientData>();

        rawXML = Resources.Load<TextAsset>("ingredients");

        string data = rawXML.text;
        ParseXML(data);
        LoadIngredientValues();

        doneLoading = true;
    }

    // Update is called once per frame
    void ParseXML(string data)
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(new StringReader(data));

        string xmlPathPattern = "//ingredients/ingredient";
        XmlNodeList nodeList = doc.SelectNodes(xmlPathPattern);
        foreach (XmlNode node in nodeList)
        {
            XmlNode idNode = node.FirstChild;
            XmlNode nameNode = idNode.NextSibling;
            XmlNode descriptionNode = nameNode.NextSibling;
            XmlNode prefabNode = descriptionNode.NextSibling;
            XmlNode baseCostNode = prefabNode.NextSibling;
            XmlNode cookableNode = baseCostNode.NextSibling;
            XmlNode cuttableNode = cookableNode.NextSibling;
            XmlNode cookTimeNode = cuttableNode.NextSibling;
            XmlNode burnTimeNode = cookTimeNode.NextSibling;

            int id = int.Parse(idNode.InnerXml);
            string name = nameNode.InnerXml;
            string description = descriptionNode.InnerXml;
            string prefab = prefabNode.InnerXml;
            float baseCost = float.Parse(baseCostNode.InnerXml);
            bool cookable = ("true" == cookableNode.InnerXml);
            bool cuttable = ("true" == cuttableNode.InnerXml);
            float cookTime = float.Parse(cookTimeNode.InnerXml);
            float burnTime = float.Parse(burnTimeNode.InnerXml);
            
            allIngredients.Add(new IngredientData(id, name, description, prefab, baseCost, cookable, cuttable, cookTime, burnTime));

        }


        // foreach (IngredientData ing in allIngredients)
        // {
        //     Debug.Log(ing.toString());
        // }

    }

    void LoadIngredientValues()
    {
        foreach (IngredientData ing in allIngredients)
        {
            foreach (GameObject g in prefabs)
            {
                if (g.name == ing.prefab)
                {
                    g.GetComponent<Ingredient>().LoadValues(ing);
                    break;
                }
            }
        }
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

    public IngredientData(int id, string name, string description, string prefab, float baseCost, bool cookable, 
                            bool cuttable, float cookTime, float burnTime)
    {
        this.id = id;
        this.name = name;
        this.description = description;
        this.prefab = prefab;
        this.cookable = cookable;
        this.cuttable = cuttable;
        this.cookTime = cookTime;
        this.burnTime = burnTime;
    }

        public string toString() 
        {
            return "-- Ingredient Data -- \n" +
            "ID: " + id + "\n" +
            "Name: " + name + "\n" +
            "Description: " + description + "\n" +
            "Prefab Name: " + prefab + "\n" +
            "Cookable: " + cookable + "\n" +
            "Cuttable: " + cuttable + "\n" +
            "cookTime: " + cookTime + "\n" +
            "burnTime: " + burnTime;
        }
}