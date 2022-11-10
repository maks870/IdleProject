using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int valueCoin;

    public int GetValue() 
    {
        Destroy(gameObject);
        return valueCoin;
    }
}
