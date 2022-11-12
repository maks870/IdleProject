using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpPoint : MonoBehaviour
{
    [SerializeField] private int expValue;

    public int GetValue()
    {
        Destroy(gameObject);
        return expValue;
    }
}
