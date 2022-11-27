using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataValue : MonoBehaviour
{
    public string valueName;
    public int value;

    public DataValue(string name, int value)
    {
        valueName = name;
        this.value = value;
    }
}
