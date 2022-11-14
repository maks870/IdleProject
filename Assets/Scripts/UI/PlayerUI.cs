using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterfaces 
{
    public class PlayerUI : MonoBehaviour
    {
        [SerializeField] private GameObject heartPref;
        [SerializeField] private List<GameObject> heartsUI = new List<GameObject>();
        [SerializeField] private Text textGold;

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

            textGold.text = Player.instance.Coins.ToString();
        }
    }

}
