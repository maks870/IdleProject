using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataObject
{
    public List<DataValue> dataValues;

    public DataObject(List<DataValue> dataValues)
    {
        this.dataValues = dataValues;
    }

    public void AddValue(string valueName, int value)
    {
        foreach (DataValue dataValue in dataValues)
        {
            if (dataValue.valueName == valueName)
            {
                dataValue.value += value;
            }
        }
    }
    public int GetValue(string valueName)
    {
        foreach (DataValue dataValue in dataValues)
        {
            if (dataValue.valueName == valueName)
            {
                return dataValue.value;
            }
        }
        return 0;
    }
}
