// // ********************************************************************************************
// //     /\_/\                           @file       DialogPanel.cs
// //    ( o.o )                          @brief     QingTang
// //     > ^ <                           @author     Basya
// //    /     \
// //   (       )                         @Modified   2025030818
// //   (___)___)                         @Copyright  Copyright (c) 2025, Basya
// // ********************************************************************************************

using System.Collections.Generic;
using Data;
using DG.Tweening;
using GamePlay;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Panel
{
    [System.Serializable]
    public class TipButton
    {
        public int tipId = -1;
        public GameObject redPoint;
        public Button btn;
        public Image img;
        public TextMeshProUGUI text;

        public void SetColor(int id = -1)
        {
            if (id == -1)
            {
                img.color = Color.white;
            }

            if (id == 0)
            {
                img.color = Color.blue;
            }

            if (id == 1)
            {
                img.color = Color.yellow;
            }

            if (id == 2)
            {
                img.color = Color.red;
            }
        }

        public void Reset()
        {
            redPoint.SetActive(false);
            img.color = Color.white;
            text.text = "";
        }
    }

    public class DialogPanel : BasePanel<DialogPanel>
    {
        [SerializeField] private DialogManager dialogManager;
        [SerializeField] private RectTransform tipPanelTransform;
        [SerializeField] private RectTransform tipBtnsParent;
        [SerializeField] private List<TipButton> tipButtons;

        public override void Init()
        {
            base.Init();
            dialogManager.Init();
            GetControl<Button>("tipBtn").onClick.AddListener(ChangeTipPanel);
            GetControl<Button>("exit").onClick.AddListener(ChangeTipPanel);
            foreach (Transform child in tipBtnsParent)
            {
                TipButton tipButton = new TipButton()
                {
                    redPoint = child.GetChild(0).gameObject,
                    btn = child.GetComponent<Button>(),
                    img = child.GetComponent<Image>(),
                    text = child.GetComponentInChildren<TextMeshProUGUI>()
                };
                tipButton.btn.onClick.AddListener(() => { tipButton.redPoint.SetActive(false); });
                tipButtons.Add(tipButton);
            }
        }

        public void SetBackGround(int sceneId)
        {
            GetControl<Image>("BG").sprite = GlobalConfig.Instance.GetBackGround(sceneId);
        }

        public void ChangeTipPanel()
        {
            GetControl<Button>("tipBtn").transform.GetChild(0).gameObject.SetActive(false);
            if (tipPanelTransform.anchoredPosition.x > 0)
            {
                tipPanelTransform.DOAnchorPosX(0, UIConst.UI_PANEL_ANIM);
            }
            else
            {
                tipPanelTransform.DOAnchorPosX(tipPanelTransform.sizeDelta.x, UIConst.UI_PANEL_ANIM);
            }
        }

        public void OnUpdateTip()
        {
            var finishedTips = SaveManager.Instance.FinishedTips;
            foreach (var tipButton in tipButtons)
            {
                tipButton.Reset();
            }

            int i = 0;
            foreach (var tip in finishedTips)
            {
                tipButtons[i].tipId = tip.Id;
                tipButtons[i].text.name = tip.Name;
                tipButtons[i].SetColor(tip.ColorId);
                i++;
            }
        }

        public void ShowRedPoint(int id)
        {
            if (tipPanelTransform.anchoredPosition.x > 0)
            {
                GetControl<Button>("tipBtn").transform.GetChild(0).gameObject.SetActive(true);
            }

            foreach (var tipButton in tipButtons)
            {
                if (tipButton.tipId != id) continue;
                tipButton.redPoint.SetActive(true);
                return;
            }
        }

        public override void OnPressedEsc()
        {
        }

        public override void ShowAnim()
        {
            gameObject.SetActive(true);
            CanvasGroupInstance.interactable = true;
            CanvasGroupInstance.DOFade(1f, UIConst.UI_PANEL_ANIM);
        }

        public override void HideAnim()
        {
            CanvasGroupInstance.interactable = false;
            CanvasGroupInstance.DOFade(0f, UIConst.UI_PANEL_ANIM).OnComplete(() => { gameObject.SetActive(false); });
        }
    }
}