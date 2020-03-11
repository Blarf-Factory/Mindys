using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;

public class LoadIngredients : MonoBehaviour
{
    TextAsset rawXML;
    List<IngredientData> allIngredients;

    // Start is called before the first frame update
    void Start()
    {
        allIngredients = new List<IngredientData>();

        rawXML = Resources.Load<TextAsset>("ingredients");

        string data = rawXML.text;
        ParseXML(data);
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
            XmlNode cookableNode = prefabNode.NextSibling;
            XmlNode cuttableNode = cookableNode.NextSibling;
            XmlNode cookTimeNode = cuttableNode.NextSibling;
            XmlNode burnTimeNode = cookTimeNode.NextSibling;

            int id = int.Parse(idNode.InnerXml);
            string name = nameNode.InnerXml;
            string description = descriptionNode.InnerXml;
            string prefab = prefabNode.InnerXml;
            bool cookable = ("true" == cookableNode.InnerXml);
            bool cuttable = ("true" == cuttableNode.InnerXml);
            float cookTime = float.Parse(cookTimeNode.InnerXml);
            float burnTime = float.Parse(burnTimeNode.InnerXml);
            
            allIngredients.Add(new IngredientData(id, name, description, prefab, cookable, cuttable, cookTime, burnTime));

        }


        foreach(IngredientData ing in allIngredients)
        {
            Debug.Log(ing.toString());
        }


    }
}
public class IngredientData
{
    int id;
    string name;
    string description;
    string prefab;
    bool cookable;
    bool cuttable;
    float cookTime;
    float burnTime;

    public IngredientData(int id, string name, string description, string prefab, bool cookable, 
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