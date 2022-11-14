using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterfaces
{
    public class AwardPresenterUI : MonoBehaviour
    {
        [SerializeField] private AwardPresenter awardPresenter;
        [SerializeField] private List<Button> buttonList = new List<Button>();
        private Button button;
        private GameObject panel;


        // Start is called before the first frame update
        void Start()
        {
            awardPresenter.SetPresenterUI = this;
            panel = transform.GetChild(0).gameObject;
            button = panel.GetComponentInChildren<Button>();
            CreateButtons(awardPresenter.GetAwardsCount);
            panel.SetActive(false);
        }
        private void CreateButtons(int awardCount)
        {
            Button newButton;
            buttonList.Add(button);
            for (int i = 0; i < awardCount - 1; i++)
            {
                newButton = Instantiate(button, panel.transform);
                buttonList.Add(newButton);
            }
        }
        private void ReplaceAwards()
        {
            List<IAward> awardList = awardPresenter.RandomAwards();
            for (int i = 0; i < awardList.Count; i++)
            {
                buttonList[i].gameObject.SetActive(true);
                buttonList[i].image.sprite = awardList[i].GetAwardSprite;
                buttonList[i].GetComponentInChildren<Text>().text = awardList[i].GetAwardName;
                buttonList[i].onClick.RemoveAllListeners();
                IAward award = awardList[i];
                buttonList[i].onClick.AddListener(() =>
                {
                    awardPresenter.GetAward(award);
                    ClosePresenterUI();
                });
            }
            for (int i = awardList.Count; i < buttonList.Count; i++ )
            {
                buttonList[i].gameObject.SetActive(false);
            }
        }
        private void OpenPresenterUI()
        {
            Time.timeScale = 0;
            panel.SetActive(true);
        }
        private void ClosePresenterUI()
        {
            panel.SetActive(false);
            Time.timeScale = 1;
        }
        public void ShowAwards()
        {
            ReplaceAwards();
            OpenPresenterUI();
        }
    }
}