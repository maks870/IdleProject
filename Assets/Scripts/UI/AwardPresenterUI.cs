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
        [SerializeField] private GameObject panel;


        // Start is called before the first frame update
        void Start()
        {
            awardPresenter.SetPresenterUI = this;
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
            List<Award> awardList = awardPresenter.GetRandomAwards();
            for (int i = 0; i < awardList.Count; i++)
            {
                buttonList[i].gameObject.SetActive(true);
                buttonList[i].GetComponentsInChildren<Image>()[1].sprite = awardList[i].Sprite;
                buttonList[i].GetComponentInChildren<Text>().text = awardList[i].Description;

                buttonList[i].onClick.RemoveAllListeners();
                Award award = awardList[i];
                buttonList[i].onClick.AddListener(() =>
                {
                    awardPresenter.GiveAward(award);
                    panel.transform.parent.gameObject.SetActive(false);
                    MenuGame.instance.SetPause(false);

                });
            }

            for (int i = awardList.Count; i < buttonList.Count; i++)
            {
                buttonList[i].gameObject.SetActive(false);
            }
        }
        public void ShowAwards()
        {
            ReplaceAwards();
            MenuGame.instance.SetPause(true);
            panel.transform.parent.gameObject.SetActive(true);
        }
    }
}