using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public string crewname;
    public int money;
    public int[] storage = new int[10];
    public int[] lastplayed = new int[3];
    public float totaltime;
}
