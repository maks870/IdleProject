using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField] private GameObject followObject;

    private void Update()
    {
        transform.position = followObject.transform.position;
    }
}
