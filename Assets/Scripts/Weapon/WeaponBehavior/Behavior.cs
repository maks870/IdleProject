using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Upgrader))]
public abstract class Behavior : MonoBehaviour
{
    public virtual void ActiveBehavior()
    {

    }
    public virtual void Use()
    {
        Debug.Log("Абстрактный Use");
    }
    public virtual void Combine()
    {
        Debug.Log("Абстрактный Combine");
    }
    public virtual void Improve(bool isMaxLevel)
    {
        Debug.Log("Абстрактный Improve");
    }
}
