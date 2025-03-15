// // ********************************************************************************************
// //     /\_/\                           @file       TryBattle.cs
// //    ( o.o )                          @brief     QingTang
// //     > ^ <                           @author     Basya
// //    /     \
// //   (       )                         @Modified   2025031420
// //   (___)___)                         @Copyright  Copyright (c) 2025, Basya
// // ********************************************************************************************

using System.Collections.Generic;
using DG.Tweening;
using GamePlay;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Panel
{
    public class TryBattlePanel : BasePanel<TryBattlePanel>
    {
        [SerializeField] private RectTransform tipBtnsParent;
        [SerializeField] private List<TipButton> tipButtons;
        public override void Init()
        {
            base.Init();
            UIManager.Instance.AddExcludedPanels(GetType());
            GetControl<Button>("exit").onClick.AddListener(HideMe);
            GetControl<Button>("confirm").onClick.AddListener(() =>
            {
                HideMe();
                DialogManager.Instance.Load(6);
            });
            foreach (Transform child in tipBtnsParent)
            {
                TipButton tipButton = new TipButton()
                {
                    redPoint = child.GetChild(0).gameObject,
                    color = child.GetChild(1).GetComponent<Image>(),
                    btn = child.GetComponent<Button>(),
                    img = child.GetComponent<Image>(),
                    text = child.GetComponentInChildren<TextMeshProUGUI>()
                };
                tipButton.btn.onClick.AddListener(() =>
                {
                    if(tipButton.tip == null) return;
                    tipButton.tip.HasRedPoint = false;
                    tipButton.redPoint.SetActive(false);
                });
                tipButtons.Add(tipButton);
            }
        }
        
        public override void ShowAnim()
        {
            OnUpdateTip();
            CanvasGroupInstance.DOKill(true);
            gameObject.SetActive(true);
            CanvasGroupInstance.interactable = true;
            CanvasGroupInstance.DOFade(1f, UIConst.UI_PANEL_ANIM);
        }

        public override void HideAnim()
        {
            CanvasGroupInstance.DOKill(true);
            CanvasGroupInstance.interactable = false;
            CanvasGroupInstance.DOFade(0f, UIConst.UI_PANEL_ANIM).OnComplete(() => { gameObject.SetActive(false); });
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
                tipButtons[i].tip = tip;
                tipButtons[i].text.text = tip.Name;
                tipButtons[i].SetColor(tip.ColorId);
                tipButtons[i].redPoint.SetActive(tip.HasRedPoint);
                i++;
            }
        }
    }
}