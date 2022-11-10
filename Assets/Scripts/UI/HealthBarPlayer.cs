using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace UserInterfaces 
{
    using UnityEngine.UI;
    public class HealthBarPlayer : MonoBehaviour
    {
        [SerializeField] private GameObject heartPref;
        [SerializeField] private List<GameObject> heartsUI = new List<GameObject>();

        private void Update()
        {
            if (heartsUI.Count > Player.instance.Hp)
            {
                GameObject heartDel = heartsUI[heartsUI.Count - 1];
                heartsUI.Remove(heartDel);
                Destroy(heartDel);
            }
            else if (heartsUI.Count < Player.instance.Hp) 
            {
                heartsUI.Add(Instantiate(heartPref, transform));
            }
        }


    }

}
