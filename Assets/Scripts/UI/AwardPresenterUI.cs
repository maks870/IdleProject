using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterfaces
{
    public class AwardPresenterUI : MonoBehaviour
    {
        [SerializeField] private AwardPresenter awardPresenter;
        [SerializeField] private List<Button> buttonList = new List<Button>();
        [SerializeField] private Button button;
        private GameObject panel;
        private bool choosingStartWeapon = true;


        // Start is called before the first frame update
        void Start()
        {
            awardPresenter.SetPresenterUI = this;
            panel = transform.GetChild(0).gameObject;
            CreateButtons(awardPresenter.GetAwardsCount);
            ShowAwards();
        }
        private void CreateButtons(int awardCount)
        {
            Button newButton;
            for (int i = 0; i < awardCount; i++)
            {
                newButton = Instantiate(button, panel.transform);
                buttonList.Add(newButton);
            }
        }
        private void ReplaceAwards()
        {
            List<IAward> awardList = awardPresenter.GetRandomAwards();
            for (int i = 0; i < awardList.Count; i++)
            {
                buttonList[i].gameObject.SetActive(true);
                buttonList[i].GetComponentsInChildren<Image>()[1].sprite = awardList[i].GetAwardSprite;

                if (choosingStartWeapon)
                    buttonList[i].GetComponentInChildren<Text>().text = awardList[i].GetAwardName;
                else
                    buttonList[i].GetComponentInChildren<Text>().text = awardList[i].GetAwardDescription;

                buttonList[i].onClick.RemoveAllListeners();
                IAward award = awardList[i];
                buttonList[i].onClick.AddListener(() =>
                {
                    awardPresenter.GiveAward(award);
                    panel.SetActive(false);
                    MenuGame.instance.SetPause(false);

                });
            }
            for (int i = awardList.Count; i < buttonList.Count; i++ )
            {
                buttonList[i].gameObject.SetActive(false);
            }
        }
        public void ShowAwards()
        {
            ReplaceAwards();
            MenuGame.instance.SetPause(true);
            panel.SetActive(true);
        }
    }
}