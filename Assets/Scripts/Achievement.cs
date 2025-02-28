using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Achievement
{
    public string name;
    public string description;
    public bool isUnlocked;

    public Achievement(string name, string description)
    {
        this.name = name;
        this.description = description;
        this.isUnlocked = false;
    }
}