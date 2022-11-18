using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterfaces 
{
    public class PlayerUI : MonoBehaviour
    {
        [SerializeField] private GameObject heartPref;
        [SerializeField] private Transform heartTransform;
        [SerializeField] private List<GameObject> heartsUI = new List<GameObject>();
        [SerializeField] private Text textGold;
        [SerializeField] private Image experienceImage;

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
                heartsUI.Add(Instantiate(heartPref, heartTransform));
            }
            CheckExperience();

            textGold.text = Player.instance.Coins.ToString(); //ÒÅÊÑÒ ÃÎÄËÛ
        }

        private void CheckExperience() 
        {
            ExperienceCollector experienceCollector = ExperienceCollector.instance;
            experienceImage.fillAmount = 1f / experienceCollector.ExpToLvlup * experienceCollector.CurrentExp;
        }
    }

}
