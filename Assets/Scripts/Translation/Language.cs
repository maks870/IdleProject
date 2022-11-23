using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Language
{
    public string name;
    public List<string> fields;

    public Language(string langName, int countFields)
    {
        name = langName;
        fields = new List<string>(countFields);
    }
}
