using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAward
{
    public Sprite GetAwardSprite { get; }
    public string GetAwardName { get; }
    public string GetAwardDescription { get; }
    public bool GetAwardAccessibility { get; }
    public void AwardAction();



}
