using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    public float speed;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Magnetized") 
        {
            Vector3 direct =Player.instance.transform.position - collision.transform.position;
            collision.transform.position += direct.normalized * Time.deltaTime * speed;
        }
    }
}
