using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Upgrader))]
public abstract class Behavior : MonoBehaviour
{
    public abstract void ActiveBehavior();
    public abstract void Use();
    public abstract void Combine();
    public abstract void Improve(bool isMaxLevel);
}
